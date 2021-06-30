using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_BoundsDataProvider : SerializationDataProvider<UnityEngine.Bounds>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Bounds obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty(obj.center);
			writer.WriteProperty(obj.size);
		}
		
		public override UnityEngine.Bounds CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new UnityEngine.Bounds();
		}
		
		public override UnityEngine.Bounds Deserialize(UnityEngine.Bounds obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.center = reader.ReadProperty<UnityEngine.Vector3>();
			obj.size = reader.ReadProperty<UnityEngine.Vector3>();
			
			return obj;
		}
		
		#endregion
	}
}