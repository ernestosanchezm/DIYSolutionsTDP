using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.External.SystemUtils
{
	public struct NullEnumerator<T> : IEnumerator<T>
	{
		#region IEnumerator implementation

		public bool MoveNext()
		{
			return false;
		}

		public void Reset()
		{}

		object IEnumerator.Current
		{
			get
			{
				return Current;
			}
		}

		#endregion

		#region IDisposable implementation

		public void Dispose()
		{}

		#endregion

		#region IEnumerator implementation

		public T Current
		{
			get
			{
				return default(T);
			}
		}

		#endregion
	}
}