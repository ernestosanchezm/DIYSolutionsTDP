using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace VoxelBusters.Serialization
{
	internal class RuntimeSceneModificationProcessor
    {
        #region Static constructors

        static RuntimeSceneModificationProcessor()
        {
			// create scene transition tracker
			SceneManager.sceneLoaded   += OnSceneLoaded;
			SceneManager.sceneUnloaded += OnSceneUnloaded;
		}

        #endregion

		#region Static events

		public static event SceneLoadedCallback sceneLoaded;
		public static event SceneStateChangeCallback sceneUnloaded;

		#endregion

		#region Callback methods

		private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			if (sceneLoaded != null)
			{
				sceneLoaded(scene, mode);
			}
		}

		private static void OnSceneUnloaded(Scene scene)
		{
			if (sceneUnloaded != null)
			{
				sceneUnloaded(scene);
			}
		}

//		private static void OnActiveSceneChange(Scene current, Scene next)
//		{
//			OnSceneUnloaded(current);
//			OnSceneLoaded(next, LoadSceneMode.Single);
//		}

//		internal static void RefreshScene(Scene scene)
//      {
//			SceneModificationProcessor.DidModifyScene(scene);
//      }

		#endregion
    }
}