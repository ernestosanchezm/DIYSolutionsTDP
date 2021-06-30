using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	internal class Generic3dArrayDataProvider<T> : SerializationDataProvider<T[,,]>
	{
		#region SerializationDataProvider abstract member implementation

		public override void Serialize(T[,,] obj, IObjectWriter writer, SerializationContext context)
		{
			int	    length0	= obj.GetLength(dimension: 0);
            int     length1 = obj.GetLength(dimension: 1);
			int     length2	= obj.GetLength(dimension: 2);
			writer.WriteArrayLength(dimension: 0, value: length0);
            writer.WriteArrayLength(dimension: 1, value: length1);
			writer.WriteArrayLength(dimension: 2, value: length2);

			for (int xIter = 0; xIter < length0; xIter++)
			{
				for (int yIter = 0; yIter < length1; yIter++)
				{
					for (int zIter = 0; zIter < length2; zIter++)
                    {  
                        writer.WriteProperty(value: obj[xIter, yIter, zIter]);
                    }
				}
			}
		}

		public override T[,,] CreateInstance(IObjectReader reader, SerializationContext context)
		{
			int 	length0	= reader.ReadArrayLength(dimension: 0);
            int     length1 = reader.ReadArrayLength(dimension: 1);
			int 	length2	= reader.ReadArrayLength(dimension: 2);
			return new T[length0, length1, length2];
		}

		public override T[,,] Deserialize(T[,,] obj, IObjectReader reader, SerializationContext context)
		{
            int     length0 = obj.GetLength(dimension: 0);
            int     length1 = obj.GetLength(dimension: 1);
            int     length2 = obj.GetLength(dimension: 2);
        
            // read elements
            for (int xIter = 0; xIter < length0; xIter++)
            {
                for (int yIter = 0; yIter < length1; yIter++)
                {
                    for (int zIter = 0; zIter < length2; zIter++)
                    {
                        obj[xIter, yIter, zIter] = reader.ReadProperty<T>();
                    }
                }
            }

            return obj;
		}

		#endregion
	}
}