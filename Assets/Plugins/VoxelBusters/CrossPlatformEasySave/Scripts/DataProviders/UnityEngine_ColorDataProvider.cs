using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_ColorDataProvider : SerializationDataProvider<UnityEngine.Color>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Color obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty(obj.r);
			writer.WriteProperty(obj.g);
			writer.WriteProperty(obj.b);
			writer.WriteProperty(obj.a);
		}
		
		public override UnityEngine.Color CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new UnityEngine.Color();
		}
		
		public override UnityEngine.Color Deserialize(UnityEngine.Color obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.r = reader.ReadProperty<float>();
			obj.g = reader.ReadProperty<float>();
			obj.b = reader.ReadProperty<float>();
			obj.a = reader.ReadProperty<float>();
			
			return obj;
		}
		
		#endregion
	}
}