using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_ScriptableObjectDataProvider : SerializationDataProvider<UnityEngine.ScriptableObject>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.ScriptableObject obj, IObjectWriter writer, SerializationContext context)
		{
			// write parent property values
			SerializationDataProvider<UnityEngine.Object>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.ScriptableObject CreateInstance(IObjectReader reader, SerializationContext context)
		{
            return ScriptableObject.CreateInstance(reader.GetObjectType());
		}
		
		public override UnityEngine.ScriptableObject Deserialize(UnityEngine.ScriptableObject obj, IObjectReader reader, SerializationContext context)
		{
			// read parent property values
			return (UnityEngine.ScriptableObject)SerializationDataProvider<UnityEngine.Object>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}