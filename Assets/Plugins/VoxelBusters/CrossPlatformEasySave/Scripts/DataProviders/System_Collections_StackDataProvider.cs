using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class System_Collections_StackDataProvider : SerializationDataProvider<System.Collections.Stack>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(System.Collections.Stack obj, IObjectWriter writer, SerializationContext context)
		{
			// construct array representation of the object
			object[] 	array 	= obj.ToArray();

			// write array
			writer.WriteProperty(array);
		}
		
		public override System.Collections.Stack CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new System.Collections.Stack(initialCapacity: 4);
		}
		
		public override System.Collections.Stack Deserialize(System.Collections.Stack obj, IObjectReader reader, SerializationContext context)
		{
			// prepare object
			obj.Clear();
			
			// update capacity and add elements
			object[] 	array 	= reader.ReadProperty<object[]>();
			int			count	= array.Length;
			
			for (int iter = 0; iter < count; iter++)
			{
				object	item	= array[iter];
				obj.Push(item);
			}

			return obj;	
		}
		
		#endregion
	}
}