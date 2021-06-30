using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public interface IStreamReader : System.IDisposable 
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

		bool ReadBoolean();
		char ReadChar();
		byte ReadByte();
		sbyte ReadSByte();
		short ReadInt16();
		ushort ReadUInt16();
		int ReadInt32();
		uint ReadUInt32();
		long ReadInt64();
		ulong ReadUInt64();
		float ReadSingle();
		double ReadDouble();
		decimal ReadDecimal();
		string ReadString();
		int SkipString();
		void ReadBytes(byte[] buffer, int count);

		byte Peek();
		long Seek(long offset, SeekOrigin origin = SeekOrigin.Begin);

		void Close();

		#endregion
	}
}