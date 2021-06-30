using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using EditorProperty = UnityEditor.SerializedProperty;

namespace VoxelBusters.Serialization
{
	[CustomEditor(typeof(SerializationBehaviour))]
	public class SerializationBehaviourEditor : Editor 
	{
		#region Fields

		private		EditorProperty		m_keyProperty;
		private		EditorProperty		m_targetObjectsProperty;
		private		EditorProperty		m_hasCustomSettingsProperty;
		private		EditorProperty		m_customSettingsProperty;

		#endregion

		#region Editor methods

		private void OnEnable()
		{
			// set properties
			m_keyProperty				= serializedObject.FindProperty("m_key");
			m_targetObjectsProperty		= serializedObject.FindProperty("m_targetObjects");
			m_hasCustomSettingsProperty	= serializedObject.FindProperty("m_hasCustomSettings");
			m_customSettingsProperty	= serializedObject.FindProperty("m_customSettings");
		}
	
		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			EditorGUILayout.PropertyField(m_keyProperty, 			includeChildren: true);
			EditorGUILayout.PropertyField(m_targetObjectsProperty, 	includeChildren: true);
			DrawCustomSettingsProperty();
			serializedObject.ApplyModifiedProperties();
		}

		#endregion

		#region Private methods

		private void DrawCustomSettingsProperty()
		{
			EditorProperty 	propertyCopy	= m_customSettingsProperty.Copy();
			EditorProperty 	endProperty		= m_customSettingsProperty.GetEndProperty();
			bool 			enterChilderen	= true;

			// draw property
			m_hasCustomSettingsProperty.boolValue = EditorGUILayout.BeginToggleGroup(m_hasCustomSettingsProperty.displayName,
			                                                                         m_hasCustomSettingsProperty.boolValue);
			while (propertyCopy.NextVisible(enterChilderen))
			{
				EditorGUILayout.PropertyField(propertyCopy, includeChildren: true);
				enterChilderen = false;
				if (propertyCopy == endProperty)
				{
					break;
				}
			}
			EditorGUILayout.EndToggleGroup();
		}

		#endregion
	}
}