using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;

using NotImplementedException = System.NotImplementedException;
using eStorageTarget = VoxelBusters.Serialization.StorageTarget;

/// <summary>
/// VoxelBusters.Serialization module implements classes that can be used for saving and restoring object values between game session.
/// </summary>
namespace VoxelBusters.Serialization
{
	/// <summary>
	/// The SerializationManager class is the interface for saving and restoring object values between game sessions.
	/// </summary>
	public class SerializationManager 
	{
		#region Fields

		private		static 			Stack<object> 		operationStack;

		#endregion

		#region Constructors

        static SerializationManager()
		{
			// init static properties
			operationStack	= new Stack<object>(4);
			IOServices.CreateDirectory(path: SerializationEnvironment.DataPath, replace: false);
			IOServices.CreateDirectory(path: SerializationEnvironment.TempDataPath, replace: false);
		}
			
		#endregion

		#region Internal static methods

		internal static Stream CreateStream(string fileName, SerializationContext context, int bufferSize)
		{
			StorageTarget 	storageTarget 	= context.GetStorageTargetForCurrentPlatform();
			if (eStorageTarget.LocalDisk == storageTarget)
			{
				FileMode 	fileMode		= FileMode.Open;
				FileAccess	fileAccess		= FileAccess.Read;
				if (SerializationMode.Serialize == context.SerializationMode)
				{
					fileMode				= FileMode.Create;
					fileAccess				= FileAccess.Write;	 
				}

				string 		path			= IOServices.CombinePath(path1: SerializationEnvironment.DataPath, 
				                                                     path2: string.Format("{0}.{1}", fileName, Constants.kFileExtension));
				return new FileStream(path: path,
				                      mode: fileMode,
				                      access: fileAccess, 
				                      share: FileShare.ReadWrite,
				                      bufferSize: bufferSize);

			}

			if (eStorageTarget.PlayerPrefs == storageTarget)
			{
				return new PlayerPrefsStream(key: fileName,
				                             capacity: bufferSize, 
				                             writable: (SerializationMode.Serialize == context.SerializationMode)); 
			}

			throw new NotSupportedException(string.Format("Storage target {0} is not supported.", storageTarget));
		}

		internal static ObjectWriter CreateObjectWriter(string name, params SerializationOption[] options)
		{
			// get settings value
			Settings				settings		= Settings.Create(options: options, useDefaultValues: true);

			// create dependent components
			SerializationContext	context			= new SerializationContext(SerializationMode.Serialize, settings, name);
			Stream					stream			= CreateStream(fileName: name, context: context, bufferSize: settings.BufferSize);

			// create serializer object
			ObjectWriter			objectWriter	= new ObjectWriter(stream: stream, context: context);
			objectWriter.onSerializeEnd 	       += OnSerializeEnd;

			return objectWriter;
		}

		internal static ObjectReader CreateObjectReader(string name)
		{
			// get basic record of past serialization operation
			SerializationRecord	record;
			if (false == SerializationRecordsManager.TryGetRecord(name, out record))
			{
				throw ErrorCentre.DataInconsistencyException("Serialization record for specified name could not be found.");
			}

			// create dependent components
			int						bufferSize		= SerializationSettings.Copy().BufferSize;

			SerializationContext	context			= new SerializationContext(SerializationMode.Deserialize, record.StorageTarget, tag: name);
			Stream					stream			= CreateStream(fileName: name, context: context, bufferSize: bufferSize);

			// create deserializer object
			ObjectReader			objectReader	= new ObjectReader(stream: stream, context: context);
			objectReader.onDeserializeEnd 	   	   += OnDeserializeEnd;

			return objectReader;
		}

		internal static string GetFilePath(string fileName, string extension)
		{
			string	fileNameWithExtension	= string.Format("{0}.{1}", fileName, extension);
			return IOServices.CombinePath(path1: SerializationEnvironment.DataPath, path2: fileNameWithExtension);
		}

		#endregion

		#region Serialize methods

        /// <summary>
        /// Begins the serialize group.
        /// </summary>
		/// <description> 
		/// Begins the serialize group.
		/// 
		/// All serialize calls enclosed inside this element will be saved in a single document. The group must be closed with a call to EndSerializeGroup.
		/// </description>
		/// <param name="key">A string value associated with the value. If specified key already exists, value replaces the existing value. If key is not found, new copy of value will be created in the specified storage.</param>
		/// <param name="options">An optional array of serialization option specifies custom settings used for this specific operation. These options overrides the SerializationSettings values.</param>
		/// <example> 
        /// @code
		/// // use this script to save and read multiple attributes of player from a single document
		/// using UnityEngine;
		/// using System.Collections;
		/// 
		/// public class ExampleClass : MonoBehaviour 
		/// {
		/// 	void SaveProfile() 
		/// 	{
		/// 		// shows how to batch multiple data components and save it in a single file
		/// 		SerializationManager.BeginSerializeGroup("profile"); 					// creates a new document called profile
		/// 		SerializationManager.SerializeString("name", "player1");				// adds name info
		/// 		SerializationManager.SerializeInt32("level", 1);						// adds level info
		/// 		SerializationManager.SerializeSingle("progress", 0.9f);					// adds progress info
		/// 		SerializationManager.EndSerializeGroup();								// marks end of the document and saves the data 
		/// 	}
		/// 
		/// 	void ReadProfile() 
		/// 	{
		/// 		// shows how to read data fields from batched document
		/// 		SerializationManager.BeginDeserializeGroup("profile"); 					// open saved document called profile
		/// 		string	name	= SerializationManager.DeserializeString("name");		// adds name info
		/// 		int 	level	= SerializationManager.DeserializeInt32("level");		// adds level info
		/// 		float	progress= SerializationManager.DeserializeSingle("progress");	// adds progress info
		/// 		SerializationManager.EndDeserializeGroup();								// marks end of the document and saves the data 
		/// 	}
		/// }
        /// @endcode
		/// </example>
		public static void BeginSerializeGroup(string key, params SerializationOption[] options)
		{
			try
			{
				// create serializer and add it to the stack
				ObjectWriter	writer		= CreateObjectWriter(name: key, options: options);
				operationStack.Push(writer);
			}
			catch (SerializationException expection)
			{
				Debug.LogError("[Serialization] The requested operation could not be completed. Please check error description for more details. Error description: " + expection.Message);
			}
		}

        /// <summary>
        /// Saves the given integer value.
        /// </summary>
		/// <param name="key">A string value associated with the value. If specified key already exists, value replaces the existing value. If key is not found, new copy of value will be created in the specified storage.</param>
        /// <param name="value">The value to be saved.</param>
		/// <param name="options">An optional array of serialization option specifies custom settings used for this specific operation. These options overrides the SerializationSettings values.</param>
		/// <example> 
        /// @code
		/// // use this script to save and read player level info (int value)
		/// using UnityEngine;
		/// using System.Collections;
		/// 
		/// public class ExampleClass : MonoBehaviour 
		/// {
		/// 	void OnReachedCheckpoint9() 
		/// 	{
		/// 		// saves new level info
		/// 		int newLevel = 10;
		/// 		SerializationManager.SerializeInt32("level", newLevel);
		/// 	}
		/// 
		/// 	int GetPlayerLevel()
		/// 	{
		/// 		// read saved value
		/// 		return SerializationManager.DeserializeInt32("level");
		/// 	}
		/// }
        /// @endcode
		/// </example>
		public static void SerializeInt32(string key, int value, params SerializationOption[] options)
		{
			try
			{
				// resolve request whether it is single or multi block data
				if (operationStack.Count > 0)
				{
					object	topObject		= operationStack.Peek();
					if (topObject is ObjectWriter)
					{
						((ObjectWriter)topObject).WriteProperty(key, value);
						return;
					}

					throw ErrorCentre.SerializationCallInconsitencyException();
				}
				else
				{
                    ObjectWriter writer = CreateObjectWriter(name: key, options: options);
                    writer.WriteProperty(key, value);
                    writer.Close();
                }
			}
			catch (SerializationException expection)
			{
				Debug.LogError("[Serialization] The requested operation could not be completed. Please check error description for more details. Error description: " + expection.Message);
			}
		}

        /// <summary>
		/// Saves the given float value.
        /// </summary>
		/// <param name="key">A string value associated with the value. If specified key already exists, value replaces the existing value. If key is not found, new copy of value will be created in the specified storage.</param>
		/// <param name="value">The value to be saved.</param>
		/// <param name="options">An optional array of serialization option specifies custom settings used for this specific operation. These options overrides the SerializationSettings values.</param>
		/// <example>
        /// @code 
		/// // use this script to save and read player progress (float value)
		/// using UnityEngine;
		/// using System.Collections;
		/// 
		/// public class ExampleClass : MonoBehaviour 
		/// {
		/// 	void OnPlayerProgressChanged(float newValue) 
		/// 	{
		/// 		// saves progress info
		/// 		SerializationManager.SerializeSingle("progress", newValue);	
		/// 	}
		/// 
		/// 	float GetPlayerProgress()
		/// 	{
		/// 		// read saved value
		/// 		return SerializationManager.DeserializeSingle("progress");
		/// 	}
		/// }
        /// @endcode
		/// </example>
		public static void SerializeSingle(string key, float value, params SerializationOption[] options)
		{
			try
			{
				// resolve request whether it is single or multi block data
				if (operationStack.Count > 0)
				{
					object	topObject		= operationStack.Peek();
					if (topObject is ObjectWriter)
					{
						((ObjectWriter)topObject).WriteProperty(key, value);
						return;
					}

					throw ErrorCentre.SerializationCallInconsitencyException();
				}
				else
				{
                    ObjectWriter writer = CreateObjectWriter(name: key, options: options);
                    writer.WriteProperty(key, value);
                    writer.Close();
                }
			}
			catch (SerializationException expection)
			{
				Debug.LogError("[Serialization] The requested operation could not be completed. Please check error description for more details. Error description: " + expection.Message);
			}
		}

        /// <summary>
		/// Saves the given string value.
        /// </summary>
		/// <param name="key">A string value associated with the value. If specified key already exists, value replaces the existing value. If key is not found, new copy of value will be created in the specified storage.</param>
		/// <param name="value">The value to be saved.</param>
		/// <param name="options">An optional array of serialization option specifies custom settings used for this specific operation. These options overrides the SerializationSettings values.</param>
		/// <example> 
        /// @code
		/// // use this script to save and read player name (string value)
		/// using UnityEngine;
		/// using System.Collections;
		/// 
		/// public class ExampleClass : MonoBehaviour 
		/// {
		/// 	void OnPlayerNameChanged(string newName) 
		/// 	{
		/// 		// saves name info
		/// 		SerializationManager.SerializeString("name", newName);		
		/// 	}
		/// 
		/// 	string GetPlayerName()
		/// 	{
		/// 		// read saved value
		/// 		return SerializationManager.DeserializeString("name");
		/// 	}
		/// }
        /// @endcode
		/// </example>
		public static void SerializeString(string key, string value, params SerializationOption[] options)
		{
			try
			{
				// resolve request whether it is single or multi block data
				if (operationStack.Count > 0)
				{
					object	topObject		= operationStack.Peek();
					if (topObject is ObjectWriter)
					{
						((ObjectWriter)topObject).WriteProperty(key, value);
						return;
					}

					throw ErrorCentre.SerializationCallInconsitencyException();
				}
				else
				{
                    ObjectWriter writer = CreateObjectWriter(name: key, options: options);
                    writer.WriteProperty(key, value);
                    writer.Close();
                }
			}
			catch (SerializationException expection)
			{
				Debug.LogError("[Serialization] The requested operation could not be completed. Please check error description for more details. Error description: " + expection.Message);
			}
		}

        /// <summary>
		/// Saves the given value.
        /// </summary>
		/// <param name="key">A string value associated with the value. If specified key already exists, value replaces the existing value. If key is not found, new copy of value will be created in the specified storage.</param>
		/// <param name="value">The value to be saved.</param>
		/// <param name="options">An optional array of serialization option specifies custom settings used for this specific operation. These options overrides the SerializationSettings values.</param>
		/// <example>
        /// @code 
		/// // use this script to save and read custom object (here, PlayerProfile)
		/// using UnityEngine;
		/// using System.Collections;
		/// 
		/// public class PlayerProfile
		/// {
		/// 	// fields
		/// 	public string 		playerName;
		/// 	public int 			playerLevel;
		/// 	public float 		playerProgress;
		/// 
		/// 	// constructors
		/// 	public PlayerProfile()
		/// 	{}
		/// 
		/// 	public PlayerProfile(string name, int level, float progress)
		/// 	{
		/// 		playerName		= name;
		/// 		playerLevel		= level;
		/// 		playerProgress	= progress;
		/// 	}
		/// }
		/// 
		/// public class ExampleClass : MonoBehaviour 
		/// {
		/// 	// fields
		/// 	PlayerProfile profile = new PlayerProfile("player1", 1, 0.9f);	
		/// 
		/// 	// methods
		/// 	void SaveProfile() 
		/// 	{
		/// 		// shows how to save custom object
		/// 		SerializationManager.Serialize("profile", profile);	
		/// 	}
		/// 
		/// 	void ReadProfile() 
		/// 	{
		/// 		// shows how to read custom object
		/// 		profile = SerializationManager.Deserialize<PlayerProfile>("profile");	
		/// 	}
		/// }
        /// @endcode
		/// </example>
		public static void Serialize<T>(string key, T value, params SerializationOption[] options)
		{
			try
			{
				// resolve request whether it is single or multi block data
				if (operationStack.Count > 0)
				{
					object	topObject		= operationStack.Peek();
					if (topObject is ObjectWriter)
					{
						((ObjectWriter)topObject).WriteProperty(key, value);
						return;
					}

					throw ErrorCentre.SerializationCallInconsitencyException();
				}
				else
				{
                    ObjectWriter writer = CreateObjectWriter(name: key, options: options);
                    writer.WriteProperty(key, value);
                    writer.Close();
                }
			}
			catch (SerializationException expection)
			{
				Debug.LogError("[Serialization] The requested operation could not be completed. Please check error description for more details. Error description: " + expection.Message);
			}
		}

		/// <summary>
		/// Closes a group started with BeginSerializeGroup.
		/// </summary>
		public static void EndSerializeGroup()
		{
			try
			{
				if (operationStack.Count > 0)
				{
					object	topObject		= operationStack.Pop();
					if (topObject is ObjectWriter)
					{
						((ObjectWriter)topObject).Close();
						return;
					}
				}

				throw ErrorCentre.SerializationCallInconsitencyException();
			}
			catch (SerializationException expection)
			{
				Debug.LogError("[Serialization] The requested operation could not be completed. Please check error description for more details. Error description: " + expection.Message);
			}
		}

		#endregion

		#region Deserialize methods

        /// <summary>
        /// Begins the deserialize group.
        /// </summary>
		/// <param name="key">Name of the key associated with saved group.</param>
		/// <example>
        /// @code 
		/// // use this script to save and read multiple attributes of player from a single document
		/// using UnityEngine;
		/// using System.Collections;
		/// 
		/// public class ExampleClass : MonoBehaviour 
		/// {
		/// 	void SaveProfile() 
		/// 	{
		/// 		// shows how to batch multiple data components and save it in a single file
		/// 		SerializationManager.BeginSerializeGroup("profile"); 					// creates a new document called profile
		/// 		SerializationManager.SerializeString("name", "player1");				// adds name info
		/// 		SerializationManager.SerializeInt32("level", 1);						// adds level info
		/// 		SerializationManager.SerializeSingle("progress", 0.9f);					// adds progress info
		/// 		SerializationManager.EndSerializeGroup();								// marks end of the document and saves the data 
		/// 	}
		/// 
		/// 	void ReadProfile() 
		/// 	{
		/// 		// shows how to read data fields from batched document
		/// 		SerializationManager.BeginDeserializeGroup("profile"); 					// open saved document called profile
		/// 		string	name	= SerializationManager.DeserializeString("name");		// adds name info
		/// 		int 	level	= SerializationManager.DeserializeInt32("level");		// adds level info
		/// 		float	progress= SerializationManager.DeserializeSingle("progress");	// adds progress info
		/// 		SerializationManager.EndDeserializeGroup();								// marks end of the document and saves the data 
		/// 	}
		/// }
        /// @endcode
		/// </example>
		public static void BeginDeserializeGroup(string key)
		{
			try
			{
				// create deserializer and add it to the stack
				ObjectReader	reader	= CreateObjectReader(name: key);
				operationStack.Push(reader);
			}
			catch (SerializationException expection)
			{
				Debug.LogError("[Serialization] The requested operation could not be completed. Please check error description for more details. Error description: " + expection.Message);
			}
		}

        /// <summary>
        /// Returns the integer value associated with the given key.
        /// </summary>
		/// <description>
		/// Returns the integer value associated with the given key.
		/// 
		/// If the value doesn't already exist in the storage the function will return defaultValue.
		/// </description>
		/// <returns>The value previously stored.</returns>
        /// <param name="key">Name of the key associated with integer value.</param>
		/// <param name="defaultValue">Integer value to return if the specified key is not found in the storage.</param>
		/// <example>
        /// @code 
		/// // use this script to save and read player level info (int value)
		/// using UnityEngine;
		/// using System.Collections;
		/// 
		/// public class ExampleClass : MonoBehaviour 
		/// {
		/// 	void OnReachedCheckpoint9() 
		/// 	{
		/// 		// saves new level info
		/// 		int newLevel = 10;
		/// 		SerializationManager.SerializeInt32("level", newLevel);
		/// 	}
		/// 
		/// 	int GetPlayerLevel()
		/// 	{
		/// 		// read saved value
		/// 		return SerializationManager.DeserializeInt32("level");
		/// 	}
		/// }
        /// @endcode
		/// </example>
		public static int DeserializeInt32(string key, int defaultValue = default(int))
		{
			return Deserialize<int>(key, defaultValue);
		}

		/// <summary>
		/// Returns the float value associated with the given key.
		/// </summary>
		/// <description>
		/// Returns the float value associated with the given key.
		/// 
		/// If the value doesn't already exist in the storage the function will return defaultValue.
		/// </description>
		/// <returns>The value previously stored.</returns>
		/// <param name="key">Name of the key associated with float value.</param>
		/// <param name="defaultValue">Float value to return if the specified key is not found in the storage.</param>
		/// <example>
        /// @code 
		/// // use this script to save and read player progress (float value)
		/// using UnityEngine;
		/// using System.Collections;
		/// 
		/// public class ExampleClass : MonoBehaviour 
		/// {
		/// 	void OnPlayerProgressChanged(float newValue) 
		/// 	{
		/// 		// saves progress info
		/// 		SerializationManager.SerializeSingle("progress", newValue);	
		/// 	}
		/// 
		/// 	float GetPlayerProgress()
		/// 	{
		/// 		// read saved value
		/// 		return SerializationManager.DeserializeSingle("progress");
		/// 	}
		/// }
        /// @endcode
		/// </example>
		public static float DeserializeSingle(string key, float defaultValue = default(float))
		{
			return Deserialize<float>(key, defaultValue);
		}

		/// <summary>
		/// Returns the string value associated with the given key.
		/// </summary>
		/// <description>
		/// Returns the string value associated with the given key.
		/// 
		/// If the value doesn't already exist in the storage the function will return defaultValue.
		/// </description>
		/// <returns>The value previously stored.</returns>
		/// <param name="key">Name of the key associated with string value.</param>
		/// <param name="defaultValue">String value to return if the specified key is not found in the storage.</param>
		/// <example>
        /// @code 
		/// // use this script to save and read player name (string value)
		/// using UnityEngine;
		/// using System.Collections;
		/// 
		/// public class ExampleClass : MonoBehaviour 
		/// {
		/// 	void OnPlayerNameChanged(string newName) 
		/// 	{
		/// 		// saves name info
		/// 		SerializationManager.SerializeString("name", newName);		
		/// 	}
		/// 
		/// 	string GetPlayerName()
		/// 	{
		/// 		// read saved value
		/// 		return SerializationManager.DeserializeString("name");
		/// 	}
		/// }
        /// @endcode
		/// </example>
		public static string DeserializeString(string key, string defaultValue = default(string))
		{
			return Deserialize<string>(key, defaultValue);
		}

		/// <summary>
		/// Returns the value associated with the given key.
		/// </summary>
		/// <description>
		/// Returns the value associated with the given key.
		/// 
		/// If the value doesn't already exist in the storage the function will return defaultValue.
		/// </description>
		/// <returns>The value previously stored.</returns>
		/// <param name="key">Name of the key associated with value.</param>
		/// <param name="defaultValue">Value to return if the specified key is not found in the storage.</param>
		/// <example>
        /// @code 
		/// // use this script to save and read custom object
		/// using UnityEngine;
		/// using System.Collections;
		/// 
		/// public class PlayerProfile
		/// {
		/// 	// fields
		/// 	public string 		playerName;
		/// 	public int 			playerLevel;
		/// 	public float 		playerProgress;
		/// 
		/// 	// constructors
		/// 	public PlayerProfile()
		/// 	{}
		/// 
		/// 	public PlayerProfile(string name, int level, float progress)
		/// 	{
		/// 		playerName		= name;
		/// 		playerLevel		= level;
		/// 		playerProgress	= progress;
		/// 	}
		/// }
		/// 
		/// public class ExampleClass : MonoBehaviour 
		/// {
		/// 	// fields
		/// 	PlayerProfile profile = new PlayerProfile("player1", 1, 0.9f);	
		/// 
		/// 	// methods
		/// 	void SaveProfile() 
		/// 	{
		/// 		// shows how to save custom object
		/// 		SerializationManager.Serialize("profile", profile);	
		/// 	}
		/// 
		/// 	void ReadProfile() 
		/// 	{
		/// 		// shows how to read custom object
		/// 		profile = SerializationManager.Deserialize<PlayerProfile>("profile");	
		/// 	}
		/// }
        /// @endcode
		/// </example>
		public static T Deserialize<T>(string key, T defaultValue = default(T))
		{
			try
			{
				// resolve request whether it is single or multi block data
				if (operationStack.Count > 0)
				{
					object	topObject		= operationStack.Peek();
					if (topObject is ObjectReader)
					{
						return ((ObjectReader)topObject).ReadProperty<T>(key);
					}

					throw ErrorCentre.DeserializationCallInconsitencyException();
				}
				else
				{
                    ObjectReader reader = CreateObjectReader(name: key);
                    T            value  = reader.ReadProperty<T>(name: key);
                    reader.Close();

                    return value;
                }
			}
			catch (SerializationException expection)
			{
				Debug.LogError("[Serialization] The requested operation could not be completed. Please check error description for more details. Error description: " + expection.Message);
				return default(T);
			}
		}

		/// <summary>
		/// Closes a group started with BeginDeserializeGroup.
		/// </summary>
		public static void EndDeserializeGroup()
		{
			try
			{
				if (operationStack.Count > 0)
				{
					object	topObject		= operationStack.Pop();
					if (topObject is ObjectReader)
					{
						((ObjectReader)topObject).Close();
						return;
					}
				}

				throw ErrorCentre.DeserializationCallInconsitencyException();
			}
			catch (SerializationException expection)
			{
				Debug.LogError("[Serialization] The requested operation could not be completed. Please check error description for more details. Error description: " + expection.Message);
			}
		}

        #endregion

        #region Memory operations

        /// <summary>
        /// Returns the serialized data of the given value. 
        /// </summary>
        /// <description>
        /// This method doesn't save the data in any form (as file or PlayerPrefs). Instead user is responsible to manage this data. 
        /// Provide this byte array as input to DeserializeFromByteArray method inorder to retrieve back original value.
        /// </description>
        /// <param name="value">The value to be saved.</param>
        /// <param name="options">An optional array of serialization option specifies custom settings used for this specific operation. These options overrides the SerializationSettings values.</param>
        /// <example>
        /// @code 
        /// // use this script to save and read custom object (here, PlayerProfile)
        /// using UnityEngine;
        /// using System.Collections;
        /// 
        /// public class PlayerProfile
        /// {
        ///     // fields
        ///     public string       playerName;
        ///     public int          playerLevel;
        ///     public float        playerProgress;
        /// 
        ///     // constructors
        ///     public PlayerProfile()
        ///     {}
        /// 
        ///     public PlayerProfile(string name, int level, float progress)
        ///     {
        ///         playerName      = name;
        ///         playerLevel     = level;
        ///         playerProgress  = progress;
        ///     }
        /// }
        /// 
        /// public class ExampleClass : MonoBehaviour 
        /// {
        ///     // fields
        ///     PlayerProfile profile = new PlayerProfile("player1", 1, 0.9f);  
        ///     byte[] rawData  = null;
        /// 
        ///     // methods
        ///     void SaveProfile() 
        ///     {
        ///         // shows how to save custom object
        ///         rawData = SerializationManager.SerializeToByteArray(profile); 
        ///     }
        /// 
        ///     void ReadProfile() 
        ///     {
        ///         // shows how to read custom object
        ///         profile = SerializationManager.DeserializeFromByteArray<PlayerProfile>(rawData);   
        ///     }
        /// }
        /// @endcode
        /// </example>
        public static byte[] SerializeToByteArray<T>(T value, params SerializationOption[] options)
        {
            return SerializeToByteArray(Constants.kUndefinedName, value, options);
        }

        /// <summary>
        /// Returns the serialized data of the given value. 
        /// </summary>
        /// <description>
        /// This method doesn't save the data in any form (as file or PlayerPrefs). Instead user is responsible to manage this data. 
        /// Provide this byte array as input to DeserializeFromByteArray method inorder to retrieve back original value.
        /// </description>
        /// <param name="key">Name of the key associated with value.</param>
        /// <param name="value">The value to be saved.</param>
        /// <param name="options">An optional array of serialization option specifies custom settings used for this specific operation. These options overrides the SerializationSettings values.</param>
        /// <example>
        /// @code 
        /// // use this script to save and read custom object (here, PlayerProfile)
        /// using UnityEngine;
        /// using System.Collections;
        /// 
        /// public class PlayerProfile
        /// {
        ///     // fields
        ///     public string       playerName;
        ///     public int          playerLevel;
        ///     public float        playerProgress;
        /// 
        ///     // constructors
        ///     public PlayerProfile()
        ///     {}
        /// 
        ///     public PlayerProfile(string name, int level, float progress)
        ///     {
        ///         playerName      = name;
        ///         playerLevel     = level;
        ///         playerProgress  = progress;
        ///     }
        /// }
        /// 
        /// public class ExampleClass : MonoBehaviour 
        /// {
        ///     // fields
        ///     PlayerProfile profile = new PlayerProfile("player1", 1, 0.9f);  
        ///     byte[] rawData  = null;
        /// 
        ///     // methods
        ///     void SaveProfile() 
        ///     {
        ///         // shows how to save custom object
        ///         rawData = SerializationManager.SerializeToByteArray("key", profile); 
        ///     }
        /// 
        ///     void ReadProfile() 
        ///     {
        ///         // shows how to read custom object
        ///         profile = SerializationManager.DeserializeFromByteArray<PlayerProfile>("key", rawData);   
        ///     }
        /// }
        /// @endcode
        /// </example>
        public static byte[] SerializeToByteArray<T>(string key, T value, params SerializationOption[] options)
        {
            // get settings to be used for this operation
            Settings                settings        = Settings.Create(options: options, useDefaultValues: true);
            settings.StorageTarget                  = eStorageTarget.InMemory;

            // create context and stream to hold serialized data
            SerializationContext    context         = new SerializationContext(SerializationMode.Serialize, settings, key);
            MemoryStream            stream          = new MemoryStream(capacity: settings.BufferSize);

            // create object writer and write the value
            ObjectWriter            objectWriter    = new ObjectWriter(stream, context);
            objectWriter.onSerializeEnd            += OnSerializeEnd;
            objectWriter.WriteProperty(value);
            objectWriter.Close();

            // return serialized data
            return stream.ToArray();
        }

        /// <summary>
        /// Returns the value associated with the serialized data.
        /// </summary>
        /// <returns>The value previously serialized.</returns>
        /// <param name="data">Serialized data.</param>
        /// <example>
        /// @code 
        /// // use this script to save and read custom object
        /// using UnityEngine;
        /// using System.Collections;
        /// 
        /// public class PlayerProfile
        /// {
        ///     // fields
        ///     public string       playerName;
        ///     public int          playerLevel;
        ///     public float        playerProgress;
        /// 
        ///     // constructors
        ///     public PlayerProfile()
        ///     {}
        /// 
        ///     public PlayerProfile(string name, int level, float progress)
        ///     {
        ///         playerName      = name;
        ///         playerLevel     = level;
        ///         playerProgress  = progress;
        ///     }
        /// }
        /// 
        /// public class ExampleClass : MonoBehaviour 
        /// {
        ///     // fields
        ///     PlayerProfile profile = new PlayerProfile("player1", 1, 0.9f);  
        ///     byte[] rawData  = null;
        /// 
        ///     // methods
        ///     void SaveProfile() 
        ///     {
        ///         // shows how to save custom object
        ///         rawData = SerializationManager.SerializeToByteArray(profile); 
        ///     }
        /// 
        ///     void ReadProfile() 
        ///     {
        ///         // shows how to read custom object
        ///         profile = SerializationManager.DeserializeFromByteArray<PlayerProfile>(rawData);   
        ///     }
        /// }
        /// @endcode
        /// </example>
        public static T DeserializeFromByteArray<T>(byte[] data)
        {
            return DeserializeFromByteArray<T>(Constants.kUndefinedName, data);
        }
 
        /// <summary>
        /// Returns the value associated with the serialized data.
        /// </summary>
        /// <returns>The value previously serialized.</returns>
        /// <param name="key">Name of the key associated with value.</param>
        /// <param name="data">Serialized data.</param>
        /// <example>
        /// @code 
        /// // use this script to save and read custom object
        /// using UnityEngine;
        /// using System.Collections;
        /// 
        /// public class PlayerProfile
        /// {
        ///     // fields
        ///     public string       playerName;
        ///     public int          playerLevel;
        ///     public float        playerProgress;
        /// 
        ///     // constructors
        ///     public PlayerProfile()
        ///     {}
        /// 
        ///     public PlayerProfile(string name, int level, float progress)
        ///     {
        ///         playerName      = name;
        ///         playerLevel     = level;
        ///         playerProgress  = progress;
        ///     }
        /// }
        /// 
        /// public class ExampleClass : MonoBehaviour 
        /// {
        ///     // fields
        ///     PlayerProfile profile = new PlayerProfile("player1", 1, 0.9f);  
        ///     byte[] rawData  = null;
        /// 
        ///     // methods
        ///     void SaveProfile() 
        ///     {
        ///         // shows how to save custom object
        ///         rawData = SerializationManager.SerializeToByteArray(profile); 
        ///     }
        /// 
        ///     void ReadProfile() 
        ///     {
        ///         // shows how to read custom object
        ///         profile = SerializationManager.DeserializeFromByteArray<PlayerProfile>(rawData);   
        ///     }
        /// }
        /// @endcode
        /// </example>
        public static T DeserializeFromByteArray<T>(string name, byte[] data)
        {
            // create context and stream to hold serialized data
            SerializationContext    context         = new SerializationContext(SerializationMode.Deserialize, eStorageTarget.InMemory, name);
            Stream                  stream          = new MemoryStream(data);

            // create reader
            ObjectReader            objectReader    = new ObjectReader(stream, context);
            objectReader.onDeserializeEnd          += OnDeserializeEnd;

            // read value
            T                       value           = objectReader.ReadProperty<T>();
            objectReader.Close();

            return value;
        }

        /// <summary>
        /// Begins the serialize group.
        /// </summary>
        /// <description> 
        /// <param name="options">An optional array of serialization option specifies custom settings used for this specific operation. These options overrides the SerializationSettings values.</param>
        public static void BeginSerializeToByteArrayGroup(params SerializationOption[] options)
        {
            BeginSerializeToByteArrayGroup(Constants.kUndefinedName, options);
        }

        /// <summary>
        /// Begins the serialize group.
        /// </summary>
        /// <description> 
        /// <param name="key">Name of the key associated with value.</param>
        /// <param name="options">An optional array of serialization option specifies custom settings used for this specific operation. These options overrides the SerializationSettings values.</param>
        public static void BeginSerializeToByteArrayGroup(string key, params SerializationOption[] options)
        {
            try
            {
                // get settings to be used for this operation
                Settings                settings        = Settings.Create(options: options, useDefaultValues: true);
                settings.StorageTarget                  = eStorageTarget.InMemory;

                // create context and stream to hold serialized data
                SerializationContext    context         = new SerializationContext(SerializationMode.Serialize, settings, key);
                MemoryStream            stream          = new MemoryStream(capacity: settings.BufferSize);

                // create object writer
                ObjectWriter            objectWriter    = new ObjectWriter(stream, context);
                objectWriter.onSerializeEnd            += OnSerializeEnd;

                // add to stack
                operationStack.Push(objectWriter);
            }
            catch (SerializationException expection)
            {
                Debug.LogError("[Serialization] The requested operation could not be completed. Please check error description for more details. Error description: " + expection.Message);
            }
        }

        /// <summary>
        /// Closes a group started with <see cref="BeginSerializeToByteArrayGroup"/>.
        /// </summary>
        public static byte[] EndSerializeToByteArrayGroup()
        {
            try
            {
                if (operationStack.Count > 0)
                {
                    // get current operation 
                    object              topObject       = operationStack.Pop();
                    if (topObject is ObjectWriter)
                    {
                        ObjectWriter    objectWriter    = (ObjectWriter)topObject;
                        Stream          stream          = objectWriter.Stream;
                        if (stream is MemoryStream)
                        {
                            stream.Close();
                            return ((MemoryStream)stream).ToArray();
                        }
                    }
                }

                throw ErrorCentre.SerializationCallInconsitencyException();
            }
            catch (SerializationException expection)
            {
                Debug.LogError("[Serialization] The requested operation could not be completed. Please check error description for more details. Error description: " + expection.Message);
                return null;
            }
        }

        /// <summary>
        /// Begins the deserialize group.
        /// </summary>
        /// <param name="data">Serialized data.</param>
        public static void BeginDeserializeToByteArrayGroup(byte[] data)
        {
            BeginDeserializeToByteArrayGroup(Constants.kUndefinedName, data);
        }

        /// <summary>
        /// Begins the deserialize group.
        /// </summary>
        /// <param name="key">Name of the key associated with value.</param>
        /// <param name="data">Serialized data.</param>
        public static void BeginDeserializeToByteArrayGroup(string key, byte[] data)
        {
            try
            {
                // create context and stream to hold serialized data
                SerializationContext    context         = new SerializationContext(SerializationMode.Deserialize, eStorageTarget.InMemory, key);
                Stream                  stream          = new MemoryStream(data);

                // create reader
                ObjectReader            objectReader    = new ObjectReader(stream, context);
                objectReader.onDeserializeEnd          += OnDeserializeEnd;

                // add operation to stack
                operationStack.Push(objectReader);
            }
            catch (SerializationException expection)
            {
                Debug.LogError("[Serialization] The requested operation could not be completed. Please check error description for more details. Error description: " + expection.Message);
            }
        }

        /// <summary>
        /// Closes a group started with <see cref="BeginDeserializeToByteArrayGroup"/>.
        /// </summary>
        public static void EndDeserializeToByteArrayGroup()
        {
            try
            {
                if (operationStack.Count > 0)
                {
                    object              topObject       = operationStack.Pop();
                    if (topObject is ObjectReader)
                    {
                        ((ObjectReader)topObject).Close();
                        return;
                    }
                }

                throw ErrorCentre.DeserializationCallInconsitencyException();
            }
            catch (SerializationException expection)
            {
                Debug.LogError("[Serialization] The requested operation could not be completed. Please check error description for more details. Error description: " + expection.Message);
            }
        }

        #endregion

		#region Static methods

		/// <summary>
		/// Determines whether storage contains value associated with given key.
		/// </summary>
		/// <returns><c>true</c> if storage has the specified key; otherwise, <c>false</c>.</returns>
		/// <param name="key">A string value used to uniquely identify the stored value.</param>
		public static bool HasKey(string key)
		{
			return SerializationRecordsManager.ContainsRecord(key);
		}

		/// <summary>
		/// Removes the value identified by the key.
		/// </summary>
		/// <param name="key">Name of the key associated with value.</param>
		public static void DeleteKey(string key)
		{
			SerializationRecord record;
			if (SerializationRecordsManager.TryGetRecord(key, out record))
			{
				// remove
				StreamServices.RemoveStreamData(record);
				SerializationRecordsManager.Delete(key);
			}
		}

		/// <summary>
		/// Removes all the data stored in the system. Use it with caution.
		/// </summary>
		/// <description>
		/// Call this function to delete all the saved information. Be careful while using this. You cannot undo this action.
		/// </description>
		public static void DeleteAll()
		{
			SerializationRecordsEnumerator recordsEnumerator	= SerializationRecordsManager.GetEnumerator();
			while (recordsEnumerator.MoveNext())
			{
				SerializationRecord record	= recordsEnumerator.Current;
				StreamServices.RemoveStreamData(record);
			}

			// create new directory by replacing old ones
			IOServices.CreateDirectory(path: SerializationEnvironment.DataPath, replace: true);
			IOServices.CreateDirectory(path: SerializationEnvironment.TempDataPath, replace: true);

			// clear history data
			SerializationRecordsManager.DeleteAll();
		}

		/// <summary>
		/// Removes all the data that have been cached by the serialization system.
		/// </summary>
		public static void ClearCache()
		{
			SerializationDataProviderServices.ClearCachedData();
			ReflectionServices.ClearCachedData();
		}

		#endregion

		#region Create option

		/// <summary>
		/// Custom serializaion option passed to specify the stream buffer size.
		/// </summary>
		/// <description>
		/// This option can be used for serialization mode.
		/// </description>
		/// <param name="value">Value.</param>
		public static SerializationOption BufferSize(int value)
		{
			return new SerializationOption(type: SerializationOption.SerializationOptionType.BufferSize, value: value);
		}

		/// <summary>
		/// Custom serializaion option passed to specify the serialization (save) method used while serializing object.
		/// </summary>
		/// <description>
		/// This option can be used for serialization mode.
		/// </description>
		/// <param name="value">Value.</param>
		public static SerializationOption SerializationMethod(SerializationMethodOptions value)
		{
			return new SerializationOption(type: SerializationOption.SerializationOptionType.SerializationMethod, value: value);
		}

		/// <summary>
		/// Custom serializaion option passed to specify the storage location where data will be saved.
		/// </summary>
		/// <param name="value">Value.</param>
		public static SerializationOption StorageTarget(eStorageTarget value)
		{
			return new SerializationOption(type: SerializationOption.SerializationOptionType.Storage, value: value);
		}

		#endregion

		#region Internal event callbacks

		private static void OnSerializeEnd(string name, SerializationContext context)
		{
            if (eStorageTarget.InMemory == context.StorageTarget)
            {
                return;
            }

            // record this activity
            SerializationRecordsManager.LogSerialization(record: new SerializationRecord(name, context.StorageTarget));
        }

        private static void OnDeserializeEnd(string name, SerializationContext context)
		{}

		#endregion

		#region External event method

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		internal static void OnBeforeRuntimeSceneLoad()
		{}

		#endregion
	}
}