using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_SkinnedMeshRendererDataProvider : SerializationDataProvider<UnityEngine.SkinnedMeshRenderer>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.SkinnedMeshRenderer obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("bones", obj.bones);
			writer.WriteProperty("rootBone", obj.rootBone);
			writer.WriteProperty("quality", obj.quality);
			writer.WriteProperty("sharedMesh", obj.sharedMesh);
			writer.WriteProperty("updateWhenOffscreen", obj.updateWhenOffscreen);
			writer.WriteProperty("skinnedMotionVectors", obj.skinnedMotionVectors);
			writer.WriteProperty("localBounds", obj.localBounds);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Renderer>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.SkinnedMeshRenderer CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.SkinnedMeshRenderer)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.SkinnedMeshRenderer Deserialize(UnityEngine.SkinnedMeshRenderer obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.bones = reader.ReadProperty<UnityEngine.Transform[]>("bones");
			obj.rootBone = reader.ReadProperty<UnityEngine.Transform>("rootBone");
			obj.quality = reader.ReadProperty<UnityEngine.SkinQuality>("quality");
			obj.sharedMesh = reader.ReadProperty<UnityEngine.Mesh>("sharedMesh");
			obj.updateWhenOffscreen = reader.ReadProperty<bool>("updateWhenOffscreen");
			obj.skinnedMotionVectors = reader.ReadProperty<bool>("skinnedMotionVectors");
			obj.localBounds = reader.ReadProperty<UnityEngine.Bounds>("localBounds");
			
			// read parent property values
			return (UnityEngine.SkinnedMeshRenderer)SerializationDataProvider<UnityEngine.Renderer>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}