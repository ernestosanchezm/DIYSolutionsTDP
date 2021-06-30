using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	internal class SubAssetLibrary : ResourceObjectIdentifierStoreBase 
	{
		#region Base class implementation

		public override ResourceObject Find(Object asset)
		{
			int index = FindObjectIndex(asset);
			if (index != -1)
			{
				return m_resourceMetaList[index];
			}

			return null;
		}

		public override ResourceObject Find(string guid)
		{
			int index = FindObjectIndex(guid);
			if (index != -1)
			{
				return m_resourceMetaList[index];
			}

			return null;
		}

		#endregion

		#region Editor methods

		#if UNITY_EDITOR
		// Sanitize the whole database
		internal override void Refresh()
		{
			#if SERIALIZATION_DEBUG
			Debug.Log("[Serialization] Updating subassets library.", this);
			#endif
			m_resourceMetaList.RemoveAll((item) => (item.Object == null));
		}
		#endif

		#endregion
	}
}