using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Type = System.Type;

namespace VoxelBusters.Serialization
{
	internal class StreamObject : StreamObjectBase 
	{
		#region Fields

		private		int 					m_memberCount;
		private		string[] 				m_memberNames;
		private		IList<IStreamObject>	m_members;

		private		int 					m_enumeratorIndex;

		#endregion

		#region Properties

		public long ObjectId
		{
			get;
			private set;
		}

		#endregion

		#region Constructors

		public StreamObject(long objectId)
		{
			// set properies 
			ObjectId	= objectId;
		}

		#endregion

		#region Internal methods

		internal void Set(int memberCount, string[] memberNames, IList<IStreamObject> members)
		{
			// set properties
			m_memberCount 	= memberCount;
			m_memberNames 	= memberNames;
			m_members 		= members;
		}

		internal void Set(int memberCount, IList<IStreamObject> members)
		{
			// set properties
			m_memberCount 	= memberCount;
			m_memberNames 	= null;
			m_members 		= members;
		}

		internal void SetMemberStartPosition()
		{
		}

		#endregion

		#region StreamObjectBase implementation

		public override IStreamObject FindProperty(string name)
		{
			if (null == m_memberNames)
			{
				throw ErrorCentre.SearchByNameIsNotSupportedException();
			}

			for (int iter = 0; iter < m_memberCount; iter++)
			{
				if (string.Equals(m_memberNames[iter], name))
				{
					m_enumeratorIndex = iter;
					return m_members[iter];
				}
			}

			return null;
		}

		public override IStreamObject Next()
		{
			return m_members[m_enumeratorIndex++];
		}

		public override bool HasNext()
		{
			return (m_enumeratorIndex < m_memberCount);
		}

		#endregion
	}
}