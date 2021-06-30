using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using EditorProperty = UnityEditor.SerializedProperty;

namespace VoxelBusters.Serialization
{
	[CustomEditor(typeof(SceneObjectIdentifierStore))]
	public class SceneObjectIdentifierStoreEditor : Editor 
	{
		#region Fields

		private		EditorProperty	 		m_sceneObjectsProperty;

		#endregion

		#region Unity methods

		private void OnEnable()
		{
			// set properties
			m_sceneObjectsProperty	= serializedObject.FindProperty("m_sceneObjects");

			// update component
			((SceneObjectIdentifierStore)target).Refresh();
		}

		public override void OnInspectorGUI()
		{
			EditorProperty propertyCopy = m_sceneObjectsProperty.Copy();
			propertyCopy.NextVisible(enterChildren: true);
			while (propertyCopy.Next(enterChildren: false))
			{
				EditorGUILayout.PropertyField(propertyCopy, true);
			};
		}

		#endregion
	}
}