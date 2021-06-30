using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_SliderJoint2DDataProvider : SerializationDataProvider<UnityEngine.SliderJoint2D>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.SliderJoint2D obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("autoConfigureAngle", obj.autoConfigureAngle);
			writer.WriteProperty("angle", obj.angle);
			writer.WriteProperty("useMotor", obj.useMotor);
			writer.WriteProperty("useLimits", obj.useLimits);
			writer.WriteProperty("motor", obj.motor);
			writer.WriteProperty("limits", obj.limits);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.AnchoredJoint2D>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.SliderJoint2D CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.SliderJoint2D)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.SliderJoint2D Deserialize(UnityEngine.SliderJoint2D obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.autoConfigureAngle = reader.ReadProperty<bool>("autoConfigureAngle");
			obj.angle = reader.ReadProperty<float>("angle");
			obj.useMotor = reader.ReadProperty<bool>("useMotor");
			obj.useLimits = reader.ReadProperty<bool>("useLimits");
			obj.motor = reader.ReadProperty<UnityEngine.JointMotor2D>("motor");
			obj.limits = reader.ReadProperty<UnityEngine.JointTranslationLimits2D>("limits");
			
			// read parent property values
			return (UnityEngine.SliderJoint2D)SerializationDataProvider<UnityEngine.AnchoredJoint2D>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}