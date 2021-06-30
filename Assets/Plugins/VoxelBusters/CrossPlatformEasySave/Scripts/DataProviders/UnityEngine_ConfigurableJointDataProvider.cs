using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_ConfigurableJointDataProvider : SerializationDataProvider<UnityEngine.ConfigurableJoint>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.ConfigurableJoint obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("secondaryAxis", obj.secondaryAxis);
			writer.WriteProperty("xMotion", obj.xMotion);
			writer.WriteProperty("yMotion", obj.yMotion);
			writer.WriteProperty("zMotion", obj.zMotion);
			writer.WriteProperty("angularXMotion", obj.angularXMotion);
			writer.WriteProperty("angularYMotion", obj.angularYMotion);
			writer.WriteProperty("angularZMotion", obj.angularZMotion);
			writer.WriteProperty("linearLimitSpring", obj.linearLimitSpring);
			writer.WriteProperty("angularXLimitSpring", obj.angularXLimitSpring);
			writer.WriteProperty("angularYZLimitSpring", obj.angularYZLimitSpring);
			writer.WriteProperty("linearLimit", obj.linearLimit);
			writer.WriteProperty("lowAngularXLimit", obj.lowAngularXLimit);
			writer.WriteProperty("highAngularXLimit", obj.highAngularXLimit);
			writer.WriteProperty("angularYLimit", obj.angularYLimit);
			writer.WriteProperty("angularZLimit", obj.angularZLimit);
			writer.WriteProperty("targetPosition", obj.targetPosition);
			writer.WriteProperty("targetVelocity", obj.targetVelocity);
			writer.WriteProperty("xDrive", obj.xDrive);
			writer.WriteProperty("yDrive", obj.yDrive);
			writer.WriteProperty("zDrive", obj.zDrive);
			writer.WriteProperty("targetRotation", obj.targetRotation);
			writer.WriteProperty("targetAngularVelocity", obj.targetAngularVelocity);
			writer.WriteProperty("rotationDriveMode", obj.rotationDriveMode);
			writer.WriteProperty("angularXDrive", obj.angularXDrive);
			writer.WriteProperty("angularYZDrive", obj.angularYZDrive);
			writer.WriteProperty("slerpDrive", obj.slerpDrive);
			writer.WriteProperty("projectionMode", obj.projectionMode);
			writer.WriteProperty("projectionDistance", obj.projectionDistance);
			writer.WriteProperty("projectionAngle", obj.projectionAngle);
			writer.WriteProperty("configuredInWorldSpace", obj.configuredInWorldSpace);
			writer.WriteProperty("swapBodies", obj.swapBodies);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Joint>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.ConfigurableJoint CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.ConfigurableJoint)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.ConfigurableJoint Deserialize(UnityEngine.ConfigurableJoint obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.secondaryAxis = reader.ReadProperty<UnityEngine.Vector3>("secondaryAxis");
			obj.xMotion = reader.ReadProperty<UnityEngine.ConfigurableJointMotion>("xMotion");
			obj.yMotion = reader.ReadProperty<UnityEngine.ConfigurableJointMotion>("yMotion");
			obj.zMotion = reader.ReadProperty<UnityEngine.ConfigurableJointMotion>("zMotion");
			obj.angularXMotion = reader.ReadProperty<UnityEngine.ConfigurableJointMotion>("angularXMotion");
			obj.angularYMotion = reader.ReadProperty<UnityEngine.ConfigurableJointMotion>("angularYMotion");
			obj.angularZMotion = reader.ReadProperty<UnityEngine.ConfigurableJointMotion>("angularZMotion");
			obj.linearLimitSpring = reader.ReadProperty<UnityEngine.SoftJointLimitSpring>("linearLimitSpring");
			obj.angularXLimitSpring = reader.ReadProperty<UnityEngine.SoftJointLimitSpring>("angularXLimitSpring");
			obj.angularYZLimitSpring = reader.ReadProperty<UnityEngine.SoftJointLimitSpring>("angularYZLimitSpring");
			obj.linearLimit = reader.ReadProperty<UnityEngine.SoftJointLimit>("linearLimit");
			obj.lowAngularXLimit = reader.ReadProperty<UnityEngine.SoftJointLimit>("lowAngularXLimit");
			obj.highAngularXLimit = reader.ReadProperty<UnityEngine.SoftJointLimit>("highAngularXLimit");
			obj.angularYLimit = reader.ReadProperty<UnityEngine.SoftJointLimit>("angularYLimit");
			obj.angularZLimit = reader.ReadProperty<UnityEngine.SoftJointLimit>("angularZLimit");
			obj.targetPosition = reader.ReadProperty<UnityEngine.Vector3>("targetPosition");
			obj.targetVelocity = reader.ReadProperty<UnityEngine.Vector3>("targetVelocity");
			obj.xDrive = reader.ReadProperty<UnityEngine.JointDrive>("xDrive");
			obj.yDrive = reader.ReadProperty<UnityEngine.JointDrive>("yDrive");
			obj.zDrive = reader.ReadProperty<UnityEngine.JointDrive>("zDrive");
			obj.targetRotation = reader.ReadProperty<UnityEngine.Quaternion>("targetRotation");
			obj.targetAngularVelocity = reader.ReadProperty<UnityEngine.Vector3>("targetAngularVelocity");
			obj.rotationDriveMode = reader.ReadProperty<UnityEngine.RotationDriveMode>("rotationDriveMode");
			obj.angularXDrive = reader.ReadProperty<UnityEngine.JointDrive>("angularXDrive");
			obj.angularYZDrive = reader.ReadProperty<UnityEngine.JointDrive>("angularYZDrive");
			obj.slerpDrive = reader.ReadProperty<UnityEngine.JointDrive>("slerpDrive");
			obj.projectionMode = reader.ReadProperty<UnityEngine.JointProjectionMode>("projectionMode");
			obj.projectionDistance = reader.ReadProperty<float>("projectionDistance");
			obj.projectionAngle = reader.ReadProperty<float>("projectionAngle");
			obj.configuredInWorldSpace = reader.ReadProperty<bool>("configuredInWorldSpace");
			obj.swapBodies = reader.ReadProperty<bool>("swapBodies");
			
			// read parent property values
			return (UnityEngine.ConfigurableJoint)SerializationDataProvider<UnityEngine.Joint>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}