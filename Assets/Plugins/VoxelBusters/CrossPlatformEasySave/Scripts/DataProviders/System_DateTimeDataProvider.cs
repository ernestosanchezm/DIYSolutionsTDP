using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class System_DateTimeDataProvider : SerializationDataProvider<System.DateTime> 
	{
		#region SerializationDataProvider abstract members implementation

		public override void Serialize(System.DateTime obj, IObjectWriter writer, SerializationContext context)
		{
			// write value as string
			writer.WriteProperty(obj.ToBinary());
		}

		public override System.DateTime CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return System.DateTime.FromBinary(reader.ReadProperty<long>());
		}

		public override System.DateTime Deserialize(System.DateTime obj, IObjectReader reader, SerializationContext context)
		{
			return obj;
		}

		#endregion
	}
}