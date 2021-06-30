using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Type = System.Type;
using Array = System.Array;

namespace VoxelBusters.Serialization
{
	internal abstract class ResourceObjectIdentifierStoreBase : ScriptableObject
    {
        #region Fields

        [SerializeField]
		protected 		List<ResourceObject> 			m_resourceMetaList 	= new List<ResourceObject>();

        #endregion

		#region Abstract methods

		public abstract ResourceObject Find(Object asset);
		public abstract ResourceObject Find(string guid);

		#if UNITY_EDITOR
		internal abstract void Refresh();
		#endif
			
		#endregion

		#region Internal methods

		// Add asset object to current database
		internal void Add(Object asset, string guid)
		{
			m_resourceMetaList.Add(new ResourceObject(asset, guid));
		}

		// Remove asset object from current database, if exists.
		internal virtual bool Remove(Object asset)
		{
			ResourceObject resourceObject = Find(asset);
			if (resourceObject != null)
			{
				m_resourceMetaList.Remove(resourceObject);
				return true;
			}

			return false;
		}

		#endregion

		#region Private methods

		protected int FindObjectIndex(Object asset)
		{
			for (int iter = 0; iter < m_resourceMetaList.Count; iter++)
			{
				if (m_resourceMetaList[iter].Object == asset)
				{
					return iter;
				}
			}

			return -1;
		}

		protected int FindObjectIndex(string guid)
		{
			for (int iter = 0; iter < m_resourceMetaList.Count; iter++)
			{
				if (m_resourceMetaList[iter].Guid == guid)
				{
					return iter;
				}
			}

			return -1;
		}

		#endregion
    }
}