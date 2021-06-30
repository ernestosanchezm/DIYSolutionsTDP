#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace VoxelBusters.Serialization
{
	[InitializeOnLoad]
	internal class UnityPlayerBuildSettings
    {
		#region Static fields

		private 	static 	int 	previousSceneCount 	= 0;

        #endregion

		#region Static property

		internal static SceneMetadata[] Scenes
		{
			get;
			private set;
		}

		internal static SceneMetadata[] ActiveScenes
		{
			get;
			private set;
		}

		#endregion

        #region Delegates

		public delegate void SceneListChangedCallback(SceneMetadata[] activeScenes);

        #endregion

        #region Static events

        public static event SceneListChangedCallback sceneListChanged;

        #endregion

		#region Static constructors

        static UnityPlayerBuildSettings()
		{
#if UNITY_2017_3_OR_NEWER
            EditorBuildSettings.sceneListChanged -= NotifySceneListChangedEvent;
            EditorBuildSettings.sceneListChanged += NotifySceneListChangedEvent;
#else
            EditorApplication.update -= MonitorBuildSceneSettings;
            EditorApplication.update += MonitorBuildSceneSettings;
#endif
            UpdateSceneInfo();
        }

        #endregion

        #region Private static methods
#if !UNITY_2017_3_OR_NEWER
        private static int GetActiveSceneCount()
        {
            return EditorBuildSettings.scenes.Length;
        }
#endif

        private static void UpdateSceneInfo()
		{
			List<SceneMetadata> 	metaList 	= new List<SceneMetadata>(4);
			foreach (EditorBuildSettingsScene buildSettingsScene in EditorBuildSettings.scenes)
			{
				string 			path 		= buildSettingsScene.path;
				string 			guid 		= AssetDatabase.AssetPathToGUID(path);
				string 			name 		= IOServices.GetFileNameWithoutExtension(path);

				SceneMetadata 	meta 		= new SceneMetadata(name, guid, path, buildSettingsScene.enabled);
				metaList.Add(meta);
			}

			// copy values
			Scenes 				= metaList.ToArray();
			ActiveScenes		= metaList.Where((scene) => scene.Enabled).ToArray();
		}

		private static void NotifySceneListChangedEvent()
		{
			if (sceneListChanged != null)
			{
                UpdateSceneInfo();
                sceneListChanged(ActiveScenes);
			}
		}

        #endregion

        #region Callback methods

#if !UNITY_2017_3_OR_NEWER
        private static void MonitorBuildSceneSettings()
        {
            int currentCount = GetActiveSceneCount();
			if (previousSceneCount != currentCount)
            {
                NotifySceneListChangedEvent();

				previousSceneCount	= currentCount;
            }
        }
#endif
        #endregion
    }
}
#endif