using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_CircleCollider2DDataProvider : SerializationDataProvider<UnityEngine.CircleCollider2D>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.CircleCollider2D obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("radius", obj.radius);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Collider2D>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.CircleCollider2D CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.CircleCollider2D)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.CircleCollider2D Deserialize(UnityEngine.CircleCollider2D obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.radius = reader.ReadProperty<float>("radius");
			
			// read parent property values
			return (UnityEngine.CircleCollider2D)SerializationDataProvider<UnityEngine.Collider2D>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}