using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Flags = System.FlagsAttribute;
using Serializable = System.SerializableAttribute;

namespace VoxelBusters.Serialization
{
	/// <summary>
	/// Enum defines serialization method option used while saving object.
	/// </summary>
	/// <description>
	/// This allows you to change the way serialization algorithm gathers data while serializing an object. You can combine multiple options (using Bitwise OR) based on your requirement.
	/// </description>
	[Serializable, Flags]
	public enum SerializationMethodOptions : int
	{
		/// <summary>
		/// Saves the object with default condition.
		/// </summary>
		Default = 1 << 0,

		/// <summary>
		/// Saves the object without using reflection technique.
		/// </summary>
		StrictMode = 1 << 1,

		/// <summary>
		/// Saves the enum value by its name.
		/// </summary>
		EnumSaveAsStringValue = 1 << 2,

		Reserved1 = 1 << 3,
		Reserved2 = 1 << 4,
		Reserved3 = 1 << 5,

		/// <summary>
		/// Saves the gameObject and its transform properties (position, rotation, scale).
		/// </summary>
		GameObjectSerializeTransform = 1 << 6,

		/// <summary>
		/// Saves the gameObject and its attached components.
		/// </summary>
		GameObjectSerializeAttachedComponents = 1 << 7,

		/// <summary>
		/// Saves the gameObject and its inner child gameObjects.
		/// </summary>
		GameObjectSerializeChildrens = 1 << 8,

		/// <summary>
		/// Disables default behaviour of saving project resource by guid. Instead resource object will be saved in its raw form.
		/// </summary>
		DisableAssetSerializationById = 1 << 9,
	}

	/// <summary>
	/// Enum defines serialization mode active in the given instance.
	/// </summary>
	[Serializable]
	public enum SerializationMode
	{
		/// <summary>
		/// Serialization process (saving object data) is active.
		/// </summary>
		Serialize = 1,

		/// <summary>
		/// Deserialization process (reading previously saved data) is active.
		/// </summary>
		Deserialize,
	}

	/// <summary>
	/// Enum defines the target location where serialized data needs to be saved.
	/// </summary>
	[Serializable]
	public enum StorageTarget
	{
		/// <summary>	
		/// Save location is not defined. This value throws Exception when used during serialization process.
		/// </summary>
		Unknown	= 0,

		/// <summary>
		/// Specifies that object data needs to be saved as a file in disk storage.
		/// 
		/// The final storage path is generated from [Application.PersistentDataPath]/[SerializationSettings.FolderName]. This option is not available in WebGL platform and all save requests defaults to PlayerPrefs storage.
		/// </summary>
		LocalDisk,

		/// <summary>
		/// Specifies that object data needs to be saved as a string in PlayerPrefs.
		/// </summary>
		PlayerPrefs,

        /// <summary>
        /// Specifies that object data needs to be saved temporarily in memory.
        /// </summary>
        InMemory,
	}
}