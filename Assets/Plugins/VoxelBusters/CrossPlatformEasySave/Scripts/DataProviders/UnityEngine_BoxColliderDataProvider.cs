using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_BoxColliderDataProvider : SerializationDataProvider<UnityEngine.BoxCollider>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.BoxCollider obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("center", obj.center);
			writer.WriteProperty("size", obj.size);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Collider>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.BoxCollider CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.BoxCollider)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.BoxCollider Deserialize(UnityEngine.BoxCollider obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.center = reader.ReadProperty<UnityEngine.Vector3>("center");
			obj.size = reader.ReadProperty<UnityEngine.Vector3>("size");
			
			// read parent property values
			return (UnityEngine.BoxCollider)SerializationDataProvider<UnityEngine.Collider>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}