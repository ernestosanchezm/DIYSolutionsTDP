#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace VoxelBusters.Serialization
{
	internal class AssetDatabaseServices 
	{
		#region Constants

		private  const   string  	kAssetsContainerFolderName 			= "PluginResources"; 
        private  const   string 	kAssetsContainerDirectory 			= Constants.kEditorRootDirectory + "/" + kAssetsContainerFolderName;

		#endregion

		#region Static fields

		private		static 		SubAssetLibrary		assetLibrary;

		#endregion

		#region Internal methods

		internal static Object[] LoadAllSubAssetsAtPath(string path, out string[] guids)
		{
			SubAssetLibrary	assetLibrary	= GetAssetLibrary();

			// find sub assets available at given path
			Object[]	subAssets	= AssetDatabase.LoadAllAssetRepresentationsAtPath(path);
			int			assetCount	= subAssets.Length;
			bool		isDirty		= false;

			// calculate guid for each subasset
			guids	= new string[assetCount];
			for (int iter = 0; iter < assetCount; iter++)
			{
				Object			asset		= subAssets[iter];
				ResourceObject	assetMeta	= assetLibrary.Find(asset);
				if (null == assetMeta)
				{
					ResourceObject newMeta	= new ResourceObject(asset);
					assetLibrary.Add(newMeta.Object, newMeta.Guid);

					// copy new value
					assetMeta	= newMeta;
					isDirty		= true;
				}

				guids[iter]	= assetMeta.Guid;
			}

			// mark that libary is changed
			if (isDirty)
			{
				EditorUtility.SetDirty(assetLibrary);
			}

			return subAssets;
		}

		internal static bool ContainsAsset(string name)
		{
			string path = GetAssetFullPath(name);
			return (AssetDatabase.AssetPathToGUID(path) != null);
		}

		internal static T LoadAsset<T>(string name) where T : ScriptableObject
		{
			string path = GetAssetFullPath(name);
			return AssetDatabase.LoadAssetAtPath<T>(path);
		}

		internal static T CreateAsset<T>(string name) where T : ScriptableObject
		{
			// create directory if it doesn't exist
			string	directory 	= kAssetsContainerDirectory;
			IOServices.CreateDirectory(directory, replace: false);

			T 		newInstance = ScriptableObject.CreateInstance<T>();
			AssetDatabase.CreateAsset(newInstance, GetAssetFullPath(name));
			AssetDatabase.SaveAssets();

			return newInstance;
		}

		#endregion

		#region Private methods

		private static SubAssetLibrary GetAssetLibrary()
		{
			if (null == assetLibrary)
			{
				SubAssetLibrary library = AssetDatabaseServices.LoadAsset<SubAssetLibrary>("SubAssetsLibrary");
				if (null == library)
				{
					library = CreateAsset<SubAssetLibrary>("SubAssetLibrary");
				}

				// store value
				assetLibrary = library;
			}

			return assetLibrary;
		}

		private static string GetAssetFullPath(string name)
		{
			return string.Format("{0}/{1}.asset", kAssetsContainerDirectory, name);
		}

		#endregion
	}
}
#endif