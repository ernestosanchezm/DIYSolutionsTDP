#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Enum = System.Enum;
using Type = System.Type;
using Array = System.Array;
using Convert = System.Convert;

namespace VoxelBusters.External.UnityEditorUtils
{
	public class EditorGUIUtility 
	{
		#region Mask field methods

		public static void EnumFlagsField(Rect position, GUIContent label, SerializedProperty property, Type type)
		{
			property.intValue	= EnumFlagsField(position, label, property.intValue, type);
		}

		public static int EnumFlagsField(Rect position, GUIContent label, int value, Type type)
		{
			EditorGUI.BeginChangeCheck();
			#if UNITY_2017_3_OR_NEWER
			Enum	newValue	= EditorGUI.EnumFlagsField(position, label, GetValueAsEnum(value, type));
			#else
			Enum	newValue	= EditorGUI.EnumMaskField(position, label, GetValueAsEnum(value, type));
			#endif
			if (EditorGUI.EndChangeCheck())
			{
				return GetEnumAsInt(newValue, type);
			}
			return value;
		}

		public static T EnumFlagsField<T>(Rect position, string label, T value)
		{
			return (T)(object)EnumFlagsField(position, new GUIContent(label), (int)(object)value, typeof(T));
		}

		#endregion

		#region Private static methods

		private static Array GetEnumValues(Type type)
		{
			return Enum.GetValues(type);
		}

		private static Enum GetValueAsEnum(int value, Type type)
		{
			return (Enum)Enum.ToObject(type, value);
		}

		private static int GetEnumAsInt(Enum value, Type type)
		{
			int  newValueInt = Convert.ToInt32(value);

			// if "Everything" is set, force Unity to unset the extra bits by iterating through them
			if (newValueInt < 0)
			{
				int bits = 0;
				foreach (var enumValue in GetEnumValues(type))
				{
					int checkBit = newValueInt & (int)enumValue;
					if (checkBit != 0)
					{
						bits  |= (int)enumValue;
					}
				}
				newValueInt	= bits;
			}
			return newValueInt;
		}

		#endregion
	}
}
#endif