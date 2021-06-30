using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_HingeJoint2DDataProvider : SerializationDataProvider<UnityEngine.HingeJoint2D>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.HingeJoint2D obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("useMotor", obj.useMotor);
			writer.WriteProperty("useLimits", obj.useLimits);
			writer.WriteProperty("motor", obj.motor);
			writer.WriteProperty("limits", obj.limits);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.AnchoredJoint2D>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.HingeJoint2D CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.HingeJoint2D)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.HingeJoint2D Deserialize(UnityEngine.HingeJoint2D obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.useMotor = reader.ReadProperty<bool>("useMotor");
			obj.useLimits = reader.ReadProperty<bool>("useLimits");
			obj.motor = reader.ReadProperty<UnityEngine.JointMotor2D>("motor");
			obj.limits = reader.ReadProperty<UnityEngine.JointAngleLimits2D>("limits");
			
			// read parent property values
			return (UnityEngine.HingeJoint2D)SerializationDataProvider<UnityEngine.AnchoredJoint2D>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}