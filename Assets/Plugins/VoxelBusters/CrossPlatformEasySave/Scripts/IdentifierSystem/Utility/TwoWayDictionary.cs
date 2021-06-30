using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Utils
{
	// Make sure A and B are unique - It should be 1-1 mapped dictionary
	internal class TwoWayDictionary<TKey, TValue> : ITwoWayDictionary<TKey, TValue>
	{
		#region Fields

        private 	Dictionary<TKey, TValue> 	m_forward;
        private 	Dictionary<TValue, TKey> 	m_reverse;

		#endregion

		#region Constructors

		public TwoWayDictionary(int capacity)
        {
			// set properties
			m_forward = new Dictionary<TKey, TValue>(capacity);
			m_reverse = new Dictionary<TValue, TKey>(capacity);
        }

		#endregion

		#region ITwoWayDictionary implementation

        public void Add(TKey key, TValue value)
        {
            m_forward[key] = value;
            m_reverse[value] = key;
        }

        public void Remove(TKey key)
        {
            if (m_forward.ContainsKey(key))
            {
                TValue value = m_forward[key];
                m_forward.Remove(key);
                m_reverse.Remove(value);
            }
        }

        public void Remove(TValue value)
        {
            if (m_reverse.ContainsKey(value))
            {
                TKey key = m_reverse[value];
                m_forward.Remove(key);
                m_reverse.Remove(value);
            }
        }

		public bool TryGetValue(TKey key, out TValue value)
		{
			return m_forward.TryGetValue(key, out value);
		}

		public bool TryGetKey(TValue value, out TKey key)
		{
			return m_reverse.TryGetValue(value, out key);
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return m_forward.GetEnumerator();
		}

        public void Clear()
        {
            m_forward.Clear();
            m_reverse.Clear();
        }

		public TValue this[TKey key]
		{
			get
			{
				return m_forward[key];
			}
			set
			{
				m_forward[key]	= value;
			}
		}

		public TKey this[TValue val]
		{
			get
			{
				return m_reverse[val];
			}
			set
			{
				m_reverse[val]	= value;
			}
		}

		public int Count
		{
			get
			{
				return m_forward.Count;
			}
		}

		#endregion
    }
}