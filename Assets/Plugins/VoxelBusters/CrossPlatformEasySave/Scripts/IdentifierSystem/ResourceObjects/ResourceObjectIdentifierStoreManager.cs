using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

using Type = System.Type;

namespace VoxelBusters.Serialization
{
	internal class ResourceObjectIdentifierStoreManager
    {
        #region Static fields

		private		static	ResourceObjectCacheSystem 				resourceCache;

		private 	static	List<ResourceObjectIdentifierStore> 	activeStoreList;
		private 	static	SceneMetadataStore 						sceneMetaStore;

        #endregion

        #region Static constructors

        static ResourceObjectIdentifierStoreManager()
        {
			// set properties
            activeStoreList 	= new List<ResourceObjectIdentifierStore>();
            resourceCache 		= new ResourceObjectCacheSystem(capacity: 4);

			// load scene info container
			sceneMetaStore 		= SceneMetadataStore.GetOrCreateStore();

			if (Application.isPlaying)
			{
				// register for events
				RuntimeSceneModificationProcessor.sceneLoaded 		+= OnSceneLoaded;
				RuntimeSceneModificationProcessor.sceneUnloaded 	+= OnSceneUnloaded;

				// manually set current scene
				OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
			}
        }

        #endregion

        #region Query methods

		public static bool TryGetObjectWithGuid(string guid, Type type, out Object asset)
        {
            // check in cache
//			if (cache.TryGetValue(guid, out asset))
//			{
//				return true;
//			}

			// check whether resource has to be loaded on demand
			if (false == SerializationSettings.PreloadResourceObjects)
			{
				LoadAllResourceStores();
			}

			// find object with specified id
			foreach (ResourceObjectIdentifierStore idStore in activeStoreList)
			{
				ResourceObject resourceObject = idStore.Find(guid, type);
				if (resourceObject != null)
				{
					// save result
					asset = resourceObject.Object;

					// update cache
					resourceCache.Add(guid, asset);

					return true;
				}
			}

			#if SERIALIZATION_DEBUG
			Debug.LogWarning("[Serialization] Could not find resource object with guid: " + guid);
			#endif
			asset	= null;
			return false;
        }

		public static bool TryGetObjectGuid(Object asset, out string guid)
        {
			// check in cache
//			if (cache.TryGetKey(asset, out guid))
//			{
//				return true;
//			}

			// check whether resource has to be loaded on demand
			if (false == SerializationSettings.PreloadResourceObjects)
			{
				LoadAllResourceStores();
			}

			// iterate and find for matching object
			foreach (ResourceObjectIdentifierStore idStore in activeStoreList)
			{
				ResourceObject resourceObject = idStore.Find(asset);
				if (resourceObject != null)
				{
					// save result
					guid = resourceObject.Guid;

					// update cache
					resourceCache.Add(guid, asset);

					return true;
				}
			}

			#if SERIALIZATION_DEBUG
			Debug.LogWarning("[Serialization] Could not find guid for resource object: " + asset.name, asset);
			#endif
			guid	= null;
			return false;
        }

        #endregion

        #region Internal methods

		internal static ResourceObjectIdentifierStore LoadResourceStore(string sceneGuid)
		{
			return ResourcesServices.LoadAsset<ResourceObjectIdentifierStore>(sceneGuid);
		}

		internal static void UnloadAllResourceStores()
		{
			for (int iter = 0; iter < activeStoreList.Count; iter++)
			{
				ResourceObjectIdentifierStore store	= activeStoreList[iter];
				ResourcesServices.UnloadAsset(store);
			}

			activeStoreList.Clear();
		}

		#endregion

		#region Private static methods

		private static void LoadAllResourceStores()
		{
			int 	activeSceneCount	= SceneManager.sceneCount;
			for (int iter = 0; iter < activeSceneCount; iter++)
			{
				Scene	scene			= SceneManager.GetSceneAt(iter);
				if (-1 == FindResourceStoreIndex(scene))
				{
					ResourceObjectIdentifierStore idStore = LoadResourceStore(scene);
					if (idStore == null)
					{
						Debug.LogWarning("[Serialization] Couldn't find resource id store for scene with name: " + scene.name);
						continue;
					}

					activeStoreList.Add(idStore);
				}
			}
		}

		private static ResourceObjectIdentifierStore LoadResourceStore(Scene scene)
		{
			SceneMetadata	sceneMeta 	= sceneMetaStore.GetSceneWithName(scene.name);
			if (sceneMeta != null)
			{
				return LoadResourceStore(sceneGuid: sceneMeta.Guid);
			}
			return null;
		}

		private static int FindResourceStoreIndex(Scene scene)
		{
			SceneMetadata	details 	= sceneMetaStore.GetSceneWithName(scene.name);
			if (details != null)
			{
				string	 	sceneGuid	= details.Guid;
				for (int i = 0; i < activeStoreList.Count; i++)
				{
					if (activeStoreList[i].name == sceneGuid)
					{
						return i;
					}
				}
			}

			return -1;
		}

        #endregion

        #region Callback methods

		private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
			if (/*SerializationSettings.PreloadResourceObjects &&*/ (-1 == FindResourceStoreIndex(scene)))
			{
				ResourceObjectIdentifierStore store = LoadResourceStore(scene);
				if (store != null)
				{
					#if SERIALIZATION_DEBUG
					Debug.Log("[Serialization] Loading resource store for scene: " + scene.name);
					#endif
					activeStoreList.Add(store);
				}
			}
        }

		private static void OnSceneUnloaded(Scene scene)
        {
			int storeIndex;
			if ((storeIndex = FindResourceStoreIndex(scene)) != -1)
			{
				#if SERIALIZATION_DEBUG
				Debug.Log("[Serialization] Unloading resource id store for scene: " + scene.name);
				#endif
				ResourceObjectIdentifierStore store	= activeStoreList[storeIndex];
				activeStoreList.RemoveAt(storeIndex);

				// unload the resoure store
				ResourcesServices.UnloadAsset(store);
			}
        }

        #endregion
    }
}