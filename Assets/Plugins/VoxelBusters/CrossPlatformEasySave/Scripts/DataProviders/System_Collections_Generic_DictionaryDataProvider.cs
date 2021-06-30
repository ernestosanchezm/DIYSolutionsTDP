using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class System_Collections_Generic_DictionaryDataProvider<TKey, TValue> : SerializationDataProvider<Dictionary<TKey, TValue>> 
	{
		#region SerializationDataProvider abstract members implementation

		public override void Serialize(Dictionary<TKey, TValue> obj, IObjectWriter writer, SerializationContext context)
		{
			// construct array representation of the object
			int count = obj.Count;
			KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[count];

			int iter = 0;
			IEnumerator<KeyValuePair<TKey, TValue>> enumerator = obj.GetEnumerator();
			while (enumerator.MoveNext())
			{
				array[iter++] = enumerator.Current;
			}

			// write array
			writer.WriteProperty(array);
		}

		public override Dictionary<TKey, TValue> CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new Dictionary<TKey, TValue>(capacity: 4);
		}

		public override Dictionary<TKey, TValue> Deserialize(Dictionary<TKey, TValue> obj, IObjectReader reader, SerializationContext context)
		{
			// prepare object
			obj.Clear();

			// read array and its elements to dictionary
			KeyValuePair<TKey, TValue>[]	array 	= reader.ReadProperty<KeyValuePair<TKey, TValue>[]>();
			int								count	= array.Length;
			
			for (int iter = 0; iter < count; iter++)
			{
				KeyValuePair<TKey, TValue>	item	= array[iter];
				obj.Add(item.Key, item.Value);
			}

			return obj;
		}

		#endregion
	}
}