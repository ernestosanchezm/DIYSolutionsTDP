using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_RigidbodyDataProvider : SerializationDataProvider<UnityEngine.Rigidbody>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Rigidbody obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("velocity", obj.velocity);
			writer.WriteProperty("angularVelocity", obj.angularVelocity);
			writer.WriteProperty("drag", obj.drag);
			writer.WriteProperty("angularDrag", obj.angularDrag);
			writer.WriteProperty("mass", obj.mass);
			writer.WriteProperty("useGravity", obj.useGravity);
			writer.WriteProperty("isKinematic", obj.isKinematic);
			writer.WriteProperty("freezeRotation", obj.freezeRotation);
			writer.WriteProperty("constraints", obj.constraints);
			writer.WriteProperty("collisionDetectionMode", obj.collisionDetectionMode);
			writer.WriteProperty("centerOfMass", obj.centerOfMass);
			writer.WriteProperty("inertiaTensorRotation", obj.inertiaTensorRotation);
			writer.WriteProperty("inertiaTensor", obj.inertiaTensor);
			writer.WriteProperty("detectCollisions", obj.detectCollisions);
			writer.WriteProperty("interpolation", obj.interpolation);
			writer.WriteProperty("sleepThreshold", obj.sleepThreshold);
			writer.WriteProperty("maxAngularVelocity", obj.maxAngularVelocity);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Component>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.Rigidbody CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.Rigidbody)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.Rigidbody Deserialize(UnityEngine.Rigidbody obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.velocity = reader.ReadProperty<UnityEngine.Vector3>("velocity");
			obj.angularVelocity = reader.ReadProperty<UnityEngine.Vector3>("angularVelocity");
			obj.drag = reader.ReadProperty<float>("drag");
			obj.angularDrag = reader.ReadProperty<float>("angularDrag");
			obj.mass = reader.ReadProperty<float>("mass");
			obj.useGravity = reader.ReadProperty<bool>("useGravity");
			obj.isKinematic = reader.ReadProperty<bool>("isKinematic");
			obj.freezeRotation = reader.ReadProperty<bool>("freezeRotation");
			obj.constraints = reader.ReadProperty<UnityEngine.RigidbodyConstraints>("constraints");
			obj.collisionDetectionMode = reader.ReadProperty<UnityEngine.CollisionDetectionMode>("collisionDetectionMode");
			obj.centerOfMass = reader.ReadProperty<UnityEngine.Vector3>("centerOfMass");
			obj.inertiaTensorRotation = reader.ReadProperty<UnityEngine.Quaternion>("inertiaTensorRotation");
			obj.inertiaTensor = reader.ReadProperty<UnityEngine.Vector3>("inertiaTensor");
			obj.detectCollisions = reader.ReadProperty<bool>("detectCollisions");
			obj.interpolation = reader.ReadProperty<UnityEngine.RigidbodyInterpolation>("interpolation");
			obj.sleepThreshold = reader.ReadProperty<float>("sleepThreshold");
			obj.maxAngularVelocity = reader.ReadProperty<float>("maxAngularVelocity");
			
			// read parent property values
			return (UnityEngine.Rigidbody)SerializationDataProvider<UnityEngine.Component>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}