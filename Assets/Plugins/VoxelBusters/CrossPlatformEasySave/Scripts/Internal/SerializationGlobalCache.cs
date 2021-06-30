using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.External.SystemUtils;

using Type = System.Type;

namespace VoxelBusters.Serialization
{
	internal class SerializationGlobalCache 
	{
		#region Static fields

		private 	static 		Dictionary<Type, ISerializationDataProvider>	dataProviderCollection;
		private		static		Dictionary<Type, DeclaredMemberInfo[]>			membersCollection;
		private		static		Dictionary<Type, DeclaredMemberInfo[]>			declaredMembersCollection;

		#endregion

		#region Constructors

		static SerializationGlobalCache()
		{
			// init static variables
			dataProviderCollection		= new Dictionary<Type, ISerializationDataProvider>(8);
			membersCollection			= new Dictionary<Type, DeclaredMemberInfo[]>(8);
			declaredMembersCollection	= new Dictionary<Type, DeclaredMemberInfo[]>(8);
		}

		#endregion

		#region Data provider methods

		internal static ISerializationDataProvider GetDataProviderOfType(Type source)
		{
			ISerializationDataProvider targetProvider;
			dataProviderCollection.TryGetValue(source, out targetProvider);

			return targetProvider;
		}

		internal static void AddDataProvider(Type source, ISerializationDataProvider dataProvider)
		{
			dataProviderCollection[source] = dataProvider;
		}

		internal static IEnumerator<ISerializationDataProvider> GetDataProvidersEnumerator()
		{
			if (dataProviderCollection != null)
			{
				return (IEnumerator<ISerializationDataProvider>)dataProviderCollection.Values.GetEnumerator();
			}

			return new NullEnumerator<ISerializationDataProvider>();
		}

		internal static void RemoveAllDataProviders()
		{
			dataProviderCollection.Clear();
		}

		#endregion

		#region Reflection methods

		internal static DeclaredMemberInfo[] GetMembers(Type type, bool declaredOnly)
		{
			DeclaredMemberInfo[] members;
			GetMemberCollection(declaredOnly).TryGetValue(type, out members);

			return members;
		}

		internal static void AddMembers(Type type, DeclaredMemberInfo[] members, bool declaredOnly)
		{
			GetMemberCollection(declaredOnly)[type] = members;
		}

		private static Dictionary<Type, DeclaredMemberInfo[]> GetMemberCollection(bool declaredOnly)
		{
			if (declaredOnly)
			{
				return declaredMembersCollection;
			}

			return membersCollection;
		}

		internal static void RemoveAllMemebers()
		{
			membersCollection.Clear();
			declaredMembersCollection.Clear();
		}

		#endregion
	}
}