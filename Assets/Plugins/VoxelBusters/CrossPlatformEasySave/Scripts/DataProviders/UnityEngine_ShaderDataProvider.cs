using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_ShaderDataProvider : SerializationDataProvider<UnityEngine.Shader>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Shader obj, IObjectWriter writer, SerializationContext context)
		{
            // write declared property values
            writer.WriteProperty("shaderName", obj.name);
            writer.WriteProperty("maximumLOD", obj.maximumLOD);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Object>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.Shader CreateInstance(IObjectReader reader, SerializationContext context)
		{
            string  shaderName  = reader.ReadProperty<string>("shaderName");
            return Shader.Find(shaderName);
        }
		
		public override UnityEngine.Shader Deserialize(UnityEngine.Shader obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.maximumLOD = reader.ReadProperty<int>("maximumLOD");
			
			// read parent property values
			return (UnityEngine.Shader)SerializationDataProvider<UnityEngine.Object>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}