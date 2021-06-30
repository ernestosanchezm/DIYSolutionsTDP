using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_Vector4DataProvider : SerializationDataProvider<UnityEngine.Vector4>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Vector4 obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty(obj.x);
			writer.WriteProperty(obj.y);
			writer.WriteProperty(obj.z);
			writer.WriteProperty(obj.w);
		}
		
		public override UnityEngine.Vector4 CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new UnityEngine.Vector4();
		}
		
		public override UnityEngine.Vector4 Deserialize(UnityEngine.Vector4 obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.x = reader.ReadProperty<float>();
			obj.y = reader.ReadProperty<float>();
			obj.z = reader.ReadProperty<float>();
			obj.w = reader.ReadProperty<float>();
			
			return obj;
		}
		
		#endregion
	}
}