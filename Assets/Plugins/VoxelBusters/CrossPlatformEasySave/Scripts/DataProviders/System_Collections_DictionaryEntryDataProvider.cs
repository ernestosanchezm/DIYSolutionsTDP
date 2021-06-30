using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class System_Collections_DictionaryEntryDataProvider : SerializationDataProvider<System.Collections.DictionaryEntry>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(System.Collections.DictionaryEntry obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty(obj.Key);
			writer.WriteProperty(obj.Value);
		}
		
		public override System.Collections.DictionaryEntry CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new System.Collections.DictionaryEntry();
		}
		
		public override System.Collections.DictionaryEntry Deserialize(System.Collections.DictionaryEntry obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.Key = reader.ReadProperty<System.Object>();
			obj.Value = reader.ReadProperty<System.Object>();
			
			return obj;
		}
		
		#endregion
	}
}