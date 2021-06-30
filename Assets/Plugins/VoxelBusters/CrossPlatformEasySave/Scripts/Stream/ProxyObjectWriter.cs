using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Type = System.Type;

namespace VoxelBusters.Serialization
{
	internal class ProxyObjectWriter : IObjectWriter 
	{
		#region Fields

		private		List<string>	m_memberNames 	= new List<string>(capacity: 4);
		private		int				m_memberCount	= 0;

		#endregion

		#region Properties

		public string[] MemberNames
		{
			get
			{
				if (m_memberCount == m_memberNames.Count)
				{
					return m_memberNames.ToArray();
				}

				return null;
			}
		}

		public int MemberCount
		{
			get
			{
				return m_memberCount;
			}
		}

		#endregion

		#region Public methods

		internal void PrepareForWrite()
		{
			m_memberNames.Clear();
			m_memberCount	= 0;
		}

		#endregion

		#region IObjectWriter implementation

		public void WriteProperty(string name, bool value)
		{
			AddMember(name);
		}

		public void WriteProperty(string name, char value)
		{
			AddMember(name);
		}

		public void WriteProperty(string name, sbyte value)
		{
			AddMember(name);
		}

		public void WriteProperty(string name, byte value)
		{
			AddMember(name);
		}

		public void WriteProperty(string name, short value)
		{
			AddMember(name);
		}

		public void WriteProperty(string name, ushort value)
		{
			AddMember(name);
		}

		public void WriteProperty(string name, int value)
		{
			AddMember(name);
		}

		public void WriteProperty(string name, uint value)
		{
			AddMember(name);
		}

		public void WriteProperty(string name, long value)
		{
			AddMember(name);
		}

		public void WriteProperty(string name, ulong value)
		{
			AddMember(name);
		}

		public void WriteProperty(string name, float value)
		{
			AddMember(name);
		}

		public void WriteProperty(string name, double value)
		{
			AddMember(name);
		}

		public void WriteProperty(string name, decimal value)
		{
			AddMember(name);
		}

		public void WriteProperty(string name, string value)
		{
			AddMember(name);
		}

        public void WriteProperty(string name, object value)
        {
            AddMember(name);
        }

		public void WriteProperty(string name, object value, Type type)
		{
			AddMember(name);
		}

		public void WriteProperty<T>(string name, T value)
		{
			AddMember(name);
		}

		public void WriteProperty(bool value)
		{
			AddMember(null);
		}

		public void WriteProperty(char value)
		{
			AddMember(null);
		}

		public void WriteProperty(sbyte value)
		{
			AddMember(null);
		}

		public void WriteProperty(byte value)
		{
			AddMember(null);
		}

		public void WriteProperty(short value)
		{
			AddMember(null);
		}

		public void WriteProperty(ushort value)
		{
			AddMember(null);
		}

		public void WriteProperty(int value)
		{
			AddMember(null);
		}

		public void WriteProperty(uint value)
		{
			AddMember(null);
		}

		public void WriteProperty(long value)
		{
			AddMember(null);
		}

		public void WriteProperty(ulong value)
		{
			AddMember(null);
		}

		public void WriteProperty(float value)
		{
			AddMember(null);
		}

		public void WriteProperty(double value)
		{
			AddMember(null);
		}

		public void WriteProperty(decimal value)
		{
			AddMember(null);
		}

		public void WriteProperty(string value)
		{
			AddMember(null);
		}

        public void WriteProperty(object value)
        {
            AddMember(null);
        }

        public void WriteProperty(object value, Type type)
		{
			AddMember(null);
		}

		public void WriteProperty<T>(T value)
		{
			AddMember(null);
		}

		public void WriteArrayLength(int dimension, int value)
		{
			throw new System.NotImplementedException();
		}

		#endregion

		#region Private methods

		private void AddMember(string name)
		{
			if (name != null)
			{
				m_memberNames.Add(name);
			}
			m_memberCount++;
		}

		#endregion
	}
}