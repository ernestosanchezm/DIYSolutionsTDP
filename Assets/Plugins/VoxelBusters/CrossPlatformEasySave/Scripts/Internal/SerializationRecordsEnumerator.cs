using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	internal class SerializationRecordsEnumerator : IEnumerator<SerializationRecord> 
	{
		#region Fields

		private		IDictionaryEnumerator 	m_dictionaryEnumerator;
		private		SerializationRecord		m_current;

		#endregion

		#region Constructors

		public SerializationRecordsEnumerator(IDictionaryEnumerator enumerator)
		{
			// assign values
			m_dictionaryEnumerator	= enumerator;
			m_current				= null;
		}

		#endregion

		#region IEnumerator implementation

		public bool MoveNext()
		{
			if (m_dictionaryEnumerator.MoveNext())
			{
				m_current = (SerializationRecord)m_dictionaryEnumerator.Value;
				return true;
			}

			m_current = null;
			return false;
		}

		public void Reset()
		{
			m_dictionaryEnumerator.Reset();
		}

		object IEnumerator.Current
		{
			get
			{
				return Current;
			}
		}

		#endregion

		#region IEnumerator implementation

		public SerializationRecord Current
		{
			get
			{
				return m_current;
			}
		}

		#endregion

		#region IDisposable implementation

		public void Dispose()
		{
			m_dictionaryEnumerator	= null;
			m_current 				= null;
		}

		#endregion
	}
}