using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_BehaviourDataProvider : SerializationDataProvider<UnityEngine.Behaviour>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Behaviour obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("enabled", obj.enabled);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Component>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.Behaviour CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.Behaviour)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.Behaviour Deserialize(UnityEngine.Behaviour obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.enabled = reader.ReadProperty<bool>("enabled");
			
			// read parent property values
			return (UnityEngine.Behaviour)SerializationDataProvider<UnityEngine.Component>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}