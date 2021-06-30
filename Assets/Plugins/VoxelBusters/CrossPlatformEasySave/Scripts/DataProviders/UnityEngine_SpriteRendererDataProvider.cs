using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_SpriteRendererDataProvider : SerializationDataProvider<UnityEngine.SpriteRenderer>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.SpriteRenderer obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("sprite", obj.sprite);
			writer.WriteProperty("drawMode", obj.drawMode);
			writer.WriteProperty("size", obj.size);
			writer.WriteProperty("adaptiveModeThreshold", obj.adaptiveModeThreshold);
			writer.WriteProperty("tileMode", obj.tileMode);
			writer.WriteProperty("color", obj.color);
			writer.WriteProperty("flipX", obj.flipX);
			writer.WriteProperty("flipY", obj.flipY);
			writer.WriteProperty("maskInteraction", obj.maskInteraction);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Renderer>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.SpriteRenderer CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.SpriteRenderer)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.SpriteRenderer Deserialize(UnityEngine.SpriteRenderer obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.sprite = reader.ReadProperty<UnityEngine.Sprite>("sprite");
			obj.drawMode = reader.ReadProperty<UnityEngine.SpriteDrawMode>("drawMode");
			obj.size = reader.ReadProperty<UnityEngine.Vector2>("size");
			obj.adaptiveModeThreshold = reader.ReadProperty<float>("adaptiveModeThreshold");
			obj.tileMode = reader.ReadProperty<UnityEngine.SpriteTileMode>("tileMode");
			obj.color = reader.ReadProperty<UnityEngine.Color>("color");
			obj.flipX = reader.ReadProperty<bool>("flipX");
			obj.flipY = reader.ReadProperty<bool>("flipY");
			obj.maskInteraction = reader.ReadProperty<UnityEngine.SpriteMaskInteraction>("maskInteraction");
			
			// read parent property values
			return (UnityEngine.SpriteRenderer)SerializationDataProvider<UnityEngine.Renderer>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}