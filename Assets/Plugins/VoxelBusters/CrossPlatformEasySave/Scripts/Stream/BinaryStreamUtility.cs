using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SeekOrigin = System.IO.SeekOrigin;

namespace VoxelBusters.Serialization
{
	internal static class BinaryStreamUtility 
	{
		#region Write methods

		internal static void SeekZero(this IStreamWriter writer)
		{
			writer.Seek(offset: 0, origin: SeekOrigin.Begin);
		}

		#endregion

		#region Reader methods

		internal static void SeekZero(this IStreamReader reader)
		{
			reader.Seek(offset: 0, origin: SeekOrigin.Begin);
		}

		internal static void SkipByte(this IStreamReader reader)
		{
			reader.Seek(sizeof(byte), SeekOrigin.Current);
		}

		internal static void SkipInt32(this IStreamReader reader)
		{
			reader.Seek(sizeof(int), SeekOrigin.Current);
		}

		internal static void SkipInt64(this IStreamReader reader)
		{
			reader.Seek(sizeof(long), SeekOrigin.Current);
		}

		internal static void SkipId(this IStreamReader reader)
		{
			reader.Seek(sizeof(long), SeekOrigin.Current);
		}

		#endregion
	}
}