using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public interface IStreamWriter : System.IDisposable
	{
		#region Properties

		Stream BaseStream
		{
			get;
		}

		long Position
		{
			get;
			set;
		}

		long Length
		{
			get;
		}

		#endregion

		#region Methods

		// write operations
		void WriteBoolean(bool value);
		void WriteChar(char ch);
		void WriteByte(byte value);
		void WriteSByte(sbyte value);
		void WriteInt16(short value);
		void WriteUInt16(ushort value);
		void WriteInt32(int value);
		void WriteUInt32(uint value);
		void WriteInt64(long value);
		void WriteUInt64(ulong value);
		void WriteSingle(float value);
		void WriteDouble(double value);
		void WriteDecimal(decimal value);
		void WriteString(string value);
		void Write(byte[] data, int offset, int count);

		// additional operations
		long Seek(long offset, SeekOrigin origin = SeekOrigin.Begin);

		void Close();

		#endregion
	}
}