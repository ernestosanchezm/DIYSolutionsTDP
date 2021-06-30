using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

using Buffer = System.Buffer;
using NotImplementedException = System.NotImplementedException;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;
	
namespace VoxelBusters.Serialization
{
	public class BinaryStreamWriter : IStreamWriter
	{
		#region Constants

		private		const int		kStringBufferLength = 256;

		#endregion

		#region Fields

		private		Stream			m_outputStream;
		private		Encoding		m_encoding;

		private		byte[] 			m_buffer;
		private		byte[] 			m_stringBuffer;
		private		int 			m_maxChars;

		private		bool			m_disposed;

		#endregion

		#region Constructors

		public BinaryStreamWriter(Stream stream, Encoding encoding)
		{
			// set properties
			m_outputStream 		= stream;
			m_encoding			= encoding;
			m_buffer 			= new byte[16];
			m_disposed 			= false;
		}

		~BinaryStreamWriter()
		{
			m_outputStream 		= null;
			m_encoding			= null;
			m_buffer 			= null;
			m_stringBuffer		= null;
		}

		#endregion

		#region Private methods

		private void Write7BitEncodedInt(int value) 
		{
			// Write out an int 7 bits at a time.  The high bit of the byte,
			// when on, tells reader to continue reading more bytes.
			uint v = (uint) value;   // support negative numbers
			while (v >= 0x80) 
			{
				WriteByte((byte) (v | 0x80));
				v >>= 7;
			}
			WriteByte((byte)v);
		}

		#endregion

		#region IStreamWriter implementation

		public void WriteBoolean(bool value)
		{
			m_outputStream.WriteByte((byte)(value ? 1 : 0));
		}

		public void WriteChar(char ch)
		{
			WriteUInt16((ushort)ch);
		}

		public void WriteByte(byte value)
		{
			m_outputStream.WriteByte(value);
		}

		public void WriteSByte(sbyte value)
		{
			m_outputStream.WriteByte((byte)value);
		}

		public unsafe void WriteInt16(short value)
		{
			fixed (byte* ptr = m_buffer)
			{
				*(short*)(ptr)	= value;
				m_outputStream.Write(m_buffer, 0, 2);
			}
		}

		public unsafe void WriteUInt16(ushort value)
		{
			fixed (byte* ptr = m_buffer)
			{
				*(ushort*)(ptr)	= value;
				m_outputStream.Write(m_buffer, 0, 2);
			}
		}

		public unsafe void WriteInt32(int value)
		{
			fixed (byte* ptr = m_buffer)
			{
				*(int*)(ptr)	= value;
				m_outputStream.Write(m_buffer, 0, 4);
			}
		}

		public unsafe void WriteUInt32(uint value)
		{
			fixed (byte* ptr = m_buffer)
			{
				*(uint*)(ptr)	= value;
				m_outputStream.Write(m_buffer, 0, 4);
			}
		}

		public unsafe void WriteInt64(long value)
		{
			fixed (byte* ptr = m_buffer)
			{
				*(long*)(ptr)	= value;
				m_outputStream.Write(m_buffer, 0, 8);
			}
		}

		public unsafe void WriteUInt64(ulong value)
		{
			fixed (byte* ptr = m_buffer)
			{
				*(ulong*)(ptr)	= value;
				m_outputStream.Write(m_buffer, 0, 8);
			}
		}

		public unsafe void WriteSingle(float value)
		{
			fixed (byte* ptr = m_buffer)
			{
				*(float*)(ptr)	= value;
				m_outputStream.Write(m_buffer, 0, 4);
			}
		}

		public unsafe void WriteDouble(double value)
		{
			fixed (byte* ptr = m_buffer)
			{
				*(double*)(ptr)	= value;
				m_outputStream.Write(m_buffer, 0, 8);
			}
		}

		public unsafe void WriteDecimal(decimal value)
		{
			fixed (byte* ptr = m_buffer)
			{
				*(decimal*)(ptr)	= value;
				m_outputStream.Write(m_buffer, 0, 16);
			}
		}

		public unsafe void WriteString(string value)
		{
			// write non empty string value
			if (null == value)
			{
				Write7BitEncodedInt(-1);
			}
			else
			{
				int			valueLength		= value.Length;

				// check whether string is empty
				if (0 == valueLength)
				{
					Write7BitEncodedInt(0);
				}
				else
				{
					// write length
					int		totalByteLength	= m_encoding.GetByteCount(value);
					Write7BitEncodedInt(totalByteLength);

					// allocate buffer
					if (null == m_stringBuffer)
					{
						m_stringBuffer		= new byte[kStringBufferLength];
						m_maxChars 			= kStringBufferLength / m_encoding.GetMaxByteCount(1);
					}

					// write string value with length
					if (totalByteLength <= kStringBufferLength) 
					{
						m_encoding.GetBytes(value, 0, valueLength, m_stringBuffer, 0);
						m_outputStream.Write(m_stringBuffer, 0, totalByteLength);
					}
					else 
					{
						// Aggressively try to not allocate memory in this loop for
						// runtime performance reasons.  Use an Encoder to write out 
						// the string correctly (handling surrogates crossing buffer
						// boundaries properly).  
						int charStart 	= 0;
						int numLeft 	= value.Length;
						while (numLeft > 0) 
						{
							// Figure out how many chars to process this round.
							int charCount = (numLeft > m_maxChars) ? m_maxChars : numLeft;
							int byteLen;

							checked 
							{
								if (charStart < 0 || charCount < 0 || charStart + charCount > value.Length) 
								{
									throw new ArgumentOutOfRangeException("charCount");
								}

								fixed (char* pChars = value) 
								{
									fixed (byte* pBytes = m_stringBuffer) 
									{
										byteLen = m_encoding.GetBytes(pChars + charStart, charCount, pBytes, kStringBufferLength);
									}
								}
							}
							m_outputStream.Write(m_stringBuffer, 0, byteLen);
							charStart 	+= charCount;
							numLeft 	-= charCount;
						}
					}
				}
			}
		}

		public void Write(byte[] data, int offset, int count)
		{
			m_outputStream.Write(data, offset, count);
		}

		public long Seek(long offset, SeekOrigin origin = SeekOrigin.Begin)
		{
			return m_outputStream.Seek(offset, origin);
		}

		public void Close()
		{
            Dispose(disposing: true);
		}

		public Stream BaseStream
		{
			get
			{
				return m_outputStream;
			}
		}

		public long Position
		{
			get
			{
				return m_outputStream.Position;
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
				return m_outputStream.Length;
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
               
                // close the stream 
                m_outputStream.Close();
                m_outputStream  = null;
            }
		}

		#endregion
	}
}