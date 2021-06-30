using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Array = System.Array;
using Enum = System.Enum;
using Type = System.Type;
#if UNITY_EDITOR
using AssetDatabase = UnityEditor.AssetDatabase;
using EditorObject = UnityEditor.SerializedObject;
using EditorProperty = UnityEditor.SerializedProperty;
#endif

namespace VoxelBusters.Serialization
{
    internal static class SerializationUtilityInternal
    {
        #region Type methods

        internal static bool CanSerialize(Type type, SerializationContext context)
        {
            // strict mode allows serialization of types with data provider only
            if (context.Supports(SerializationMethodOptions.StrictMode))
            {
                return (TypeServices.IsEnumType(type) || 
                        TypeServices.IsArrayType(type) || 
                        TypeServices.IsInterfaceType(type) || 
                        SerializationDataProviderServices.HasDataProvider(type));
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region Array methods

        public static int GetJaggedArrayRank()
        {
            return 0;
        }

        public static void GetArrayProperties(Type type, out Type elementType, out int rank)
        {
            elementType = TypeServices.GetElementType(type);
            rank        = TypeServices.GetArrayRank(type);
            if (1 == rank)
            {
                if (TypeServices.IsArrayType(elementType))
                {
                    rank        = 0; 
                    elementType = TypeServices.GetElementType(elementType);
                }
            }
        }

        internal static Type CreateArrayType(Type elementType, int rank)
        {
            if (0 == rank)
            {
                return elementType.MakeArrayType().MakeArrayType();
            }
            else if (1 == rank)
            {
                return elementType.MakeArrayType();
            }
            else
            {
                return elementType.MakeArrayType(rank);
            }
        }

        #endregion

        #region Enum utility methods

        internal static int FindEnumValueIndex(string enumStringValue, Type enumType)
        {
            return Array.FindIndex(Enum.GetNames(enumType), (_enum) => string.Equals(enumStringValue, _enum));
        }

        #endregion

        #region SerializationMethodOptions methods

        internal static bool Contains(this SerializationMethodOptions options, SerializationMethodOptions value)
        {
            return ((options & value) == value);
        }

        #endregion

        #region Editor methods

#if UNITY_EDITOR
        internal static void CreateTag(string tagName)
        {
            EditorObject tagManager = new EditorObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            EditorProperty tagsProperty = tagManager.FindProperty("tags");

            // first check if it is not already present
            bool found = false;
            for (int i = 0; i < tagsProperty.arraySize; i++)
            {
                EditorProperty element = tagsProperty.GetArrayElementAtIndex(i);
                if (string.Equals(element.stringValue, tagName))
                {
                    found = true;
                    break;
                }
            }

            // if not found, add it
            if (!found)
            {
                tagsProperty.InsertArrayElementAtIndex(0);
                EditorProperty newProperty = tagsProperty.GetArrayElementAtIndex(0);
                newProperty.stringValue = tagName;

                tagManager.ApplyModifiedProperties();
            }
        }
#endif
        #endregion
    }
}