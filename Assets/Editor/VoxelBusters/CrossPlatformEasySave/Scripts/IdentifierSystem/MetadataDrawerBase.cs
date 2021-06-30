using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using EditorProperty = UnityEditor.SerializedProperty;

namespace VoxelBusters.Serialization
{
	public class MetadataDrawerBase : PropertyDrawer 
	{
		public override void OnGUI(Rect position, EditorProperty property, GUIContent label)
		{
			bool	guiState 	= GUI.enabled;
			GUI.enabled 		= false;

			try
			{
				// define rects for each property
				float	flexiWidth	= 0.5f * position.width;
				Rect	objectRect 	= new Rect(position.x, position.y, flexiWidth, position.height);
				Rect	guidRect 	= new Rect(position.x + flexiWidth, position.y, position.width - flexiWidth, position.height);

				// draw internal properties
				EditorGUI.PropertyField(objectRect, property.FindPropertyRelative("m_object"), GUIContent.none);
				EditorGUI.PropertyField(guidRect, property.FindPropertyRelative("m_guid"), GUIContent.none);
			}
			finally
			{
				// reset state
				GUI.enabled		= guiState;
			}
		}
	}
}