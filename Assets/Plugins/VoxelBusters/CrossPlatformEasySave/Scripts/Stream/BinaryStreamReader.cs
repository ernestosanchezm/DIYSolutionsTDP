// string encoding and decoding behaviour is derived from binary formatter
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

using Encoding = System.Text.Encoding;
using Buffer = System.Buffer;
using NotImplementedException = System.NotImplementedException;
using ArgumentNullException = System.ArgumentNullException;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;

namespace VoxelBusters.Serialization
{
	public class BinaryStreamReader : IStreamReader 
	{
		#region Constants

		private		const 	int		kBufferLength			= 16;
		private		const 	int		kStringBufferLength		= 128;

		#endregion

		#region Fields

		private		Stream			m_inputStream;
		private		Encoding 		m_encoding;

		private		byte[]			m_buffer;
		private		byte[]			m_stringBuffer;
		private		char[]			m_charBuffer;
		private 	int      		m_maxCharsSize;

		private		bool			m_disposed;

		#endregion

		#region Constructors

		public BinaryStreamReader(Stream stream, Encoding encoding)
		{
			// check for exceptions
			if (null == stream)
			{
				throw new ArgumentNullException("stream");
			}

			// set values
			m_inputStream	= stream;
			m_encoding		= encoding;
			m_buffer		= new byte[kBufferLength];
			m_stringBuffer	= null;
			m_charBuffer	= null;
			m_maxCharsSize 	= encoding.GetMaxCharCount(kStringBufferLength);

			m_disposed 		= false;
		}

		~BinaryStreamReader()
		{
			m_inputStream	= null;
			m_encoding		= null;
			m_buffer		= null;
			m_stringBuffer	= null;
			m_charBuffer	= null;
		}

		#endregion

		#region Private methods

		private void CopyBytes(int size)
		{
			// check for exceptions
			if (m_disposed)
			{
				throw ErrorCentre.StreamClosedException();
			}
			if (size > kBufferLength)
			{
				throw ErrorCentre.Exception("Insufficient buffer size.");
			}

			if (1 == size)
			{
				int read = m_inputStream.ReadByte();
				if (-1 == read)
				{
					throw ErrorCentre.EOFException();
				}
				m_buffer[0] = (byte)read;
			}
			else
			{
				int		offset	= 0;
				do 
				{
					int bytes	= m_inputStream.Read(m_buffer, offset, (size - offset));
					if (0 == bytes)
					{
						throw ErrorCentre.EOFException();
					}
					offset     += bytes;
				} while (offset < size);
			}
		}

		private int Read7BitEncodedInt() 
		{
			// Read out an Int32 7 bits at a time.  The high bit
			// of the byte when on means to continue reading more bytes.
			int 	count = 0;
			int		shift = 0;
			byte 	value;
			do 
			{
				// Check for a corrupted stream.  Read a max of 5 bytes.
				// In a future version, add a DataFormatException.
				if (shift == 5 * 7)  // 5 bytes max per Int32, shift += 7
				{
					throw ErrorCentre.Exception("String length format is incorrect.");
				}

				// ReadByte handles end of stream cases for us.
				value 	= ReadByte();
				count  |= (value & 0x7F) << shift;
				shift  += 7;
			} while ((value & 0x80) != 0);

			return count;
		}

		#endregion

		#region IStreamReader implementation

		public bool ReadBoolean()
		{
			return (ReadByte() == 0) ? false: true;
		}

		public char ReadChar()
		{
			return (char)ReadUInt16();
		}

		public byte ReadByte()
		{
			if (m_disposed)
			{
				throw ErrorCentre.StreamClosedException();
			}

			int value = m_inputStream.ReadByte();
			if (value == -1)
			{
				throw ErrorCentre.EOFException();
			}

			return (byte)value;
		}

		public sbyte ReadSByte()
		{
			if (m_disposed)
			{
				throw ErrorCentre.StreamClosedException();
			}

			int read = m_inputStream.ReadByte();
			if (read == -1)
			{
				throw ErrorCentre.EOFException();
			}

			return (sbyte)read;
		}

		public unsafe short ReadInt16()
		{
			CopyBytes(2);

			fixed (byte* ptr = m_buffer)
			{
				return *(short*)ptr;
			}
		}

		public unsafe ushort ReadUInt16()
		{
			CopyBytes(2);

			fixed (byte* ptr = m_buffer)
			{
				return *(ushort*)ptr;
			}
		}

		public unsafe int ReadInt32()
		{
			CopyBytes(4);

			fixed (byte* ptr = m_buffer)
			{
				return *(int*)ptr;
			}
		}

		public unsafe uint ReadUInt32()
		{
			CopyBytes(4);

			fixed (byte* ptr = m_buffer)
			{
				return *(uint*)ptr;
			}
		}

		public unsafe long ReadInt64()
		{
			CopyBytes(8);

			fixed (byte* ptr = m_buffer)
			{
				return *(long*)ptr;
			}
		}

		public unsafe ulong ReadUInt64()
		{
			CopyBytes(8);

			fixed (byte* ptr = m_buffer)
			{
				return *(ulong*)ptr;
			}
		}

		public unsafe float ReadSingle()
		{
			CopyBytes(4);

			fixed (byte* ptr = m_buffer)
			{
				return *(float*)ptr;
			}
		}

		public unsafe double ReadDouble()
		{
			CopyBytes(8);

			fixed (byte* ptr = m_buffer)
			{
				return *(double*)ptr;
			}
		}

		public unsafe decimal ReadDecimal()
		{
			CopyBytes(16);

			fixed (byte* ptr = m_buffer)
			{
				return *(decimal*)ptr;
			}
		}

		public string ReadString()
		{
			if (m_disposed)
			{
				throw ErrorCentre.StreamClosedException();
			}

			int offset = 0;
			int readLength;
			int charsRead;

			// read length of the string
			int stringLength = Read7BitEncodedInt();
			if (stringLength < 0) 
			{
				return null;
			}
			if (0 == stringLength) 
			{
				return string.Empty;
			}

			if (null == m_stringBuffer) 
			{
				m_stringBuffer  = new byte[kStringBufferLength];
			}

			if (null == m_charBuffer) 
			{
				m_charBuffer 	= new char[m_maxCharsSize];
			}

			StringBuilder stringBuilder = null; 
			do
			{
				readLength 		= ((stringLength - offset) > kStringBufferLength) ? kStringBufferLength : (stringLength - offset);

				int bytesRead 	= m_inputStream.Read(m_stringBuffer, 0, readLength);
				if (0 == bytesRead) 
				{
					throw ErrorCentre.EOFException();
				}

				charsRead 		= m_encoding.GetChars(m_stringBuffer, 0, bytesRead, m_charBuffer, 0);
				if (offset == 0 && bytesRead == stringLength)
				{
					return new string(m_charBuffer, 0, charsRead);
				}

				if (stringBuilder == null)
				{
					stringBuilder	= new StringBuilder(stringLength); // Actual string length in chars may be smaller.
				}
				stringBuilder.Append(m_charBuffer, 0, charsRead);
				offset 		  += bytesRead;

			} while (offset<stringLength);

			return stringBuilder.ToString();
		}

		public int SkipString()
		{
			if (m_disposed)
			{
				throw ErrorCentre.StreamClosedException();
			}

			int byteLength = Read7BitEncodedInt();
			if (byteLength > 0)
			{
				Seek(byteLength, SeekOrigin.Current);
			}

			return byteLength;
		}

		public void ReadBytes(byte[] buffer, int count)
		{
			if (count < 1)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (m_disposed)
			{
				throw ErrorCentre.StreamClosedException();
			}

			// read bytes data
			int 	offset	= 0;
			do 
			{
				int bytes	= m_inputStream.Read(buffer, offset, (count - offset));
				if (0 == bytes)
				{
					break;
				}
				offset     += bytes;
			} while (offset < count);

			if (offset != count)
			{
				throw ErrorCentre.DataInconsistencyException();
			}
		}

		public byte Peek()
		{
			long	actualPosition	= m_inputStream.Position;
			try
			{
				return ReadByte();
			}
			finally
			{
				Seek(actualPosition, SeekOrigin.Begin);
			}
		}

		public long Seek(long offset, SeekOrigin origin = SeekOrigin.Begin)
		{
			if (m_disposed)
			{
				throw ErrorCentre.StreamClosedException();
			}

			return m_inputStream.Seek(offset, origin);
		}

		public void Close()
		{
            Dispose(disposing: true);
		}

		public Stream BaseStream
		{
			get
			{
				return m_inputStream;
			}
		}

		public long Position
		{
			get
			{
				return m_inputStream.Position;
			}
			set
			{
				Seek(value, SeekOrigin.Begin);
			}
		}

		public long Length
		{
			get
			{
				return m_inputStream.Length;
			}
		}

		#endregion

		#region IDisposable implementation

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		public void Dispose(bool disposing)
		{
            if (m_disposed)
            {
                return;
            }

			if (disposing)
            {
                m_disposed      = true;

                // release properties
                m_inputStream.Close();

                m_inputStream   = null;
                m_buffer        = null;
                m_stringBuffer  = null;
            }
		}

		#endregion
	}
}