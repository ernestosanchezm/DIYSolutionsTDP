using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace VoxelBusters.Serialization
{
    [InitializeOnLoad]
    internal class SceneMetadataStoreModifier
    {
        #region Constructors

        static SceneMetadataStoreModifier()
        {
            // unregister from events
            EditorSceneAssetModificationProcessor.sceneSaved    -= OnSceneSaved;
            EditorSceneAssetModificationProcessor.sceneDeleted  -= OnSceneDeleted;
            UnityPlayerBuildSettings.sceneListChanged           -= OnSceneListChanged;

            // register to events
            EditorSceneAssetModificationProcessor.sceneSaved    += OnSceneSaved;
            EditorSceneAssetModificationProcessor.sceneDeleted  += OnSceneDeleted;
            UnityPlayerBuildSettings.sceneListChanged           += OnSceneListChanged;
        }

        #endregion

        #region Private static methods

        private static void RefreshData()
        {
            SceneMetadataStore.GetOrCreateStore().Refresh();
        }

        #endregion

        #region Callback methods

        private static void OnSceneSaved(Scene scene)
        {
            RefreshData();
        }

        private static void OnSceneDeleted(Scene scene)
        {
            RefreshData();
        }

        private static void OnSceneListChanged(SceneMetadata[] activeScenes)
        {
            RefreshData();
        }

        #endregion

        #region Nested types

#if !STOP_GENERATING_ASSETS
        private class SceneMetadataStorePostProcessor : AssetPostprocessor
        {
            #region Callback methods

            private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
            {
                if (deletedAssets != null)
                {
                    foreach (string eachAsset in deletedAssets)
                    {
                        if (eachAsset.EndsWith(SceneMetadataStore.kFileNameWithExtension))
                        {
                            SceneMetadataStore.GetOrCreateStore().Refresh();
                            EditorApplication.delayCall += () =>
                            {
                                AssetDatabase.Refresh();
                                RefreshData();
                            };
                        }
                    }
                }
            }

            #endregion
        }
#endif

        #endregion

    }
}