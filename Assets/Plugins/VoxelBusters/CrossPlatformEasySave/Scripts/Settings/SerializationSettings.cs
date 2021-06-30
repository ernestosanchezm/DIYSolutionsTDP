using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.External.SystemUtils;
using VoxelBusters.External.UnityEngineUtils;

namespace VoxelBusters.Serialization
{
	[System.Serializable]
	[AssetCollectionFolderAttribute(VoxelBusters.Serialization.Constants.kSerializationSettingsFolderPath)]
    internal class SerializationSettings : SingletonScriptableObject<SerializationSettings> 
	{
		#region Defaults

		internal class Defaults
		{
			internal	const 		byte 							kSerializedVersion			= 1;
			internal 	const		int 							kBufferSize					= 1024;
			internal 	const		SerializationMethodOptions 		kSerializationMethod		= (SerializationMethodOptions.GameObjectSerializeTransform | 
			                                                                                       SerializationMethodOptions.GameObjectSerializeAttachedComponents |
			                                                                                       SerializationMethodOptions.GameObjectSerializeChildrens);
			internal 	const		StorageTarget 					kStorageTarget				= StorageTarget.LocalDisk;
			internal 	const		string 							kFolderName					= "SerializedData";

			internal 	const		int 							kDepthLimit					= 32;
            internal    const       bool                            kPreloadResourceObjects     = true;
		}

		#endregion

		#region Fields

		[SerializeField, Tooltip("Define your stream writer buffer size.")]
		private						int 							m_bufferSize;
		[SerializeField, Tooltip("Select the serialization method options.")]
		[EnumMaskField(typeof(SerializationMethodOptions))]
		private						SerializationMethodOptions		m_serializationMethod;
		[SerializeField, Tooltip("Select the location where output needs to be saved.")]
		private						StorageTarget					m_storageTarget;
		[SerializeField, Tooltip("Give a name to the folder which contains all saved files.")]
		private						string 							m_saveFolderName;
		[SerializeField, Tooltip("Check this to allow plugin to preload resource objects referred by the scene.")]
		private						bool	 						m_preloadResourceObjects;

		#endregion

		#region Static properties

		internal static string SaveFolderName
		{
			get
			{
				SerializationSettings   sharedInstance  = GetSerializationSettings();
                return sharedInstance != null ? sharedInstance.m_saveFolderName : Defaults.kFolderName;
            }
        }

        internal static byte SerializedVersion
		{
			get
			{
				return Defaults.kSerializedVersion;
			}
		}

		internal static bool PreloadResourceObjects
		{
			get
			{
                SerializationSettings   sharedInstance  = GetSerializationSettings();
                return sharedInstance != null ? sharedInstance.m_preloadResourceObjects : Defaults.kPreloadResourceObjects;
			}
		}

		internal static int DepthLimit
		{
			get
			{
				return Defaults.kDepthLimit;
			}
		}

		#endregion

		#region Static methods

		internal static Settings Copy()
		{
            SerializationSettings   sharedInstance  = GetSerializationSettings();
            return sharedInstance != null ? sharedInstance.CreateSettingsCopy() : CreateDefaultSettings();
        }

        #endregion

        #region Private static methods

        private static Settings CreateDefaultSettings()
		{
			// create settings with default value
			Settings	defaultSettings			= new Settings();
			defaultSettings.BufferSize			= Defaults.kBufferSize;
			defaultSettings.SerializationMethod	= Defaults.kSerializationMethod;
			defaultSettings.StorageTarget		= Defaults.kStorageTarget;

			return defaultSettings;
		}

        private static SerializationSettings GetSerializationSettings()
        {
            SerializationSettings sharedInstance    = Instance;
            if (sharedInstance == null)
            {
                Debug.LogWarning("[Serialization] Plugin could not find settings file. You can create one from menu (Windows->VoxelBusters->Cross Platform Easy Save->Settings)");
                return null;
            }

            return sharedInstance;

        }

		#endregion

		#region Private methods

		protected override void Reset()
		{
			base.Reset();

			// set properties
			m_bufferSize				= Defaults.kBufferSize;
			m_serializationMethod		= Defaults.kSerializationMethod;
			m_storageTarget				= Defaults.kStorageTarget;
			m_saveFolderName			= Defaults.kFolderName;
			m_preloadResourceObjects	= true;
		}

		private Settings CreateSettingsCopy()
		{
			Settings	settingsCopy			= new Settings();
			settingsCopy.BufferSize				= m_bufferSize;
			settingsCopy.SerializationMethod	= m_serializationMethod;
			settingsCopy.StorageTarget			= m_storageTarget;

			return settingsCopy;
		}

		#endregion
	}
}