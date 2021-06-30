using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_UI_GraphicDataProvider : SerializationDataProvider<UnityEngine.UI.Graphic>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.UI.Graphic obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("color", obj.color);
			writer.WriteProperty("raycastTarget", obj.raycastTarget);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.EventSystems.UIBehaviour>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.UI.Graphic CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.UI.Graphic)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.UI.Graphic Deserialize(UnityEngine.UI.Graphic obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.color = reader.ReadProperty<UnityEngine.Color>("color");
			obj.raycastTarget = reader.ReadProperty<bool>("raycastTarget");
			
			// read parent property values
			return (UnityEngine.UI.Graphic)SerializationDataProvider<UnityEngine.EventSystems.UIBehaviour>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}