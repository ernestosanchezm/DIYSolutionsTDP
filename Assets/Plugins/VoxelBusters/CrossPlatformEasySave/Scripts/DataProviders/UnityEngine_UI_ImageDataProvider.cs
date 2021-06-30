using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_UI_ImageDataProvider : SerializationDataProvider<UnityEngine.UI.Image>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.UI.Image obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("sprite", obj.sprite);
			writer.WriteProperty("type", obj.type);
			writer.WriteProperty("preserveAspect", obj.preserveAspect);
			writer.WriteProperty("fillCenter", obj.fillCenter);
			writer.WriteProperty("fillMethod", obj.fillMethod);
			writer.WriteProperty("fillAmount", obj.fillAmount);
			writer.WriteProperty("fillClockwise", obj.fillClockwise);
			writer.WriteProperty("fillOrigin", obj.fillOrigin);
			writer.WriteProperty("alphaHitTestMinimumThreshold", obj.alphaHitTestMinimumThreshold);
			writer.WriteProperty("material", obj.material);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.UI.MaskableGraphic>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.UI.Image CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.UI.Image)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.UI.Image Deserialize(UnityEngine.UI.Image obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.sprite = reader.ReadProperty<UnityEngine.Sprite>("sprite");
			obj.type = reader.ReadProperty<UnityEngine.UI.Image.Type>("type");
			obj.preserveAspect = reader.ReadProperty<bool>("preserveAspect");
			obj.fillCenter = reader.ReadProperty<bool>("fillCenter");
			obj.fillMethod = reader.ReadProperty<UnityEngine.UI.Image.FillMethod>("fillMethod");
			obj.fillAmount = reader.ReadProperty<float>("fillAmount");
			obj.fillClockwise = reader.ReadProperty<bool>("fillClockwise");
			obj.fillOrigin = reader.ReadProperty<int>("fillOrigin");
			obj.alphaHitTestMinimumThreshold = reader.ReadProperty<float>("alphaHitTestMinimumThreshold");
			obj.material = reader.ReadProperty<UnityEngine.Material>("material");
			
			// read parent property values
			return (UnityEngine.UI.Image)SerializationDataProvider<UnityEngine.UI.MaskableGraphic>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}