using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_JointMotorDataProvider : SerializationDataProvider<UnityEngine.JointMotor>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.JointMotor obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("targetVelocity", obj.targetVelocity);
			writer.WriteProperty("force", obj.force);
			writer.WriteProperty("freeSpin", obj.freeSpin);
		}
		
		public override UnityEngine.JointMotor CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new UnityEngine.JointMotor();
		}
		
		public override UnityEngine.JointMotor Deserialize(UnityEngine.JointMotor obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.targetVelocity = reader.ReadProperty<float>();
			obj.force = reader.ReadProperty<float>();
			obj.freeSpin = reader.ReadProperty<bool>();
			
			return obj;
		}
		
		#endregion
	}
}