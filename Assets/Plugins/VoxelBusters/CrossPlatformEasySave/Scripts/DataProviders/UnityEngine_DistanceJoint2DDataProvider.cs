using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_DistanceJoint2DDataProvider : SerializationDataProvider<UnityEngine.DistanceJoint2D>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.DistanceJoint2D obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("autoConfigureDistance", obj.autoConfigureDistance);
			writer.WriteProperty("distance", obj.distance);
			writer.WriteProperty("maxDistanceOnly", obj.maxDistanceOnly);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.AnchoredJoint2D>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.DistanceJoint2D CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.DistanceJoint2D)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.DistanceJoint2D Deserialize(UnityEngine.DistanceJoint2D obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.autoConfigureDistance = reader.ReadProperty<bool>("autoConfigureDistance");
			obj.distance = reader.ReadProperty<float>("distance");
			obj.maxDistanceOnly = reader.ReadProperty<bool>("maxDistanceOnly");
			
			// read parent property values
			return (UnityEngine.DistanceJoint2D)SerializationDataProvider<UnityEngine.AnchoredJoint2D>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}