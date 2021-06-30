using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_BoxCollider2DDataProvider : SerializationDataProvider<UnityEngine.BoxCollider2D>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.BoxCollider2D obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("size", obj.size);
			writer.WriteProperty("edgeRadius", obj.edgeRadius);
			writer.WriteProperty("autoTiling", obj.autoTiling);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Collider2D>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.BoxCollider2D CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.BoxCollider2D)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.BoxCollider2D Deserialize(UnityEngine.BoxCollider2D obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.size = reader.ReadProperty<UnityEngine.Vector2>("size");
			obj.edgeRadius = reader.ReadProperty<float>("edgeRadius");
			obj.autoTiling = reader.ReadProperty<bool>("autoTiling");
			
			// read parent property values
			return (UnityEngine.BoxCollider2D)SerializationDataProvider<UnityEngine.Collider2D>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}