using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_MeshDataProvider : SerializationDataProvider<UnityEngine.Mesh>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Mesh obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("boneWeights", obj.boneWeights);
			writer.WriteProperty("bindposes", obj.bindposes);
			writer.WriteProperty("vertices", obj.vertices);
			writer.WriteProperty("normals", obj.normals);
			writer.WriteProperty("tangents", obj.tangents);
			writer.WriteProperty("uv", obj.uv);
			writer.WriteProperty("uv2", obj.uv2);
			writer.WriteProperty("uv3", obj.uv3);
			writer.WriteProperty("uv4", obj.uv4);
			writer.WriteProperty("colors", obj.colors);
			writer.WriteProperty("triangles", obj.triangles);

			int				subMeshCount			= obj.subMeshCount;
			int[][]			subMeshIndices			= new int[subMeshCount][];
			MeshTopology[]	subMeshTopologyArray	= new MeshTopology[subMeshCount];
			for (int iter = 0; iter < subMeshCount; iter++)
			{
				subMeshIndices[iter]				= obj.GetIndices(iter);
				subMeshTopologyArray[iter]			= obj.GetTopology(iter);
			}
			writer.WriteProperty("subMeshCount", subMeshCount);
			writer.WriteProperty("subMeshIndices", subMeshIndices);
			writer.WriteProperty("subMeshTopology", subMeshTopologyArray);

			// write parent property values
			SerializationDataProvider<UnityEngine.Object>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.Mesh CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new UnityEngine.Mesh();
		}
		
		public override UnityEngine.Mesh Deserialize(UnityEngine.Mesh obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.boneWeights = reader.ReadProperty<UnityEngine.BoneWeight[]>("boneWeights");
			obj.bindposes = reader.ReadProperty<UnityEngine.Matrix4x4[]>("bindposes");
			obj.vertices = reader.ReadProperty<UnityEngine.Vector3[]>("vertices");
			obj.normals = reader.ReadProperty<UnityEngine.Vector3[]>("normals");
			obj.tangents = reader.ReadProperty<UnityEngine.Vector4[]>("tangents");
			obj.uv = reader.ReadProperty<UnityEngine.Vector2[]>("uv");
			obj.uv2 = reader.ReadProperty<UnityEngine.Vector2[]>("uv2");
			obj.uv3 = reader.ReadProperty<UnityEngine.Vector2[]>("uv3");
			obj.uv4 = reader.ReadProperty<UnityEngine.Vector2[]>("uv4");
			obj.colors = reader.ReadProperty<UnityEngine.Color[]>("colors");
			obj.triangles = reader.ReadProperty<int[]>("triangles");
			obj.subMeshCount	= reader.ReadProperty<int>("subMeshCount");

			int[][]			subMeshIndices			= reader.ReadProperty<int[][]>("subMeshIndices");
			MeshTopology[]	subMeshTopologyArray	= reader.ReadProperty<MeshTopology[]>("subMeshTopology");
			for (int iter = 0; iter < obj.subMeshCount; iter++)
			{
				obj.SetIndices(subMeshIndices[iter], subMeshTopologyArray[iter], iter);
			}
			
			// read parent property values
			return (UnityEngine.Mesh)SerializationDataProvider<UnityEngine.Object>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}