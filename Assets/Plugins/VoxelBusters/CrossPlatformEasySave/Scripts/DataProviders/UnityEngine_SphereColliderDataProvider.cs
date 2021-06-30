using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_SphereColliderDataProvider : SerializationDataProvider<UnityEngine.SphereCollider>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.SphereCollider obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("center", obj.center);
			writer.WriteProperty("radius", obj.radius);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Collider>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.SphereCollider CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.SphereCollider)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.SphereCollider Deserialize(UnityEngine.SphereCollider obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.center = reader.ReadProperty<UnityEngine.Vector3>("center");
			obj.radius = reader.ReadProperty<float>("radius");
			
			// read parent property values
			return (UnityEngine.SphereCollider)SerializationDataProvider<UnityEngine.Collider>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}