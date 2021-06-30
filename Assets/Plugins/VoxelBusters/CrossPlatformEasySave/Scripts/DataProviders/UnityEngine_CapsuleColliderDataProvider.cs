using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_CapsuleColliderDataProvider : SerializationDataProvider<UnityEngine.CapsuleCollider>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.CapsuleCollider obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("center", obj.center);
			writer.WriteProperty("radius", obj.radius);
			writer.WriteProperty("height", obj.height);
			writer.WriteProperty("direction", obj.direction);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Collider>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.CapsuleCollider CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.CapsuleCollider)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.CapsuleCollider Deserialize(UnityEngine.CapsuleCollider obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.center = reader.ReadProperty<UnityEngine.Vector3>("center");
			obj.radius = reader.ReadProperty<float>("radius");
			obj.height = reader.ReadProperty<float>("height");
			obj.direction = reader.ReadProperty<int>("direction");
			
			// read parent property values
			return (UnityEngine.CapsuleCollider)SerializationDataProvider<UnityEngine.Collider>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}