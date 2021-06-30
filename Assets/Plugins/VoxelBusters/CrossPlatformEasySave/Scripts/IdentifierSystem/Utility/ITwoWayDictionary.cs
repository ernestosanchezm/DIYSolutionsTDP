using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Utils
{
	internal interface ITwoWayDictionary<TKey, TValue>
	{
		#region Properties

		TValue this[TKey key]
		{
			get;
			set;
		}

		TKey this[TValue val]
		{
			get;
			set;
		}

		int Count
		{
			get;
		}

		#endregion

		#region Methods

		void Add(TKey key, TValue value);
		void Remove(TKey key);
		void Remove(TValue value);

		bool TryGetValue(TKey key, out TValue value);
		bool TryGetKey(TValue value, out TKey key);

		IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator();

		void Clear();

		#endregion
	}
}