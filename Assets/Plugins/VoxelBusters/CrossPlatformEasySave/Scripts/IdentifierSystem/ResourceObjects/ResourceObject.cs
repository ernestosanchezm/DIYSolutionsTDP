using UnityEngine;

namespace VoxelBusters.Serialization
{
    [System.Serializable]
	internal class ResourceObject : Metadata<Object>
    {
		#region Constructors

		public ResourceObject(Object asset)
			: base(asset)
		{}

        public ResourceObject(Object asset, string guid)
			: base(asset, guid)
        {}

		#endregion
    }
}