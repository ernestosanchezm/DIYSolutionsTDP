using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class System_GuidDataProvider : SerializationDataProvider<System.Guid>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(System.Guid obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty<byte[]>(obj.ToByteArray());
		}
		
		public override System.Guid CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new System.Guid(reader.ReadProperty<byte[]>());
		}
		
		public override System.Guid Deserialize(System.Guid obj, IObjectReader reader, SerializationContext context)
		{
			return obj;
		}
		
		#endregion
	}
}