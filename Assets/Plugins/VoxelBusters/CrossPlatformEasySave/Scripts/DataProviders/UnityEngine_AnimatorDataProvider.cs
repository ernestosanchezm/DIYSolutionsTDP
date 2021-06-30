using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_AnimatorDataProvider : SerializationDataProvider<UnityEngine.Animator>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Animator obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("applyRootMotion", obj.applyRootMotion);
#if !UNITY_2017_3_OR_NEWER
            writer.WriteProperty("linearVelocityBlending", obj.linearVelocityBlending);
#endif
            writer.WriteProperty("updateMode", obj.updateMode);
			writer.WriteProperty("stabilizeFeet", obj.stabilizeFeet);
			writer.WriteProperty("feetPivotActive", obj.feetPivotActive);
			writer.WriteProperty("speed", obj.speed);
			writer.WriteProperty("cullingMode", obj.cullingMode);
			writer.WriteProperty("runtimeAnimatorController", obj.runtimeAnimatorController);
			writer.WriteProperty("avatar", obj.avatar);
			writer.WriteProperty("layersAffectMassCenter", obj.layersAffectMassCenter);
			writer.WriteProperty("logWarnings", obj.logWarnings);
			writer.WriteProperty("fireEvents", obj.fireEvents);

            AnimatorStateInfo info = obj.GetCurrentAnimatorStateInfo(0);

            writer.WriteProperty("normalizedTime", info.normalizedTime);
            writer.WriteProperty("fullPathHash", info.fullPathHash);

            // write parent property values
            SerializationDataProvider<UnityEngine.Behaviour>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.Animator CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.Animator)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.Animator Deserialize(UnityEngine.Animator obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.applyRootMotion = reader.ReadProperty<bool>("applyRootMotion");
#if !UNITY_2017_3_OR_NEWER
            obj.linearVelocityBlending = reader.ReadProperty<bool>("linearVelocityBlending");
#endif
            obj.updateMode = reader.ReadProperty<UnityEngine.AnimatorUpdateMode>("updateMode");
			obj.stabilizeFeet = reader.ReadProperty<bool>("stabilizeFeet");
			obj.feetPivotActive = reader.ReadProperty<float>("feetPivotActive");
			obj.speed = reader.ReadProperty<float>("speed");
			obj.cullingMode = reader.ReadProperty<UnityEngine.AnimatorCullingMode>("cullingMode");
			obj.runtimeAnimatorController = reader.ReadProperty<UnityEngine.RuntimeAnimatorController>("runtimeAnimatorController");
			obj.avatar = reader.ReadProperty<UnityEngine.Avatar>("avatar");
			obj.layersAffectMassCenter = reader.ReadProperty<bool>("layersAffectMassCenter");
			obj.logWarnings = reader.ReadProperty<bool>("logWarnings");
			obj.fireEvents = reader.ReadProperty<bool>("fireEvents");

            float normalizedTime = reader.ReadProperty<float>("normalizedTime");
            int fullHashPath = reader.ReadProperty<int>("fullPathHash");

            // set the animation normalised time
            obj.Play(fullHashPath, 0, normalizedTime);

            // read parent property values
            return (UnityEngine.Animator)SerializationDataProvider<UnityEngine.Behaviour>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}