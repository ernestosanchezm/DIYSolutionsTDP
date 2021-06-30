using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_Vector3DataProvider : SerializationDataProvider<UnityEngine.Vector3>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Vector3 obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty(obj.x);
			writer.WriteProperty(obj.y);
			writer.WriteProperty(obj.z);
		}
		
		public override UnityEngine.Vector3 CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new UnityEngine.Vector3();
		}
		
		public override UnityEngine.Vector3 Deserialize(UnityEngine.Vector3 obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.x = reader.ReadProperty<float>();
			obj.y = reader.ReadProperty<float>();
			obj.z = reader.ReadProperty<float>();
			
			return obj;
		}
		
		#endregion
	}
}