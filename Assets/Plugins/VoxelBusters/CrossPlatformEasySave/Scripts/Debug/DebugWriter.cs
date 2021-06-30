using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	internal class DebugWriter : IStreamWriter 
	{
		#region Static fields

		internal		static		DebugWriter		defaultWriter		= new DebugWriter();

		#endregion

		#region Fields

		private						StringBuilder	m_streamBuilder		= new StringBuilder(capacity: 1024);
		private						string			m_space				= " ";
		internal					IStreamWriter	underlyingWriter	= null;

		#endregion

		#region IStreamWriter implementation

		public void WriteBoolean(bool value)
		{
			m_streamBuilder.Append(value).Append(m_space);
			underlyingWriter.WriteBoolean(value);
		}

		public void WriteChar(char ch)
		{
			m_streamBuilder.Append(ch).Append(m_space);
			underlyingWriter.WriteChar(ch);
		}

		public void WriteByte(byte value)
		{
			m_streamBuilder.Append(value).Append(m_space);
			underlyingWriter.WriteByte(value);
		}

		public void WriteSByte(sbyte value)
		{
			m_streamBuilder.Append(value).Append(m_space);
			underlyingWriter.WriteSByte(value);
		}

		public void WriteInt16(short value)
		{
			m_streamBuilder.Append(value).Append(m_space);
			underlyingWriter.WriteInt16(value);
		}

		public void WriteUInt16(ushort value)
		{
			m_streamBuilder.Append(value).Append(m_space);
			underlyingWriter.WriteUInt16(value);
		}

		public void WriteInt32(int value)
		{
			m_streamBuilder.Append(value).Append(m_space);
			underlyingWriter.WriteInt32(value);
		}

		public void WriteUInt32(uint value)
		{
			m_streamBuilder.Append(value).Append(m_space);
			underlyingWriter.WriteUInt32(value);
		}

		public void WriteInt64(long value)
		{
			m_streamBuilder.Append(value).Append(m_space);
			underlyingWriter.WriteInt64(value);
		}

		public void WriteUInt64(ulong value)
		{
			m_streamBuilder.Append(value).Append(m_space);
			underlyingWriter.WriteUInt64(value);
		}

		public void WriteSingle(float value)
		{
			m_streamBuilder.Append(value).Append(m_space);
			underlyingWriter.WriteSingle(value);
		}

		public void WriteDouble(double value)
		{
			m_streamBuilder.Append(value).Append(m_space);
			underlyingWriter.WriteDouble(value);
		}

		public void WriteDecimal(decimal value)
		{
			m_streamBuilder.Append(value).Append(m_space);
			underlyingWriter.WriteDecimal(value);
		}

		public void WriteString(string value)
		{
			m_streamBuilder.AppendFormat("\"{0}\"", value).Append(m_space);
			underlyingWriter.WriteString(value);
		}

		public void Write(byte[] data, int offset, int count)
		{
			m_streamBuilder.Append(System.Convert.ToBase64String(data)).Append(m_space);
			underlyingWriter.Write(data, offset, count);
		}

		public long Seek(long offset, System.IO.SeekOrigin origin = System.IO.SeekOrigin.Current)
		{
			return underlyingWriter.Seek(offset, origin);
		}

		public void Close()
		{
			Debug.Log(m_streamBuilder.ToString());
			underlyingWriter.Close();
		}

		public Stream BaseStream
		{
			get
			{
				return underlyingWriter.BaseStream;
			}
		}

		public long Position
		{
			get
			{
				return underlyingWriter.Position;
			}

			set
			{
				underlyingWriter.Position	= value;
			}
		}

		public long Length
		{
			get
			{
				return underlyingWriter.Length;
			}
		}

		#endregion

		#region IDisposable implementation

		public void Dispose()
		{
			underlyingWriter.Dispose();
		}

		#endregion
	}
}