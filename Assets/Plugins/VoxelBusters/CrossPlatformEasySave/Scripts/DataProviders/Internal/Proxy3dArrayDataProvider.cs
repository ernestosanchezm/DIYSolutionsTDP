using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Type = System.Type;
using Array = System.Array;

namespace VoxelBusters.Serialization
{
    internal class Proxy3dArrayDataProvider : ISerializationDataProvider
    {
        #region ISerializationDataProvider implementation

        public void Serialize(object obj, IObjectWriter writer, SerializationContext context)
        {
            Type    arrayType   = obj.GetType();
            Type    elementType = TypeServices.GetElementType(arrayType);

            // write array length
            Array   array       = (Array)obj;
            int     length0     = array.GetLength(dimension: 0);
            int     length1     = array.GetLength(dimension: 1);
            int     length2     = array.GetLength(dimension: 2);
            writer.WriteArrayLength(dimension: 0, value: length0);
            writer.WriteArrayLength(dimension: 1, value: length1);
            writer.WriteArrayLength(dimension: 2, value: length2);

            // write array elements
            for (int x = 0; x < length0; x++)
            {
                for (int y = 0; y < length1; y++)
                {
                    for (int z = 0; z < length2; z++)
                    {
                        object value = array.GetValue(x, y, z);
                        writer.WriteProperty(value, elementType);
                    }
                }
            }
        }

        public object CreateInstance(IObjectReader reader, SerializationContext context)
        {
            Type    arrayType   = reader.GetObjectType();
            Type    elementType = TypeServices.GetElementType(arrayType);

            // create array instance
            return Array.CreateInstance(elementType: elementType,
                                        length1: reader.ReadArrayLength(dimension: 0),
                                        length2: reader.ReadArrayLength(dimension: 1),
                                        length3: reader.ReadArrayLength(dimension: 2));
        }

        public object Deserialize(object obj, IObjectReader reader, SerializationContext context)
        {
            Type    arrayType   = reader.GetObjectType();
            Type    elementType = TypeServices.GetElementType(arrayType);

            // read array elements
            Array   array       = (Array)obj;
            int     length0     = array.GetLength(dimension: 0);
            int     length1     = array.GetLength(dimension: 1);
            int     length2     = array.GetLength(dimension: 2);
            for (int x = 0; x < length0; x++)
            {
                for (int y = 0; y < length1; y++)
                {
                    for (int z = 0; z < length2; z++)
                    {
                        object value = reader.ReadProperty(elementType);
                        array.SetValue(value, x, y, z);
                    }
                }
            }

            return array;
        }

        #endregion
    }
}