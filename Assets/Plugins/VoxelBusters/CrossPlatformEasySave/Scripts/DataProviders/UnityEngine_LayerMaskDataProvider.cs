using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_LayerMaskDataProvider : SerializationDataProvider<UnityEngine.LayerMask>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.LayerMask obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("value", obj.value);
		}
		
		public override UnityEngine.LayerMask CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new UnityEngine.LayerMask();
		}
		
		public override UnityEngine.LayerMask Deserialize(UnityEngine.LayerMask obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.value = reader.ReadProperty<int>("value");
			
			return obj;
		}
		
		#endregion
	}
}