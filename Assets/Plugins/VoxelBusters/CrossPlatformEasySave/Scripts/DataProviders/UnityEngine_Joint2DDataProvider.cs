using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_Joint2DDataProvider : SerializationDataProvider<UnityEngine.Joint2D>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Joint2D obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("connectedBody", obj.connectedBody);
			writer.WriteProperty("enableCollision", obj.enableCollision);
			writer.WriteProperty("breakForce", obj.breakForce);
			writer.WriteProperty("breakTorque", obj.breakTorque);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Behaviour>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.Joint2D CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.Joint2D)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.Joint2D Deserialize(UnityEngine.Joint2D obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.connectedBody = reader.ReadProperty<UnityEngine.Rigidbody2D>("connectedBody");
			obj.enableCollision = reader.ReadProperty<bool>("enableCollision");
			obj.breakForce = reader.ReadProperty<float>("breakForce");
			obj.breakTorque = reader.ReadProperty<float>("breakTorque");
			
			// read parent property values
			return (UnityEngine.Joint2D)SerializationDataProvider<UnityEngine.Behaviour>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}