using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_EdgeCollider2DDataProvider : SerializationDataProvider<UnityEngine.EdgeCollider2D>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.EdgeCollider2D obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("edgeRadius", obj.edgeRadius);
			writer.WriteProperty("points", obj.points);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Collider2D>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.EdgeCollider2D CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.EdgeCollider2D)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.EdgeCollider2D Deserialize(UnityEngine.EdgeCollider2D obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.edgeRadius = reader.ReadProperty<float>("edgeRadius");
			obj.points = reader.ReadProperty<UnityEngine.Vector2[]>("points");
			
			// read parent property values
			return (UnityEngine.EdgeCollider2D)SerializationDataProvider<UnityEngine.Collider2D>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}