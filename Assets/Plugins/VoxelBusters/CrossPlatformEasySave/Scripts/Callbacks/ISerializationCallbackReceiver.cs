using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public interface ISerializationCallbackReceiver 
	{
		#region Methods

		void OnSerialize();
		void OnDeserialize();

		#endregion
	}
}