using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
using VoxelBusters.External.UnityEditorUtils;
#endif

namespace VoxelBusters.External.UnityEngineUtils
{
	public static class ScriptableObjectUtils
	{
		#region Create methods

		#if UNITY_EDITOR
		public static T Create<T>(string path) where T : ScriptableObject
		{
			if (EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isPaused)
				return null;

			// create specified folder
			AssetDatabaseUtils.CreateFolder(path);

			// create asset file
			T	instance = ScriptableObject.CreateInstance<T>();
			AssetDatabase.CreateAsset(instance, AssetDatabase.GenerateUniqueAssetPath(path));
			instance.SaveChanges();

			return instance;
		}

		public static T Create<T>() where T : ScriptableObject
		{
			// get selected path
			string	path = AssetDatabase.GetAssetPath(Selection.activeObject);
			if (path == "")
			{
				path = "Assets";
			}
			else if (Path.GetExtension(path) != "")
			{
				path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
			}

			// append file name
			path	+= typeof(T).Name + ".asset";

			// create asset file
			return Create<T>(path);
		}
		#endif

		#endregion

		#region Save methods

		#if UNITY_EDITOR
		public static void SaveChanges<T>(this T instance) where T : ScriptableObject
		{
			// Save operation is allowed only on Unity Editor
			// and that too while player is in edit mode
			if (EditorApplication.isPlaying || EditorApplication.isPaused)
				return;

			if (typeof(ISaveAssetCallback).IsAssignableFrom(instance.GetType()))
			{
				((ISaveAssetCallback)instance).OnBeforeSave();
			}

			// Save the changes
			EditorUtility.SetDirty(instance);
			AssetDatabase.SaveAssets();

			AssetDatabase.Refresh();
		}
		#endif

		#endregion

		#region Load methods

		public static T LoadAssetAtPath<T>(string path) where T : ScriptableObject
		{
			#if UNITY_EDITOR
			return (T)AssetDatabase.LoadAssetAtPath(path, typeof(T));
			#else
			string 	resourcePath 		= "Assets/Resources/";
			string	pathInResources		= path.Replace(resourcePath, "");

			// remove file extension
			pathInResources				= Path.ChangeExtension(pathInResources, null);
			return Resources.Load<T>(pathInResources);
			#endif
		}

		#endregion
	}
}