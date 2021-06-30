using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	internal class Generic1dArrayDataProvider<T> : SerializationDataProvider<T[]>
	{
		#region SerializationDataProvider abstract member implementation

		public override void Serialize(T[] obj, IObjectWriter writer, SerializationContext context)
		{
			int		length0	= obj.Length;
			writer.WriteArrayLength(dimension: 0, value: length0);

			for (int iter = 0; iter < length0; iter++)
			{
				writer.WriteProperty(value: obj[iter]);
			}
		}

		public override T[] CreateInstance(IObjectReader reader, SerializationContext context)
		{
			int 	length0	= reader.ReadArrayLength(dimension: 0);
			return new T[length0];
		}

		public override T[] Deserialize(T[] obj, IObjectReader reader, SerializationContext context)
		{
			int		length0	= obj.Length;
			for (int iter = 0; iter < length0; iter++)
			{
				obj[iter]	= reader.ReadProperty<T>();
			}
			return obj;
		}

		#endregion
	}
}