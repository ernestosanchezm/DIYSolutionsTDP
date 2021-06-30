using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using VoxelBusters.External.SystemUtils;

namespace VoxelBusters.Serialization
{
	public class PlayerPrefsStream : Stream
	{
		#region Constants

		public 		const 	string 				kDataKeyFormat				= "{0}.$data";
		public 		const 	string 				kDataExtensionKeyFormat		= "{0}.$ext";

		#endregion

		#region Fields

		private				string 				m_dataKey;
		private				MemoryStream 		m_memoryStream;
        private             bool                m_disposed;

		#endregion

		#region Constructors

		public PlayerPrefsStream(string key, int capacity, bool writable)
		{
			// set properties
			m_dataKey			= key;
			m_memoryStream		= CreateMemoryStream(capacity, writable);
            m_disposed          = false;
		}

		#endregion

		#region Static methods

		internal static void Delete(string key)
		{
			// delete data
			PlayerPrefs.DeleteKey(string.Format(kDataKeyFormat, key));
		}

		#endregion

		#region Private methods

		private MemoryStream CreateMemoryStream(int capacity, bool writable)
		{
			if (writable)
			{
				return new MemoryStream(capacity);
			}
			else
			{
				return new MemoryStream(buffer: LoadData(), writable: writable);
			}
		}

		private byte[] LoadData()
		{
			// use the generated key to get the saved value
			string	dataStr	= PlayerPrefs.GetString(key: string.Format(kDataKeyFormat, m_dataKey), defaultValue: string.Empty);
			if (!string.IsNullOrEmpty(dataStr))
			{
				return Convert.FromBase64String(dataStr);
			}

			throw ErrorCentre.DataNotFoundException();
		}

		private void SaveData(byte[] bytes)
		{
			// save the data using the generated key name
			string	dataStr	= Convert.ToBase64String(bytes);
			PlayerPrefs.SetString(key: string.Format(kDataKeyFormat, m_dataKey), value: dataStr);
			PlayerPrefs.Save();
		}

        #endregion

        #region Stream implementation

        protected override void Dispose(bool disposing)
		{
            if (m_disposed)
            {
                return;
            }

			if (disposing)
            {
                m_disposed = true;

                // check whether we need to save the stream data to PlayerPrefs
                if (m_memoryStream.CanWrite)
                {
                    SaveData(bytes: m_memoryStream.ToArray());
                }

                // release internal properties
                base.Dispose(disposing);
                m_memoryStream.Close();
            }
		}

		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, System.AsyncCallback callback, object state)
		{
			return m_memoryStream.BeginRead(buffer, offset, count, callback, state);
		}

		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, System.AsyncCallback callback, object state)
		{
			return m_memoryStream.BeginWrite(buffer, offset, count, callback, state);
		}

		public override int EndRead(IAsyncResult asyncResult)
		{
			return m_memoryStream.EndRead(asyncResult);
		}

		public override void EndWrite(IAsyncResult asyncResult)
		{
			m_memoryStream.EndWrite(asyncResult);
		}

		public override int ReadByte()
		{
			return m_memoryStream.ReadByte();
		}

		public override void WriteByte(byte value)
		{
			m_memoryStream.WriteByte(value);
		}

		public override void Flush()
		{
			m_memoryStream.Flush();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return m_memoryStream.Read(buffer, offset, count);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return m_memoryStream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			m_memoryStream.SetLength(value);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			m_memoryStream.Write(buffer, offset, count);
		}

		public override bool CanRead
		{
			get
			{
				return m_memoryStream.CanRead;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return m_memoryStream.CanSeek;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return m_memoryStream.CanWrite;
			}
		}

		public override long Length
		{
			get
			{
				return m_memoryStream.Length;
			}
		}

		public override long Position
		{
			get
			{
				return m_memoryStream.Position;
			}
			set
			{
				m_memoryStream.Position = value;
			}
		}

		#endregion
	}
}