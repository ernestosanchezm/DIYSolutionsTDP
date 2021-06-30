using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_MeshColliderDataProvider : SerializationDataProvider<UnityEngine.MeshCollider>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.MeshCollider obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("sharedMesh", obj.sharedMesh);
			writer.WriteProperty("convex", obj.convex);
#if !UNITY_2018_3_OR_NEWER
            writer.WriteProperty("inflateMesh", obj.inflateMesh);
			writer.WriteProperty("skinWidth", obj.skinWidth);
#endif

            // write parent property values
            SerializationDataProvider<UnityEngine.Collider>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.MeshCollider CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.MeshCollider)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.MeshCollider Deserialize(UnityEngine.MeshCollider obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.sharedMesh = reader.ReadProperty<UnityEngine.Mesh>("sharedMesh");
			obj.convex = reader.ReadProperty<bool>("convex");
#if !UNITY_2018_3_OR_NEWER
            obj.inflateMesh = reader.ReadProperty<bool>("inflateMesh");
			obj.skinWidth = reader.ReadProperty<float>("skinWidth");
#endif

            // read parent property values
            return (UnityEngine.MeshCollider)SerializationDataProvider<UnityEngine.Collider>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}