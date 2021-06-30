using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Type = System.Type;
using BindingFlag = System.Reflection.BindingFlags;

namespace VoxelBusters.Serialization
{
    // fallback case to handle object type serialization using reflection technique
    internal class ProxyObjectDataProvider : ISerializationDataProvider
    {
        #region Constants

        private     static  readonly    BindingFlag     kBindingAttribute   = ReflectionServices.GetDefaultBindingAttributes() | BindingFlag.DeclaredOnly;

        #endregion

        #region ISerializationDataProvider implementation

        public void Serialize(object obj, IObjectWriter writer, SerializationContext context)
        {
            Type    objectType      = obj.GetType();
            Type    currentType     = objectType;
            #if SERIALIZATION_DEBUG
            Debug.LogWarning("[Serialization] Using proxy object for serializing object of type " + objectType);
            #endif
            do
            {
                // write properties declared within this type
                DeclaredMemberInfo[]    members         = ReflectionServices.FindMembersOfType(currentType, kBindingAttribute);
                int                     memberCount     = members.Length;
                for (int mIter = 0; mIter < memberCount; mIter++)
                {
                    DeclaredMemberInfo  currentMember   = members[mIter];
                    if (currentMember.IsSerializable)
                    {
                        writer.WriteProperty(currentMember.Name, currentMember.GetValue(obj), currentMember.UnderlyingType);
                    }
                }

                // go through object hierarchy and include parent properties
                Type    parentType  = currentType.BaseType;
                if (CanIgnoreType(parentType))
                {
                    break;
                }

                // try to use data provider for writing parent properties
                ISerializationDataProvider parentDataProvider = SerializationDataProviderServices.LoadFromCacheOrCreateDataProvider(parentType);
                if (parentDataProvider != null)
                {
                    parentDataProvider.Serialize(obj, writer, context);
                    break;
                }

                // continue using reflection technqiue until we reach end of object heirarchy or until we find suitable data provider
                currentType     = parentType;
            } while (true);
        }

        public object CreateInstance(IObjectReader reader, SerializationContext context)
        {
            Type    valueType   = reader.GetObjectType();
            // unlike normal object types, MonoBehaviour, Component types are attached to gameobject
            // and in order to ensure this works as intended, we are writing a special case to handle this scenario
            if (TypeServices.IsComponentType(valueType))
            {
                return SerializationDataProvider<Component>.Default.CreateInstance(reader, context);
            }
            else if (TypeServices.IsScriptableObjectType(valueType))
            {
                return SerializationDataProvider<ScriptableObject>.Default.CreateInstance(reader, context);
            }
            else
            {
                // rest of the types will use reflection to create instance
                // serialized objects are concrete types only
                // so for safe handling, we will allow system to create instances using private constructors as well
                return ReflectionServices.CreateInstance(type: valueType, nonPublic: true);
            }
        }

        public object Deserialize(object obj, IObjectReader reader, SerializationContext context)
        {
            Type    objectType      = obj.GetType();
            Type    currentType     = objectType;
            #if SERIALIZATION_DEBUG
            Debug.LogWarning("[Serialization] Using proxy object for deserializing object of " + objectType);
            #endif
            do
            {
                // read values declared under this type
                DeclaredMemberInfo[]    members         = ReflectionServices.FindMembersOfType(currentType, kBindingAttribute);
                int                     memberCount     = members.Length;
                for (int mIter = 0; mIter < memberCount; mIter++)
                {
                    DeclaredMemberInfo  currentMember   = members[mIter];
                    if (currentMember.IsSerializable)
                    {
                        object propertyValue = reader.ReadProperty(currentMember.Name, currentMember.UnderlyingType);
                        currentMember.SetValue(obj, propertyValue);
                    }
                }

                // forward call to parent type
                Type    parentType  = currentType.BaseType;
                if (CanIgnoreType(parentType))
                {
                    break;
                }

                // try using data provider to read parent properties
                ISerializationDataProvider parentDataProvider = SerializationDataProviderServices.LoadFromCacheOrCreateDataProvider(parentType);
                if (parentDataProvider != null)
                {
                    obj = parentDataProvider.Deserialize(obj, reader, context);
                    break;
                }

                // continue using reflection technqiue until we reach end of object heirarchy or until we find suitable data provider
                currentType = parentType;

            } while (true);

            return obj;
        }

        #endregion

        #region Private methods

        private bool CanIgnoreType(Type type)
        {
            return (null == type) || (typeof(System.Object) == type) || (typeof(System.ValueType) == type);
        }

        #endregion
    }
}