using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

using Type = System.Type;

namespace VoxelBusters.Serialization
{
	internal class SerializationDataProviderGenerator 
	{
		#region Static fields

		private		readonly	static 		string[]			kCommonNamespaces			= new string[]
		{
			"System.Collections",
			"System.Collections.Generic",
			"UnityEngine",
		};
		private		readonly	static		Type[]				blackListedTypes 			= new Type[]
		{
			typeof(System.Object),
			typeof(UnityEngine.Object),
			typeof(UnityEngine.GameObject),
			typeof(UnityEngine.Component),
		};	
		private		const 					string				kSerializeMethodName		= "Serialize";
		private		const 					string				kCreateInstanceMethodName	= "CreateInstance";
		private		const 					string				kDeserializeMethodName		= "Deserialize";
		private		const 					string				kWriterVariableName			= "writer";
		private		const 					string				kReaderVariableName			= "reader";
		private		const 					string				kObjVariableName			= "obj";
		private		const 					string				kContextVariableName		= "context";

		private		readonly	static 		string				kSavePath					= Constants.kDataProviderSavePath;
		private					static		int					indent						= 0;

		#endregion

		#region Create methods

		internal static void GenerateCodeForType(Type type, DeclaredMemberInfo[] members, bool dependsOnParent)
		{
			// prepare for code generation
			// collect external namespaces
			Reset();

			// get basic properties
			string 	className			= GetDataProviderNameForType(type);
			string 	savePath			= IOServices.CombinePath(kSavePath, className + ".cs");
			Type	baseType			= typeof(SerializationDataProvider<>).MakeGenericType(type);

			// create file handle
			if (IOServices.FileExists(savePath))
			{
				throw new System.Exception();
			}
			using (StreamWriter streamWriter = new StreamWriter(savePath))
			{
				// write import statements
				WriteImportStatements(streamWriter);
				WriteLine(streamWriter);

				// start namespace scope
				WriteLine(streamWriter, CodeBuilder.CreateNameSpace("VoxelBusters.Serialization"));
				WriteLine(streamWriter, CodeBuilder.CodeBlockScope.Start);

				// start class scope
				WriteLine(streamWriter, CodeBuilder.CreateClassDeclaration(CodeBuilder.MemberAttributes.Public, className, baseType));
				WriteLine(streamWriter, CodeBuilder.CodeBlockScope.Start);

				// create region directive
				WriteLine(streamWriter, CodeBuilder.CreateRegionDeclaration(CodeBuilder.CodeRegionMode.Start, "SerializationDataProvider abstract members implementation"));
				WriteLine(streamWriter);
				WriteSerializeMethod(streamWriter, type, members, dependsOnParent);
				WriteLine(streamWriter);
				WriteCreateInstanceMethod(streamWriter, type);
				WriteLine(streamWriter);
				WriteDeserializeMethod(streamWriter, type, members, dependsOnParent);
				WriteLine(streamWriter);
				WriteLine(streamWriter, CodeBuilder.CreateRegionDeclaration(CodeBuilder.CodeRegionMode.End));

				// end class scope
				WriteLine(streamWriter, CodeBuilder.CodeBlockScope.End);

				// end namespace scope
				WriteLine(streamWriter, CodeBuilder.CodeBlockScope.End);
			}
		}

		#endregion

		#region Private static methods

		private static void Reset()
		{
			indent = 0;
			CodeBuilder.Reset();
		}

		private static string GetIndentationSpace()
		{
			return new string('\t', Mathf.Max(0, indent));
		}

		internal static string GetDataProviderNameForType(Type type)
		{
			string	name 			= TypeServices.GetTypeFullName(type);
			string	formattedName	= string.Join("_", name.Split('.', '+')); 
			return string.Format("{0}DataProvider", formattedName);
		}

		internal static bool IsBlacklistedType(Type type)
		{
			return blackListedTypes.Contains(type);
		}

		private static string GetWriteFunctionName(Type variableType)
		{
			return "WriteProperty";
		}

		private static bool CanIgnoreType(Type type)
		{
			return (type == null) || (type == typeof(System.Object));
		}

		#endregion 

		#region Writer methods

		private static void WriteLine(StreamWriter writer)
		{
			writer.Write(GetIndentationSpace());
			writer.WriteLine();
		}

		private static void WriteLine(StreamWriter writer, CodeBuilder.CodeBlockScope scope)
		{
			switch (scope)
			{
				case CodeBuilder.CodeBlockScope.Start:
					WriteLine(writer, CodeBuilder.CreateScopeDeclaration(scope));
					indent++;
					break;
			
				case CodeBuilder.CodeBlockScope.End:
					indent--;
					if (indent > 0)
					{
						WriteLine(writer, CodeBuilder.CreateScopeDeclaration(scope)); 
					}
					else
					{
						Write(writer, CodeBuilder.CreateScopeDeclaration(scope));
					}
					break;

				default:
					break;
			}
		}

		private static void Write(StreamWriter writer, string value)
		{
			writer.Write(GetIndentationSpace());
			writer.Write(value);
		}

		private static void WriteLine(StreamWriter writer, string value)
		{
			writer.Write(GetIndentationSpace());
			writer.WriteLine(value);
		}

		private static void WriteImportStatements(StreamWriter writer)
		{
			List<string> externalNamespaceList	= new List<string>(kCommonNamespaces);
			foreach (string value in externalNamespaceList)
			{
				WriteLine(writer, CodeBuilder.CreateNameSpaceImport(value));
			}
		}

		private static void WriteSerializeMethod(StreamWriter writer, Type type, DeclaredMemberInfo[] members, bool dependsOnParent)
		{
			WriteLine(writer, CodeBuilder.CreateMethodDeclaraction(CodeBuilder.MemberAttributes.Override | CodeBuilder.MemberAttributes.Public, 
			                                                       null, 
			                                                       kSerializeMethodName,
			                                                       new CodeBuilder.Parameter { type = type, name = kObjVariableName },
			                                                       new CodeBuilder.Parameter { type = typeof(IObjectWriter), name = kWriterVariableName },
			                                                       new CodeBuilder.Parameter { type = typeof(SerializationContext), name = kContextVariableName }));
			WriteLine(writer, CodeBuilder.CodeBlockScope.Start);

			// write declared property values
			WriteLine(writer, CodeBuilder.CreateComment("write declared property values"));
			if (members.Length > 0)
			{
				foreach (DeclaredMemberInfo member in members)
				{
					WriteLine(writer, CodeBuilder.CreateMethodInvokeDeclaration(kWriterVariableName, 
					                                                            GetWriteFunctionName(member.UnderlyingType),
					                                                            new CodeBuilder.Parameter() { name = string.Format("\"{0}\"", member.Name) },
					                                                            new CodeBuilder.Parameter() { name = CodeBuilder.CreateGetMemberValueDefinition(kObjVariableName, member.Name) }));

				}
			}

			// write parent properties
			Type parentType = type.BaseType;
			if (dependsOnParent && (false == CanIgnoreType(parentType)))
			{
				WriteLine(writer);
				WriteLine(writer, CodeBuilder.CreateComment("write parent property values"));
				WriteLine(writer, CodeBuilder.CreateMethodInvokeDeclaration(CodeBuilder.CreateGetMemberValueDefinition(CodeBuilder.CreateClassReference(typeof(SerializationDataProvider<>).MakeGenericType(parentType)), "Default"), 
				                                                            kSerializeMethodName,
				                                                            new CodeBuilder.Parameter { name = kObjVariableName },
				                                                            new CodeBuilder.Parameter { name = kWriterVariableName },
				                                                            new CodeBuilder.Parameter { name = kContextVariableName }));
			}
			WriteLine(writer, CodeBuilder.CodeBlockScope.End);
		}

		private static void WriteCreateInstanceMethod(StreamWriter writer, Type type)
		{
			WriteLine(writer, CodeBuilder.CreateMethodDeclaraction(CodeBuilder.MemberAttributes.Override | CodeBuilder.MemberAttributes.Public, 
			                                                       type, 
			                                                       kCreateInstanceMethodName,
			                                                       new CodeBuilder.Parameter { type = typeof(IObjectReader), name = kReaderVariableName },
			                                                       new CodeBuilder.Parameter { type = typeof(SerializationContext), name = kContextVariableName }));
			WriteLine(writer, CodeBuilder.CodeBlockScope.Start);
			if (TypeServices.IsComponentType(type))
			{
				WriteLine(writer, CodeBuilder.CreateComment("special case: Component data provider contains instructions to create instances of its derived types"));
				WriteLine(writer, CodeBuilder.CreateReturnDeclaraction(CodeBuilder.CreateMethodInvokeExpression(CodeBuilder.CreateGetMemberValueDefinition(CodeBuilder.CreateClassReference(typeof(SerializationDataProvider<Component>)), "Default"), 
				                                                                                                kCreateInstanceMethodName,
				                                                                                                new CodeBuilder.Parameter { name = kReaderVariableName },
				                                                                                                new CodeBuilder.Parameter { name = kContextVariableName }),
				                                                       type));
			}
            else if(TypeServices.IsScriptableObjectType(type))
            {
                WriteLine(writer, CodeBuilder.CreateComment("special case: ScriptableObject data provider contains instructions to create instances of its derived types"));
                WriteLine(writer, CodeBuilder.CreateReturnDeclaraction(CodeBuilder.CreateMethodInvokeExpression(CodeBuilder.CreateGetMemberValueDefinition(CodeBuilder.CreateClassReference(typeof(SerializationDataProvider<ScriptableObject>)), "Default"),
                                                                                                                kCreateInstanceMethodName,
                                                                                                                new CodeBuilder.Parameter { name = kReaderVariableName },
                                                                                                                new CodeBuilder.Parameter { name = kContextVariableName }),
                                                                       type));
            }
            else
			{
				WriteLine(writer, CodeBuilder.CreateReturnDeclaraction(CodeBuilder.CreateConstructorInvokeExpression(type)));
			}
			WriteLine(writer, CodeBuilder.CodeBlockScope.End);
		}

		private static void WriteDeserializeMethod(StreamWriter writer, Type type, DeclaredMemberInfo[] members, bool dependsOnParent)
		{
			WriteLine(writer, CodeBuilder.CreateMethodDeclaraction(CodeBuilder.MemberAttributes.Override | CodeBuilder.MemberAttributes.Public, 
			                                                       type, 
			                                                       kDeserializeMethodName,
			                                                       new CodeBuilder.Parameter { type = type, name = kObjVariableName },
			                                                       new CodeBuilder.Parameter { type = typeof(IObjectReader), name = kReaderVariableName },
			                                                       new CodeBuilder.Parameter { type = typeof(SerializationContext), name = kContextVariableName }));
			WriteLine(writer, CodeBuilder.CodeBlockScope.Start);

			// read declared properties
			WriteLine(writer, CodeBuilder.CreateComment("read declared property values"));
			if (members.Length > 0)
			{
				foreach (DeclaredMemberInfo member in members)
				{
					WriteLine(writer, CodeBuilder.CreateAssignmentDeclaraction(variable: CodeBuilder.CreateGetMemberValueDefinition(kObjVariableName, member.Name),
					                                                           expression: CodeBuilder.CreateGenericMethodInvokeExpression(kReaderVariableName, 
					                                                                                                                       "ReadProperty", 
					                                                                                                                       member.UnderlyingType,
					                                                                                                                       new CodeBuilder.Parameter() { name = string.Format("\"{0}\"", member.Name) })));
				}
			}	
			WriteLine(writer);

			// read parent properties
			Type parentType = type.BaseType;
			if (dependsOnParent && (false == CanIgnoreType(parentType)))
			{
				WriteLine(writer, CodeBuilder.CreateComment("read parent property values"));
				WriteLine(writer, CodeBuilder.CreateReturnDeclaraction(expression: CodeBuilder.CreateMethodInvokeExpression(CodeBuilder.CreateGetMemberValueDefinition(CodeBuilder.CreateClassReference(typeof(SerializationDataProvider<>).MakeGenericType(parentType)),
						                                                                                                                                                    "Default"), 
				                                                                                                            kDeserializeMethodName,
				                                                                                                            new CodeBuilder.Parameter { name = kObjVariableName },
				                                                                                                            new CodeBuilder.Parameter { name = kReaderVariableName },
				                                                                                                            new CodeBuilder.Parameter { name = kContextVariableName }),
				                                                       typecast: type));
			}
			else
			{
				WriteLine(writer, CodeBuilder.CreateReturnDeclaraction(kObjVariableName));
			}

			WriteLine(writer, CodeBuilder.CodeBlockScope.End);
		}

		#endregion
	}
}