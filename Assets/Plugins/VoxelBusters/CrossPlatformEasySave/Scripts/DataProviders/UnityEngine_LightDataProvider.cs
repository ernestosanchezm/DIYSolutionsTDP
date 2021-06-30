using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
    public class UnityEngine_LightDataProvider : SerializationDataProvider<UnityEngine.Light>
    {
        #region SerializationDataProvider abstract members implementation

        public override void Serialize(UnityEngine.Light obj, IObjectWriter writer, SerializationContext context)
        {
            // write declared property values
            writer.WriteProperty("type", obj.type);
            writer.WriteProperty("color", obj.color);
            writer.WriteProperty("colorTemperature", obj.colorTemperature);
            writer.WriteProperty("intensity", obj.intensity);
            writer.WriteProperty("bounceIntensity", obj.bounceIntensity);
            writer.WriteProperty("shadows", obj.shadows);
            writer.WriteProperty("shadowStrength", obj.shadowStrength);
            writer.WriteProperty("shadowResolution", obj.shadowResolution);
            writer.WriteProperty("shadowCustomResolution", obj.shadowCustomResolution);
            writer.WriteProperty("shadowBias", obj.shadowBias);
            writer.WriteProperty("shadowNormalBias", obj.shadowNormalBias);
            writer.WriteProperty("shadowNearPlane", obj.shadowNearPlane);
            writer.WriteProperty("range", obj.range);
            writer.WriteProperty("spotAngle", obj.spotAngle);
            writer.WriteProperty("cookieSize", obj.cookieSize);
            writer.WriteProperty("cookie", obj.cookie);
            writer.WriteProperty("flare", obj.flare);
            writer.WriteProperty("renderMode", obj.renderMode);
#if UNITY_2017_3_OR_NEWER
            writer.WriteProperty("bakingOutput", obj.bakingOutput);
#else
            writer.WriteProperty("alreadyLightmapped", obj.alreadyLightmapped);
#endif

            writer.WriteProperty("cullingMask", obj.cullingMask);
#if UNITY_EDITOR
            writer.WriteProperty("areaSize", obj.areaSize);
            writer.WriteProperty("lightmapBakeType", obj.lightmapBakeType);
#endif

            // write parent property values
            SerializationDataProvider<UnityEngine.Behaviour>.Default.Serialize(obj, writer, context);
        }

        public override UnityEngine.Light CreateInstance(IObjectReader reader, SerializationContext context)
        {
            // special case: Component data provider contains instructions to create instances of its derived types
            return (UnityEngine.Light)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
        }

        public override UnityEngine.Light Deserialize(UnityEngine.Light obj, IObjectReader reader, SerializationContext context)
        {
            // read declared property values
            obj.type = reader.ReadProperty<UnityEngine.LightType>("type");
            obj.color = reader.ReadProperty<UnityEngine.Color>("color");
            obj.colorTemperature = reader.ReadProperty<float>("colorTemperature");
            obj.intensity = reader.ReadProperty<float>("intensity");
            obj.bounceIntensity = reader.ReadProperty<float>("bounceIntensity");
            obj.shadows = reader.ReadProperty<UnityEngine.LightShadows>("shadows");
            obj.shadowStrength = reader.ReadProperty<float>("shadowStrength");
            obj.shadowResolution = reader.ReadProperty<UnityEngine.Rendering.LightShadowResolution>("shadowResolution");
            obj.shadowCustomResolution = reader.ReadProperty<int>("shadowCustomResolution");
            obj.shadowBias = reader.ReadProperty<float>("shadowBias");
            obj.shadowNormalBias = reader.ReadProperty<float>("shadowNormalBias");
            obj.shadowNearPlane = reader.ReadProperty<float>("shadowNearPlane");
            obj.range = reader.ReadProperty<float>("range");
            obj.spotAngle = reader.ReadProperty<float>("spotAngle");
            obj.cookieSize = reader.ReadProperty<float>("cookieSize");
            obj.cookie = reader.ReadProperty<UnityEngine.Texture>("cookie");
            obj.flare = reader.ReadProperty<UnityEngine.Flare>("flare");
            obj.renderMode = reader.ReadProperty<UnityEngine.LightRenderMode>("renderMode");
#if UNITY_2017_3_OR_NEWER
            obj.bakingOutput = reader.ReadProperty<UnityEngine.LightBakingOutput>("bakingOutput");
#else
            obj.alreadyLightmapped = reader.ReadProperty<bool>("alreadyLightmapped");
#endif
            obj.cullingMask = reader.ReadProperty<int>("cullingMask");
#if UNITY_EDITOR
            obj.areaSize = reader.ReadProperty<UnityEngine.Vector2>("areaSize");
            obj.lightmapBakeType = reader.ReadProperty<UnityEngine.LightmapBakeType>("lightmapBakeType");
#endif

            // read parent property values
            return (UnityEngine.Light)SerializationDataProvider<UnityEngine.Behaviour>.Default.Deserialize(obj, reader, context);
        }

        #endregion
    }
}