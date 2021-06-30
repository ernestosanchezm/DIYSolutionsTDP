using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Array = System.Array;
using Exception = System.Exception;
using IndexOutOfRangeException = System.IndexOutOfRangeException;

namespace VoxelBusters.Serialization
{
	public class ResizableArray<T>
	{
		#region Fields

		private 	T[] 		m_items;
		private		int 		m_count;
		private		int 		m_index;

		#endregion

		#region Constructors

		public ResizableArray(int size)
		{
			// set properties
			m_items		= new T[size];
			m_count		= size;
		}

		#endregion

		#region Properties

		internal T this[int index]
		{
			get
			{
				if (index < m_count)
					return m_items[index];

				throw new IndexOutOfRangeException();
			}
		}

		#endregion

		#region Methods

		public void Add(T item)
		{
			if (m_count < m_index)
			{
				Resize();
			}

			// add item
			m_items[m_index] = item;
			m_index++;
		}

		#endregion

		#region Private methods

		internal void Resize()
		{
			try
			{
				int 	newSize 	= m_count * 2;
				T[] 	newItems 	= new T[newSize];
				Array.Copy(m_items, 0, newItems, 0, m_count);

				// update values
				m_items = newItems;
				m_count	= newSize;
			}
			catch (Exception)
			{
				throw ErrorCentre.Exception("Array resize failed.");
			}
		}

		#endregion
	}
}