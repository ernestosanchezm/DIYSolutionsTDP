using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_SpriteDataProvider : SerializationDataProvider<UnityEngine.Sprite>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Sprite obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("texture", obj.texture);
			writer.WriteProperty("rect", obj.rect);
			writer.WriteProperty("pivot", CalculatePivot(obj));
			writer.WriteProperty("pixelsPerUnit", obj.pixelsPerUnit);
			writer.WriteProperty("border", obj.border);

			// write parent property values
			SerializationDataProvider<UnityEngine.Object>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.Sprite CreateInstance(IObjectReader reader, SerializationContext context)
		{
			Texture2D	_texture		= reader.ReadProperty<Texture2D>();
			Rect 		_rect			= reader.ReadProperty<Rect>();
			Vector2		_pivot			= reader.ReadProperty<Vector2>();
			float		_pixelsPerUnit	= reader.ReadProperty<float>();
			Vector4		_border			= reader.ReadProperty<Vector4>();

			return Sprite.Create(_texture, _rect, _pivot, _pixelsPerUnit, extrude: 0, meshType: SpriteMeshType.Tight, border: _border);
		}
		
		public override UnityEngine.Sprite Deserialize(UnityEngine.Sprite obj, IObjectReader reader, SerializationContext context)
		{
			// read parent property values
			return (UnityEngine.Sprite)SerializationDataProvider<UnityEngine.Object>.Default.Deserialize(obj, reader, context);;
		}
		
		#endregion

		#region Private methods

		private Vector2 CalculatePivot(Sprite sprite)
		{
			Bounds	bounds		= sprite.bounds;
			Vector2 pivot 		= new Vector2((bounds.center.x / bounds.extents.x * -0.5f) + 0.5f, 
			                                  (bounds.center.y / bounds.extents.y * -0.5f) + 0.5f);
			return pivot;
		}

		#endregion
	}
}