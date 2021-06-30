using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class System_KeyValuePairDataProvider<TKey, TValue> : SerializationDataProvider<KeyValuePair<TKey, TValue>>
	{
		#region implemented abstract members of SerializationDataProvider

		public override void Serialize(KeyValuePair<TKey, TValue> obj, IObjectWriter writer, SerializationContext context)
		{
			writer.WriteProperty(obj.Key);
			writer.WriteProperty(obj.Value);
		}

		public override KeyValuePair<TKey, TValue> CreateInstance(IObjectReader reader, SerializationContext context)
		{
			TKey 	key 	= reader.ReadProperty<TKey>();
			TValue	value	= reader.ReadProperty<TValue>();
			return new KeyValuePair<TKey, TValue>(key, value);
		}

		public override KeyValuePair<TKey, TValue> Deserialize(KeyValuePair<TKey, TValue> obj, IObjectReader reader, SerializationContext context)
		{
			return obj;
		}

		#endregion
	}
}