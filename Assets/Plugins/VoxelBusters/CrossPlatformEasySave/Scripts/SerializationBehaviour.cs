using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityObject = UnityEngine.Object;
using Exception = System.Exception;

namespace VoxelBusters.Serialization
{
	[AddComponentMenu("Serialization/Serialization Behaviour")]
	public class SerializationBehaviour : MonoBehaviour 
	{
		#region Fields

		[SerializeField]
		private			string 					m_key;
		[SerializeField]
		private			UnityObject[] 			m_targetObjects;
		[SerializeField]
		private			bool 					m_hasCustomSettings;
		[SerializeField]
		private			Settings				m_customSettings;

		private			SerializationOption[]	m_customOptions;

		#endregion

		#region Unity methods

		private void Awake()
		{
			// set properties
			SetCustomOptionsInternal();

			// validate component
			if (string.IsNullOrEmpty(m_key))
			{
				Debug.LogError("[Serialization] Key cannot be empty/null.", gameObject);
			}
		}
			
		private void Reset()
		{
			// reset properties
			m_key				= null;
			m_targetObjects 	= new UnityObject[0];
			m_hasCustomSettings	= false;
			m_customSettings	= SerializationSettings.Copy();
		}

		#endregion

		#region Public methods

		public void Serialize()
		{
			try
			{
				SerializationManager.Serialize(m_key, m_targetObjects, m_customOptions);
			}
			catch (Exception exception)
			{
				Debug.LogError("[Serialization] Serialization operation could not be completed. Please read exception message for more information. Exception: " + exception.Message,
				               gameObject);
			}
		}

		public void Deserialize()
		{
			// read saved data and restore it
			try
			{
				m_targetObjects	= SerializationManager.Deserialize<UnityObject[]>("m_targets");
			}
			catch (Exception exception)
			{
				Debug.LogError("[Serialization] Deserialization operation could not be completed. Please read exception message for more information. Exception: " + exception.Message,
				               gameObject);
			}
		}

		#endregion

		#region Private methods

		private void SetCustomOptionsInternal()
		{
			SerializationOption[] options 	= m_hasCustomSettings ? m_customSettings.ToOptions() : null;
			m_customOptions 				= options;
		}

		#endregion
	}
}