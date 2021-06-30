using UnityEngine;
using System.Collections;

namespace VoxelBusters.Serialization
{
    [System.Serializable]
    internal class SceneMetadata
    {
		#region Fields

		[SerializeField]
		private			string 		m_name;
        [SerializeField]
		private 		string 		m_guid;
        [SerializeField]
		private 		string 		m_path;
		[SerializeField, HideInInspector]
		private			bool 		m_enabled;

		#endregion

		#region Properties

		public string Name
		{
			get
			{
				return m_name;
			}
		}

		public string Guid
		{
			get
			{
				return m_guid;
			}
		}

		public string Path
		{
			get
			{
				return m_path;
			}
		}

		public bool Enabled
		{
			get
			{
				return m_enabled;
			}
		}

		#endregion

		#region Constructors

		public SceneMetadata(string name, string guid, string path, bool enabled = true)
        {
			// set property values
			m_name			= name;
            m_guid 			= guid;
            m_path 			= path;
			m_enabled		= enabled;
        }

		#endregion
    }
}