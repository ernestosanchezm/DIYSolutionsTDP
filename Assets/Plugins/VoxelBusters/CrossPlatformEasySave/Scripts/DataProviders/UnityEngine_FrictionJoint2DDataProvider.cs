using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_FrictionJoint2DDataProvider : SerializationDataProvider<UnityEngine.FrictionJoint2D>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.FrictionJoint2D obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("maxForce", obj.maxForce);
			writer.WriteProperty("maxTorque", obj.maxTorque);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.AnchoredJoint2D>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.FrictionJoint2D CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.FrictionJoint2D)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.FrictionJoint2D Deserialize(UnityEngine.FrictionJoint2D obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.maxForce = reader.ReadProperty<float>("maxForce");
			obj.maxTorque = reader.ReadProperty<float>("maxTorque");
			
			// read parent property values
			return (UnityEngine.FrictionJoint2D)SerializationDataProvider<UnityEngine.AnchoredJoint2D>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}