using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_AnchoredJoint2DDataProvider : SerializationDataProvider<UnityEngine.AnchoredJoint2D>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.AnchoredJoint2D obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("anchor", obj.anchor);
			writer.WriteProperty("connectedAnchor", obj.connectedAnchor);
			writer.WriteProperty("autoConfigureConnectedAnchor", obj.autoConfigureConnectedAnchor);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Joint2D>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.AnchoredJoint2D CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.AnchoredJoint2D)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.AnchoredJoint2D Deserialize(UnityEngine.AnchoredJoint2D obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.anchor = reader.ReadProperty<UnityEngine.Vector2>("anchor");
			obj.connectedAnchor = reader.ReadProperty<UnityEngine.Vector2>("connectedAnchor");
			obj.autoConfigureConnectedAnchor = reader.ReadProperty<bool>("autoConfigureConnectedAnchor");
			
			// read parent property values
			return (UnityEngine.AnchoredJoint2D)SerializationDataProvider<UnityEngine.Joint2D>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}