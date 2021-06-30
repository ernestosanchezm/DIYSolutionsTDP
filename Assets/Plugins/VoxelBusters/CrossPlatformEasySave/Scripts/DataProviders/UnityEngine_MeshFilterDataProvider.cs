using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_MeshFilterDataProvider : SerializationDataProvider<UnityEngine.MeshFilter>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.MeshFilter obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			bool isSharedMesh = (obj.sharedMesh != null);
			Mesh mesh = isSharedMesh ? obj.sharedMesh : obj.mesh;
			writer.WriteProperty("isSharedMesh", isSharedMesh);
			writer.WriteProperty("mesh", mesh);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Component>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.MeshFilter CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.MeshFilter)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.MeshFilter Deserialize(UnityEngine.MeshFilter obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			bool isSharedMesh = reader.ReadProperty<bool>("isSharedMesh");
			Mesh mesh = reader.ReadProperty<UnityEngine.Mesh>("mesh");
			if (isSharedMesh)
			{
				obj.sharedMesh = mesh;
			}
			else
			{
				obj.mesh = mesh;
			}
			
			// read parent property values
			return (UnityEngine.MeshFilter)SerializationDataProvider<UnityEngine.Component>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}