using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_SpringJointDataProvider : SerializationDataProvider<UnityEngine.SpringJoint>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.SpringJoint obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("spring", obj.spring);
			writer.WriteProperty("damper", obj.damper);
			writer.WriteProperty("minDistance", obj.minDistance);
			writer.WriteProperty("maxDistance", obj.maxDistance);
			writer.WriteProperty("tolerance", obj.tolerance);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Joint>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.SpringJoint CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.SpringJoint)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.SpringJoint Deserialize(UnityEngine.SpringJoint obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.spring = reader.ReadProperty<float>("spring");
			obj.damper = reader.ReadProperty<float>("damper");
			obj.minDistance = reader.ReadProperty<float>("minDistance");
			obj.maxDistance = reader.ReadProperty<float>("maxDistance");
			obj.tolerance = reader.ReadProperty<float>("tolerance");
			
			// read parent property values
			return (UnityEngine.SpringJoint)SerializationDataProvider<UnityEngine.Joint>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}