using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_PolygonCollider2DDataProvider : SerializationDataProvider<UnityEngine.PolygonCollider2D>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.PolygonCollider2D obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("points", obj.points);
			writer.WriteProperty("pathCount", obj.pathCount);
			writer.WriteProperty("autoTiling", obj.autoTiling);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Collider2D>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.PolygonCollider2D CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.PolygonCollider2D)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.PolygonCollider2D Deserialize(UnityEngine.PolygonCollider2D obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.points = reader.ReadProperty<UnityEngine.Vector2[]>("points");
			obj.pathCount = reader.ReadProperty<int>("pathCount");
			obj.autoTiling = reader.ReadProperty<bool>("autoTiling");
			
			// read parent property values
			return (UnityEngine.PolygonCollider2D)SerializationDataProvider<UnityEngine.Collider2D>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}