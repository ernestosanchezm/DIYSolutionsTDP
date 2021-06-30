using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class System_Collections_Generic_ListDataProvider<T> : SerializationDataProvider<List<T>> 
	{
		#region SerializationDataProvider abstract members implementation

		public override void Serialize(List<T> obj, IObjectWriter writer, SerializationContext context)
		{
			// construct array representation of the object
			int			count	= obj.Count;
			T[] 		array 	= new T[count];
			for (int iter = 0; iter < count; iter++)
			{
				array[iter]		= obj[iter];
			}

			// write array
			writer.WriteProperty(array);
		}

		public override List<T> CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new List<T>(capacity: 4);
		}

		public override List<T> Deserialize(List<T> obj, IObjectReader reader, SerializationContext context)
		{
			// prepare object
			obj.Clear();
			
			// update capacity and add elements
			T[] 		array 	= reader.ReadProperty<T[]>();
			int			count	= array.Length;
			
			obj.Capacity 		= count;
			for (int iter = 0; iter < count; iter++)
			{
				T		item	= array[iter];
				obj.Add(item);
			}

			return obj;	
		}

		#endregion
	}
}