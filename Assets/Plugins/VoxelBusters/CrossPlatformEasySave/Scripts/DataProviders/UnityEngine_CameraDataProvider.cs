using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_CameraDataProvider : SerializationDataProvider<UnityEngine.Camera>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Camera obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("fieldOfView", obj.fieldOfView);
			writer.WriteProperty("nearClipPlane", obj.nearClipPlane);
			writer.WriteProperty("farClipPlane", obj.farClipPlane);
			writer.WriteProperty("renderingPath", obj.renderingPath);
			writer.WriteProperty("allowHDR", obj.allowHDR);
			writer.WriteProperty("forceIntoRenderTexture", obj.forceIntoRenderTexture);
			writer.WriteProperty("allowMSAA", obj.allowMSAA);
			writer.WriteProperty("orthographicSize", obj.orthographicSize);
			writer.WriteProperty("orthographic", obj.orthographic);
			writer.WriteProperty("opaqueSortMode", obj.opaqueSortMode);
			writer.WriteProperty("transparencySortMode", obj.transparencySortMode);
			writer.WriteProperty("transparencySortAxis", obj.transparencySortAxis);
			writer.WriteProperty("depth", obj.depth);
			writer.WriteProperty("aspect", obj.aspect);
			writer.WriteProperty("cullingMask", obj.cullingMask);
			writer.WriteProperty("eventMask", obj.eventMask);
			writer.WriteProperty("backgroundColor", obj.backgroundColor);
			writer.WriteProperty("targetTexture", obj.targetTexture);
			writer.WriteProperty("clearFlags", obj.clearFlags);
			writer.WriteProperty("stereoSeparation", obj.stereoSeparation);
			writer.WriteProperty("stereoConvergence", obj.stereoConvergence);
			writer.WriteProperty("cameraType", obj.cameraType);
#if !UNITY_2018_1_OR_NEWER
            writer.WriteProperty("stereoMirrorMode", obj.stereoMirrorMode);
#endif
            writer.WriteProperty("stereoTargetEye", obj.stereoTargetEye);
			writer.WriteProperty("targetDisplay", obj.targetDisplay);
			writer.WriteProperty("useOcclusionCulling", obj.useOcclusionCulling);
			writer.WriteProperty("layerCullDistances", obj.layerCullDistances);
			writer.WriteProperty("layerCullSpherical", obj.layerCullSpherical);
			writer.WriteProperty("depthTextureMode", obj.depthTextureMode);
			writer.WriteProperty("clearStencilAfterLightingPass", obj.clearStencilAfterLightingPass);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Behaviour>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.Camera CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.Camera)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.Camera Deserialize(UnityEngine.Camera obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.fieldOfView = reader.ReadProperty<float>("fieldOfView");
			obj.nearClipPlane = reader.ReadProperty<float>("nearClipPlane");
			obj.farClipPlane = reader.ReadProperty<float>("farClipPlane");
			obj.renderingPath = reader.ReadProperty<UnityEngine.RenderingPath>("renderingPath");
			obj.allowHDR = reader.ReadProperty<bool>("allowHDR");
			obj.forceIntoRenderTexture = reader.ReadProperty<bool>("forceIntoRenderTexture");
			obj.allowMSAA = reader.ReadProperty<bool>("allowMSAA");
			obj.orthographicSize = reader.ReadProperty<float>("orthographicSize");
			obj.orthographic = reader.ReadProperty<bool>("orthographic");
			obj.opaqueSortMode = reader.ReadProperty<UnityEngine.Rendering.OpaqueSortMode>("opaqueSortMode");
			obj.transparencySortMode = reader.ReadProperty<UnityEngine.TransparencySortMode>("transparencySortMode");
			obj.transparencySortAxis = reader.ReadProperty<UnityEngine.Vector3>("transparencySortAxis");
			obj.depth = reader.ReadProperty<float>("depth");
			obj.aspect = reader.ReadProperty<float>("aspect");
			obj.cullingMask = reader.ReadProperty<int>("cullingMask");
			obj.eventMask = reader.ReadProperty<int>("eventMask");
			obj.backgroundColor = reader.ReadProperty<UnityEngine.Color>("backgroundColor");
			obj.targetTexture = reader.ReadProperty<UnityEngine.RenderTexture>("targetTexture");
			obj.clearFlags = reader.ReadProperty<UnityEngine.CameraClearFlags>("clearFlags");
			obj.stereoSeparation = reader.ReadProperty<float>("stereoSeparation");
			obj.stereoConvergence = reader.ReadProperty<float>("stereoConvergence");
			obj.cameraType = reader.ReadProperty<UnityEngine.CameraType>("cameraType");
#if !UNITY_2018_1_OR_NEWER
            obj.stereoMirrorMode = reader.ReadProperty<bool>("stereoMirrorMode");
#endif
            obj.stereoTargetEye = reader.ReadProperty<UnityEngine.StereoTargetEyeMask>("stereoTargetEye");
			obj.targetDisplay = reader.ReadProperty<int>("targetDisplay");
			obj.useOcclusionCulling = reader.ReadProperty<bool>("useOcclusionCulling");
			obj.layerCullDistances = reader.ReadProperty<float[]>("layerCullDistances");
			obj.layerCullSpherical = reader.ReadProperty<bool>("layerCullSpherical");
			obj.depthTextureMode = reader.ReadProperty<UnityEngine.DepthTextureMode>("depthTextureMode");
			obj.clearStencilAfterLightingPass = reader.ReadProperty<bool>("clearStencilAfterLightingPass");
			
			// read parent property values
			return (UnityEngine.Camera)SerializationDataProvider<UnityEngine.Behaviour>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}