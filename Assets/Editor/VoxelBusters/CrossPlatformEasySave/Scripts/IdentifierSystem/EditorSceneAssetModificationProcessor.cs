using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

using EditorAssetModificationProcessor = UnityEditor.AssetModificationProcessor;

namespace VoxelBusters.Serialization
{
	internal class EditorSceneAssetModificationProcessor : EditorAssetModificationProcessor
    {
        // Find out if path of scene gets changed
        // Find out if build index gets changed in EditorBuildSettings
        // Maintain list of scenes and its indices
        // Find out when a scene gets updated

		#region Static fields

		//private		static 		bool 		hierarchyChanged;

		#endregion

        #region vents

        public static event SceneStateChangeCallback sceneOpened;
		public static event SceneStateChangeCallback sceneCreated;
		public static event SceneStateChangeCallback sceneDeleted;
        public static event SceneStateChangeCallback sceneSaving;
		public static event SceneStateChangeCallback sceneSaved;
		public static event SceneStateChangeCallback sceneImported;

        #endregion

        #region Static constructors

        static EditorSceneAssetModificationProcessor()
        {
            // unregister from events
            EditorApplicationModificationProcessor.hierarchyChanged     -= OnHierarchyChanged;
            EditorSceneManager.newSceneCreated                          -= OnNewSceneCreated;
            EditorSceneManager.sceneOpened                              -= OnSceneOpened;
            EditorSceneManager.sceneSaving                              -= OnSceneSaving;
            EditorSceneManager.sceneSaved                               -= OnSceneSaved;

            // register for events
			EditorApplicationModificationProcessor.hierarchyChanged 	+= OnHierarchyChanged;
            EditorSceneManager.newSceneCreated                          += OnNewSceneCreated;
            EditorSceneManager.sceneOpened                              += OnSceneOpened;
            EditorSceneManager.sceneSaving                              += OnSceneSaving;
            EditorSceneManager.sceneSaved                               += OnSceneSaved;
        }

		#endregion

        #region EditorSceneManager callback methods

        private static void OnNewSceneCreated(Scene scene, NewSceneSetup setup, NewSceneMode mode)
        {
#if SERIALIZATION_DEBUG
            Debug.Log(string.Format("[AssetModificationProcessor] New scene {0} is created.", scene.name));
#endif
            if (sceneCreated != null)
            {
                sceneCreated(scene);
            }
        }

        private static void OnSceneOpened(Scene scene, OpenSceneMode mode)
        {
#if SERIALIZATION_DEBUG
            Debug.Log(string.Format("[AssetModificationProcessor] Scene {0} is opened.", scene.name));
#endif
            if (sceneOpened != null)
            {
                sceneOpened(scene);
            }
        }

        private static void OnSceneSaving(Scene scene, string path)
        {
#if SERIALIZATION_DEBUG
            Debug.Log(string.Format("[AssetModificationProcessor] Scene {0} is saving.", scene.name));
#endif
            if (sceneSaving != null)
            {
                sceneSaving(scene);
            }
        }

        private static void OnSceneSaved(Scene scene)
        {
#if SERIALIZATION_DEBUG
            Debug.Log(string.Format("[AssetModificationProcessor] Scene {0} is saved.", scene.name));
#endif
            if (sceneSaved != null)
            {
                sceneSaved(scene);
            }
        }

        #endregion

        #region AssetModificationProcessor callbacks

        /*
        private static void OnWillCreateAsset(string assetName)
        {
			if (SceneUtility.IsSceneAsset(assetName))
            {
                EditorApplication.delayCall += () =>
                {
					AssetDatabase.Refresh();
					
					Scene scene = SceneUtility.GetSceneByPath(assetName);
#if SERIALIZATION_DEBUG
					Debug.Log(string.Format("[AssetModificationProcessor] Scene {0} is created.", scene.name));
#endif
                    if (sceneCreated != null)
					{
                    	sceneCreated(scene);
					}
                };
            }
        }
        */

		private static AssetDeleteResult OnWillDeleteAsset(string assetName, RemoveAssetOptions options)
        {
			if (SceneUtility.IsSceneAsset(assetName))
            {
                EditorApplication.delayCall += () =>
                {
					Scene scene = SceneUtility.GetSceneByPath(assetName);
#if SERIALIZATION_DEBUG
					Debug.Log(string.Format("[AssetModificationProcessor] Scene {0} is deleted.", scene.name));
#endif

					if (sceneDeleted != null)
					{
                        sceneDeleted(scene);
					}
                };
            }

            return AssetDeleteResult.DidNotDelete;
        }

        /*
        private static AssetMoveResult OnWillMoveAsset(string sourcePath, string destinationPath)
        {
			if (SceneUtility.IsSceneAsset(sourcePath))
            {
                EditorApplication.delayCall += () =>
                {
					Scene scene = SceneUtility.GetSceneByPath(sourcePath);
#if SERIALIZATION_DEBUG
					Debug.Log(string.Format("[AssetModificationProcessor] Scene {0} is moved.", scene.name));
#endif

					if (sceneMoved != null)
					{
						sceneMoved(scene);
					}
                };
            }

            return AssetMoveResult.DidNotMove;
        }

		private static string[] OnWillSaveAssets(string[] paths)
        {
			try
			{
	            foreach (string path in paths)
	            {
					if (SceneUtility.IsSceneAsset(path))
	                {
	                    if (hierarchyChanged)
	                    {
							Scene scene = SceneUtility.GetSceneByPath(path);
							RaiseSceneModifiedEvent(scene);

	                        EditorApplication.delayCall += () =>
	                        {
#if SERIALIZATION_DEBUG
								Debug.Log(string.Format("[AssetModificationProcessor] Scene {0} is saved.", scene.name));
#endif

								if (sceneSaved != null)
								{
									sceneSaved(scene);
								}
	                        };
	                    }
	                }
	            }

	            return paths;
			}
			finally
			{
				// unset flag
				hierarchyChanged	= false;
			}
		}
		*/

		private static void OnHierarchyChanged()
		{
//			hierarchyChanged = true;
		}

		internal static void DidImportScene(string path)
		{
			Scene scene = SceneUtility.GetSceneByPath(path);
#if SERIALIZATION_DEBUG
			Debug.Log(string.Format("[AssetModificationProcessor] Scene {0} is imported.", scene.name));
#endif
			if (sceneImported != null)
			{
				sceneImported(scene);
			}
		}	

		#endregion

		#region Nested types

		private class ScenePostProcessor : AssetPostprocessor
		{
			#region Callback methods

			private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
			{
				if (importedAssets != null)
				{
					foreach (string importedAsset in importedAssets)
					{
						if (SceneUtility.IsSceneAsset(importedAsset))
						{
							DidImportScene(importedAsset);
						}
					}
				}
			}

			#endregion
		}

		#endregion
    }
}