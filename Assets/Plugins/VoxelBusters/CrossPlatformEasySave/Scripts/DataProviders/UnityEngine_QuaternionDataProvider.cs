using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_QuaternionDataProvider : SerializationDataProvider<UnityEngine.Quaternion>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Quaternion obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty(obj.x);
			writer.WriteProperty(obj.y);
			writer.WriteProperty(obj.z);
			writer.WriteProperty(obj.w);
		}
		
		public override UnityEngine.Quaternion CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new UnityEngine.Quaternion();
		}
		
		public override UnityEngine.Quaternion Deserialize(UnityEngine.Quaternion obj, IObjectReader reader, SerializationContext context)
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