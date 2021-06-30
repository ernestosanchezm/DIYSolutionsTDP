using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Enum = System.Enum;

namespace VoxelBusters.Serialization
{
	[CustomEditor(typeof(SerializationSettings))]
	public class SerializationSettingsEditor : Editor 
	{
        #region Fields

        private     string              m_pluginName;
        private     string              m_pluginVersion;
        private     string              m_copyrightInfo;
        private     SerializedProperty  m_firstProperty;
        private     TabButton[]         m_tabButtons;

        #endregion

        #region Unity methods

        public void OnEnable()
        {
            // set properties
            m_pluginName        = Constants.kPluginDisplayName;
            m_pluginVersion     = "Version " + Constants.kPluginVersion;
            m_copyrightInfo     = Constants.kCopyright;

            m_firstProperty     = GetFirstProperty();
            m_tabButtons        = new TabButton[]
            {
                new TabButton { displayName = "Documentation",  guiStyle = "ButtonLeft" },
                new TabButton { displayName = "Forum",          guiStyle = "ButtonMid" },
                new TabButton { displayName = "Tutorial",       guiStyle = "ButtonMid" },
                new TabButton { displayName = "Videos",         guiStyle = "ButtonMid" },
                new TabButton { displayName = "Support",        guiStyle = "ButtonMid" },
                new TabButton { displayName = "Write Review",    guiStyle = "ButtonRight" },
            };
        }

        public override void OnInspectorGUI()
		{
			serializedObject.Update();
			try
			{
                ShowProductInfo();
                EditorGUILayout.Space();
                DrawTopBarButtons();
                ShowGlobalSettings();
                EditorGUILayout.Space();
                ShowUtility();
                EditorGUILayout.Space();
            }
            finally
			{
				serializedObject.ApplyModifiedProperties();
			}
		}

		#endregion

        #region Private methods

        private void ShowProductInfo()
        {
            // show basic information
            GUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Label(m_pluginName);
            GUILayout.Label(m_pluginVersion);
            GUILayout.Label(m_copyrightInfo, EditorStyles.wordWrappedLabel);
            GUILayout.EndVertical();
        }

        private void DrawTopBarButtons()
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                for (int iter = 0; iter < m_tabButtons.Length; iter++)
                {
                    TabButton button = m_tabButtons[iter];
                    if (GUILayout.Button(button.displayName, button.guiStyle))
                    {
                        OnTabButtonClick(iter, button);
                    }
                }
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();
        }

        private void OnTabButtonClick(int index, TabButton button)
        {
            switch (button.displayName)
            {
                case "Documentation":
                    Application.OpenURL("http://bit.ly/2NU8fdP");
                    break;

                case "Forum":
                    Application.OpenURL("http://bit.ly/2q6clWP");
                    break;

                case "Tutorial":
                    Application.OpenURL("http://bit.ly/2JbJDwH");
                    break;

                case "Videos":
                    Application.OpenURL("http://bit.ly/2R8kbLe");
                    break;

                case "Support":
                    Application.OpenURL("http://bit.ly/2J8Ukji");
                    break;

                case "Write Review":
                    UnityEditorInternal.AssetStore.Open("content/129177");
                    break;

                default:
                    break;
            }
        }

        private void ShowGlobalSettings()
        {
            GUILayout.Label("Global Settings", EditorStyles.boldLabel);
            SerializedProperty current = m_firstProperty.Copy();
            while (current.Next(false))
            {
                EditorGUILayout.PropertyField(current);
            }
        }

        private void ShowUtility()
        {
            EditorGUILayout.BeginHorizontal();
            // show options to generate code
            EditorGUILayout.HelpBox("You can optimise class serialization by creating data providers.", MessageType.Info, true);
            if (GUILayout.Button("Create", GUILayout.Height(38f)))
            {
                Menu.OpenDataProviderGeneratorWindow();
            }
            EditorGUILayout.EndHorizontal();
        }

        private SerializedProperty GetFirstProperty()
        {
            SerializedProperty  baseProperty = serializedObject.GetIterator();
            bool                canEnter     = true;
            while (baseProperty.Next(canEnter))
            {
                if (baseProperty.name == "m_EditorClassIdentifier")
                {
                    break;
                }
                canEnter = false;
            }

            return baseProperty.Copy();
        }

        #endregion

        #region Nested types

        private struct TabButton
        {
            public string displayName;
            public string guiStyle;
        }

        #endregion
    }
}