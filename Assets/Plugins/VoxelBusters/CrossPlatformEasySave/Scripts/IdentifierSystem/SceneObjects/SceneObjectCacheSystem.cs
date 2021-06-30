using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	internal class SceneObjectCacheSystem
	{
		#region Fields

		private		Dictionary<string, SceneObject>		sceneObjectMap;
		private		Dictionary<GameObject, string>		gameObjectMap;

		#endregion

		#region Constructors

		internal SceneObjectCacheSystem(int capacity)
		{
			// set properties
			sceneObjectMap	= new Dictionary<string, SceneObject>(capacity);
			gameObjectMap	= new Dictionary<GameObject, string>(capacity);
		}

		#endregion

		#region Public methods

		public bool TryGetGuid(GameObject gameObject, out string guid)
		{
			return gameObjectMap.TryGetValue(gameObject, out guid);
		}

		public bool TryGetSceneObject(string guid, out SceneObject sceneObject)
		{
			return sceneObjectMap.TryGetValue(guid, out sceneObject);
		}

		public bool TryGetSceneObject(GameObject gameObject, out SceneObject sceneObject)
		{
			string guid;
			if (gameObjectMap.TryGetValue(gameObject, out guid))
			{
				sceneObject	= sceneObjectMap[guid];
				return true;
			}

			sceneObject = null;
			return false;
		}

		public bool TryGetGameObject(string guid, out GameObject gameObject)
		{
			SceneObject sceneObject;
			if (sceneObjectMap.TryGetValue(guid, out sceneObject))
			{
				gameObject = sceneObject.Object;
				return true;
			}

			gameObject = null;
			return false;
		}

		public void Add(string guid, SceneObject value)
		{
			sceneObjectMap.Add(guid, value);
			gameObjectMap.Add(value.Object, guid);
		}

		public bool Remove(string guid)
		{
			SceneObject sceneObject;
			if (TryGetSceneObject(guid, out sceneObject))
			{
				sceneObjectMap.Remove(guid);
				gameObjectMap.Remove(sceneObject.Object);

				return true;
			}

			return false;
		}

		public bool Remove(GameObject gameObject)
		{
			string	guid;
			if (TryGetGuid(gameObject, out guid))
			{
				sceneObjectMap.Remove(guid);
				gameObjectMap.Remove(gameObject);
			
				return true;
			}

			return false;
		}

		public void Clear()
		{
			sceneObjectMap.Clear();
			gameObjectMap.Clear();
		}

		public SceneObject this[string key]
		{
			get
			{
				return sceneObjectMap[key];
			}
			set
			{
				sceneObjectMap[key]			= value;
				gameObjectMap[value.Object]	= key;
			}
		}

		#endregion
	}
}