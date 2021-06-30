using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_JointSpringDataProvider : SerializationDataProvider<UnityEngine.JointSpring>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.JointSpring obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("spring", obj.spring);
			writer.WriteProperty("damper", obj.damper);
			writer.WriteProperty("targetPosition", obj.targetPosition);
		}
		
		public override UnityEngine.JointSpring CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new UnityEngine.JointSpring();
		}
		
		public override UnityEngine.JointSpring Deserialize(UnityEngine.JointSpring obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.spring = reader.ReadProperty<float>();
			obj.damper = reader.ReadProperty<float>();
			obj.targetPosition = reader.ReadProperty<float>();
			
			return obj;
		}
		
		#endregion
	}
}