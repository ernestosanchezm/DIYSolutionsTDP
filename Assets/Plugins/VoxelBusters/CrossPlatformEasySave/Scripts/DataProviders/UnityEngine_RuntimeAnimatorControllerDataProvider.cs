using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NotImplementedException = System.NotImplementedException;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_RuntimeAnimatorControllerDataProvider : SerializationDataProvider<UnityEngine.RuntimeAnimatorController>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.RuntimeAnimatorController obj, IObjectWriter writer, SerializationContext context)
		{
            throw new NotImplementedException();
        }

        public override UnityEngine.RuntimeAnimatorController CreateInstance(IObjectReader reader, SerializationContext context)
		{
            throw new NotImplementedException();
		}
		
		public override UnityEngine.RuntimeAnimatorController Deserialize(UnityEngine.RuntimeAnimatorController obj, IObjectReader reader, SerializationContext context)
		{
            throw new NotImplementedException();
		}
		
		#endregion
	}
}