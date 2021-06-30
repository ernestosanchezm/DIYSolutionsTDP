using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_TargetJoint2DDataProvider : SerializationDataProvider<UnityEngine.TargetJoint2D>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.TargetJoint2D obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("anchor", obj.anchor);
			writer.WriteProperty("target", obj.target);
			writer.WriteProperty("autoConfigureTarget", obj.autoConfigureTarget);
			writer.WriteProperty("maxForce", obj.maxForce);
			writer.WriteProperty("dampingRatio", obj.dampingRatio);
			writer.WriteProperty("frequency", obj.frequency);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Joint2D>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.TargetJoint2D CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.TargetJoint2D)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.TargetJoint2D Deserialize(UnityEngine.TargetJoint2D obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.anchor = reader.ReadProperty<UnityEngine.Vector2>("anchor");
			obj.target = reader.ReadProperty<UnityEngine.Vector2>("target");
			obj.autoConfigureTarget = reader.ReadProperty<bool>("autoConfigureTarget");
			obj.maxForce = reader.ReadProperty<float>("maxForce");
			obj.dampingRatio = reader.ReadProperty<float>("dampingRatio");
			obj.frequency = reader.ReadProperty<float>("frequency");
			
			// read parent property values
			return (UnityEngine.TargetJoint2D)SerializationDataProvider<UnityEngine.Joint2D>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}