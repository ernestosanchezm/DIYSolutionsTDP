using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace VoxelBusters.Serialization
{
	[InitializeOnLoad]
	public class SceneObjectIdentifierStoreModifier 
	{
		#region Constructors

		static SceneObjectIdentifierStoreModifier()
		{
			// unregister from events
            EditorSceneAssetModificationProcessor.sceneOpened   -= OnSceneOpened;
			EditorSceneAssetModificationProcessor.sceneSaving   -= OnSceneSaving;

			// register to events
            EditorSceneAssetModificationProcessor.sceneOpened   += OnSceneOpened;
			EditorSceneAssetModificationProcessor.sceneSaving   += OnSceneSaving;
		}

		#endregion

		#region Callback methods

        private static void OnSceneOpened(Scene scene)
        {
            if (false == SceneObjectIdentifierStoreManager.HasIdStoreForScene(scene))
            {
                SceneObjectIdentifierStoreManager.CreateIdStore();
            }
        }

		private static void OnSceneSaving(Scene scene)
		{
			SceneObjectIdentifierStoreManager.Rebuild();
		}

		#endregion
	}
}