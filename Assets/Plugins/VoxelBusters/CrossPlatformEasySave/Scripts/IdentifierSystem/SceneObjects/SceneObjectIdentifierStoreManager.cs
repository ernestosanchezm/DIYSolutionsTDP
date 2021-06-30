using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

using Array = System.Array;

namespace VoxelBusters.Serialization
{
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	internal class SceneObjectIdentifierStoreManager
    {
        #region Fields

		private 	static		Dictionary<Scene, SceneObjectIdentifierStore> 	storeCollection;
		private 	static		SceneObjectCacheSystem 							sceneObjectCache;
		private 	static		SceneObjectIdentifierStore 						currentStore;

        #endregion

		#region Static constructors

		// in editor, constructor will be invoked by the attribute
		// whereas in runtime mode, scene id store's will indirectly invoke it while registering to this component
        static SceneObjectIdentifierStoreManager()
        {
			// initialise static properties
            storeCollection			= new Dictionary<Scene, SceneObjectIdentifierStore>();
			sceneObjectCache		= new SceneObjectCacheSystem(capacity: 8);

            // register for events
			SceneHierarchyModificationProcessor.gameObjectCreated 	+= HandleGameObjectCreated;
			SceneHierarchyModificationProcessor.gameObjectRemoved 	+= HandleGameObjectRemoved;
			SceneHierarchyModificationProcessor.componentCreated 	+= HandleComponentCreated;
			SceneHierarchyModificationProcessor.componentRemoved 	+= HandleComponentRemoved;
        }

        #endregion

        #region Registration methods

		internal static void AddStore(Scene scene, SceneObjectIdentifierStore store)
        {
#if SERIALIZATION_DEBUG
			Debug.Log(string.Format("[Serialization] Adding id store belonging to scene {0}", scene.name), store);
#endif
			storeCollection[scene] = store;
        }

		internal static void RemoveStore(Scene scene, SceneObjectIdentifierStore store)
        {
			if (storeCollection.Remove(scene))
            {
#if SERIALIZATION_DEBUG
				Debug.Log(string.Format("[Serialization] Removing id store belonging to scene {0}", scene.name), store);
#endif
            }
        }

        #endregion

		#region Query methods

		public static bool TryGetGameObjectGuid(GameObject gameObject, out string guid)
		{
			SceneObject sceneObject;

			// check in cache
			if (false == sceneObjectCache.TryGetSceneObject(gameObject, out sceneObject))
			{
				// search for scene object 
				sceneObject	= FindSceneObject(gameObject);
				if (null == sceneObject)
				{
#if SERIALIZATION_DEBUG
					Debug.LogWarning("[Serialization] Could not find guid for game object. Incase if this object is created at runtime, make sure you are instantiating objects using SerializationUtility.Instantiate API.", gameObject);
#endif

					guid	= null;
					return false;
				}			
			}

			guid = sceneObject.Guid;
			return true;
		}

		public static bool TryGetGameObjectWithGuid(string guid, out GameObject gameObject)
        {
			// check in cache
			if (false == sceneObjectCache.TryGetGameObject(guid, out gameObject))
			{
				// search for scene object with specified id
				SceneObject sceneObject	= FindSceneObjectWithGuid(guid);
				if (null == sceneObject)
				{
					gameObject	= null;
					return false;
				}
				gameObject	= sceneObject.Object;
			}

			return true;
		}

		public static bool TryGetComponentGuid(Component component, out string guid)
		{
			SceneObject sceneObject;

			// check in cache
			if (false == sceneObjectCache.TryGetSceneObject(component.gameObject, out sceneObject))
			{
				// search for scene object 
				sceneObject		= FindSceneObject(component.gameObject);
				if (null == sceneObject)
				{
#if SERIALIZATION_DEBUG
					Debug.LogWarning("[Serialization] Could not find guid for component. Incase if this component is created at runtime, make sure you are creating it using SerializationUtility.AddComponent API.", component);
#endif
					guid		= null;
					return false;
				}
			}

			guid = sceneObject.GetComponentGuid(component);
			return (guid != null);
		}

		public static bool TryGetComponentWithGuid(string guid, GameObject gameObject, out Component component)
		{
			SceneObject sceneObject;

			// check in cache
			if (false == sceneObjectCache.TryGetSceneObject(gameObject, out sceneObject))
			{
				// search for scene object 
				sceneObject		= FindSceneObject(gameObject);
				if (null == sceneObject)
				{
					component	= null;
					return false;
				}
			}

			component = sceneObject.GetComponentWithGuid(guid);
			return (component != null);
		}

		#endregion

		#region Private static methods

		private static SceneObject FindSceneObjectWithGuid(string guid)
		{
			SceneObject	sceneObject = null;

			// search in store
			foreach (SceneObjectIdentifierStore store in storeCollection.Values)
			{
				sceneObject = store.FindSceneObject(guid);
				if (sceneObject != null)
				{
					// add to cache
					sceneObjectCache[guid]	= sceneObject;

					return sceneObject;
				}
			}

			return null;
		}

		private static SceneObject FindSceneObject(GameObject gameObject)
		{
			SceneObject	sceneObject = null;

			// search in store
			foreach (SceneObjectIdentifierStore store in storeCollection.Values)
			{
				sceneObject = store.FindSceneObject(gameObject);
				if (sceneObject != null)
				{
					// add to cache
					sceneObjectCache[sceneObject.Guid]	= sceneObject;
					return sceneObject;
				}
			}

			return null;
		}

        private static SceneObjectIdentifierStore GetStore(Scene scene)
        {
            // find store entry
            SceneObjectIdentifierStore store;
            if (false == storeCollection.TryGetValue(scene, out store))
            {
                store       = FindIdStoreForScene(scene);
                if (store == null)
                {
                    Debug.LogWarning("[Serialization] Couldn't find id store for scene " + scene.name);
                    store   = CreateIdStore();
                }

                // safe case: changing code to dict.[value] based assignment instead of dic.Add
                // as new id store component registers itself as soon as its created
                storeCollection[scene]  = store;
            }

            return store;
        }

        #endregion

        #region Callback methods

		private static void HandleGameObjectCreated(Scene scene, GameObject gameObject, string guid)
        {
			SceneObjectIdentifierStore store = GetStore(scene);

			// check whether object created is already tracked or not
			if (guid != null)
			{
#if SERIALIZATION_DEBUG
				Debug.Log(string.Format("[Serialization] Adding known gameObject {0} to id store.", gameObject), gameObject);
#endif
				store.Add(gameObject, guid);
			}
			else
			{
#if SERIALIZATION_DEBUG
				Debug.Log(string.Format("[Serialization] Adding new gameObject hierarchy {0} to id store.", gameObject), gameObject);
#endif
				Transform[] transforms	= gameObject.GetComponentsInChildren<Transform>(includeInactive: true);
				int 		count 		= transforms.Length;
				for (int iter = (count - 1); iter >= 0; iter--)
				{
					store.Add(transforms[iter].gameObject);
				}
			}
        }

		private static void HandleGameObjectRemoved(Scene scene, GameObject gameObject)
        {
#if SERIALIZATION_DEBUG
			Debug.Log(string.Format("[Serialization] Removing gameObject hierarchy {0} from id store.", gameObject), gameObject);
#endif

			SceneObjectIdentifierStore	store 		= GetStore(scene);

			Transform[] transforms	= gameObject.GetComponentsInChildren<Transform>(includeInactive: true);
			int 		count 		= transforms.Length;
			for (int iter = (count - 1); iter >= 0; iter--)
			{
				GameObject currentGO = transforms[iter].gameObject;
				store.Remove(currentGO);

				// update cache
				sceneObjectCache.Remove(currentGO);
			}
        }

		private static void HandleComponentCreated(Scene scene, Component component, string guid)
        {
			SceneObjectIdentifierStore 	store 		= GetStore(scene);
			SceneObject 				sceneObject = store.FindSceneObject(component.gameObject);
			// check whether gameobject is tracked by id store
			if (null == sceneObject)
			{
				Debug.LogError("[Serialization] Failed to generate identifier for specified component. Check whether owner gameobject is tracked by the system.", component);
				return;
			}

			// add meta information for given component
			if (guid != null)
			{
#if SERIALIZATION_DEBUG
				Debug.Log(string.Format("[Serialization] Adding known component {0} to id store.", component), component);
#endif

				sceneObject.Add(component, guid);
			}
			else
			{
#if SERIALIZATION_DEBUG
				Debug.Log(string.Format("[Serialization] Adding new component {0} to id store.", component), component);
#endif

				sceneObject.Add(component);
			}
        }

		private static void HandleComponentRemoved(Scene scene, Component component)
        {
			SceneObjectIdentifierStore 	store 		= GetStore(scene);
			SceneObject 				sceneObject = store.FindSceneObject(component.gameObject);
			// check whether gameobject is tracked by id store
			if (null == sceneObject)
			{
				Debug.LogError("[Serialization] Failed to generate identifier for specified component. Check whether owner gameobject is tracked by the system.", component);
				return;
			}

#if SERIALIZATION_DEBUG
			Debug.Log(string.Format("[Serialization] Removing component {0} from id store.", component), component);
#endif

			sceneObject.Remove(component);
        }       

        #endregion

		#region Editor methods

#if UNITY_EDITOR
		internal static void Rebuild()
		{
            // rebuilding is not supported for additive mode
            if (EditorSceneManager.loadedSceneCount > 1)
            {
                Debug.LogWarning("[Serialization] Scene id store is not updated for additive modes.");
                return;
            }
 
			// flush invalid entries
			Scene[]	nullKeys	= storeCollection.Where((kvPair) => (kvPair.Value == null))
				.Select((kvPair) => kvPair.Key)
				.ToArray();
			foreach (Scene key in nullKeys)
			{
				storeCollection.Remove(key);
			}

            // update id information
            Scene                       scene       = EditorSceneManager.GetActiveScene();
			SceneObjectIdentifierStore  idStore     = FindIdStoreForScene(scene);
			if (null == idStore)
			{
				idStore     = CreateIdStore();
			}
#if SERIALIZATION_DEBUG
            idStore.gameObject.hideFlags	= HideFlags.None;
#else
			idStore.gameObject.hideFlags	= HideFlags.HideInHierarchy;
#endif
            idStore.Refresh();
		}
#endif

		#endregion

		#region Internal methods

        internal static bool HasIdStoreForScene(Scene scene)
        { 
            GameObject[]    storeObjects    = GameObject.FindGameObjectsWithTag(Constants.kSceneObjectIdStoreTagName);
            return (storeObjects.Length != 0);
        }

        internal static SceneObjectIdentifierStore CreateIdStore()
        {
#if SERIALIZATION_DEBUG
            Debug.Log(string.Format("[Serialization] Creating new id store for scene {0}.", SceneManager.GetActiveScene().name));
#endif
            GameObject      gameObject      = new GameObject("SceneObjectIdentifierStore");
            gameObject.tag                  = Constants.kSceneObjectIdStoreTagName;
#if UNITY_EDITOR
            Scene           scene           = EditorSceneManager.GetActiveScene();
            EditorSceneManager.MarkSceneDirty(scene);
#endif
            return gameObject.AddComponent<SceneObjectIdentifierStore>();
        }

        internal static SceneObjectIdentifierStore FindIdStoreForScene(Scene scene)
        {
            GameObject[]    storeObjects    = GameObject.FindGameObjectsWithTag(Constants.kSceneObjectIdStoreTagName);
            int             objectCount     = storeObjects.Length;

            for (int iter = 0; iter < objectCount; iter++)
            {
                GameObject  current         = storeObjects[iter];
                if (current.scene == scene)
                {
                    return current.GetComponent<SceneObjectIdentifierStore>();
                }
            }

            return null;
        }

		#endregion
    }
}