using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_MeshRendererDataProvider : SerializationDataProvider<UnityEngine.MeshRenderer>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.MeshRenderer obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("additionalVertexStreams", obj.additionalVertexStreams);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Renderer>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.MeshRenderer CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.MeshRenderer)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.MeshRenderer Deserialize(UnityEngine.MeshRenderer obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.additionalVertexStreams = reader.ReadProperty<UnityEngine.Mesh>("additionalVertexStreams");
			
			// read parent property values
			return (UnityEngine.MeshRenderer)SerializationDataProvider<UnityEngine.Renderer>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}