using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_CharacterControllerDataProvider : SerializationDataProvider<UnityEngine.CharacterController>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.CharacterController obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("radius", obj.radius);
			writer.WriteProperty("height", obj.height);
			writer.WriteProperty("center", obj.center);
			writer.WriteProperty("slopeLimit", obj.slopeLimit);
			writer.WriteProperty("stepOffset", obj.stepOffset);
			writer.WriteProperty("skinWidth", obj.skinWidth);
			writer.WriteProperty("minMoveDistance", obj.minMoveDistance);
			writer.WriteProperty("detectCollisions", obj.detectCollisions);
			writer.WriteProperty("enableOverlapRecovery", obj.enableOverlapRecovery);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Collider>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.CharacterController CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.CharacterController)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.CharacterController Deserialize(UnityEngine.CharacterController obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.radius = reader.ReadProperty<float>("radius");
			obj.height = reader.ReadProperty<float>("height");
			obj.center = reader.ReadProperty<UnityEngine.Vector3>("center");
			obj.slopeLimit = reader.ReadProperty<float>("slopeLimit");
			obj.stepOffset = reader.ReadProperty<float>("stepOffset");
			obj.skinWidth = reader.ReadProperty<float>("skinWidth");
			obj.minMoveDistance = reader.ReadProperty<float>("minMoveDistance");
			obj.detectCollisions = reader.ReadProperty<bool>("detectCollisions");
			obj.enableOverlapRecovery = reader.ReadProperty<bool>("enableOverlapRecovery");
			
			// read parent property values
			return (UnityEngine.CharacterController)SerializationDataProvider<UnityEngine.Collider>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}