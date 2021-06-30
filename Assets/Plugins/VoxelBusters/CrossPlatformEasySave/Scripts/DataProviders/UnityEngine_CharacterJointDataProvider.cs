using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_CharacterJointDataProvider : SerializationDataProvider<UnityEngine.CharacterJoint>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.CharacterJoint obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("swingAxis", obj.swingAxis);
			writer.WriteProperty("twistLimitSpring", obj.twistLimitSpring);
			writer.WriteProperty("swingLimitSpring", obj.swingLimitSpring);
			writer.WriteProperty("lowTwistLimit", obj.lowTwistLimit);
			writer.WriteProperty("highTwistLimit", obj.highTwistLimit);
			writer.WriteProperty("swing1Limit", obj.swing1Limit);
			writer.WriteProperty("swing2Limit", obj.swing2Limit);
			writer.WriteProperty("enableProjection", obj.enableProjection);
			writer.WriteProperty("projectionDistance", obj.projectionDistance);
			writer.WriteProperty("projectionAngle", obj.projectionAngle);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Joint>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.CharacterJoint CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.CharacterJoint)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.CharacterJoint Deserialize(UnityEngine.CharacterJoint obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.swingAxis = reader.ReadProperty<UnityEngine.Vector3>("swingAxis");
			obj.twistLimitSpring = reader.ReadProperty<UnityEngine.SoftJointLimitSpring>("twistLimitSpring");
			obj.swingLimitSpring = reader.ReadProperty<UnityEngine.SoftJointLimitSpring>("swingLimitSpring");
			obj.lowTwistLimit = reader.ReadProperty<UnityEngine.SoftJointLimit>("lowTwistLimit");
			obj.highTwistLimit = reader.ReadProperty<UnityEngine.SoftJointLimit>("highTwistLimit");
			obj.swing1Limit = reader.ReadProperty<UnityEngine.SoftJointLimit>("swing1Limit");
			obj.swing2Limit = reader.ReadProperty<UnityEngine.SoftJointLimit>("swing2Limit");
			obj.enableProjection = reader.ReadProperty<bool>("enableProjection");
			obj.projectionDistance = reader.ReadProperty<float>("projectionDistance");
			obj.projectionAngle = reader.ReadProperty<float>("projectionAngle");
			
			// read parent property values
			return (UnityEngine.CharacterJoint)SerializationDataProvider<UnityEngine.Joint>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}