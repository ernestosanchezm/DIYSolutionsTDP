using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_JointLimitsDataProvider : SerializationDataProvider<UnityEngine.JointLimits>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.JointLimits obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("min", obj.min);
			writer.WriteProperty("max", obj.max);
			writer.WriteProperty("bounciness", obj.bounciness);
			writer.WriteProperty("bounceMinVelocity", obj.bounceMinVelocity);
			writer.WriteProperty("contactDistance", obj.contactDistance);
		}
		
		public override UnityEngine.JointLimits CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new UnityEngine.JointLimits();
		}
		
		public override UnityEngine.JointLimits Deserialize(UnityEngine.JointLimits obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.min = reader.ReadProperty<float>();
			obj.max = reader.ReadProperty<float>();
			obj.bounciness = reader.ReadProperty<float>();
			obj.bounceMinVelocity = reader.ReadProperty<float>();
			obj.contactDistance = reader.ReadProperty<float>();
			
			return obj;
		}
		
		#endregion
	}
}