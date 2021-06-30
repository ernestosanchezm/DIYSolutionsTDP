using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_AudioSourceDataProvider : SerializationDataProvider<UnityEngine.AudioSource>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.AudioSource obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("volume", obj.volume);
			writer.WriteProperty("pitch", obj.pitch);
			writer.WriteProperty("time", obj.time);
			writer.WriteProperty("clip", obj.clip);
			writer.WriteProperty("outputAudioMixerGroup", obj.outputAudioMixerGroup);
			writer.WriteProperty("loop", obj.loop);
			writer.WriteProperty("ignoreListenerVolume", obj.ignoreListenerVolume);
			writer.WriteProperty("playOnAwake", obj.playOnAwake);
			writer.WriteProperty("ignoreListenerPause", obj.ignoreListenerPause);
			writer.WriteProperty("velocityUpdateMode", obj.velocityUpdateMode);
			writer.WriteProperty("panStereo", obj.panStereo);
			writer.WriteProperty("spatialBlend", obj.spatialBlend);
			writer.WriteProperty("spatialize", obj.spatialize);
			writer.WriteProperty("spatializePostEffects", obj.spatializePostEffects);
			writer.WriteProperty("reverbZoneMix", obj.reverbZoneMix);
			writer.WriteProperty("bypassEffects", obj.bypassEffects);
			writer.WriteProperty("bypassListenerEffects", obj.bypassListenerEffects);
			writer.WriteProperty("bypassReverbZones", obj.bypassReverbZones);
			writer.WriteProperty("dopplerLevel", obj.dopplerLevel);
			writer.WriteProperty("spread", obj.spread);
			writer.WriteProperty("priority", obj.priority);
			writer.WriteProperty("mute", obj.mute);
			writer.WriteProperty("minDistance", obj.minDistance);
			writer.WriteProperty("maxDistance", obj.maxDistance);
			writer.WriteProperty("rolloffMode", obj.rolloffMode);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Behaviour>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.AudioSource CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.AudioSource)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.AudioSource Deserialize(UnityEngine.AudioSource obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.volume = reader.ReadProperty<float>("volume");
			obj.pitch = reader.ReadProperty<float>("pitch");
			obj.time = reader.ReadProperty<float>("time");
			obj.clip = reader.ReadProperty<UnityEngine.AudioClip>("clip");
			obj.outputAudioMixerGroup = reader.ReadProperty<UnityEngine.Audio.AudioMixerGroup>("outputAudioMixerGroup");
			obj.loop = reader.ReadProperty<bool>("loop");
			obj.ignoreListenerVolume = reader.ReadProperty<bool>("ignoreListenerVolume");
			obj.playOnAwake = reader.ReadProperty<bool>("playOnAwake");
			obj.ignoreListenerPause = reader.ReadProperty<bool>("ignoreListenerPause");
			obj.velocityUpdateMode = reader.ReadProperty<UnityEngine.AudioVelocityUpdateMode>("velocityUpdateMode");
			obj.panStereo = reader.ReadProperty<float>("panStereo");
			obj.spatialBlend = reader.ReadProperty<float>("spatialBlend");
			obj.spatialize = reader.ReadProperty<bool>("spatialize");
			obj.spatializePostEffects = reader.ReadProperty<bool>("spatializePostEffects");
			obj.reverbZoneMix = reader.ReadProperty<float>("reverbZoneMix");
			obj.bypassEffects = reader.ReadProperty<bool>("bypassEffects");
			obj.bypassListenerEffects = reader.ReadProperty<bool>("bypassListenerEffects");
			obj.bypassReverbZones = reader.ReadProperty<bool>("bypassReverbZones");
			obj.dopplerLevel = reader.ReadProperty<float>("dopplerLevel");
			obj.spread = reader.ReadProperty<float>("spread");
			obj.priority = reader.ReadProperty<int>("priority");
			obj.mute = reader.ReadProperty<bool>("mute");
			obj.minDistance = reader.ReadProperty<float>("minDistance");
			obj.maxDistance = reader.ReadProperty<float>("maxDistance");
			obj.rolloffMode = reader.ReadProperty<UnityEngine.AudioRolloffMode>("rolloffMode");
			
			// read parent property values
			return (UnityEngine.AudioSource)SerializationDataProvider<UnityEngine.Behaviour>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}