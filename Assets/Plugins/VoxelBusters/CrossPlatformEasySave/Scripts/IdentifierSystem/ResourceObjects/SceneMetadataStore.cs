using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace VoxelBusters.Serialization
{
	internal class SceneMetadataStore : ScriptableObject
    {
		#region Constants

		internal	const	string 					kFileName				= "SceneMetadataStore";
        internal	const	string 					kFileNameWithExtension	= kFileName + ".asset";

		#endregion

        #region Fields

        [SerializeField]	
		private 			List<SceneMetadata> 	m_sceneMetaList 		= new List<SceneMetadata>();

        #endregion

		#region Public static methods

		public static SceneMetadataStore GetOrCreateStore()
		{
			SceneMetadataStore	store	= ResourcesServices.LoadAsset<SceneMetadataStore>(kFileName);
#if UNITY_EDITOR
			if (null == store)
			{
				store	= ResourcesServices.CreateAsset<SceneMetadataStore>(kFileName);
			}
#endif
			return store;
		}

		#endregion

        #region Editor methods

#if UNITY_EDITOR
		internal void Refresh()
        {
#if SERIALIZATION_DEBUG
			Debug.Log("[Serialization] Updating scene metadata container.", this);
#endif

			// remove old details
			m_sceneMetaList.Clear();
            
			// copy new information
			foreach (SceneMetadata meta in UnityPlayerBuildSettings.ActiveScenes)
            {
				m_sceneMetaList.Add(new SceneMetadata(name: meta.Name,
				                                      guid: meta.Guid,
				                                      path: meta.Path));
            }

			EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
#endif

        #endregion

        #region Utility methods

		public SceneMetadata GetSceneWithName(string name)
		{
			SceneMetadata	metadata	= null;
			for (int i = 0; i < m_sceneMetaList.Count; i++)
			{
				SceneMetadata scene = m_sceneMetaList[i];
				if (scene.Name == name)
				{
					metadata = scene;
					break;
				}
			}

			return metadata;
		}

        public SceneMetadata GetSceneWithPath(string path)
        {
			SceneMetadata	metadata	= null;
			for (int i = 0; i < m_sceneMetaList.Count; i++)
			{
				SceneMetadata scene = m_sceneMetaList[i];
				if (scene.Path == path)
                {
					metadata = scene;
                    break;
                }
            }

            return metadata;
        }

        public SceneMetadata GetSceneWithGUID(string guid)
        {
			SceneMetadata	metadata	= null;
			for (int i = 0; i < m_sceneMetaList.Count; i++)
            {
				SceneMetadata scene = m_sceneMetaList[i];
				if (scene.Guid == guid)
                {
                    metadata = scene;
                    break;
                }
            }

            return metadata;
        }

        #endregion
    }
}
