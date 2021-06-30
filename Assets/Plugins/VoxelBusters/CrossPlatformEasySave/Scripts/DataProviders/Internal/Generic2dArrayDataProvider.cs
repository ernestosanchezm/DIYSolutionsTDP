using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	internal class Generic2dArrayDataProvider<T> : SerializationDataProvider<T[,]>
	{
		#region SerializationDataProvider abstract member implementation

		public override void Serialize(T[,] obj, IObjectWriter writer, SerializationContext context)
		{
			int	length0	= obj.GetLength(dimension: 0);
			int length1	= obj.GetLength(dimension: 1);
			writer.WriteArrayLength(dimension: 0, value: length0);
			writer.WriteArrayLength(dimension: 1, value: length1);

			for (int xIter = 0; xIter < length0; xIter++)
			{
				for (int yIter = 0; yIter < length1; yIter++)
				{
					writer.WriteProperty(value: obj[xIter, yIter]);
				}
			}
		}

		public override T[,] CreateInstance(IObjectReader reader, SerializationContext context)
		{
			int 	length0	= reader.ReadArrayLength(dimension: 0);
			int 	length1	= reader.ReadArrayLength(dimension: 1);
			return new T[length0, length1];
		}

		public override T[,] Deserialize(T[,] obj, IObjectReader reader, SerializationContext context)
		{
			if (!EqualityComparer<T[,]>.Default.Equals(obj, default(T[,])))
			{
				int length0	= reader.ReadProperty<int>();
				int length1	= reader.ReadProperty<int>();
				for (int xIter = 0; xIter < length0; xIter++)
				{
					for (int yIter = 0; yIter < length1; yIter++)
					{
						obj[xIter, yIter] = reader.ReadProperty<T>();
					}
				}
			}
			return obj;
		}

		#endregion
	}
}