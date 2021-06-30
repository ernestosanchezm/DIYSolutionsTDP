using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_WheelJoint2DDataProvider : SerializationDataProvider<UnityEngine.WheelJoint2D>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.WheelJoint2D obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("suspension", obj.suspension);
			writer.WriteProperty("useMotor", obj.useMotor);
			writer.WriteProperty("motor", obj.motor);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.AnchoredJoint2D>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.WheelJoint2D CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.WheelJoint2D)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.WheelJoint2D Deserialize(UnityEngine.WheelJoint2D obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.suspension = reader.ReadProperty<UnityEngine.JointSuspension2D>("suspension");
			obj.useMotor = reader.ReadProperty<bool>("useMotor");
			obj.motor = reader.ReadProperty<UnityEngine.JointMotor2D>("motor");
			
			// read parent property values
			return (UnityEngine.WheelJoint2D)SerializationDataProvider<UnityEngine.AnchoredJoint2D>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}