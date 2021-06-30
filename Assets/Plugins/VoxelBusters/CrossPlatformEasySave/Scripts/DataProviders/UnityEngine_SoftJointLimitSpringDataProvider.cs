using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_SoftJointLimitSpringDataProvider : SerializationDataProvider<UnityEngine.SoftJointLimitSpring>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.SoftJointLimitSpring obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("spring", obj.spring);
			writer.WriteProperty("damper", obj.damper);
		}
		
		public override UnityEngine.SoftJointLimitSpring CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new UnityEngine.SoftJointLimitSpring();
		}
		
		public override UnityEngine.SoftJointLimitSpring Deserialize(UnityEngine.SoftJointLimitSpring obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.spring = reader.ReadProperty<float>();
			obj.damper = reader.ReadProperty<float>();
			
			return obj;
		}
		
		#endregion
	}
}