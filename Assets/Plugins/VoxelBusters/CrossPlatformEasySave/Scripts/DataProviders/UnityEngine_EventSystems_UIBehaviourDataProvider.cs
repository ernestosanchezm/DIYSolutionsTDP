using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_EventSystems_UIBehaviourDataProvider : SerializationDataProvider<UnityEngine.EventSystems.UIBehaviour>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.EventSystems.UIBehaviour obj, IObjectWriter writer, SerializationContext context)
		{
			// write parent property values
			SerializationDataProvider<UnityEngine.MonoBehaviour>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.EventSystems.UIBehaviour CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.EventSystems.UIBehaviour)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.EventSystems.UIBehaviour Deserialize(UnityEngine.EventSystems.UIBehaviour obj, IObjectReader reader, SerializationContext context)
		{
			// read parent property values
			return (UnityEngine.EventSystems.UIBehaviour)SerializationDataProvider<UnityEngine.MonoBehaviour>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}