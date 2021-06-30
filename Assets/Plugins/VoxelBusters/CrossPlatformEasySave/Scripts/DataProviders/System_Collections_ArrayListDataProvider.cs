using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class System_Collections_ArrayListDataProvider : SerializationDataProvider<System.Collections.ArrayList>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(System.Collections.ArrayList obj, IObjectWriter writer, SerializationContext context)
		{
			// construct array representation of the object
			int			count	= obj.Count;
			object[] 	array 	= new object[count];
			for (int iter = 0; iter < count; iter++)
			{
				array[iter]		= obj[iter];
			}

			// write array
			writer.WriteProperty(array);
		}
		
		public override System.Collections.ArrayList CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new ArrayList(capacity: 4);
		}
		
		public override System.Collections.ArrayList Deserialize(System.Collections.ArrayList obj, IObjectReader reader, SerializationContext context)
		{
			// prepare object
			obj.Clear();
			
			// update capacity and add elements
			object[] 	array 	= reader.ReadProperty<object[]>();
			int			count	= array.Length;
			
			obj.Capacity 		= count;
			for (int iter = 0; iter < count; iter++)
			{
				object	item	= array[iter];
				obj.Add(item);
			}

			return obj;	
		}
		
		#endregion
	}
}