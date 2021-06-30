using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using Type = System.Type;
using Array = System.Array;

namespace VoxelBusters.Serialization
{
	internal class CodeBuilder 
	{
		#region Constants

		private		static	Dictionary<Type, string>	knowTypesCollection = new Dictionary<Type, string>()
		{
			{ typeof(bool), 	"bool" },
			{ typeof(char), 	"char" },
			{ typeof(sbyte), 	"sbyte" },
			{ typeof(byte), 	"byte" },
			{ typeof(short),	"short" },
			{ typeof(ushort), 	"ushort" },
			{ typeof(int), 		"int" },
			{ typeof(uint), 	"uint" },
			{ typeof(long), 	"long" },
			{ typeof(ulong), 	"ulong" },
			{ typeof(float), 	"float" },
			{ typeof(double), 	"double" },
			{ typeof(decimal), 	"decimal" },
			{ typeof(string), 	"string" },
			{ typeof(IObjectWriter), 	"IObjectWriter" },
			{ typeof(IObjectReader), 	"IObjectReader" },
			{ typeof(SerializationContext), "SerializationContext" },
			{ typeof(SerializationDataProvider<>), "SerializationDataProvider" },
		};

		#endregion

		#region Methods

		public static void Reset()
		{}

		public static string CreateScopeDeclaration(CodeBlockScope scope)
		{
			if (scope == CodeBlockScope.Start)
			{
				return "{";
			}
			else
			{
				return "}";
			}
		}

		public static string CreateNameSpaceImport(string name)
		{
			return string.Format("using {0};", name); 
		}

		public static string CreateNameSpace(string name)
		{
			return string.Format("namespace {0}", name);
		}

		public static string CreateRegionDeclaration(CodeRegionMode region, string text = null)
		{
			if (region == CodeRegionMode.Start)
			{
				return string.Format("#region {0}", text);
			}
			else
			{
				return "#endregion";
			}
		}

		public static string CreateGetMemberValueDefinition(string objectName, string memberName)
		{
			return string.Format("{0}.{1}", objectName, memberName);
		}

		public static string CreateClassDeclaration(MemberAttributes attribute, string className, Type baseType)
		{
			return string.Format("{0} class {1} : {2}",
			                     ConvertAccessModifierToString(attribute),
			                     className,
			                     ConvertTypeToScriptReference(baseType));
		}

		public static string CreateClassReference(Type type)
		{
			return ConvertTypeToScriptReference(type);
		}

		public static string CreateMethodDeclaraction(MemberAttributes attribute, Type returnType, string methodName, params Parameter[] parameters)
		{
			string parametersStr = (parameters == null) ? string.Empty : string.Join(", ", Array.ConvertAll(parameters, (_item) => string.Format("{0} {1}", ConvertTypeToScriptReference(_item.type), _item.name)));
			return string.Format("{0} {1} {2}({3})", 
			                     ConvertAccessModifierToString(attribute), 
			                     ConvertTypeToScriptReference(returnType), 
			                     methodName,
			                     parametersStr);
		}

		public static string CreateMethodInvokeDeclaration(string instance, string method, params Parameter[] parameters)
		{
			return string.Format("{0};", CreateMethodInvokeExpression(instance, method, parameters));
		}

		public static string CreateMethodInvokeExpression(string instance, string method, params Parameter[] parameters)
		{
			return string.Format("{0}.{1}({2})", instance, method, GetMethodParameters(parameters));
		}

		public static string CreateAssignmentDeclaraction(string variable, string expression, Type typecast = null)
		{
			if (typecast == null)
			{
				return string.Format("{0} = {1};", variable, expression);
			}
			else
			{
				return string.Format("{0} = ({1}){2};", variable, ConvertTypeToScriptReference(typecast), expression);
			}
		}

		public static string CreateReturnDeclaraction(string expression, Type typecast = null)
		{
			if (typecast == null)
			{
				return string.Format("return {0};", expression);
			}
			else
			{
				return string.Format("return ({0}){1};", ConvertTypeToScriptReference(typecast), expression);
			}
		}

		public static string CreateGenericMethodInvokeExpression(string instance, string method, Type genericType, params Parameter[] parameters)
		{
			return string.Format("{0}.{1}<{2}>({3})", instance, method, ConvertTypeToScriptReference(genericType), GetMethodParameters(parameters));
		}

		public static string CreateConstructorInvokeExpression(Type type, params Parameter[] parameters)
		{
			return string.Format("new {0}()", ConvertTypeToScriptReference(type));
		}

		public static string CreateComment(string text)
		{
			return string.Format("// {0}", text);
		}

		private static string GetMethodSignatureParameters(Parameter[] parameters)
		{
			return (parameters == null) 
				? string.Empty 
				: string.Join(", ", Array.ConvertAll(parameters, (item) => string.Format("{0} {1}", ConvertTypeToScriptReference(item.type), item.name)));
		}

		private static string GetMethodParameters(Parameter[] parameters)
		{
			return (parameters == null) 
				? string.Empty 
				: string.Join(", ", Array.ConvertAll(parameters, (item) => item.name));
		}

		private static string ConvertAccessModifierToString(MemberAttributes attributes)
		{
			string text = null;
			if (MemberAttributeContains(attributes, MemberAttributes.Public))
			{
				text	= "public";
			}
			else if (MemberAttributeContains(attributes, MemberAttributes.Protected))
			{
				text	= "protected";
			}
			else if (MemberAttributeContains(attributes, MemberAttributes.Private))
			{
				text	= "private";
			}
			else if (MemberAttributeContains(attributes, MemberAttributes.Internal))
			{
				text	= "internal";
			}

			if (MemberAttributeContains(attributes, MemberAttributes.Override))
			{
				text	= (text != null) ? string.Concat(text, " override") : "override";
			}
			return text;
		}

		private static bool MemberAttributeContains(MemberAttributes attributes, MemberAttributes value)
		{
			return ((attributes & value) != 0);
		}

		private static string ConvertTypeToScriptReference(Type type)
		{
			if (null == type)
			{
				return "void";
			}
			    
			string typeStr;
			if (knowTypesCollection.TryGetValue(type, out typeStr))
			{
				return typeStr;
			}

			// hack to avoid using full name for SerializationDataProvider type
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(SerializationDataProvider<>))
			{
				string		definition	= "SerializationDataProvider";
				string[]	arguments 	= Array.ConvertAll(type.GetGenericArguments(), (_item) => ConvertTypeToScriptReference(_item));
				return string.Format("{0}<{1}>", definition, string.Join(", ", arguments));
			}

			return TypeServices.GetTypeFormattedName(type, includeNamespace: true);
		}

		#endregion

		#region Nested Types
	
		public struct Parameter
		{
			public Type type;
			public string name;
		}

		public enum MemberAttributes
		{
			Public = 1 << 1,
			Protected = 1 << 2,
			Private = 1 << 3,
			Internal = 1 << 4,
			Override = 1 << 5,
		}

		public enum CodeBlockScope
		{
			Start,
			End
		}

		public enum CodeRegionMode
		{
			Start,
			End
		}

		#endregion
	}
}