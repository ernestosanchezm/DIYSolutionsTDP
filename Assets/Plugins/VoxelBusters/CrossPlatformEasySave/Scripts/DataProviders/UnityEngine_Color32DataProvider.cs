using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_Color32DataProvider : SerializationDataProvider<UnityEngine.Color32>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Color32 obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty(obj.r);
			writer.WriteProperty(obj.g);
			writer.WriteProperty(obj.b);
			writer.WriteProperty(obj.a);
		}
		
		public override UnityEngine.Color32 CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new UnityEngine.Color32();
		}
		
		public override UnityEngine.Color32 Deserialize(UnityEngine.Color32 obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.r = reader.ReadProperty<byte>();
			obj.g = reader.ReadProperty<byte>();
			obj.b = reader.ReadProperty<byte>();
			obj.a = reader.ReadProperty<byte>();
			
			return obj;
		}
		
		#endregion
	}
}