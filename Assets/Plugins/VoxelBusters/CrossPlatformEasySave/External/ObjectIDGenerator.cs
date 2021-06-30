//#define ENABLE_FAST_SEARCH_MODE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.External.SystemUtils;

namespace VoxelBusters.External.Utils
{
	public class ObjectIDGenerator<TObject> where TObject : class
	{
		#region Constants

		private		const		long 					kObjectIdStartsFrom			= 1;

		#endregion

		#region Fields

		#if ENABLE_FAST_SEARCH_MODE
		private		ITwoWayDictionary<TObject, long>	m_objectCollection;
		#else
		private		Dictionary<TObject, long> 			m_objectCollection;
		#endif
		private		long								m_idCounter;

		#endregion

		#region Properties

		public int Count
		{
			get
			{
				return m_objectCollection.Count;
			}
		}

		#endregion

		#region Constructors 

		public ObjectIDGenerator(long idStartsFrom = kObjectIdStartsFrom)
			: this(capacity: 0, idStartsFrom: idStartsFrom)
		{}

		public ObjectIDGenerator(int capacity, long idStartsFrom = kObjectIdStartsFrom)
		{
			// set properties
			m_idCounter			= idStartsFrom;

			if (capacity > 0)
			{
				#if ENABLE_FAST_SEARCH_MODE
				m_objectCollection	= new TwoWayDictionary<TObject, long>(capacity);
				#else
				m_objectCollection	= new Dictionary<TObject, long>(capacity);
				#endif
			}
		}

		~ObjectIDGenerator()
		{
			// release
			if (m_objectCollection != null)
			{
				m_objectCollection.Clear();
				m_objectCollection	= null;
			}
		}

		#endregion

		#region Public methods

		public bool HasId(TObject obj, out long objectId)
		{
			if (m_objectCollection == null)
			{
				objectId	= 0;
				return false;
			}

			return m_objectCollection.TryGetValue(obj, out objectId);
		}

		public long GetId(TObject obj, out bool firstTime)
		{
			EnsureCapacity();

			// check whether item is already recorded
			long	objId;
			if (firstTime = (false == m_objectCollection.TryGetValue(obj, out objId))) 
			{
				objId		= m_idCounter++;
				m_objectCollection.Add(obj, objId);
			}

			return objId;
		}

		#endregion

		#region Private methods

		private void EnsureCapacity(int capacity = 4)
		{
			if (m_objectCollection == null)
			{
				#if ENABLE_FAST_SEARCH_MODE
				m_objectCollection	= new TwoWayDictionary<TObject, long>(capacity);
				#else
				m_objectCollection	= new Dictionary<TObject, long>(capacity);
				#endif
			}
		}

		#endregion

		#region IEnumerable implementation

		public IObjectIDEnumerator GetEnumerator()
		{
			if (m_objectCollection == null)
			{
				return new NullEnumerator();
			}

			return new Enumerator(enumerator: m_objectCollection.GetEnumerator());
		}

		#endregion

		#region Nested types

		public interface IObjectIDEnumerator : IEnumerator<TObject>
		{
			#region Properties

			TObject CurrentObject
			{
				get;
			}

			long CurrentObjectId
			{
				get;
			}

			#endregion
		}

		private struct NullEnumerator : IObjectIDEnumerator
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
					return CurrentObject;
				}
			}

			#endregion

			#region IDisposable implementation

			public void Dispose()
			{}

			#endregion

			#region IObjectIDEnumerator implementation

			public TObject CurrentObject
			{
				get
				{
					return CurrentObject;
				}
			}
			public long CurrentObjectId
			{
				get
				{
					return -1;
				}
			}

			#endregion

			#region IEnumerator<T> implementation

			TObject IEnumerator<TObject>.Current
			{
				get
				{
					return CurrentObject;
				}
			}

			#endregion
		}

		private struct Enumerator : IObjectIDEnumerator
		{
			#region Fields

			private 	IEnumerator<KeyValuePair<TObject, long>>	 	m_enumerator;

			#endregion

			#region Constructors

			public Enumerator(IEnumerator<KeyValuePair<TObject, long>> enumerator)
			{
				// set properties
				m_enumerator	= enumerator;
			}

			#endregion

			#region IEnumerator implementation

			public bool MoveNext()
			{
				return m_enumerator.MoveNext();
			}

			public void Reset()
			{
				// couldn't find Reset method on Enumerator object
				// so creating new instance
				throw new System.NotSupportedException();
			}

			object IEnumerator.Current
			{
				get
				{
					return CurrentObject;
				}
			}

			#endregion

			#region IDisposable implementation

			public void Dispose()
			{
				m_enumerator.Dispose();
			}

			#endregion

			#region IEnumerator<T> implementation

			TObject IEnumerator<TObject>.Current
			{
				get
				{
					return CurrentObject;
				}
			}

			#endregion

			#region IObjectIDEnumerator implementation

			public TObject CurrentObject
			{
				get
				{
					return m_enumerator.Current.Key;
				}
			}

			public long CurrentObjectId
			{
				get
				{
					return m_enumerator.Current.Value;
				}
			}

			#endregion
		}

		#endregion
	}
}