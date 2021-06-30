using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_FixedJoint2DDataProvider : SerializationDataProvider<UnityEngine.FixedJoint2D>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.FixedJoint2D obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("dampingRatio", obj.dampingRatio);
			writer.WriteProperty("frequency", obj.frequency);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.AnchoredJoint2D>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.FixedJoint2D CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.FixedJoint2D)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.FixedJoint2D Deserialize(UnityEngine.FixedJoint2D obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.dampingRatio = reader.ReadProperty<float>("dampingRatio");
			obj.frequency = reader.ReadProperty<float>("frequency");
			
			// read parent property values
			return (UnityEngine.FixedJoint2D)SerializationDataProvider<UnityEngine.AnchoredJoint2D>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}