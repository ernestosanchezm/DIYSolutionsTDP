using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Type = System.Type;

namespace VoxelBusters.Serialization
{
	/// <summary>
	/// Utility functions for implementing and extending the serialization support for Unity objects created at runtime.
	/// </summary>
	public static class SerializationUtility 
	{
		#region GameObject methods

		/// <summary>
		/// Creates a new game object.
		/// </summary>
		/// <returns>The game object.</returns>
		public static GameObject CreateGameObject()
		{
			GameObject newObject = new GameObject();
			SceneHierarchyModificationProcessor.DidCreateGameObject(newObject);

			return newObject;
		}

		/// <summary>
		/// Creates a new game object with specified name.
		/// </summary>
		/// <returns>The game object.</returns>
		/// <param name="name">The name that the GameObject is created with.</param>
		public static GameObject CreateGameObject(string name)
		{
			GameObject newObject = new GameObject(name);
			SceneHierarchyModificationProcessor.DidCreateGameObject(newObject);

			return newObject;
		}

		/// <summary>
		/// Creates the game object with specified name and components.
		/// </summary>
		/// <returns>The game object.</returns>
		/// <param name="name">The name that the GameObject is created with.</param>
		/// <param name="components">An array of Components to add to the GameObject on creation.</param>
		public static GameObject CreateGameObject(string name, params Type[] components)
		{
			GameObject newObject = new GameObject(name, components);
			SceneHierarchyModificationProcessor.DidCreateGameObject(newObject);

			return newObject;
		}

		internal static GameObject CreateGameObjectWithGuid(string guid)
		{
			GameObject newObject = new GameObject();
			SceneHierarchyModificationProcessor.DidCreateGameObject(newObject, guid);

			return newObject;
		}

		#endregion

		#region Instantiate methods

		/// <summary>
		/// Clones the object original and returns the clone.
		/// </summary>
		/// <param name="original">An existing object that you want to make a copy of.</param>
		public static GameObject Instantiate(GameObject original)
		{
			GameObject newCopy = GameObject.Instantiate(original);
			SceneHierarchyModificationProcessor.DidCreateGameObject(newCopy);

			return newCopy;
		}

		/// <summary>
		/// Clones the object original and returns the clone.
		/// </summary>
		/// <param name="original">An existing object that you want to make a copy of.</param>
		/// <param name="position">Position for the new object.</param>
		/// <param name="rotation">Orientation of the new object.</param>
		public static GameObject Instantiate(GameObject original, Vector3 position, Quaternion rotation)
		{
			GameObject newCopy = GameObject.Instantiate(original, position, rotation);
			SceneHierarchyModificationProcessor.DidCreateGameObject(newCopy);

			return newCopy;
		}

		/// <summary>
		/// Clones the object original and returns the clone.
		/// </summary>
		/// <param name="original">An existing object that you want to make a copy of.</param>
		/// <param name="parent">Parent that will be assigned to the new object.</param>
		/// <param name="worldPositionStays">Pass true when assigning a parent Object to maintain the world position of the Object, instead of setting its position relative to the new parent. Pass false to set the Object's position relative to its new parent.</param>
		public static GameObject Instantiate(GameObject original, Transform parent, bool worldPositionStays)
		{
			GameObject newCopy = GameObject.Instantiate(original, parent, worldPositionStays);
			SceneHierarchyModificationProcessor.DidCreateGameObject(newCopy);

			return newCopy;
		}

		/// <summary>
		/// Clones the object original and returns the clone.
		/// </summary>
		/// <param name="original">An existing object that you want to make a copy of.</param>
		/// <param name="position">Position for the new object.</param>
		/// <param name="rotation">Orientation of the new object.</param>
		/// <param name="parent">Parent that will be assigned to the new object.</param>
		public static GameObject Instantiate(GameObject original, Vector3 position, Quaternion rotation, Transform parent)
		{
			GameObject newCopy = GameObject.Instantiate(original, position, rotation, parent);
			SceneHierarchyModificationProcessor.DidCreateGameObject(newCopy);

			return newCopy;
		}

		#endregion

		#region Component methods

		/// <summary>
		/// Adds a component class of type componentType to the game object.
		/// </summary>
		/// <returns>The component attached to the game object.</returns>
		/// <param name="gameObject">Game object.</param>
		/// <param name="componentType">Component type.</param>
		public static Component AddComponent(GameObject gameObject, Type componentType)
		{
			Component	newComponent	= gameObject.AddComponent(componentType);
			SceneHierarchyModificationProcessor.DidCreateComponent(newComponent);
			return newComponent;
		}

		internal static Component AddComponentWithGuid(GameObject gameObject, Type componentType, string guid)
		{
			Component	newComponent	= gameObject.AddComponent(componentType);
			SceneHierarchyModificationProcessor.DidCreateComponent(newComponent, guid);
			return newComponent;
		}

		/// <summary>
		/// Adds a component class of type T to the game object.
		/// </summary>
		/// <returns>The component attached to the game object.</returns>
		/// <param name="gameObject">Game object.</param>
		public static T AddComponent<T>(GameObject gameObject) where T : Component
		{
			T	newComponent	= gameObject.AddComponent<T>();
			SceneHierarchyModificationProcessor.DidCreateComponent(newComponent);
			return newComponent;
		}

		internal static T AddComponentWithGuid<T>(GameObject gameObject, string guid) where T : Component
		{
			T	newComponent	= gameObject.AddComponent<T>();
			SceneHierarchyModificationProcessor.DidCreateComponent(newComponent, guid);
			return newComponent;
		}

		#endregion

		#region Destory methods

		/// <summary>
		/// Removes the specified gameobject.
		/// </summary>
		/// <param name="gameObject">The game object to be destroyed.</param>
		public static void Destroy(GameObject gameObject)
		{
			SceneHierarchyModificationProcessor.DidRemoveGameObject(gameObject);
			Object.Destroy(gameObject);
		}

		/// <summary>
		/// Removes the specified component.
		/// </summary>
		/// <param name="gameObject">The component to be destroyed.</param>
		public static void Destroy(Component component)
		{
			SceneHierarchyModificationProcessor.DidRemoveComponent(component);
			Object.Destroy(component);
		}

		#endregion
	}
}