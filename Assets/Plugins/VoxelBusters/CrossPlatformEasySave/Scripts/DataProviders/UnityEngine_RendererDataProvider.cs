using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_RendererDataProvider : SerializationDataProvider<UnityEngine.Renderer>
	{
		#region SerializationDataProvider abstract members implementation

		public override void Serialize(UnityEngine.Renderer obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("enabled", obj.enabled);
			writer.WriteProperty("shadowCastingMode", obj.shadowCastingMode);
			writer.WriteProperty("receiveShadows", obj.receiveShadows);
			bool isSharedMaterials = (obj.sharedMaterials != null);
			Material[] materials = isSharedMaterials ? obj.sharedMaterials : obj.materials;
			writer.WriteProperty("isSharedMaterials", isSharedMaterials);
			writer.WriteProperty("materials", materials);

			writer.WriteProperty("lightmapIndex", obj.lightmapIndex);
			writer.WriteProperty("realtimeLightmapIndex", obj.realtimeLightmapIndex);
			writer.WriteProperty("lightmapScaleOffset", obj.lightmapScaleOffset);
			writer.WriteProperty("motionVectorGenerationMode", obj.motionVectorGenerationMode);
			writer.WriteProperty("realtimeLightmapScaleOffset", obj.realtimeLightmapScaleOffset);
			writer.WriteProperty("lightProbeUsage", obj.lightProbeUsage);
			writer.WriteProperty("lightProbeProxyVolumeOverride", obj.lightProbeProxyVolumeOverride);
			writer.WriteProperty("probeAnchor", obj.probeAnchor);
			writer.WriteProperty("reflectionProbeUsage", obj.reflectionProbeUsage);
			writer.WriteProperty("sortingLayerName", obj.sortingLayerName);
			writer.WriteProperty("sortingLayerID", obj.sortingLayerID);
			writer.WriteProperty("sortingOrder", obj.sortingOrder);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Component>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.Renderer CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.Renderer)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.Renderer Deserialize(UnityEngine.Renderer obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.enabled = reader.ReadProperty<bool>("enabled");
			obj.shadowCastingMode = reader.ReadProperty<UnityEngine.Rendering.ShadowCastingMode>("shadowCastingMode");
			obj.receiveShadows = reader.ReadProperty<bool>("receiveShadows");
			bool isSharedMaterials = reader.ReadProperty<bool>("isSharedMaterials");
			Material[] materials = reader.ReadProperty<UnityEngine.Material[]>("materials");
			if (isSharedMaterials)
			{
				obj.sharedMaterials = materials;
			}
			else
			{
				obj.materials = materials;
			}
			obj.lightmapIndex = reader.ReadProperty<int>("lightmapIndex");
			obj.realtimeLightmapIndex = reader.ReadProperty<int>("realtimeLightmapIndex");
			obj.lightmapScaleOffset = reader.ReadProperty<UnityEngine.Vector4>("lightmapScaleOffset");
			obj.motionVectorGenerationMode = reader.ReadProperty<UnityEngine.MotionVectorGenerationMode>("motionVectorGenerationMode");
			obj.realtimeLightmapScaleOffset = reader.ReadProperty<UnityEngine.Vector4>("realtimeLightmapScaleOffset");
			obj.lightProbeUsage = reader.ReadProperty<UnityEngine.Rendering.LightProbeUsage>("lightProbeUsage");
			obj.lightProbeProxyVolumeOverride = reader.ReadProperty<UnityEngine.GameObject>("lightProbeProxyVolumeOverride");
			obj.probeAnchor = reader.ReadProperty<UnityEngine.Transform>("probeAnchor");
			obj.reflectionProbeUsage = reader.ReadProperty<UnityEngine.Rendering.ReflectionProbeUsage>("reflectionProbeUsage");
			obj.sortingLayerName = reader.ReadProperty<string>("sortingLayerName");
			obj.sortingLayerID = reader.ReadProperty<int>("sortingLayerID");
			obj.sortingOrder = reader.ReadProperty<int>("sortingOrder");
			
			// read parent property values
			return (UnityEngine.Renderer)SerializationDataProvider<UnityEngine.Component>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}