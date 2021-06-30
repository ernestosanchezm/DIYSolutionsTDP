using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class System_TimeSpanDataProvider : SerializationDataProvider<System.TimeSpan>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(System.TimeSpan obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("ticks", obj.Ticks);
		}
		
		public override System.TimeSpan CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new System.TimeSpan(reader.ReadProperty<long>());
		}
		
		public override System.TimeSpan Deserialize(System.TimeSpan obj, IObjectReader reader, SerializationContext context)
		{
			return obj;
		}
		
		#endregion
	}
}