using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VoxelBusters.Serialization
{
	internal class SceneHierarchyModificationProcessor
    {
       #region Static events

		public static event GameObjectCreateCallback gameObjectCreated;
		public static event GameObjectRemoveCallback gameObjectRemoved;
		public static event ComponentCreateCallback componentCreated;
		public static event ComponentRemoveCallback componentRemoved;

        #endregion

		#region Static constructors

        static SceneHierarchyModificationProcessor()
        {}

        #endregion

        #region Internal callback methods

		internal static void DidCreateGameObject(GameObject gameObject, string guid = null)
        {
			if (gameObjectCreated != null)
			{
				gameObjectCreated(gameObject.scene, gameObject, guid);
			}
        }

		internal static void DidRemoveGameObject(GameObject gameObject)
        {
			if (gameObjectRemoved != null)
			{
				gameObjectRemoved(gameObject.scene, gameObject);
			}
        }

		internal static void DidCreateComponent(Component component, string guid = null)
		{
			if (componentCreated != null)
			{
				componentCreated(component.gameObject.scene, component, guid);
			}
		}

		internal static void DidRemoveComponent(Component component)
        {
			if (componentRemoved != null)
			{
				componentRemoved(component.gameObject.scene, component);
			}
        }

        #endregion
    }
}