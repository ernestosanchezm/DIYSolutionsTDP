using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.Utils;

namespace VoxelBusters.Serialization
{
	internal class ResourceObjectCacheSystem : TwoWayDictionary<string, Object>
    {
		#region Constructors

		internal ResourceObjectCacheSystem(int capacity)
			: base(capacity)
		{}

		#endregion
	}
}