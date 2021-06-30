using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VoxelBusters.Serialization
{
	#region Delegates

	public delegate void GameObjectCreateCallback(Scene scene, GameObject gameObject, string guid = null);
	public delegate void GameObjectRemoveCallback(Scene scene, GameObject gameObject);
	public delegate void ComponentCreateCallback(Scene scene, Component component, string guid = null);
	public delegate void ComponentRemoveCallback(Scene scene, Component component);

	public delegate void SceneLoadedCallback(Scene scene, LoadSceneMode mode);
	public delegate void SceneStateChangeCallback(Scene scene);

	#endregion
}