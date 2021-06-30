using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VoxelBusters.Serialization
{
	internal class SceneObjectIdentifierStore : MonoBehaviour
    {
        #region Fields

		[SerializeField]
        private 	List<SceneObject> 	m_sceneObjects 		= new List<SceneObject>();

        #endregion

        #region Unity methods

        private void Awake()
        {
            // add to the manager
			SceneObjectIdentifierStoreManager.AddStore(gameObject.scene, this);
        }

        private void OnDestroy()
        {
			// unregister
			SceneObjectIdentifierStoreManager.RemoveStore(gameObject.scene, this);
        }

        #endregion

        #region Public methods

		// Add sceneObject to current database
		public void Add(SceneObject sceneObject)
		{
			m_sceneObjects.Add(sceneObject);
		}

        // Add gameobject to current database
		public void Add(GameObject gameObject)
        {
            m_sceneObjects.Add(new SceneObject(gameObject));
        }

		public void Add(GameObject gameObject, string id)
		{
			m_sceneObjects.Add(new SceneObject(gameObject, id));
		}

        // Remove gameobject from current database, if exists.
        public bool Remove(GameObject gameObject)
        {
			int index;
			if ((index = FindSceneObjectIndex(gameObject)) != -1)
			{		
				m_sceneObjects.RemoveAt(index);
				return true;
			}

            return false;
        }

        // Find the SceneObjectMetadata reference for the given gameobject
        public SceneObject FindSceneObject(GameObject gameObject)
        {
			int index;
			if ((index = FindSceneObjectIndex(gameObject)) != -1)
			{
				return m_sceneObjects[index];
			}

			return null;
        }

        public SceneObject FindSceneObject(string guid)
        {
			int index;
			if ((index = FindSceneObjectIndex(guid)) != -1)
			{
				return m_sceneObjects[index];
			}

			return null;
        }

		#endregion

		#region Editor methods

		#if UNITY_EDITOR
		// Sanitize the whole database
		public void Refresh()
        {
			#if SERIALIZATION_DEBUG
			Debug.Log("[Serialization] Updating scene id store.", this);
			#endif

			// delete all items where gameobject is already null
			m_sceneObjects.RemoveAll(item => (item.Object == null));

            // update all entries comparing with available gameobjects
			GameObject[] gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
			for (int i = 0; i < gameObjects.Length; i++)
            {
				GameObject eachGameObject = gameObjects[i];
                if (this.gameObject != eachGameObject)
                {
					SceneObject sceneObject = FindSceneObject(eachGameObject);
                    if (sceneObject == null)
                    {
                        Add(eachGameObject);
                    }
                    else
                    {
						// just pass a refresh so that the components updation will be refreshed internally
						sceneObject.Refresh(); 
					}
                }
            }

			// mark component as dirty
			EditorUtility.SetDirty(this);
        }
		#endif

        #endregion

        #region Private methods

        // To initialise the meta data initially
        private void InitialiseSceneObjectsMetadata()
        {
			GameObject[] gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
			for (int i = 0; i < gameObjects.Length; i++)
            {
				GameObject eachGameObject = gameObjects[i];
                if (this.gameObject != eachGameObject)
                {
                    Add(eachGameObject);
                }
            }
        }

		private int FindSceneObjectIndex(GameObject gameObject)
		{
			for (int i = 0; i < m_sceneObjects.Count; i++)
			{
				SceneObject currentObject = m_sceneObjects[i];
				if (currentObject.Object == gameObject)
				{
					return i;
				}
			}

			return -1;
		}

		private int FindSceneObjectIndex(string guid)
		{
			for (int i = 0; i < m_sceneObjects.Count; i++)
			{
				SceneObject currentObject = m_sceneObjects[i];
				if (currentObject.Guid == guid)
				{
					return i;
				}
			}

			return -1;
		}

        private void Reset()
        {
            if (m_sceneObjects == null || m_sceneObjects.Count == 0)
            {
                InitialiseSceneObjectsMetadata();
            }
        }

        #endregion
    }
}