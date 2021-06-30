using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_SpringJoint2DDataProvider : SerializationDataProvider<UnityEngine.SpringJoint2D>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.SpringJoint2D obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("autoConfigureDistance", obj.autoConfigureDistance);
			writer.WriteProperty("distance", obj.distance);
			writer.WriteProperty("dampingRatio", obj.dampingRatio);
			writer.WriteProperty("frequency", obj.frequency);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.AnchoredJoint2D>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.SpringJoint2D CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.SpringJoint2D)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.SpringJoint2D Deserialize(UnityEngine.SpringJoint2D obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.autoConfigureDistance = reader.ReadProperty<bool>("autoConfigureDistance");
			obj.distance = reader.ReadProperty<float>("distance");
			obj.dampingRatio = reader.ReadProperty<float>("dampingRatio");
			obj.frequency = reader.ReadProperty<float>("frequency");
			
			// read parent property values
			return (UnityEngine.SpringJoint2D)SerializationDataProvider<UnityEngine.AnchoredJoint2D>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}