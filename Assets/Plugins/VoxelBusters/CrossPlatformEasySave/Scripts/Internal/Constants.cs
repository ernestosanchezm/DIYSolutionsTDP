using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	internal class Constants 
	{
        #region Constants

        // plugin info
        internal    const   string  kPluginName                         = "CrossPlatformEasySave";
        internal    const   string  kPluginDisplayName                  = "Cross Platform Easy Save";
        internal    const   string  kPluginVersion                      = "1.5";
        internal    const   string  kCopyright                          = "Copyright © 2019 Voxel Busters Interactive LLP. All rights reserved.";

        // file management
        internal    const   string  kPluginRootDirectory              	= "Assets/Plugins/VoxelBusters" + "/" + kPluginName;
		internal	const	string  kEditorRootDirectory                = "Assets/Editor/VoxelBusters" + "/" + kPluginName;

        // data provider
        internal    const 	string 	kDataProviderSavePath				= kPluginRootDirectory + "/Scripts/DataProviders";
        internal    const   string  kUndefinedName                      = "undefined";

		// save data
		internal 	const 	string 	kFileExtension						= "bytes";

		// scene object section
		internal	const	string 	kSceneObjectIdStoreTagName			= "SceneObjectIdStore";

		// global settings 
		internal 	const   string  kSerializationSettingsFolderPath	= "Assets/Resources" + "/" + kPluginName;

		#endregion
	}
}