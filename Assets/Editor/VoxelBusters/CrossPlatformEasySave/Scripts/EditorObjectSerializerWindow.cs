using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using UnityObject = UnityEngine.Object;
using UnityEditorGUIUtility = VoxelBusters.External.UnityEditorUtils.EditorGUIUtility;

namespace VoxelBusters.Serialization
{
	public class EditorObjectSerializerWindow : EditorWindow 
	{
		#region Static fields

		private		static		string				keyName;
		private		static		UnityObject[]		targetObjects;
		private		static		bool				useCustomSettings;
		private		static		Settings			customSettings;

		private		static		Vector2 			scrollPosition;

		#endregion

		#region Static methods

		internal static void ShowWindow(UnityObject[] objects)
		{
			EditorObjectSerializerWindow window = EditorWindow.GetWindow<EditorObjectSerializerWindow>();
			window.ShowPopup();

			// set properties
			targetObjects		= objects;
		}

		#endregion

		#region Editor methods

		private void OnEnable()
		{
			ResetWindow();
		}

		private void OnGUI()
		{
			// check whether we have required data
			if (null == targetObjects)
			{
				return;
			}

			// draw the contents
			keyName			= EditorGUILayout.TextField("Key", keyName);
			EditorGUILayout.LabelField("Target Objects");

			scrollPosition	= EditorGUILayout.BeginScrollView(scrollPosition);
			int iter		= 0;
			EditorGUI.indentLevel++;
			foreach (UnityObject _targetObject in targetObjects)
			{
				EditorGUILayout.ObjectField(string.Format("Element {0}", iter++), _targetObject, _targetObject.GetType(), false);
			}
			EditorGUI.indentLevel--;
			EditorGUILayout.EndScrollView();

			useCustomSettings 					= EditorGUILayout.BeginToggleGroup("Has Custom Settings", useCustomSettings);
			customSettings.BufferSize 			= EditorGUILayout.IntField("Buffer Size", customSettings.BufferSize);
			customSettings.SerializationMethod	= UnityEditorGUIUtility.EnumFlagsField<SerializationMethodOptions>(EditorGUILayout.GetControlRect(true), "Serialization Method", customSettings.SerializationMethod);
			customSettings.StorageTarget 		= (StorageTarget)EditorGUILayout.EnumPopup("Storage Target", customSettings.StorageTarget);

			EditorGUILayout.EndToggleGroup();

			// draw button
			if (GUILayout.Button("Serialize"))
			{
				Serialize();	
			}
			GUILayout.FlexibleSpace();
		}

		#endregion

		#region Private methods

		private static void ResetWindow()
		{
			// reset properties
			keyName				= string.Empty;
			targetObjects 		= null;
			scrollPosition		= Vector2.zero;
			useCustomSettings	= false;
			customSettings		= SerializationSettings.Copy();
		}

		private static void Serialize()
		{ 
			// validate request
			if (string.IsNullOrEmpty(keyName))
			{
				throw ErrorCentre.Exception("Invalid key.");
			}
			if ((null == targetObjects) || (0 == targetObjects.Length))
			{
				throw ErrorCentre.Exception("Target objects array is invalid.");
			}

			// note: method supports save to disk only
			Settings settings 		= useCustomSettings ? customSettings : SerializationSettings.Copy();
			settings.StorageTarget	= StorageTarget.LocalDisk;

			SerializationManager.Serialize(keyName, targetObjects, settings.ToOptions());

			// show saved file location
			EditorUtility.RevealInFinder(SerializationEnvironment.DataPath);
		}

		#endregion
	}
}