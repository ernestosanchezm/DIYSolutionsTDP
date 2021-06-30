using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Type = System.Type;
using Array = System.Array;

namespace VoxelBusters.Serialization
{
    internal class Proxy1dArrayDataProvider : ISerializationDataProvider
    {
        #region ISerializationDataProvider implementation

        public void Serialize(object obj, IObjectWriter writer, SerializationContext context)
        {
            Type    arrayType   = obj.GetType();
            Type    elementType = TypeServices.GetElementType(arrayType);

            // write array length
            Array   array       = (Array)obj;
            int     length0     = array.GetLength(dimension: 0);
            writer.WriteArrayLength(dimension: 0, value: length0);

            // write array elements
            for (int iter = 0; iter < length0; iter++)
            {
                object  value   = array.GetValue(iter);
                writer.WriteProperty(value, elementType);
            }
        }

        public object CreateInstance(IObjectReader reader, SerializationContext context)
        {
            Type    arrayType   = reader.GetObjectType();
            Type    elementType = TypeServices.GetElementType(arrayType);

            // create array instance
            return Array.CreateInstance(elementType: elementType, length: reader.ReadArrayLength(dimension: 0));
        }

        public object Deserialize(object obj, IObjectReader reader, SerializationContext context)
        {
            Type    arrayType   = reader.GetObjectType();
            Type    elementType = TypeServices.GetElementType(arrayType);

            // read array elements
            Array   array       = (Array)obj;
            int     length0     = array.GetLength(dimension: 0);
            for (int iter = 0; iter < length0; iter++)
            {
                object  value   = reader.ReadProperty(elementType);
                array.SetValue(value, iter);
            }

            return array;
        }

        #endregion
    }
}