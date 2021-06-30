﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Array = System.Array;

namespace VoxelBusters.Serialization
{
	internal class StringArray 
	{
		#region Internal methods

		internal static void Write1dArray(string[] array, IStreamWriter streamWriter)
		{
			int length0	= array.Length;
			streamWriter.WriteInt32(length0);

			for (int x = 0; x < length0; x++)
			{
				streamWriter.WriteString(array[x]);
			}
		}

		internal static void WriteJaggedArray(string[][] array, IStreamWriter streamWriter)
		{
			int length0	= array.GetLength(dimension: 0);
			streamWriter.WriteInt32(length0);

			for (int x = 0; x < length0; x++)
			{
				Write1dArray(array[x], streamWriter);
			}
		}

		internal static void Write2dArray(string[,] array, IStreamWriter streamWriter)
		{
			int length0	= array.GetLength(dimension: 0);
			int length1	= array.GetLength(dimension: 1);
			streamWriter.WriteInt32(length0);
			streamWriter.WriteInt32(length1);

			for (int x = 0; x < length0; x++)
			{
				for (int y = 0; y < length1; y++)
				{
					streamWriter.WriteString(array[x, y]);
				}
			}
		}

		internal static void Write3dArray(string[,,] array, int rank, IStreamWriter streamWriter)
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
						streamWriter.WriteString(array[x, y, z]);
					}
				}
			}
		}

		#endregion

		#region Read methods

		internal static string[] Read1dArray(IStreamReader streamReader)
		{
			int 		length0	= streamReader.ReadInt32();
			string[]	array	= new string[length0];

			for (int x = 0; x < length0; x++)
			{
				array[x]		= streamReader.ReadString();
			}

			return array;
		}

		internal static string[][] ReadJaggedArray(IStreamReader streamReader)
		{
			int 		length0	= streamReader.ReadInt32();
			string[][]	array	= new string[length0][];

			for (int x = 0; x < length0; x++)
			{
				array[x]		= Read1dArray(streamReader);
			}

			return array;
		}

		internal static string[,] Read2dArray(IStreamReader streamReader)
		{
			int 		length0	= streamReader.ReadInt32();
			int 		length1	= streamReader.ReadInt32();
			string[,]	array	= new string[length0, length1];

			for (int x = 0; x < length0; x++)
			{
				for (int y = 0; y < length1; y++)
				{
					array[x,y]	= streamReader.ReadString();
				}
			}

			return array;
		}

		internal static string[,,] Read3dArray(IStreamReader streamReader)
		{
			// read length 
			int 		length0	= streamReader.ReadInt32();
			int 		length1	= streamReader.ReadInt32();
			int 		length2	= streamReader.ReadInt32();
			string[,,]	array	= new string[length0, length1, length2];

			// read elements
			for (int x = 0; x < length0; x++)
			{
				for (int y = 0; y < length1; y++)
				{
					for (int z = 0; z < length2; y++)
					{
						array[x,y,z]	= streamReader.ReadString();
					}
				}
			}

			return array;
		}

		#endregion
	}
}