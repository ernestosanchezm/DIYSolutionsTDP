using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Type = System.Type;
using BindingFlag = System.Reflection.BindingFlags;

// NOTE:
// unlike normal object types, MonoBehaviour, Component types are attached to gameobject
// and in order to ensure this works as intended, we are writing a special case to handle this scenario
namespace VoxelBusters.Serialization
{
	internal class GenericObjectDataProvider<T> : SerializationDataProvider<T>
	{
		#region Constants

		private		static	readonly	BindingFlag		kBindingAttribute = ReflectionServices.GetDefaultBindingAttributes() | BindingFlag.DeclaredOnly;

		#endregion

		#region Fields

		private							Type 			m_targetType;

		#endregion

		#region Constructors

		internal GenericObjectDataProvider()
		{
			// set properties
			m_targetType	= typeof(T);
		}

		#endregion

		#region SerializationDataProvider abstract member implementation

		public override void Serialize(T obj, IObjectWriter writer, SerializationContext context)
		{
			// write properties declared in this type
			DeclaredMemberInfo[] 	members		= ReflectionServices.FindMembersOfType(m_targetType, kBindingAttribute);
			int 					memberCount	= members.Length;
			for (int mIter = 0; mIter < memberCount; mIter++)
			{
				DeclaredMemberInfo	currentMember	= members[mIter];
				if (currentMember.IsSerializable)
				{
					writer.WriteProperty(currentMember.Name, currentMember.GetValue(obj), currentMember.UnderlyingType);
				}
			}

			// check whether call needs to be delegated to parent type
			Type 	parentType = m_targetType.BaseType;
			if (false == CanIgnoreType(parentType))
			{
				SerializationDataProviderServices.LoadFromCacheOrCreateDataProvider(parentType).Serialize(obj, writer, context);
			}
		}

		public override T CreateInstance(IObjectReader reader, SerializationContext context)
		{
			if (TypeServices.IsComponentType(m_targetType))
			{
                return (T)(object)SerializationDataProvider<Component>.Default.CreateInstance(reader, context);
            }
            else if (TypeServices.IsScriptableObjectType(m_targetType))
            {
                return (T)(object)SerializationDataProvider<ScriptableObject>.Default.CreateInstance(reader, context);
            }
            else
			{
				// rest of the types will use reflection to create instance
				// serialized objects are concrete types only
				// so for safe handling, we will allow system to create instances using private constructors as well
				return (T)ReflectionServices.CreateInstance(type: reader.GetObjectType(), nonPublic: true);
			}
		}

		public override T Deserialize(T obj, IObjectReader reader, SerializationContext context)
		{
			// read values of declared properties
			DeclaredMemberInfo[] 	members		= ReflectionServices.FindMembersOfType(m_targetType, kBindingAttribute);
			int 					memberCount	= members.Length;
			for (int mIter = 0; mIter < memberCount; mIter++)
			{
				DeclaredMemberInfo	currentMember	= members[mIter];
				if (currentMember.IsSerializable)
				{
					object			propertyValue 	= reader.ReadProperty(currentMember.Name, currentMember.UnderlyingType);
					currentMember.SetValue(obj, propertyValue);
				}
			}

			// forward call to parent 
			Type 	parentType = m_targetType.BaseType;
			if (false == CanIgnoreType(parentType))
			{
				obj			= (T)SerializationDataProviderServices.LoadFromCacheOrCreateDataProvider(parentType).Deserialize(obj, reader, context);
			}

			return obj;
		}

		#endregion

		#region Private methods

		private bool CanIgnoreType(Type type)
		{
			return (null == type) || (typeof(System.Object) == type)  || (typeof(System.ValueType) == type);
		}

		#endregion
	}
}