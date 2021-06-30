using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;

using Array = System.Array;
using Type	= System.Type;
using StringComparison = System.StringComparison;
using BindingFlag = System.Reflection.BindingFlags;

namespace VoxelBusters.Serialization
{
	internal class SerializationDataProviderGeneratorWindow : EditorWindow 
	{
		#region Fields

		private		string 					m_searchInput;
		private		Type[] 					m_types;
		private		string[] 				m_typeNames;
		private		int			 			m_selectedTypeIndex;

		private		SelectableMember[]		m_typeMembers;
		private		Vector2 				m_scrollPosition;

		#endregion

		#region Static methods

		internal static void ShowWindow()
		{
			// prepare the system
			SerializationDataProviderServices.SearchAllDataProviderTypes();

			// open window
			SerializationDataProviderGeneratorWindow window	= EditorWindow.GetWindow<SerializationDataProviderGeneratorWindow>();
			window.ShowPopup();
		}

		#endregion

		#region Editor methods

		private void OnEnable()
		{
			// set default values
			SetSearchInput(value: string.Empty);
		}

		private void OnGUI()
		{
			bool guiState = GUI.enabled;
			try
			{
				// ask for user input
				SetSearchInput(value: EditorGUILayout.TextField("Search type:", m_searchInput));

				// pick type
				GUI.enabled	= (m_types != null);
				SetTypeIndexInternal(value: EditorGUILayout.Popup(label: "Select type:",
				                                                  selectedIndex: m_selectedTypeIndex,
				                                                  displayedOptions: m_typeNames));

				// show properties available for selection 
				GUI.enabled	= (m_selectedTypeIndex != -1);
				if (m_typeMembers.Length > 0)
				{
					EditorGUILayout.LabelField("Choose the properties you want to include while serializing object.");

					EditorGUI.indentLevel++;
					m_scrollPosition = EditorGUILayout.BeginScrollView(m_scrollPosition);
					for (int iter = 0; iter < m_typeMembers.Length; iter++)
					{
						SelectableMember current 	= m_typeMembers[iter];
						current.CanSerialize		= EditorGUILayout.ToggleLeft(current.Name, current.CanSerialize);
					}
					EditorGUILayout.EndScrollView();
					EditorGUI.indentLevel--;
				}

				// show button to generate code
				if (GUILayout.Button("Generate Code"))
				{
					// filter the properties selected by the user 
					var 	groups			= m_typeMembers.Where((member) => member.CanSerialize)
						.Select((member) => member.Member)
						.GroupBy((member) => member.DeclaringType);
					Type[] 					sourceTypes			= groups.Select((item) => item.Key).ToArray();
					DeclaredMemberInfo[][] 	sourceMembersMap	= groups.Select((item) => item.ToArray()).ToArray();
					if (sourceTypes.Length == 0)
					{
						sourceTypes 		= new Type[] { GetSelectedType() };
						sourceMembersMap	= new DeclaredMemberInfo[][]
						{
							new DeclaredMemberInfo[0]
						};
					}

					// generate data providers for specified types
					for (int iter = (sourceTypes.Length - 1); iter >= 0; iter--)
					{
						Type 	sourceType 			= sourceTypes[iter];
						Type 	parentType 			= sourceType.BaseType;
						bool	parentHasProperties = (parentType != null) && (SerializationDataProviderServices.HasDataProvider(parentType) || ((iter + 1) < sourceTypes.Length));
						bool	dataProviderExists	= SerializationDataProviderServices.HasDataProvider(sourceType);

						if (!dataProviderExists)
						{
							try
							{
								DeclaredMemberInfo[] selectedMembers	= sourceMembersMap[iter];
								SerializationDataProviderGenerator.GenerateCodeForType(type: sourceType, 
								                                                       members: selectedMembers, 
								                                                       dependsOnParent: parentHasProperties);
							}
							catch (System.Exception)
							{
								Debug.LogWarning("[Serialization] Couldn't generate code for type: " + sourceType);
							}
						}
					}

					AssetDatabase.Refresh();
				}
			}
			finally
			{
				// reset gui state
				GUI.enabled	= guiState;
			}
		}

		#endregion

		#region Private methods

		private void SetSearchInput(string value)
		{
			if ((false == string.IsNullOrEmpty(value)) && string.Equals(m_searchInput, value))
			{
				return;
			}

			// copy new value
			m_searchInput = value;
			if (string.IsNullOrEmpty(m_searchInput))
			{
				m_types	= new Type[0];
			}
			else
			{
				m_types	= ReflectionServices.GetAllAssemblies()
					.Where((assembly) => (false == ReflectionServices.GetAssemblyShortName(assembly).StartsWith("UnityEditor")))
					.SelectMany((assembly) => assembly.GetTypes())
					.Where((type) => StringContains(TypeServices.GetTypeFullName(type), m_searchInput, StringComparison.OrdinalIgnoreCase) && IsCompatibleType(type))
					.ToArray();
			}
			m_typeNames	= Array.ConvertAll(m_types, (_type) => TypeServices.GetTypeFullName(_type));
			SetTypeIndexInternal(value: -1);
		}

		private void SetTypeIndexInternal(int value)
		{
			if ((value != -1) && (m_selectedTypeIndex == value))
			{
				return;
			}

			// assign new value
			m_selectedTypeIndex	= value;

			// update properties
			if (-1 == m_selectedTypeIndex)
			{
				m_typeMembers	= new SelectableMember[0];
			}
			else
			{
				m_typeMembers	= ReflectionServices.FindMembersOfType(GetSelectedType(), bindingAttribute: ReflectionServices.GetDefaultBindingAttributes())
					.Where((member) => (member.IsSerializable && !member.IsObsolete && IsCompatibleType(member.DeclaringType)))
					.Select((member) => new SelectableMember(member))
					.ToArray();
				                                                       
			}
			m_scrollPosition	= Vector2.zero;
		}

		private bool IsCompatibleType(Type type)
		{
			return !(TypeServices.IsNotPublic(type) || 
			         TypeServices.IsEnumType(type) || 
			         TypeServices.IsGenericType(type) || SerializationDataProviderGenerator.IsBlacklistedType(type));
		}

		private Type GetSelectedType()
		{
			try
			{
				return m_types[m_selectedTypeIndex];
			}
			catch
			{
				return null;
			}			                                                   
		}

		private bool StringContains(string a, string b, StringComparison comparison)
		{
			return a.IndexOf(b, comparison) >= 0;
		}

		#endregion

		#region Nested types

		private class SelectableMember
		{
			#region Properties

			public bool CanSerialize
			{
				get;
				set;
			}

			public DeclaredMemberInfo Member
			{
				get;
				set;
			}

			public string Name
			{
				get
				{
					return Member.Name;
				}
			}

			#endregion

			#region Constructors

			public SelectableMember(DeclaredMemberInfo member)
			{
				CanSerialize 	= true;
				Member			= member;
			}

			#endregion
		}

		#endregion

	}
}