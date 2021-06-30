using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_HingeJointDataProvider : SerializationDataProvider<UnityEngine.HingeJoint>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.HingeJoint obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("motor", obj.motor);
			writer.WriteProperty("limits", obj.limits);
			writer.WriteProperty("spring", obj.spring);
			writer.WriteProperty("useMotor", obj.useMotor);
			writer.WriteProperty("useLimits", obj.useLimits);
			writer.WriteProperty("useSpring", obj.useSpring);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Joint>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.HingeJoint CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.HingeJoint)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.HingeJoint Deserialize(UnityEngine.HingeJoint obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.motor = reader.ReadProperty<UnityEngine.JointMotor>("motor");
			obj.limits = reader.ReadProperty<UnityEngine.JointLimits>("limits");
			obj.spring = reader.ReadProperty<UnityEngine.JointSpring>("spring");
			obj.useMotor = reader.ReadProperty<bool>("useMotor");
			obj.useLimits = reader.ReadProperty<bool>("useLimits");
			obj.useSpring = reader.ReadProperty<bool>("useSpring");
			
			// read parent property values
			return (UnityEngine.HingeJoint)SerializationDataProvider<UnityEngine.Joint>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}