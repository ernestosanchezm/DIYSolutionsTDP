using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_JointDriveDataProvider : SerializationDataProvider<UnityEngine.JointDrive>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.JointDrive obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("positionSpring", obj.positionSpring);
			writer.WriteProperty("positionDamper", obj.positionDamper);
			writer.WriteProperty("maximumForce", obj.maximumForce);
		}
		
		public override UnityEngine.JointDrive CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new UnityEngine.JointDrive();
		}
		
		public override UnityEngine.JointDrive Deserialize(UnityEngine.JointDrive obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.positionSpring = reader.ReadProperty<float>();
			obj.positionDamper = reader.ReadProperty<float>();
			obj.maximumForce = reader.ReadProperty<float>();
			
			return obj;
		}
		
		#endregion
	}
}