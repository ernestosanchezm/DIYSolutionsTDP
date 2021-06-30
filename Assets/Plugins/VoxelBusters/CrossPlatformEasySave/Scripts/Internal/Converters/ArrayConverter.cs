using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Array = System.Array;
using Type = System.Type;

namespace VoxelBusters.Serialization
{
	internal class ArrayConverter 
	{
		#region Write methods

		internal static void WritePrimitiveArray(Array array, Type elementType, int rank, IStreamWriter streamWriter)
		{
			byte	typeCode	= TypeServices.GetTypeCode(elementType);
			switch (typeCode)
			{
				case SerializationTypeCode.Boolean:
					WriteBooleanArray(array, rank, streamWriter);
					break;

				case SerializationTypeCode.Char:
					WriteCharArray(array, rank, streamWriter);
					break;

				case SerializationTypeCode.SByte:
					WriteSByteArray(array, rank, streamWriter);
					break;

				case SerializationTypeCode.Byte:
					WriteByteArray(array, rank, streamWriter);
					break;

				case SerializationTypeCode.Int16:
					WriteInt16Array(array, rank, streamWriter);
					break;

				case SerializationTypeCode.UInt16:
					WriteUInt16Array(array, rank, streamWriter);
					break;

				case SerializationTypeCode.Int32:
					WriteInt32Array(array, rank, streamWriter);
					break;

				case SerializationTypeCode.UInt32:
					WriteUInt32Array(array, rank, streamWriter);
					break;

				case SerializationTypeCode.Int64:
					WriteInt64Array(array, rank, streamWriter);
					break;

				case SerializationTypeCode.UInt64:
					WriteUInt64Array(array, rank, streamWriter);
					break;

				case SerializationTypeCode.Single:
					WriteSingleArray(array, rank, streamWriter);
					break;

				case SerializationTypeCode.Double:
					WriteDoubleArray(array, rank, streamWriter);
					break;

				case SerializationTypeCode.Decimal:
					WriteDecimalArray(array, rank, streamWriter);
					break;

				default:
					throw ErrorCentre.Exception("Couldn't find array write function for specified type " + elementType);
			}
		}

		internal static void WriteBooleanArray(Array array, int rank, IStreamWriter streamWriter)
		{
			if (0 == rank)
			{
                BooleanArray.WriteJaggedArray((bool[][])array, streamWriter);
			}
			else if (1 == rank)
			{
                BooleanArray.Write1dArray((bool[])array, streamWriter);
            }
            else if (2 == rank)
			{
				BooleanArray.Write2dArray((bool[,])array, streamWriter);
			}
			else if (3 == rank)
			{
				BooleanArray.Write3dArray((bool[,,])array, rank, streamWriter);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static void WriteCharArray(Array array, int rank, IStreamWriter streamWriter)
		{
			if (0 == rank)
			{
                CharArray.WriteJaggedArray((char[][])array, streamWriter);
            }
            else if (1 == rank)
			{
                CharArray.Write1dArray((char[])array, streamWriter);
            }
            else if (2 == rank)
			{
				CharArray.Write2dArray((char[,])array, streamWriter);
			}
			else if (3 == rank)
			{
				CharArray.Write3dArray((char[,,])array, rank, streamWriter);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static void WriteSByteArray(Array array, int rank, IStreamWriter streamWriter)
		{
			if (0 == rank)
			{
                SByteArray.WriteJaggedArray((sbyte[][])array, streamWriter);
			}
			else if (1 == rank)
			{
                SByteArray.Write1dArray((sbyte[])array, streamWriter);
            }
            else if (2 == rank)
			{
				SByteArray.Write2dArray((sbyte[,])array, streamWriter);
			}
			else if (3 == rank)
			{
				SByteArray.Write3dArray((sbyte[,,])array, rank, streamWriter);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static void WriteByteArray(Array array, int rank, IStreamWriter streamWriter)
		{
			if (0 == rank)
			{
                ByteArray.WriteJaggedArray((byte[][])array, streamWriter);
			}
			else if (1 == rank)
			{
                ByteArray.Write1dArray((byte[])array, streamWriter);
            }
            else if (2 == rank)
			{
				ByteArray.Write2dArray((byte[,])array, streamWriter);
			}
			else if (3 == rank)
			{
				ByteArray.Write3dArray((byte[,,])array, rank, streamWriter);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static void WriteInt16Array(Array array, int rank, IStreamWriter streamWriter)
		{
			if (0 == rank)
			{
                Int16Array.WriteJaggedArray((short[][])array, streamWriter);
			}
			else if (1 == rank)
			{
                Int16Array.Write1dArray((short[])array, streamWriter);
            }
            else if (2 == rank)
			{
				Int16Array.Write2dArray((short[,])array, streamWriter);
			}
			else if (3 == rank)
			{
				Int16Array.Write3dArray((short[,,])array, rank, streamWriter);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static void WriteUInt16Array(Array array, int rank, IStreamWriter streamWriter)
		{
			if (0 == rank)
			{
                UInt16Array.WriteJaggedArray((ushort[][])array, streamWriter);
			}
			else if (1 == rank)
			{
                UInt16Array.Write1dArray((ushort[])array, streamWriter);
            }
            else if (2 == rank)
			{
				UInt16Array.Write2dArray((ushort[,])array, streamWriter);
			}
			else if (3 == rank)
			{
				UInt16Array.Write3dArray((ushort[,,])array, rank, streamWriter);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static void WriteInt32Array(Array array, int rank, IStreamWriter streamWriter)
		{
			if (0 == rank)
			{
                Int32Array.WriteJaggedArray((int[][])array, streamWriter);
			}
			else if (1 == rank)
			{
                Int32Array.Write1dArray((int[])array, streamWriter);
            }
            else if (2 == rank)
			{
				Int32Array.Write2dArray((int[,])array, streamWriter);
			}
			else if (3 == rank)
			{
				Int32Array.Write3dArray((int[,,])array, rank, streamWriter);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static void WriteUInt32Array(Array array, int rank, IStreamWriter streamWriter)
		{
			if (0 == rank)
			{
                UInt32Array.WriteJaggedArray((uint[][])array, streamWriter);
			}
			else if (1 == rank)
			{
                UInt32Array.Write1dArray((uint[])array, streamWriter);
            }
            else if (2 == rank)
			{
				UInt32Array.Write2dArray((uint[,])array, streamWriter);
			}
			else if (3 == rank)
			{
				UInt32Array.Write3dArray((uint[,,])array, rank, streamWriter);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static void WriteInt64Array(Array array, int rank, IStreamWriter streamWriter)
		{
			if (0 == rank)
			{
                Int64Array.WriteJaggedArray((long[][])array, streamWriter);
			}
			else if (1 == rank)
			{
                Int64Array.Write1dArray((long[])array, streamWriter);
            }
            else if (2 == rank)
			{
				Int64Array.Write2dArray((long[,])array, streamWriter);
			}
			else if (3 == rank)
			{
				Int64Array.Write3dArray((long[,,])array, rank, streamWriter);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static void WriteUInt64Array(Array array, int rank, IStreamWriter streamWriter)
		{
			if (0 == rank)
			{
                UInt64Array.WriteJaggedArray((ulong[][])array, streamWriter);
			}
			else if (1 == rank)
			{
                UInt64Array.Write1dArray((ulong[])array, streamWriter);
            }
            else if (2 == rank)
			{
				UInt64Array.Write2dArray((ulong[,])array, streamWriter);
			}
			else if (3 == rank)
			{
				UInt64Array.Write3dArray((ulong[,,])array, rank, streamWriter);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static void WriteSingleArray(Array array, int rank, IStreamWriter streamWriter)
		{
			if (0 == rank)
			{
                SingleArray.WriteJaggedArray((float[][])array, streamWriter);
			}
			else if (1 == rank)
			{
                SingleArray.Write1dArray((float[])array, streamWriter);
            }
            else if (2 == rank)
			{
				SingleArray.Write2dArray((float[,])array, streamWriter);
			}
			else if (3 == rank)
			{
				SingleArray.Write3dArray((float[,,])array, rank, streamWriter);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static void WriteDoubleArray(Array array, int rank, IStreamWriter streamWriter)
		{
			if (0 == rank)
			{
                DoubleArray.WriteJaggedArray((double[][])array, streamWriter);
			}
			else if (1 == rank)
			{
                DoubleArray.Write1dArray((double[])array, streamWriter);
            }
            else if (2 == rank)
			{
				DoubleArray.Write2dArray((double[,])array, streamWriter);
			}
			else if (3 == rank)
			{
				DoubleArray.Write3dArray((double[,,])array, rank, streamWriter);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static void WriteDecimalArray(Array array, int rank, IStreamWriter streamWriter)
		{
			if (0 == rank)
			{
                DecimalArray.WriteJaggedArray((decimal[][])array, streamWriter);
			}
			else if (1 == rank)
			{
                DecimalArray.Write1dArray((decimal[])array, streamWriter);
            }
            else if (2 == rank)
			{
				DecimalArray.Write2dArray((decimal[,])array, streamWriter);
			}
			else if (3 == rank)
			{
				DecimalArray.Write3dArray((decimal[,,])array, rank, streamWriter);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static void WriteStringArray(Array array, int rank, IStreamWriter streamWriter)
		{
			if (0 == rank)
			{
                StringArray.WriteJaggedArray((string[][])array, streamWriter);
			}
			else if (1 == rank)
			{
                StringArray.Write1dArray((string[])array, streamWriter);
            }
            else if (2 == rank)
			{
				StringArray.Write2dArray((string[,])array, streamWriter);
			}
			else if (3 == rank)
			{
				StringArray.Write3dArray((string[,,])array, rank, streamWriter);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		#endregion

		#region Read methods

		internal static Array ReadPrimitiveArray(Type elementType, int rank, IStreamReader streamReader)
		{
			byte	typeCode	= TypeServices.GetTypeCode(elementType);
			switch (typeCode)
			{
				case SerializationTypeCode.Boolean:
					return ReadBooleanArray(rank, streamReader);

				case SerializationTypeCode.Char:
					return ReadCharArray(rank, streamReader);

				case SerializationTypeCode.SByte:
					return ReadSByteArray(rank, streamReader);

				case SerializationTypeCode.Byte:
					return ReadByteArray(rank, streamReader);

				case SerializationTypeCode.Int16:
					return ReadInt16Array(rank, streamReader);

				case SerializationTypeCode.UInt16:
					return ReadUInt16Array(rank, streamReader);

				case SerializationTypeCode.Int32:
					return ReadInt32Array(rank, streamReader);

				case SerializationTypeCode.UInt32:
					return ReadUInt32Array(rank, streamReader);

				case SerializationTypeCode.Int64:
					return ReadInt64Array(rank, streamReader);

				case SerializationTypeCode.UInt64:
					return ReadUInt64Array(rank, streamReader);

				case SerializationTypeCode.Single:
					return ReadSingleArray(rank, streamReader);

				case SerializationTypeCode.Double:
					return ReadDoubleArray(rank, streamReader);

				case SerializationTypeCode.Decimal:
					return ReadDecimalArray(rank, streamReader);

				default:
					throw ErrorCentre.Exception("Couldn't find array write function for specified type " + elementType);
			}
		}

		internal static Array ReadBooleanArray(int rank, IStreamReader streamReader)
		{
			if (0 == rank)
			{
                return BooleanArray.ReadJaggedArray(streamReader);
			}
			else if (1 == rank)
			{
                return BooleanArray.Read1dArray(streamReader);
            }
            else if (2 == rank)
			{
				return BooleanArray.Read2dArray(streamReader);
			}
			else if (3 == rank)
			{
				return BooleanArray.Read3dArray(streamReader);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static Array ReadCharArray(int rank, IStreamReader streamReader)
		{
			if (0 == rank)
			{
                return CharArray.ReadJaggedArray(streamReader);
			}
			else if (1 == rank)
			{
                return CharArray.Read1dArray(streamReader);
            }
            else if (2 == rank)
			{
				return CharArray.Read2dArray(streamReader);
			}
			else if (3 == rank)
			{
				return CharArray.Read3dArray(streamReader);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static Array ReadSByteArray(int rank, IStreamReader streamReader)
		{
			if (0 == rank)
			{
                return SByteArray.ReadJaggedArray(streamReader);
			}
			else if (1 == rank)
			{
                return SByteArray.Read1dArray(streamReader);
            }
            else if (2 == rank)
			{
				return SByteArray.Read2dArray(streamReader);
			}
			else if (3 == rank)
			{
				return SByteArray.Read3dArray(streamReader);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static Array ReadByteArray(int rank, IStreamReader streamReader)
		{
			if (0 == rank)
			{
                return ByteArray.ReadJaggedArray(streamReader);
			}
			else if (1 == rank)
			{
                return ByteArray.Read1dArray(streamReader);
            }
            else if (2 == rank)
			{
				return ByteArray.Read2dArray(streamReader);
			}
			else if (3 == rank)
			{
				return ByteArray.Read3dArray(streamReader);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static Array ReadInt16Array(int rank, IStreamReader streamReader)
		{
			if (0 == rank)
			{
                return Int16Array.ReadJaggedArray(streamReader);
			}
			else if (1 == rank)
			{
                return Int16Array.Read1dArray(streamReader);
            }
            else if (2 == rank)
			{
				return Int16Array.Read2dArray(streamReader);
			}
			else if (3 == rank)
			{
				return Int16Array.Read3dArray(streamReader);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static Array ReadUInt16Array(int rank, IStreamReader streamReader)
		{
			if (0 == rank)
			{
                return UInt16Array.ReadJaggedArray(streamReader);
			}
			else if (1 == rank)
			{
                return UInt16Array.Read1dArray(streamReader);
            }
            else if (2 == rank)
			{
				return UInt16Array.Read2dArray(streamReader);
			}
			else if (3 == rank)
			{
				return UInt16Array.Read3dArray(streamReader);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static Array ReadInt32Array(int rank, IStreamReader streamReader)
		{
			if (0 == rank)
			{
                return Int32Array.ReadJaggedArray(streamReader);
			}
			else if (1 == rank)
			{
                return Int32Array.Read1dArray(streamReader);
            }
            else if (2 == rank)
			{
				return Int32Array.Read2dArray(streamReader);
			}
			else if (3 == rank)
			{
				return Int32Array.Read3dArray(streamReader);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static Array ReadUInt32Array(int rank, IStreamReader streamReader)
		{
			if (0 == rank)
			{
                return UInt32Array.ReadJaggedArray(streamReader);
			}
			else if (1 == rank)
			{
                return UInt32Array.Read1dArray(streamReader);
            }
            else if (2 == rank)
			{
				return UInt32Array.Read2dArray(streamReader);
			}
			else if (3 == rank)
			{
				return UInt32Array.Read3dArray(streamReader);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static Array ReadInt64Array(int rank, IStreamReader streamReader)
		{
			if (0 == rank)
			{
                return Int64Array.ReadJaggedArray(streamReader);
			}
			else if (1 == rank)
			{
                return Int64Array.Read1dArray(streamReader);
            }
            else if (2 == rank)
			{
				return Int64Array.Read2dArray(streamReader);
			}
			else if (3 == rank)
			{
				return Int64Array.Read3dArray(streamReader);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static Array ReadUInt64Array(int rank, IStreamReader streamReader)
		{
			if (0 == rank)
			{
                return UInt64Array.ReadJaggedArray(streamReader);
			}
			else if (1 == rank)
			{
                return UInt64Array.Read1dArray(streamReader);
            }
            else if (2 == rank)
			{
				return UInt64Array.Read2dArray(streamReader);
			}
			else if (3 == rank)
			{
				return UInt64Array.Read3dArray(streamReader);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static Array ReadSingleArray(int rank, IStreamReader streamReader)
		{
			if (0 == rank)
			{
                return SingleArray.ReadJaggedArray(streamReader);
			}
			else if (1 == rank)
			{
                return SingleArray.Read1dArray(streamReader);
            }
            else if (2 == rank)
			{
				return SingleArray.Read2dArray(streamReader);
			}
			else if (3 == rank)
			{
				return SingleArray.Read3dArray(streamReader);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static Array ReadDoubleArray(int rank, IStreamReader streamReader)
		{
			if (0 == rank)
			{
                return DoubleArray.ReadJaggedArray(streamReader);
			}
			else if (1 == rank)
			{
                return DoubleArray.Read1dArray(streamReader);
            }
            else if (2 == rank)
			{
				return DoubleArray.Read2dArray(streamReader);
			}
			else if (3 == rank)
			{
				return DoubleArray.Read3dArray(streamReader);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static Array ReadDecimalArray(int rank, IStreamReader streamReader)
		{
			if (0 == rank)
			{
                return DecimalArray.ReadJaggedArray(streamReader);
			}
			else if (1 == rank)
			{
                return DecimalArray.Read1dArray(streamReader);
            }
            else if (2 == rank)
			{
				return DecimalArray.Read2dArray(streamReader);
			}
			else if (3 == rank)
			{
				return DecimalArray.Read3dArray(streamReader);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		internal static Array ReadStringArray(int rank, IStreamReader streamReader)
		{
			if (0 == rank)
			{
                return StringArray.ReadJaggedArray(streamReader);
			}
			else if (1 == rank)
			{
                return StringArray.Read1dArray(streamReader);
            }
            else if (2 == rank)
			{
				return StringArray.Read2dArray(streamReader);
			}
			else if (3 == rank)
			{
				return StringArray.Read3dArray(streamReader);
			}
			else
			{
				throw ErrorCentre.ArrayRankNotSupportedException(rank);
			}
		}

		#endregion
	}
}