using System;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
    [System.Serializable]
	internal class SceneObject : Metadata<GameObject>
    {
        #region Fields

        [SerializeField]
        private 	List<ComponentMetadata> 	m_componentsMeta;

        #endregion

        #region Constructors

        public SceneObject(GameObject gameObject) 
			: base(gameObject)
        {
            Initialise(gameObject);
        }

        public SceneObject(GameObject gameObject, string guid) 
			: base(gameObject, guid)
        {
            Initialise(gameObject);
        }

        #endregion

        #region Public methods

		public void Add(Component component)
        {
			int metaIndex = FindComponentMetaIndex(component);
			if (-1 == metaIndex)
            {
				m_componentsMeta.Add(new ComponentMetadata(component));
            }
		}

		public void Add(Component component, string guid)
		{
			int metaIndex = FindComponentMetaIndex(component);
			if (-1 == metaIndex)
			{
				m_componentsMeta.Add(new ComponentMetadata(component, guid));
			}
		}

		public string GetComponentGuid(Component component)
		{
			int metaIndex = FindComponentMetaIndex(component);
			if (metaIndex != -1)
			{
				return m_componentsMeta[metaIndex].Guid;
			}

			return null;
		}

		public Component GetComponentWithGuid(string guid)
		{
			int metaIndex = FindComponentMetaIndex(guid);
			if (metaIndex != -1)
			{
				return m_componentsMeta[metaIndex].Object;
			}

			return null;
		}

        public bool Remove(Component component)
        {
			int metaIndex = FindComponentMetaIndex(component);
			if (metaIndex != -1)
			{
				m_componentsMeta.RemoveAt(metaIndex);
				return true;
			}

			return false;
        }

        public void Refresh()
        {
            // remove all deleted components if exists
			m_componentsMeta.RemoveAll(item => (item.Object == null));

			// generate metadata for new components
			Component[] components = Object.GetComponents<Component>();
			for (int i = 0; i < components.Length; i++)
            {
				Add(components[i]);
            }
        }

        #endregion

        #region Private methods

        private void Initialise(GameObject gameObject)
        {
			Component[]	components = gameObject.GetComponents<Component>();
            m_componentsMeta = new List<ComponentMetadata>(components.Length);

            for (int eachIndex = 0; eachIndex < components.Length; eachIndex++)
            {
				Component component = components[eachIndex];
                if ((component is Transform))
                {
					m_componentsMeta.Add(new ComponentMetadata(component, this.Guid));
                }
                else
                {
					m_componentsMeta.Add(new ComponentMetadata(component));
                }
            }
        }

		private int FindComponentMetaIndex(Component component)
		{
			for (int i = 0; i < m_componentsMeta.Count; i++)
			{
				if (m_componentsMeta[i].Object == component)
				{
					return i;
				}
			}

			return -1;
		}

		private int FindComponentMetaIndex(string guid)
		{
			for (int i = 0; i < m_componentsMeta.Count; i++)
			{
				if (m_componentsMeta[i].Guid == guid)
				{
					return i;
				}
			}

			return -1;
		}

        #endregion
    }
}