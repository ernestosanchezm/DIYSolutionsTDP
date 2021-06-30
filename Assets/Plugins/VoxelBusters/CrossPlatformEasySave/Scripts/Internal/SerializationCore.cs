using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using Type = System.Type;
using Exception = System.Exception;

namespace VoxelBusters.Serialization
{
	internal partial class SerializationCore
	{
		#region Defaults

		private class Defaults
		{
			internal		const		long		kNullObjectReferenceIdValue		= 0;
		}

		#endregion

		#region Misc. methods

		internal static bool IsNull(long objectReferenceIdValue)
		{
			return (objectReferenceIdValue == GetNullObjectReferenceIdValue());
		}

		internal static long GetNullObjectReferenceIdValue()
		{
			return Defaults.kNullObjectReferenceIdValue;
		}

		internal static bool IsReservedPropertyName(string name)
		{
			return (name != null && name.StartsWith("$"));
		}

		internal static SerializedPropertyType GetPropertyType(Type type)
		{
			if (SerializationKnownTypes.NullType == type)
			{
				return SerializedPropertyType.Unknown;
			}
			else if (TypeServices.IsValueType(type))
			{
				if (TypeServices.IsPrimitiveType(type) || typeof(decimal) == type)
				{
					return SerializedPropertyType.Primitive;
				}
				else if (TypeServices.IsEnumType(type))
				{
					return SerializedPropertyType.Enum;
				}
				else
				{
					return SerializedPropertyType.Value;
				}
			}
			else
			{
				if (typeof(string) == type)
				{
					return SerializedPropertyType.String;
				}
				else if (TypeServices.IsGenericType(type))
				{
					return SerializedPropertyType.Generic;
				}
				else if (TypeServices.IsArrayType(type))
				{
					Type 					elementType 		= TypeServices.GetElementType(type);
					SerializedPropertyType	elementPropertyType = GetPropertyType(elementType);
					switch (elementPropertyType)
					{
						case SerializedPropertyType.Primitive:
							return SerializedPropertyType.ArrayOfPrimitive;

						case SerializedPropertyType.String:
							return SerializedPropertyType.ArrayOfString;

						case SerializedPropertyType.Enum:
							return SerializedPropertyType.ArrayOfEnum;

						case SerializedPropertyType.Value:
							return SerializedPropertyType.ArrayOfValue;

						case SerializedPropertyType.Generic:
							return SerializedPropertyType.ArrayOfGeneric;

						default:
							return SerializedPropertyType.ArrayOfObject;
					}
				}

				return SerializedPropertyType.Object;
			}
		}

		#endregion
	}
}