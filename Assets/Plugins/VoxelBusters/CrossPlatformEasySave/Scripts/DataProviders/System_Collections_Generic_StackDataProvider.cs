using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class System_Collections_Generic_StackDataProvider<T> : SerializationDataProvider<System.Collections.Generic.Stack<T>>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(System.Collections.Generic.Stack<T> obj, IObjectWriter writer, SerializationContext context)
		{
			// construct array representation of the object
			T[] 	array 	= obj.ToArray();

			// write array
			writer.WriteProperty(array);
		}
		
		public override System.Collections.Generic.Stack<T> CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new System.Collections.Generic.Stack<T>(4);
		}
		
		public override System.Collections.Generic.Stack<T> Deserialize(System.Collections.Generic.Stack<T> obj, IObjectReader reader, SerializationContext context)
		{
			// prepare object
			obj.Clear();
			
			// update capacity and add elements
			T[] 		array 	= reader.ReadProperty<T[]>();
			int			count	= array.Length;
			
			for (int iter = 0; iter < count; iter++)
			{
				T		item	= array[iter];
				obj.Push(item);
			}

			return obj;	
		}
		
		#endregion
	}
}