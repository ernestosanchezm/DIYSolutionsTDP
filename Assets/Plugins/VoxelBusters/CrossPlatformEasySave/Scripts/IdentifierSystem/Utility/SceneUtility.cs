using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace VoxelBusters.Serialization
{
	internal class SceneUtility  
	{
		#region Static fields

		private 	static		Dictionary<string, Scene> 		sceneMap;

		#endregion

		#region Editor methods

		#if UNITY_EDITOR
		internal static LoadSceneMode ToLoadSceneMode(OpenSceneMode mode)
		{
			LoadSceneMode value = (mode == OpenSceneMode.Single) 
				? LoadSceneMode.Single
				: LoadSceneMode.Additive;
			return value;
		}

		internal static bool IsSceneAsset(string path)
		{
			if (path.EndsWith(".unity", System.StringComparison.Ordinal) || path.EndsWith(".unity.meta", System.StringComparison.Ordinal))
			{
				return true;
			}

			return false;
		}

		internal static bool IsSceneInBuildSettings(string guid)
		{
			// find whether given scene exists in the build settings
			foreach (SceneMetadata sceneMeta in UnityPlayerBuildSettings.ActiveScenes)
			{
				if (string.Equals(guid, sceneMeta.Guid, System.StringComparison.InvariantCulture))
				{
					return true;
				}
			}

			return false;
		}

		internal static bool IsSceneInBuildSettings(Scene scene)
		{
			string	guid	= AssetDatabase.AssetPathToGUID(scene.path);
			return IsSceneInBuildSettings(guid);
		}

		internal static string GetSceneGuid(string assetName)
		{
			assetName 		= assetName.Replace(".meta", string.Empty);
			string	guid	= AssetDatabase.AssetPathToGUID(assetName);
			return guid;
		}

		internal static string GetSceneGuid(Scene scene)
		{
			return AssetDatabase.AssetPathToGUID(scene.path);
		}

		internal static Scene GetSceneByPath(string path)
		{
			path = path.Replace(".meta", string.Empty);
			return SceneManager.GetSceneByPath(path);
		}
		#endif

		#endregion
	}
}