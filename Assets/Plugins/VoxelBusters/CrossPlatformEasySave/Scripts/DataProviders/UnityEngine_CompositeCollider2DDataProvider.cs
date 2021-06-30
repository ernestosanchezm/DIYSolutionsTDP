using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_CompositeCollider2DDataProvider : SerializationDataProvider<UnityEngine.CompositeCollider2D>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.CompositeCollider2D obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("geometryType", obj.geometryType);
			writer.WriteProperty("generationType", obj.generationType);
			writer.WriteProperty("vertexDistance", obj.vertexDistance);
			writer.WriteProperty("edgeRadius", obj.edgeRadius);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Collider2D>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.CompositeCollider2D CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.CompositeCollider2D)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.CompositeCollider2D Deserialize(UnityEngine.CompositeCollider2D obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.geometryType = reader.ReadProperty<UnityEngine.CompositeCollider2D.GeometryType>("geometryType");
			obj.generationType = reader.ReadProperty<UnityEngine.CompositeCollider2D.GenerationType>("generationType");
			obj.vertexDistance = reader.ReadProperty<float>("vertexDistance");
			obj.edgeRadius = reader.ReadProperty<float>("edgeRadius");
			
			// read parent property values
			return (UnityEngine.CompositeCollider2D)SerializationDataProvider<UnityEngine.Collider2D>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}