using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_AudioClipDataProvider : SerializationDataProvider<UnityEngine.AudioClip>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.AudioClip obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("samples", obj.samples);
			writer.WriteProperty("channels", obj.channels);
			writer.WriteProperty("frequency", obj.frequency);

			float[] data = new float[obj.samples * obj.channels];
			obj.GetData(data, 0);
			writer.WriteProperty("data", data);

			// write parent property values
			SerializationDataProvider<UnityEngine.Object>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.AudioClip CreateInstance(IObjectReader reader, SerializationContext context)
		{
			UnityEngine.AudioClip audioClip = UnityEngine.AudioClip.Create(name: string.Empty,
			                                                               lengthSamples: reader.ReadProperty<int>(),
			                                                               channels: reader.ReadProperty<int>(),
			                                                               frequency: reader.ReadProperty<int>(),
			                                                               stream: false);
			audioClip.SetData(reader.ReadProperty<float[]>(), 0);

			return audioClip;
		}
		
		public override UnityEngine.AudioClip Deserialize(UnityEngine.AudioClip obj, IObjectReader reader, SerializationContext context)
		{
			// read parent property values
			return (UnityEngine.AudioClip)SerializationDataProvider<UnityEngine.Object>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}