using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_Texture2DDataProvider : SerializationDataProvider<UnityEngine.Texture2D>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Texture2D obj, IObjectWriter writer, SerializationContext context)
		{
            // write declared property values
			writer.WriteProperty<TextureFormat>("format", obj.format);
			writer.WriteProperty("mipMapCount", obj.mipmapCount);
			writer.WriteProperty("width", obj.width);
            writer.WriteProperty("height", obj.height);
            writer.WriteProperty("filterMode", obj.filterMode);
            writer.WriteProperty("anisoLevel", obj.anisoLevel);
            writer.WriteProperty("wrapMode", obj.wrapMode);
            writer.WriteProperty("mipMapBias", obj.mipMapBias);
			writer.WriteProperty<byte[]>("rawTextureData", obj.GetRawTextureData());
		}
		
		public override UnityEngine.Texture2D CreateInstance(IObjectReader reader, SerializationContext context)
		{
			TextureFormat	format	= reader.ReadProperty<TextureFormat>("format");
			bool			mipmap	= reader.ReadProperty<int>("mipMapCount") > 0;
			return new UnityEngine.Texture2D(2, 2, format, mipmap, true);
		}
		
		public override UnityEngine.Texture2D Deserialize(UnityEngine.Texture2D obj, IObjectReader reader, SerializationContext context)
		{
            // read declared property values
            obj.width = reader.ReadProperty<int>("width");
            obj.height = reader.ReadProperty<int>("height");
            obj.filterMode = reader.ReadProperty<UnityEngine.FilterMode>("filterMode");
            obj.anisoLevel = reader.ReadProperty<int>("anisoLevel");
            obj.wrapMode = reader.ReadProperty<UnityEngine.TextureWrapMode>("wrapMode");
            obj.mipMapBias = reader.ReadProperty<float>("mipMapBias");

			// read declared property values
			obj.LoadRawTextureData(reader.ReadProperty<byte[]>("rawTextureData"));
            obj.Apply();

			return obj;
		}
		
		#endregion
	}
}