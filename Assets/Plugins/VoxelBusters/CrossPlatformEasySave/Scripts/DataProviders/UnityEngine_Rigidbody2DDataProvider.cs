using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_Rigidbody2DDataProvider : SerializationDataProvider<UnityEngine.Rigidbody2D>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Rigidbody2D obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("position", obj.position);
			writer.WriteProperty("rotation", obj.rotation);
			writer.WriteProperty("velocity", obj.velocity);
			writer.WriteProperty("angularVelocity", obj.angularVelocity);
			writer.WriteProperty("useAutoMass", obj.useAutoMass);
			writer.WriteProperty("mass", obj.mass);
			writer.WriteProperty("sharedMaterial", obj.sharedMaterial);
			writer.WriteProperty("centerOfMass", obj.centerOfMass);
			writer.WriteProperty("inertia", obj.inertia);
			writer.WriteProperty("drag", obj.drag);
			writer.WriteProperty("angularDrag", obj.angularDrag);
			writer.WriteProperty("gravityScale", obj.gravityScale);
			writer.WriteProperty("bodyType", obj.bodyType);
			writer.WriteProperty("useFullKinematicContacts", obj.useFullKinematicContacts);
			writer.WriteProperty("isKinematic", obj.isKinematic);
			writer.WriteProperty("freezeRotation", obj.freezeRotation);
			writer.WriteProperty("constraints", obj.constraints);
			writer.WriteProperty("simulated", obj.simulated);
			writer.WriteProperty("interpolation", obj.interpolation);
			writer.WriteProperty("sleepMode", obj.sleepMode);
			writer.WriteProperty("collisionDetectionMode", obj.collisionDetectionMode);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Component>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.Rigidbody2D CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.Rigidbody2D)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.Rigidbody2D Deserialize(UnityEngine.Rigidbody2D obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.position = reader.ReadProperty<UnityEngine.Vector2>("position");
			obj.rotation = reader.ReadProperty<float>("rotation");
			obj.velocity = reader.ReadProperty<UnityEngine.Vector2>("velocity");
			obj.angularVelocity = reader.ReadProperty<float>("angularVelocity");
			obj.useAutoMass = reader.ReadProperty<bool>("useAutoMass");
			obj.mass = reader.ReadProperty<float>("mass");
			obj.sharedMaterial = reader.ReadProperty<UnityEngine.PhysicsMaterial2D>("sharedMaterial");
			obj.centerOfMass = reader.ReadProperty<UnityEngine.Vector2>("centerOfMass");
			obj.inertia = reader.ReadProperty<float>("inertia");
			obj.drag = reader.ReadProperty<float>("drag");
			obj.angularDrag = reader.ReadProperty<float>("angularDrag");
			obj.gravityScale = reader.ReadProperty<float>("gravityScale");
			obj.bodyType = reader.ReadProperty<UnityEngine.RigidbodyType2D>("bodyType");
			obj.useFullKinematicContacts = reader.ReadProperty<bool>("useFullKinematicContacts");
			obj.isKinematic = reader.ReadProperty<bool>("isKinematic");
			obj.freezeRotation = reader.ReadProperty<bool>("freezeRotation");
			obj.constraints = reader.ReadProperty<UnityEngine.RigidbodyConstraints2D>("constraints");
			obj.simulated = reader.ReadProperty<bool>("simulated");
			obj.interpolation = reader.ReadProperty<UnityEngine.RigidbodyInterpolation2D>("interpolation");
			obj.sleepMode = reader.ReadProperty<UnityEngine.RigidbodySleepMode2D>("sleepMode");
			obj.collisionDetectionMode = reader.ReadProperty<UnityEngine.CollisionDetectionMode2D>("collisionDetectionMode");
			
			// read parent property values
			return (UnityEngine.Rigidbody2D)SerializationDataProvider<UnityEngine.Component>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}