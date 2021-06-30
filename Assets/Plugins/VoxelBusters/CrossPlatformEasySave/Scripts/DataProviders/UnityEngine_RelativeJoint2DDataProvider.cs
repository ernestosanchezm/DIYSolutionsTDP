using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_RelativeJoint2DDataProvider : SerializationDataProvider<UnityEngine.RelativeJoint2D>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.RelativeJoint2D obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("maxForce", obj.maxForce);
			writer.WriteProperty("maxTorque", obj.maxTorque);
			writer.WriteProperty("correctionScale", obj.correctionScale);
			writer.WriteProperty("autoConfigureOffset", obj.autoConfigureOffset);
			writer.WriteProperty("linearOffset", obj.linearOffset);
			writer.WriteProperty("angularOffset", obj.angularOffset);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Joint2D>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.RelativeJoint2D CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.RelativeJoint2D)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.RelativeJoint2D Deserialize(UnityEngine.RelativeJoint2D obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.maxForce = reader.ReadProperty<float>("maxForce");
			obj.maxTorque = reader.ReadProperty<float>("maxTorque");
			obj.correctionScale = reader.ReadProperty<float>("correctionScale");
			obj.autoConfigureOffset = reader.ReadProperty<bool>("autoConfigureOffset");
			obj.linearOffset = reader.ReadProperty<UnityEngine.Vector2>("linearOffset");
			obj.angularOffset = reader.ReadProperty<float>("angularOffset");
			
			// read parent property values
			return (UnityEngine.RelativeJoint2D)SerializationDataProvider<UnityEngine.Joint2D>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}