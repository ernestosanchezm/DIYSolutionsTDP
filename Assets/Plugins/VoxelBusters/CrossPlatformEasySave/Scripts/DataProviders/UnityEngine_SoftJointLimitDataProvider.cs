using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_SoftJointLimitDataProvider : SerializationDataProvider<UnityEngine.SoftJointLimit>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.SoftJointLimit obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("limit", obj.limit);
			writer.WriteProperty("bounciness", obj.bounciness);
			writer.WriteProperty("contactDistance", obj.contactDistance);
		}
		
		public override UnityEngine.SoftJointLimit CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new UnityEngine.SoftJointLimit();
		}
		
		public override UnityEngine.SoftJointLimit Deserialize(UnityEngine.SoftJointLimit obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.limit = reader.ReadProperty<float>();
			obj.bounciness = reader.ReadProperty<float>();
			obj.contactDistance = reader.ReadProperty<float>();
			
			return obj;
		}
		
		#endregion
	}
}