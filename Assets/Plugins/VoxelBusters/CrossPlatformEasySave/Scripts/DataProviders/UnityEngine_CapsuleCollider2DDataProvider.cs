using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_CapsuleCollider2DDataProvider : SerializationDataProvider<UnityEngine.CapsuleCollider2D>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.CapsuleCollider2D obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("size", obj.size);
			writer.WriteProperty("direction", obj.direction);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Collider2D>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.CapsuleCollider2D CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.CapsuleCollider2D)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.CapsuleCollider2D Deserialize(UnityEngine.CapsuleCollider2D obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.size = reader.ReadProperty<UnityEngine.Vector2>("size");
			obj.direction = reader.ReadProperty<UnityEngine.CapsuleDirection2D>("direction");
			
			// read parent property values
			return (UnityEngine.CapsuleCollider2D)SerializationDataProvider<UnityEngine.Collider2D>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}