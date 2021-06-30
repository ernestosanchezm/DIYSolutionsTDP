using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

using Type = System.Type;
using Array = System.Array;
using Activator = System.Activator;
using AppDomain = System.AppDomain;
using ObsoleteAttribute	= System.ObsoleteAttribute;

[assembly: InternalsVisibleTo("Assembly-CSharp-Editor")]
namespace VoxelBusters.Serialization
{
	internal static class ReflectionServices 
	{
		#region Defaults

		private class Defaults
		{
			internal 	const		MemberTypes  		kPreferredMemberTypes 				= MemberTypes.Field | MemberTypes.Property;
			internal 	const		BindingFlags		kDefaultBindingAttributes 			= BindingFlags.Public | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.Instance;
		}

		#endregion

		#region Activator methods

		internal static object CreateInstance(Type type, bool nonPublic = false)
		{
			return Activator.CreateInstance(type, nonPublic);
		}

		internal static void StaticConstructor(this Type type)
		{
			RuntimeHelpers.RunClassConstructor(type.TypeHandle);
		}

		#endregion

		#region Assembly methods

		internal static Assembly[] GetAllAssemblies()
		{
			return AppDomain.CurrentDomain.GetAssemblies();
		}

		internal static Assembly CreateAssembly(string name)
		{
			if (null == name)
			{
				return null;
			}

			return Assembly.Load(name);
		}

		internal static Type CreateType(Assembly assembly, string fullName)
		{
			return assembly.GetType(fullName);
		}

		internal static Type[] GetAllTypes(Assembly assembly)
		{
			return assembly.GetTypes();
		}

		internal static string GetAssemblyFullName(Assembly assembly)
		{
			return assembly.FullName;
		}

		internal static string GetAssemblyShortName(Assembly assembly)
		{
			return assembly.GetName().Name;
		}

		#endregion

		#region Static methods

		internal static BindingFlags GetDefaultBindingAttributes()
		{
			return Defaults.kDefaultBindingAttributes;
		}

		internal static DeclaredMemberInfo[] FindMembersOfType(Type type, BindingFlags bindingAttribute)
		{
			// check whether value for given type exist
			bool 					declaredOnly	= (bindingAttribute & BindingFlags.DeclaredOnly) != 0;
			DeclaredMemberInfo[] 	members			= SerializationGlobalCache.GetMembers(type, declaredOnly);
			if (members == null)
			{
				// find members defined for the given type
				members	= Array.ConvertAll(array: type.FindMembers(memberType: Defaults.kPreferredMemberTypes,
				                                                       bindingAttr: bindingAttribute,
				                                                       filter: null,
				                                                       filterCriteria: null),
				                           converter: (member) =>
				{
					return DeclaredMemberInfo.Create(member);
				});

				// cache this data for future use
				SerializationGlobalCache.AddMembers(type, members, declaredOnly);
			}

			return members;
		}
		   
		internal static bool IsObsolete(MemberInfo member)
		{
			object[] attributes = member.GetCustomAttributes(typeof(ObsoleteAttribute), false);
			return (attributes.Length > 0);
		}

		#endregion

		#region Internal methods

		internal static void ClearCachedData()
		{
			SerializationGlobalCache.RemoveAllMemebers();
		}

		#endregion
	}
}