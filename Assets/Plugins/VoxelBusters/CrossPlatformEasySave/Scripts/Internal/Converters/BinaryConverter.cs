using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Type = System.Type;

namespace VoxelBusters.Serialization
{
	internal class BinaryConverter	 
	{
		#region Static fields

		private 	static Dictionary<byte, object>		writeValueHash;
		private 	static Dictionary<byte, object>		readValueHash;

		#endregion

		#region Constructors

		static BinaryConverter()
		{
			// set static properties
			writeValueHash	= new Dictionary<byte, object>() 
			{
				{ SerializationTypeCode.Boolean, 	(System.Action<bool, 	IStreamWriter>)WriteBooleanInternal },
				{ SerializationTypeCode.Char, 		(System.Action<char, 	IStreamWriter>)WriteCharInternal },
				{ SerializationTypeCode.SByte, 		(System.Action<sbyte, 	IStreamWriter>)WriteSByteInternal },
				{ SerializationTypeCode.Byte, 		(System.Action<byte, 	IStreamWriter>)WriteByteInternal },
				{ SerializationTypeCode.Int16, 		(System.Action<short, 	IStreamWriter>)WriteInt16Internal },
				{ SerializationTypeCode.UInt16, 	(System.Action<ushort, 	IStreamWriter>)WriteUInt16Internal },
				{ SerializationTypeCode.Int32, 		(System.Action<int, 	IStreamWriter>)WriteInt32Internal },
				{ SerializationTypeCode.UInt32, 	(System.Action<uint, 	IStreamWriter>)WriteUInt32Internal },
				{ SerializationTypeCode.Int64, 		(System.Action<long, 	IStreamWriter>)WriteInt64Internal },
				{ SerializationTypeCode.UInt64,		(System.Action<ulong,	IStreamWriter>)WriteUInt64Internal },
				{ SerializationTypeCode.Single, 	(System.Action<float, 	IStreamWriter>)WriteSingleInternal },
				{ SerializationTypeCode.Double, 	(System.Action<double, 	IStreamWriter>)WriteDoubleInternal },
				{ SerializationTypeCode.Decimal, 	(System.Action<decimal, IStreamWriter>)WriteDecimalInternal },
			};
			readValueHash	= new Dictionary<byte, object>() 
			{
				{ SerializationTypeCode.Boolean, 	(System.Func<IStreamReader, bool>)ReadBooleanInternal },
				{ SerializationTypeCode.Char, 		(System.Func<IStreamReader, char>)ReadCharInternal },
				{ SerializationTypeCode.SByte, 		(System.Func<IStreamReader, sbyte>)ReadSByteInternal },
				{ SerializationTypeCode.Byte, 		(System.Func<IStreamReader, byte>)ReadByteInternal },
				{ SerializationTypeCode.Int16, 		(System.Func<IStreamReader, short>)ReadInt16Internal },
				{ SerializationTypeCode.UInt16, 	(System.Func<IStreamReader, ushort>)ReadUInt16Internal },
				{ SerializationTypeCode.Int32, 		(System.Func<IStreamReader, int>)ReadInt32Internal },
				{ SerializationTypeCode.UInt32, 	(System.Func<IStreamReader, uint>)ReadUInt32Internal },
				{ SerializationTypeCode.Int64, 		(System.Func<IStreamReader, long>)ReadInt64Internal },
				{ SerializationTypeCode.UInt64, 	(System.Func<IStreamReader, ulong>)ReadUInt64Internal },
				{ SerializationTypeCode.Single, 	(System.Func<IStreamReader, float>)ReadSingleInternal },
				{ SerializationTypeCode.Double, 	(System.Func<IStreamReader, double>)ReadDoubleInternal },
				{ SerializationTypeCode.Decimal, 	(System.Func<IStreamReader, decimal>)ReadDecimalInternal },
				{ SerializationTypeCode.String, 	(System.Func<IStreamReader, string>)ReadStringInternal },
			};
		}

		#endregion

		#region Write methods

		internal static void WritePrimitiveInternal<T>(T value, IStreamWriter streamWriter)
		{
			object	writeMethod = writeValueHash[TypeServices.GetTypeCode(typeof(T))];
			((System.Action<T, IStreamWriter>)writeMethod).Invoke(value, streamWriter);
		}

		internal static void WritePrimitiveInternal(object value, Type type, IStreamWriter streamWriter)
		{
			byte	typeCode	= TypeServices.GetTypeCode(type);
			switch (typeCode)
			{
				case SerializationTypeCode.Boolean:
					streamWriter.WriteBoolean((bool)value);
					break;

				case SerializationTypeCode.Char:
					streamWriter.WriteChar((char)value);
					break;

				case SerializationTypeCode.SByte:
					streamWriter.WriteSByte((sbyte)value);
					break;

				case SerializationTypeCode.Byte:
					streamWriter.WriteByte((byte)value);
					break;

				case SerializationTypeCode.Int16:
					streamWriter.WriteInt16((short)value);
					break;

				case SerializationTypeCode.UInt16:
					streamWriter.WriteUInt16((ushort)value);
					break;

				case SerializationTypeCode.Int32:
					streamWriter.WriteInt32((int)value);
					break;

				case SerializationTypeCode.UInt32:
					streamWriter.WriteUInt32((uint)value);
					break;

				case SerializationTypeCode.Int64:
					streamWriter.WriteInt64((long)value);
					break;

				case SerializationTypeCode.UInt64:
					streamWriter.WriteUInt64((ulong)value);
					break;

				case SerializationTypeCode.Single:
					streamWriter.WriteSingle((float)value);
					break;

				case SerializationTypeCode.Double:
					streamWriter.WriteDouble((double)value);
					break;

				case SerializationTypeCode.Decimal:
					streamWriter.WriteDecimal((decimal)value);
					break;

				default:
					throw ErrorCentre.Exception("Couldn't find write function for specified type " + type);
			}
		}

		#endregion

		#region Read methods

		internal static T ReadPrimitiveInternal<T>(Type type, IStreamReader streamReader)
		{
			object	readMethod = readValueHash[TypeServices.GetTypeCode(type)];
			return ((System.Func<IStreamReader, T>)readMethod).Invoke(streamReader);
		}

		internal static object ReadPrimitiveInternal(Type type, IStreamReader streamReader)
		{
			byte	typeCode	= TypeServices.GetTypeCode(type);
			switch (typeCode)
			{
				case SerializationTypeCode.Boolean:
					return (object)streamReader.ReadBoolean();

				case SerializationTypeCode.Char:
					return (object)streamReader.ReadChar();

				case SerializationTypeCode.SByte:
					return (object)streamReader.ReadSByte();

				case SerializationTypeCode.Byte:
					return (object)streamReader.ReadByte();

				case SerializationTypeCode.Int16:
					return (object)streamReader.ReadInt16();

				case SerializationTypeCode.UInt16:
					return (object)streamReader.ReadUInt16();

				case SerializationTypeCode.Int32:
					return (object)streamReader.ReadInt32();

				case SerializationTypeCode.UInt32:
					return (object)streamReader.ReadUInt32();

				case SerializationTypeCode.Int64:
					return (object)streamReader.ReadInt64();

				case SerializationTypeCode.UInt64:
					return (object)streamReader.ReadUInt64();

				case SerializationTypeCode.Single:
					return (object)streamReader.ReadSingle();

				case SerializationTypeCode.Double:
					return (object)streamReader.ReadDouble();

				case SerializationTypeCode.Decimal:
					return (object)streamReader.ReadDecimal();

				default:
					throw ErrorCentre.Exception("Couldn't find write function for specified type " + type);
			}
		}

		#endregion

		#region Private static methods

		private static void WriteBooleanInternal(bool value, IStreamWriter streamWriter)
		{
			streamWriter.WriteBoolean(value); 
		}

		private static void WriteCharInternal(char value, IStreamWriter streamWriter)
		{
			streamWriter.WriteChar(value); 
		}

		private static void WriteSByteInternal(sbyte value, IStreamWriter streamWriter)
		{
			streamWriter.WriteSByte(value); 
		}

		private static void WriteByteInternal(byte value, IStreamWriter streamWriter)
		{
			streamWriter.WriteByte(value); 
		}

		private static void WriteInt16Internal(short value, IStreamWriter streamWriter)
		{
			streamWriter.WriteInt16(value); 
		}

		private static void WriteUInt16Internal(ushort value, IStreamWriter streamWriter)
		{
			streamWriter.WriteUInt16(value); 
		}

		private static void WriteInt32Internal(int value, IStreamWriter streamWriter)
		{
			streamWriter.WriteInt32(value); 
		}

		private static void WriteUInt32Internal(uint value, IStreamWriter streamWriter)
		{
			streamWriter.WriteUInt32(value); 
		}

		private static void WriteInt64Internal(long value, IStreamWriter streamWriter)
		{
			streamWriter.WriteInt64(value); 
		}

		private static void WriteUInt64Internal(ulong value, IStreamWriter streamWriter)
		{
			streamWriter.WriteUInt64(value); 
		}

		private static void WriteSingleInternal(float value, IStreamWriter streamWriter)
		{
			streamWriter.WriteSingle(value); 
		}

		private static void WriteDoubleInternal(double value, IStreamWriter streamWriter)
		{
			streamWriter.WriteDouble(value); 
		}

		private static void WriteDecimalInternal(decimal value, IStreamWriter streamWriter)
		{
			streamWriter.WriteDecimal(value);  
		}

		private static bool ReadBooleanInternal(IStreamReader streamReader)
		{
			return streamReader.ReadBoolean(); 
		}

		private static char ReadCharInternal(IStreamReader streamReader)
		{
			return streamReader.ReadChar(); 
		}

		private static sbyte ReadSByteInternal(IStreamReader streamReader)
		{
			return streamReader.ReadSByte(); 
		}

		private static byte ReadByteInternal(IStreamReader streamReader)
		{
			return streamReader.ReadByte(); 
		}

		private static short ReadInt16Internal(IStreamReader streamReader)
		{
			return streamReader.ReadInt16(); 
		}

		private static ushort ReadUInt16Internal(IStreamReader streamWriter)
		{
			return streamWriter.ReadUInt16(); 
		}

		private static int ReadInt32Internal(IStreamReader streamReader)
		{
			return streamReader.ReadInt32(); 
		}

		private static uint ReadUInt32Internal(IStreamReader streamReader)
		{
			return streamReader.ReadUInt32(); 
		}

		private static long ReadInt64Internal(IStreamReader streamReader)
		{
			return streamReader.ReadInt64(); 
		}

		private static ulong ReadUInt64Internal(IStreamReader streamReader)
		{
			return streamReader.ReadUInt64(); 
		}

		private static float ReadSingleInternal(IStreamReader streamReader)
		{
			return streamReader.ReadSingle(); 
		}

		private static double ReadDoubleInternal(IStreamReader streamReader)
		{
			return streamReader.ReadDouble(); 
		}

		private static decimal ReadDecimalInternal(IStreamReader streamReader)
		{
			return streamReader.ReadDecimal();  
		}

		private static string ReadStringInternal(IStreamReader streamReader)
		{
			return streamReader.ReadString();  
		}

		#endregion
	}
}