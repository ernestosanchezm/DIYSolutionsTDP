using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace VoxelBusters.Serialization
{
	[InitializeOnLoad]
	public class ResourceObjectIdentifierStoreModifier 
	{
		#region Constructors

		static ResourceObjectIdentifierStoreModifier()
		{
			// unregister from events
			EditorSceneAssetModificationProcessor.sceneSaved 	-= OnSceneSaved;
			EditorSceneAssetModificationProcessor.sceneDeleted 	-= OnSceneDeleted;
			UnityPlayerBuildSettings.sceneListChanged 			-= OnSceneListChanged;

			// register to events
			EditorSceneAssetModificationProcessor.sceneSaved 	+= OnSceneSaved;
			EditorSceneAssetModificationProcessor.sceneDeleted 	+= OnSceneDeleted;
			UnityPlayerBuildSettings.sceneListChanged 			+= OnSceneListChanged;
		}

        #endregion

        #region Public static methods

        public static void ForceRefreshStores()
        {
            RebuildAllResourceStore();
        }

        #endregion

		#region Private static methods

		private static void RebuildAllResourceStore()
		{
			string[]	guids	= AssetDatabase.FindAssets("t:Scene");
			foreach (string guid in guids)
			{
				ResourceObjectIdentifierStore	store	= ResourceObjectIdentifierStoreManager.LoadResourceStore(guid);

				// check whether scene is used in this project
				// remove unused scene assets
				if (false == SceneUtility.IsSceneInBuildSettings(guid))
				{
					if (store != null)
					{
#if SERIALIZATION_DEBUG
						string path = AssetDatabase.GUIDToAssetPath(guid);
						Debug.Log("[Serialization] Deleting resource store belonging to scene " + IOServices.GetFileNameWithoutExtension(path));
#endif
						AssetDatabase.DeleteAsset(path: AssetDatabase.GetAssetPath(store));
					}

					continue;
				}

				// create store if not found
				if (null == store)
				{
					store		= ResourcesServices.CreateAsset<ResourceObjectIdentifierStore>(guid);
				}

				store.Refresh();
				Resources.UnloadAsset(store);
			}

			AssetDatabase.Refresh();
		}

		#endregion

		#region Callback methods

		private static void OnSceneSaved(Scene scene)
		{
			if (SceneUtility.IsSceneInBuildSettings(scene))
			{
				ResourceObjectIdentifierStore store = ResourceObjectIdentifierStoreManager.LoadResourceStore(sceneGuid: SceneUtility.GetSceneGuid(scene));
				if (store != null)
				{
					store.Refresh();
					Resources.UnloadAsset(store);
				}
			}
		}

		private static void OnSceneDeleted(Scene scene)
		{
			RebuildAllResourceStore();
		}

		private static void OnSceneListChanged(SceneMetadata[] activeScenes)
		{
			RebuildAllResourceStore();
		}

		#endregion
	}
}