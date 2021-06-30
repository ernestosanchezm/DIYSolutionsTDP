using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VoxelBusters.Serialization
{
	internal class ResourcesServices
	{
		#region Constants

		private  const   string  	kResourceRootDirectory              = "Assets/Resources";
		private  const   string  	kResourceContainerFolderName 		= Constants.kPluginName;
		private  const   string 	kResourceContainerDirectory 		= kResourceRootDirectory + "/" + kResourceContainerFolderName;

		#endregion

		#region Static methods

		internal static T LoadAsset<T>(string name) where T : Object
		{
			string path = string.Format("{0}/{1}", kResourceContainerFolderName, name);
			return Resources.Load<T>(path);
		}

		internal static void UnloadAsset(Object asset)
		{
			Resources.UnloadAsset(asset);
		}

		#endregion

		#region Editor methods

		#if UNITY_EDITOR
		internal static bool ContainsAsset(string name)
		{
			string path = GetAssetFullPath(name);
			return (AssetDatabase.AssetPathToGUID(path) != null);
		}

		internal static T CreateAsset<T>(string name) where T : ScriptableObject
		{
			// create directory if it doesn't exist
			string	directory 	= kResourceContainerDirectory;
			IOServices.CreateDirectory(directory, replace: false);

			T 		newInstance = ScriptableObject.CreateInstance<T>();
			AssetDatabase.CreateAsset(newInstance, GetAssetFullPath(name));
			AssetDatabase.SaveAssets();

			return newInstance;
		}

		private static string GetAssetFullPath(string name)
		{
			return string.Format("{0}/{1}.asset", kResourceContainerDirectory, name);
		}
		#endif

		#endregion
	}
}