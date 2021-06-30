using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using UnityObject = UnityEngine.Object;

namespace VoxelBusters.Serialization
{
	public class Menu 
	{
		#region Constants

		private		const	string		kMenuPath				= "Window/Voxel Busters/Cross Platform Easy Save/";
		private		const	string		kGameObjectMenuPath		= "GameObject/";
        private     const   string      kTutorialsURL           = "https://assetstore.easysave.voxelbusters.com/tutorials/";

		#endregion

		#region Static methods

		[MenuItem(kMenuPath + "Settings")]
		public static void OpenSettings()
		{
			SerializationSettings	instance	= SerializationSettings.Instance;
			if (instance != null)
			{
				EditorGUIUtility.PingObject(instance);
				Selection.activeObject	= instance;
			}
		}

        [MenuItem(kMenuPath + "Tutorials")]
        public static void OpenTutorials()
        {
            Application.OpenURL(kTutorialsURL);
        }

		[MenuItem(kMenuPath + "Generate DataProvider")]
		public static void OpenDataProviderGeneratorWindow()
		{
			SerializationDataProviderGeneratorWindow.ShowWindow();
		}

		[MenuItem(kGameObjectMenuPath + "Serialize", false, 20)]
		public static void Serialize()
		{
			UnityObject[] targetObjects = Selection.gameObjects;
			EditorObjectSerializerWindow.ShowWindow(objects: targetObjects);
		}

		[MenuItem(kGameObjectMenuPath + "Serialize", true)]
		public static bool ValidateSerialize()
		{
			return (false == EditorApplication.isPlayingOrWillChangePlaymode);
		}

        [MenuItem(kMenuPath + "Force Refresh Scene Resource Stores")]
        public static void ForceRefreshSceneStores()
        {
            ResourceObjectIdentifierStoreModifier.ForceRefreshStores();
        }

        #endregion
    }
}