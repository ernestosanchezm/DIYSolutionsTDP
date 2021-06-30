using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Array = System.Array;

namespace VoxelBusters.Serialization
{
	internal class SByteArray 
	{
		#region Internal methods

		internal static void Write1dArray(sbyte[] array, IStreamWriter streamWriter)
		{
			int length0	= array.Length;
			streamWriter.WriteInt32(length0);

			for (int x = 0; x < length0; x++)
			{
				streamWriter.WriteSByte(array[x]);
			}
		}

		internal static void WriteJaggedArray(sbyte[][] array, IStreamWriter streamWriter)
		{
			int length0	= array.GetLength(dimension: 0);
			streamWriter.WriteInt32(length0);

			for (int x = 0; x < length0; x++)
			{
				Write1dArray(array[x], streamWriter);
			}
		}

		internal static void Write2dArray(sbyte[,] array, IStreamWriter streamWriter)
		{
			int length0	= array.GetLength(dimension: 0);
			int length1	= array.GetLength(dimension: 1);
			streamWriter.WriteInt32(length0);
			streamWriter.WriteInt32(length1);

			for (int x = 0; x < length0; x++)
			{
				for (int y = 0; y < length1; y++)
				{
					streamWriter.WriteSByte(array[x, y]);
				}
			}
		}

		internal static void Write3dArray(sbyte[,,] array, int rank, IStreamWriter streamWriter)
		{
			// write length 
			int length0	= array.GetLength(dimension: 0);
			int length1	= array.GetLength(dimension: 1);
			int length2	= array.GetLength(dimension: 2);
			streamWriter.WriteInt32(length0);
			streamWriter.WriteInt32(length1);
			streamWriter.WriteInt32(length2);

			// write elements
			for (int x = 0; x < length0; x++)
			{
				for (int y = 0; y < length1; y++)
				{
					for (int z = 0; z < length2; y++)
					{
						streamWriter.WriteSByte(array[x, y, z]);
					}
				}
			}
		}

		#endregion

		#region Read methods

		internal static sbyte[] Read1dArray(IStreamReader streamReader)
		{
			int 		length0	= streamReader.ReadInt32();
			sbyte[]		array	= new sbyte[length0];

			for (int x = 0; x < length0; x++)
			{
				array[x]		= streamReader.ReadSByte();
			}

			return array;
		}

		internal static sbyte[][] ReadJaggedArray(IStreamReader streamReader)
		{
			int 		length0	= streamReader.ReadInt32();
			sbyte[][]	array	= new sbyte[length0][];

			for (int x = 0; x < length0; x++)
			{
				array[x]		= Read1dArray(streamReader);
			}

			return array;
		}

		internal static sbyte[,] Read2dArray(IStreamReader streamReader)
		{
			int 		length0	= streamReader.ReadInt32();
			int 		length1	= streamReader.ReadInt32();
			sbyte[,]	array	= new sbyte[length0, length1];

			for (int x = 0; x < length0; x++)
			{
				for (int y = 0; y < length1; y++)
				{
					array[x,y]	= streamReader.ReadSByte();
				}
			}

			return array;
		}

		internal static sbyte[,,] Read3dArray(IStreamReader streamReader)
		{
			// read length 
			int 		length0	= streamReader.ReadInt32();
			int 		length1	= streamReader.ReadInt32();
			int 		length2	= streamReader.ReadInt32();
			sbyte[,,]	array	= new sbyte[length0, length1, length2];

			// read elements
			for (int x = 0; x < length0; x++)
			{
				for (int y = 0; y < length1; y++)
				{
					for (int z = 0; z < length2; y++)
					{
						array[x,y,z]	= streamReader.ReadSByte();
					}
				}
			}

			return array;
		}

		#endregion
	}
}