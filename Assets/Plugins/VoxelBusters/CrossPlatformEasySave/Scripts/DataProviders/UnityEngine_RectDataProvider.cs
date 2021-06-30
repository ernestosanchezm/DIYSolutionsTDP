using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_RectDataProvider : SerializationDataProvider<UnityEngine.Rect>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Rect obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty(obj.x);
			writer.WriteProperty(obj.y);
			writer.WriteProperty(obj.width);
			writer.WriteProperty(obj.height);
		}
		
		public override UnityEngine.Rect CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new UnityEngine.Rect();
		}
		
		public override UnityEngine.Rect Deserialize(UnityEngine.Rect obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.x = reader.ReadProperty<float>();
			obj.y = reader.ReadProperty<float>();
			obj.width = reader.ReadProperty<float>();
			obj.height = reader.ReadProperty<float>();
			
			return obj;
		}
		
		#endregion
	}
}