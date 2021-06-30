using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_UI_MaskableGraphicDataProvider : SerializationDataProvider<UnityEngine.UI.MaskableGraphic>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.UI.MaskableGraphic obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("maskable", obj.maskable);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.UI.Graphic>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.UI.MaskableGraphic CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.UI.MaskableGraphic)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.UI.MaskableGraphic Deserialize(UnityEngine.UI.MaskableGraphic obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.maskable = reader.ReadProperty<bool>("maskable");
			
			// read parent property values
			return (UnityEngine.UI.MaskableGraphic)SerializationDataProvider<UnityEngine.UI.Graphic>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}