using System.Collections;
using System.Collections.Generic;
#if NETFX_CORE
using System.Reflection;
#endif
using UnityEngine;
using VoxelBusters.Utils;

using Type = System.Type;
using Array = System.Array;
using TypeCode = System.TypeCode;
using Activator	= System.Activator;
using NotImplementedException = System.NotImplementedException;

namespace VoxelBusters.Serialization
{
	internal class TypeServices 
	{
		#region Defaults

		internal class Defaults
		{

            internal    static      Type                            systemObjectType            = typeof(System.Object);
            internal    static		Type 							unityObjectType 			= typeof(UnityEngine.Object);
			internal 	static		Type 							componentType 				= typeof(UnityEngine.Component);
            internal 	static		Type 							monoBehaviourType 			= typeof(UnityEngine.MonoBehaviour);
            internal    static      Type                            scriptableObjectType        = typeof(UnityEngine.ScriptableObject);
            internal    static		Dictionary<Type, string>		knownTypeNamesCollection	= new Dictionary<Type, string>()
			{
				{ typeof(bool), 	"bool" },
				{ typeof(byte), 	"byte" },
				{ typeof(char), 	"char" },
				{ typeof(decimal), 	"decimal" },
				{ typeof(double), 	"double" },
				{ typeof(short), 	"short" },
				{ typeof(int), 		"int" },
				{ typeof(long), 	"long" },
				{ typeof(sbyte), 	"sbyte" },
				{ typeof(float), 	"float" },
				{ typeof(string), 	"string" },
				{ typeof(ushort), 	"ushort" },
				{ typeof(uint), 	"uint" },
				{ typeof(ulong), 	"ulong"},
			};
		}

		#endregion

		#region Static constructors

		static TypeServices()
		{}

		#endregion

		#region Static methods

		internal static string GetTypeFullName(Type type)
		{
			return type.FullName;
		}

		internal static string GetTypeShortName(Type type)
		{
			return type.Name;
		}

		internal static string GetTypeFormattedName(Type type, bool includeNamespace)
		{
			if (includeNamespace)
			{
				return GetTypeFormattedName(type, TypeServices.GetTypeFullName);
			}
			else
			{
				return GetTypeFormattedName(type, TypeServices.GetTypeShortName);
			}
		}

		private static string GetTypeFormattedName(Type type, System.Func<Type, string> getTypeNameFunc)
		{
			if (IsArrayType(type))
			{
				int		rank 			= GetArrayRank(type);
				Type 	elementType 	= GetElementType(type);
				if (rank == 1)
				{
					if (IsArrayType(elementType))
					{
						return string.Format("{0}[][]", GetTypeFormattedName(GetElementType(elementType), getTypeNameFunc));
					}
				}
				return string.Format("{0}[{1}]", GetTypeFormattedName(elementType, getTypeNameFunc), new string(',', (rank - 1)));
			}
			else if (IsGenericType(type))
			{
				string	definition 		= getTypeNameFunc(type.GetGenericTypeDefinition());
				definition 				= definition.Substring(0, definition.IndexOf('`'));
				string[] arguments 		= Array.ConvertAll(type.GetGenericArguments(), (_item) => GetTypeFormattedName(_item, getTypeNameFunc));

				return string.Format("{0}<{1}>", definition, string.Join(", ", arguments));
			}
			else
			{
				string name;
				if (false == Defaults.knownTypeNamesCollection.TryGetValue(type, out name))
				{
					name				= getTypeNameFunc(type).Replace('+', '.');
				}
				return name;
			}
		}

        internal static bool IsSerializable(Type type)
        {
            return type.IsSerializable;
        }

        internal static bool IsAbstract(Type type)
        { 
            return type.IsAbstract;
        }

        internal static bool IsEnumType(Type type)
		{
			#if NETFX_CORE
			return type.GetTypeInfo().IsEnum;
			#else 
			return type.IsEnum;
			#endif
		}

		internal static bool IsPrimitiveType(Type type)
		{
			#if NETFX_CORE
			return type.GetTypeInfo().IsPrimitive;
			#else
			return type.IsPrimitive;
			#endif
		}

		internal static bool IsValueType(Type type)
		{
			#if NETFX_CORE
			return type.GetTypeInfo().IsValueType;
			#else
			return type.IsValueType;
			#endif
		}

		internal static bool IsArrayType(Type type)
		{
			#if NETFX_CORE
			return type.GetTypeInfo().IsArray;
			#else
			return type.IsArray;
			#endif
		}

		internal static bool IsGenericType(Type type)
		{
			#if NETFX_CORE
			return type.GetTypeInfo().IsGenericType;
			#else
			return type.IsGenericType;
			#endif
		}

		internal static bool IsInterfaceType(Type type)
		{
			#if NETFX_CORE
			return type.GetTypeInfo().IsInterface;
			#else 
			return type.IsInterface;
			#endif
		}

		internal static bool IsPublic(Type type)
		{
			#if NETFX_CORE
			return type.GetTypeInfo().IsPublic;
			#else 
			return type.IsPublic;
			#endif
		}

		internal static bool IsNotPublic(Type type)
		{
			#if NETFX_CORE
			return type.GetTypeInfo().IsNotPublic;
			#else 
			return type.IsNotPublic;
			#endif
		}

		internal static bool IsUnityObjectType(Type type)
		{
			return type.IsSubclassOf(Defaults.unityObjectType);
		}

		internal static bool IsComponentType(Type type)
		{
			return type.IsSubclassOf(Defaults.componentType);
		}

        internal static bool IsMonoBehaviourType(Type type)
		{
			return type.IsSubclassOf(Defaults.monoBehaviourType);
        }

        internal static bool IsScriptableObjectType(Type type)
        {
            return type.IsSubclassOf(Defaults.scriptableObjectType);
        }

        internal static bool IsReflectionFriendly(Type type)
		{
			if (TypeServices.IsAssignableFrom(typeof(Component), type))
			{
				return IsMonoBehaviourType(type);
			}
				
			return true;
		}

		internal static bool IsAssignableFrom(Type type, Type c)
		{
			return type.IsAssignableFrom(c);
		}

		internal static Type GetElementType(Type type)
		{
			return type.GetElementType();
		}

		internal static int GetArrayRank(Type type)
		{
			return type.GetArrayRank();
		}

		internal static object GetDefault(Type type)
		{
			if (type.IsValueType)
			{
				return Activator.CreateInstance(type);
			}

			return null;
		}

		internal static Type GetUnderlyingType<T>(T value)
		{
			Type type = typeof(T);
			if (IsValueType(type) || EqualityComparer<T>.Default.Equals(value, default(T)))
			{
				return type;
			}

			return value.GetType();
		}

		internal static Type GetUnderlyingType(object value, Type defaultType)
		{
			return (null == value) ? defaultType : value.GetType();
		}
			
		#endregion

		#region Type methods

		internal static byte GetTypeCode(Type type)
		{
			return (byte)Type.GetTypeCode(type);
		}

		internal static Type GetTypeFromTypeCode(TypeCode typeCode)
		{
			return GetTypeFromTypeCode((byte)typeCode);
		}

		internal static Type GetTypeFromTypeCode(byte typeCode)
		{
			switch (typeCode)
			{
				case SerializationTypeCode.Boolean:
					return SerializationKnownTypes.BoolType;

				case SerializationTypeCode.Char:
					return SerializationKnownTypes.CharType;

				case SerializationTypeCode.SByte:
					return SerializationKnownTypes.SByteType;

				case SerializationTypeCode.Byte:
					return SerializationKnownTypes.ByteType;

				case SerializationTypeCode.Int16:
					return SerializationKnownTypes.Int16Type;

				case SerializationTypeCode.UInt16:
					return SerializationKnownTypes.UInt16Type;

				case SerializationTypeCode.Int32:
					return SerializationKnownTypes.Int32Type;

				case SerializationTypeCode.UInt32:
					return SerializationKnownTypes.UInt32Type;

				case SerializationTypeCode.Int64:
					return SerializationKnownTypes.Int64Type;

				case SerializationTypeCode.UInt64:
					return SerializationKnownTypes.UInt64Type;

				case SerializationTypeCode.Single:
					return SerializationKnownTypes.SingleType;

				case SerializationTypeCode.Double:
					return SerializationKnownTypes.DoubleType;

				case SerializationTypeCode.Decimal:
					return SerializationKnownTypes.DecimalType;

				case SerializationTypeCode.String:
					return SerializationKnownTypes.StringType;

				default:
					throw new NotImplementedException(message: string.Format("Typecode {0} is not handled", typeCode));
			}
		}

		internal static bool CheckTypeCompatiblity(Type type, Type c)
		{
			if (type.IsAssignableFrom(c))
			{
				return true;
			}

			return false;
		}

		internal static Type FindEndType(Type type)
		{
			if (type.IsInterface)
			{
				return null;
			}
			else if (type.IsSubclassOf(TypeServices.Defaults.unityObjectType))
			{
				return TypeServices.Defaults.unityObjectType;
			}
			else
			{
				return TypeServices.Defaults.systemObjectType;
			}
		}

		internal static int SizeOfPrimitiveType(Type type)
		{
			return SizeOfPrimitiveType(GetTypeCode(type));
		}

		internal static int SizeOfPrimitiveType(byte typeCode)
		{
			switch (typeCode)
			{
				case SerializationTypeCode.Boolean:
					return sizeof(bool);

				case SerializationTypeCode.Char:
					return sizeof(char);

				case SerializationTypeCode.SByte:
					return sizeof(sbyte);

				case SerializationTypeCode.Byte:
					return sizeof(byte);

				case SerializationTypeCode.Int16:
					return sizeof(short);

				case SerializationTypeCode.UInt16:
					return sizeof(ushort);

				case SerializationTypeCode.Int32:
					return sizeof(int);

				case SerializationTypeCode.UInt32:
					return sizeof(uint);

				case SerializationTypeCode.Int64:
					return sizeof(long);

				case SerializationTypeCode.UInt64:
					return sizeof(ulong);

				case SerializationTypeCode.Single:
					return sizeof(float);

				case SerializationTypeCode.Double:
					return sizeof(double);

				case SerializationTypeCode.Decimal:
					return sizeof(decimal);

				default:
					throw new NotImplementedException(message: string.Format("Typecode {0} is not handled", typeCode));
			}
		}

		#endregion
	}
}