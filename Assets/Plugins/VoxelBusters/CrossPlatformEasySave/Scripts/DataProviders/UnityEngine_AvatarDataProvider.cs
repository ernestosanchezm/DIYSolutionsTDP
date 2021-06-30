using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NotImplementedException = System.NotImplementedException;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_AvatarDataProvider : SerializationDataProvider<UnityEngine.Avatar>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Avatar obj, IObjectWriter writer, SerializationContext context)
		{
            throw new NotImplementedException();
        }

        public override UnityEngine.Avatar CreateInstance(IObjectReader reader, SerializationContext context)
		{
            throw new NotImplementedException();
        }

        public override UnityEngine.Avatar Deserialize(UnityEngine.Avatar obj, IObjectReader reader, SerializationContext context)
		{
            throw new NotImplementedException();
		}
		
		#endregion
	}
}