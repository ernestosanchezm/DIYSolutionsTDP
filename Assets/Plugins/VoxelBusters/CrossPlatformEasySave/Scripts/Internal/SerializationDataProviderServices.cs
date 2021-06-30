#if (UNITY_EDITOR && FORCE_AOT) || (!UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID))
#define UNITY_AOT
#endif
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	internal static class SerializationDataProviderServices 
	{
		#region Defaults

		private class Defaults
		{
			internal	static	readonly	string[] 		        kAllowedAssemblyPrefixes            = new string[] 
			{
				"Assembly",
			};
		}

		#endregion

		#region Static fields

		private			static 		    Dictionary<Type, Type> 	        userDefinedDataProviderTable		= new Dictionary<Type, Type>(capacity: 8);
		#if !UNITY_AOT
        private			static 		    Dictionary<Type, Type> 	        systemGeneratedDataProviderTable	= new Dictionary<Type, Type>(capacity: 4);
        #endif
        private         static readonly ISerializationDataProvider      proxyObjectDataProvider             = new ProxyObjectDataProvider();
        private         static readonly ISerializationDataProvider      proxyIEnumerableDataProvider        = new ProxyIEnumerableDataProvider();
        private         static readonly ISerializationDataProvider[]    proxyArrayDataProviders             = new ISerializationDataProvider[] 
        {
            new Proxy1dArrayDataProvider(),
            new Proxy2dArrayDataProvider(),
            new Proxy3dArrayDataProvider(),
        };

        #endregion

        #region Static methods

        internal static void SearchAllDataProviderTypes()
		{
			// find all the user defined type formatters
			Dictionary<Type, Type> newCollection = new Dictionary<Type, Type>();
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				// our search is limited to Assembly*
				// rest of the assemblies are skipped
				if (false == Array.Exists(Defaults.kAllowedAssemblyPrefixes, (_prefix) => ReflectionServices.GetAssemblyShortName(assembly).StartsWith(_prefix)))
				{
					continue;
				}

				// iterate through the available types
				foreach (Type type in assembly.GetTypes())
				{
                    if (TypeServices.IsNotPublic(type) || 
                        TypeServices.IsAbstract(type) ||
                        #if UNITY_AOT
                        TypeServices.IsGenericType(type) ||
                        #endif
                        TypeServices.IsValueType(type))
                    {
                        continue;
					}

					Type sourceType = FindDataProviderSource(type);
					if (sourceType != null)
					{
						newCollection.Add(sourceType, type);
                        #if SERIALIZATION_DEBUG
						Debug.Log(string.Format("[Serialization] Found data provider {0} for type {1}", type, sourceType));
                        #endif
					}
				}
			}

			// save this information
			userDefinedDataProviderTable = newCollection;
		}

        internal static void AddDataProvider(Type source, Type dataProvider)
        {
            userDefinedDataProviderTable.Add(source, dataProvider);
        }

		internal static SerializationDataProvider<T> LoadFromCacheOrCreateDataProvider<T>()
		{
			return (SerializationDataProvider<T>)LoadFromCacheOrCreateDataProvider(source: typeof(T));
		}

		internal static ISerializationDataProvider LoadFromCacheOrCreateDataProvider(Type source)
		{
			// before creating instance, check whether required type exists in cache
			// note that this method gets called only for serializable types
			// so at any given point this method shouldn't return null
			ISerializationDataProvider instance = SerializationGlobalCache.GetDataProviderOfType(source);
			if (null == instance)
			{
				// create data provider instance and add it to cache except proxy items
				Type	dataProviderType	= FindDataProvider(source);
				if (dataProviderType != null)
				{
					instance 				= ReflectionServices.CreateInstance(dataProviderType) as ISerializationDataProvider;
					SerializationGlobalCache.AddDataProvider(source, instance);
				}
			}
			return instance;
		}

        internal static ISerializationDataProvider GetProxyObjectDataProvider(Type type)
        {
            if (TypeServices.IsArrayType(type))
            {
                int rank = TypeServices.GetArrayRank(type);
                return proxyArrayDataProviders[(rank - 1)];
            }
            else if (TypeServices.IsAssignableFrom(SerializationKnownTypes.IEnumerableType, type))
            {
                return proxyIEnumerableDataProvider;
            }
            else
            {
                return proxyObjectDataProvider;
            }
        }

        internal static bool HasDataProvider(Type source)
		{
			// similar to create instructions we will try to find best match for the given type
			return (FindDataProvider(source) != null);
		}

		internal static void ClearCachedData()
		{
			// unset all default instance
			IEnumerator<ISerializationDataProvider> enumerator = SerializationGlobalCache.GetDataProvidersEnumerator();
			while (enumerator.MoveNext())
			{
				try
				{
					MemberInfo[] targetMembers = enumerator.Current.GetType().GetMember("defaultInstance", BindingFlags.Static | BindingFlags.NonPublic);
					((FieldInfo)targetMembers[0]).SetValue(obj: null, value: null);
				}
				catch (Exception)
				{
					Debug.Log("[Serialization] Failed to unset Default value of type: " + enumerator.Current);
					continue;
				}
			}

			SerializationGlobalCache.RemoveAllDataProviders();
		}

        #endregion

        #region Private static methods

		private static Type FindDataProviderSource(Type type)
		{
			Type baseType = null;
			if ((null == type) || (null == (baseType = type.BaseType)))
			{
				return null;
			}

			if (TypeServices.IsGenericType(baseType) && baseType.GetGenericTypeDefinition() == typeof(SerializationDataProvider<>))
			{
				Type argument	= baseType.GetGenericArguments()[0];
				if (TypeServices.IsGenericType(argument))
				{
					return argument.GetGenericTypeDefinition();
				}
				return argument;
			}

			return null;
		}

		private static Type FindDataProvider(Type source)
		{
			// this method includes two tests to find closest match
			// #1 we try to find exact matching type in our table
			Type	dataProviderType;
			if (false == userDefinedDataProviderTable.TryGetValue(source, out dataProviderType))
			{
                #if !UNITY_AOT
				// #2 we try to find best matching type definition
				if (false == systemGeneratedDataProviderTable.TryGetValue(source, out dataProviderType))
				{
					Type	newDataProviderType	= FindBestMatchingDataProvider(source);
					if (newDataProviderType != null)
					{
						systemGeneratedDataProviderTable.Add(source, newDataProviderType);
					}
					return newDataProviderType;
				}
                #endif
			}
			return dataProviderType;
		}	

        internal static Type FindBestMatchingDataProvider(Type source)
		{
			if (TypeServices.IsArrayType(source))
			{
				return FindGenericArrayObjectDataProvider(source);
			}
			else if (TypeServices.IsGenericType(source))
			{
				return FindGenericObjectDataProvider(source);
			}
			else
			{
				return null;
			}
		}

		private static Type FindGenericArrayObjectDataProvider(Type source)
		{
			Type elementType = TypeServices.GetElementType(source);
			switch (TypeServices.GetArrayRank(source))
			{
				case 1:
					if (TypeServices.IsArrayType(elementType))
					{
						Type underlyingElementType = TypeServices.GetElementType(elementType);
						return typeof(GenericJaggedArrayDataProvider<>).MakeGenericType(underlyingElementType);
					}
					else
					{
						return typeof(Generic1dArrayDataProvider<>).MakeGenericType(elementType);
					}

				case 2:
					return typeof(Generic2dArrayDataProvider<>).MakeGenericType(elementType);

                case 3:
                    return typeof(Generic3dArrayDataProvider<>).MakeGenericType(elementType);
            
				default:
					return null;
			}
		}

		private static Type FindGenericObjectDataProvider(Type source)
		{
			Type dataProviderType;
			if (userDefinedDataProviderTable.TryGetValue(source.GetGenericTypeDefinition(), out dataProviderType))
			{
				return dataProviderType.MakeGenericType(source.GetGenericArguments());
			}
			return null;
		}
        
        #endregion
	}
}