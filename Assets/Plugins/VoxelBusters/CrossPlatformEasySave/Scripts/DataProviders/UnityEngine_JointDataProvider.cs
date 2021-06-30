using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_JointDataProvider : SerializationDataProvider<UnityEngine.Joint>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Joint obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("connectedBody", obj.connectedBody);
			writer.WriteProperty("axis", obj.axis);
			writer.WriteProperty("anchor", obj.anchor);
			writer.WriteProperty("connectedAnchor", obj.connectedAnchor);
			writer.WriteProperty("autoConfigureConnectedAnchor", obj.autoConfigureConnectedAnchor);
			writer.WriteProperty("breakForce", obj.breakForce);
			writer.WriteProperty("breakTorque", obj.breakTorque);
			writer.WriteProperty("enableCollision", obj.enableCollision);
			writer.WriteProperty("enablePreprocessing", obj.enablePreprocessing);
			writer.WriteProperty("massScale", obj.massScale);
			writer.WriteProperty("connectedMassScale", obj.connectedMassScale);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Component>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.Joint CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.Joint)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.Joint Deserialize(UnityEngine.Joint obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.connectedBody = reader.ReadProperty<UnityEngine.Rigidbody>("connectedBody");
			obj.axis = reader.ReadProperty<UnityEngine.Vector3>("axis");
			obj.anchor = reader.ReadProperty<UnityEngine.Vector3>("anchor");
			obj.connectedAnchor = reader.ReadProperty<UnityEngine.Vector3>("connectedAnchor");
			obj.autoConfigureConnectedAnchor = reader.ReadProperty<bool>("autoConfigureConnectedAnchor");
			obj.breakForce = reader.ReadProperty<float>("breakForce");
			obj.breakTorque = reader.ReadProperty<float>("breakTorque");
			obj.enableCollision = reader.ReadProperty<bool>("enableCollision");
			obj.enablePreprocessing = reader.ReadProperty<bool>("enablePreprocessing");
			obj.massScale = reader.ReadProperty<float>("massScale");
			obj.connectedMassScale = reader.ReadProperty<float>("connectedMassScale");
			
			// read parent property values
			return (UnityEngine.Joint)SerializationDataProvider<UnityEngine.Component>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}