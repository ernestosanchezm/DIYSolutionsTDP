using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_Vector2DataProvider : SerializationDataProvider<UnityEngine.Vector2>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Vector2 obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty(obj.x);
			writer.WriteProperty(obj.y);
		}
		
		public override UnityEngine.Vector2 CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new UnityEngine.Vector2();
		}
		
		public override UnityEngine.Vector2 Deserialize(UnityEngine.Vector2 obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.x = reader.ReadProperty<float>();
			obj.y = reader.ReadProperty<float>();

			return obj;
		}
		
		#endregion
	}
}