using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class System_Collections_HashtableDataProvider : SerializationDataProvider<System.Collections.Hashtable>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(System.Collections.Hashtable obj, IObjectWriter writer, SerializationContext context)
		{
			// construct array representation of the object
			int count = obj.Count;
			DictionaryEntry[] array = new DictionaryEntry[count];

			int iter = 0;
			IDictionaryEnumerator enumerator = obj.GetEnumerator();
			while (enumerator.MoveNext())
			{
				array[iter++] = enumerator.Entry;
			}

			// write array
			writer.WriteProperty(array);
		}

		public override System.Collections.Hashtable CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new Hashtable(capacity: 4);
		}
		
		public override System.Collections.Hashtable Deserialize(System.Collections.Hashtable obj, IObjectReader reader, SerializationContext context)
		{
			// prepare object
			obj.Clear();

			// read array and its elements to dictionary
			DictionaryEntry[]	array 	= reader.ReadProperty<DictionaryEntry[]>();
			int					count	= array.Length;
			
			for (int iter = 0; iter < count; iter++)
			{
				DictionaryEntry	item	= array[iter];
				obj.Add(item.Key, item.Value);
			}

			return obj;
		}
		
		#endregion
	}
}