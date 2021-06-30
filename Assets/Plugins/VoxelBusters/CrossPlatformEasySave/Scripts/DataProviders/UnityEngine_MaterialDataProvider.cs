using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_MaterialDataProvider : SerializationDataProvider<UnityEngine.Material>
	{
		#region SerializationDataProvider abstract members implementation

		public override void Serialize(UnityEngine.Material obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("shader", obj.shader);
			writer.WriteProperty("color", obj.color);
			writer.WriteProperty("mainTexture", obj.mainTexture);
			writer.WriteProperty("mainTextureOffset", obj.mainTextureOffset);
			writer.WriteProperty("mainTextureScale", obj.mainTextureScale);
			writer.WriteProperty("renderQueue", obj.renderQueue);
			writer.WriteProperty("shaderKeywords", obj.shaderKeywords);
			writer.WriteProperty("globalIlluminationFlags", obj.globalIlluminationFlags);
			writer.WriteProperty("enableInstancing", obj.enableInstancing);
			writer.WriteProperty("doubleSidedGI", obj.doubleSidedGI);

			// write parent property values
			SerializationDataProvider<UnityEngine.Object>.Default.Serialize(obj, writer, context);
		}

		public override UnityEngine.Material CreateInstance(IObjectReader reader, SerializationContext context)
		{
			Shader shader = reader.ReadProperty<UnityEngine.Shader>("shader");
            return new UnityEngine.Material(shader);
		}

		public override UnityEngine.Material Deserialize(UnityEngine.Material obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.color = reader.ReadProperty<UnityEngine.Color>("color");
			obj.mainTexture = reader.ReadProperty<UnityEngine.Texture>("mainTexture");
			obj.mainTextureOffset = reader.ReadProperty<UnityEngine.Vector2>("mainTextureOffset");
			obj.mainTextureScale = reader.ReadProperty<UnityEngine.Vector2>("mainTextureScale");
			obj.renderQueue = reader.ReadProperty<int>("renderQueue");
			obj.shaderKeywords = reader.ReadProperty<string[]>("shaderKeywords");
			obj.globalIlluminationFlags = reader.ReadProperty<UnityEngine.MaterialGlobalIlluminationFlags>("globalIlluminationFlags");
			obj.enableInstancing = reader.ReadProperty<bool>("enableInstancing");
			obj.doubleSidedGI = reader.ReadProperty<bool>("doubleSidedGI");

			// read parent property values
			return (UnityEngine.Material)SerializationDataProvider<UnityEngine.Object>.Default.Deserialize(obj, reader, context);
		}

		#endregion
	}
}
