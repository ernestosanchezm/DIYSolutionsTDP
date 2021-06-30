using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class System_Collections_QueueDataProvider : SerializationDataProvider<System.Collections.Queue>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(System.Collections.Queue obj, IObjectWriter writer, SerializationContext context)
		{
			// construct array representation of the object
			object[] 	array 	= obj.ToArray();

			// write array
			writer.WriteProperty(array);
		}
		
		public override System.Collections.Queue CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new System.Collections.Queue(capacity: 4);
		}
		
		public override System.Collections.Queue Deserialize(System.Collections.Queue obj, IObjectReader reader, SerializationContext context)
		{
			// prepare object
			obj.Clear();
			
			// update capacity and add elements
			object[] 	array 	= reader.ReadProperty<object[]>();
			int			count	= array.Length;
			
			for (int iter = 0; iter < count; iter++)
			{
				object	item	= array[iter];
				obj.Enqueue(item);
			}

			return obj;	
		}
		
		#endregion
	}
}