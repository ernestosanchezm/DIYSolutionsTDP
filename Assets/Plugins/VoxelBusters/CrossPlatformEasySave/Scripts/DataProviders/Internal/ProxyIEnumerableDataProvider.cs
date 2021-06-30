using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Type = System.Type;

namespace VoxelBusters.Serialization
{
    internal class ProxyIEnumerableDataProvider : ISerializationDataProvider
    {
        #region ISerializationDataProvider implementation

        public void Serialize(object obj, IObjectWriter writer, SerializationContext context)
        {
            if (obj is IList)
            {
                IEnumerator enumerator  = ((IList)obj).GetEnumerator();
                object[]    array       = enumerator.ToArray();

                // set properties
                writer.WriteProperty(array);
            }
            else if (obj is IDictionary)
            {
                DictionaryEntry[]   array   = ((IDictionary)obj).ToArray();

                // set properties
                writer.WriteProperty(array);
            }
        }

        public object CreateInstance(IObjectReader reader, SerializationContext context)
        {
            Type    valueType   = reader.GetObjectType();
            return ReflectionServices.CreateInstance(type: valueType, nonPublic: true);
        }

        public object Deserialize(object obj, IObjectReader reader, SerializationContext context)
        {
            if (obj is IList)
            {
                object[]    array   = (object[])reader.ReadProperty(type: typeof(object[]));
                int         length  = array.Length;

                IList       list    = (IList)obj;
                for (int iter = 0; iter < length; iter++)
                {
                    list.Add(array[iter]);
                }
            }
            else if (obj is IDictionary)
            {
                DictionaryEntry[]   array       = (DictionaryEntry[])reader.ReadProperty(type: typeof(DictionaryEntry[]));
                int                 length      = array.Length;

                IDictionary         dictionary  = (IDictionary)obj;
                for (int iter = 0; iter < length; iter++)
                {
                    DictionaryEntry entry       = (DictionaryEntry)array[iter];
                    dictionary.Add(entry.Key, entry.Value);
                }
            }

            return obj;
        }

        #endregion
    }
}