using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VoxelBusters.Serialization;

namespace VoxelBusters.Serialization.Examples
{
	public class SampleSerializationExample : MonoBehaviour 
	{
		#region Fields

		private		Sample 		m_sampleObject;
		public		Text 		m_text;

		#endregion

		#region Unity methods

		private void Awake()
		{
			UpdateText();
		}

		#endregion

		#region Private methods

		private void UpdateText()
		{
			if (m_sampleObject == null)
			{
				m_text.text	= "Sample is NULL";
			}
			else
			{
				m_text.text	= m_sampleObject.ToString();
			}
		}

		#endregion

		#region UI callback methods

		public void CreateInstance()
		{
			m_sampleObject	= new Sample();
			m_sampleObject.SetRandomValues();

			UpdateText();
		}

		public void Serialize()
		{
			SerializationManager.Serialize(key: "Sample", value: m_sampleObject);
		}

		public void Deserialize()
		{
			m_sampleObject	= SerializationManager.Deserialize<Sample>(key: "Sample");
			UpdateText();
		}

		#endregion
	}
}