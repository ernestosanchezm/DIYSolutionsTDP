using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	internal static class DebugUtility 
	{
		#region Print methods

		internal static string GetPrintableString<T>(this T[] array)
		{
			if (null == array)
			{
				return "NULL";
			}

			StringBuilder 	stringBuilder	= new StringBuilder(capacity: 32);
			bool 			skip			= true;
			for (int iter = 0; iter < array.Length; iter++)
			{
				if (false == skip)
				{
					stringBuilder.AppendLine();
				}

				skip	= false;
				stringBuilder.AppendFormat("[{0}]: {1}", iter, array[iter]);
			}
			return stringBuilder.ToString();
		}

		internal static string GetPrintableString<T>(this IList<T> list)
		{
			if (null == list)
			{
				return "NULL";
			}

			StringBuilder 	stringBuilder	= new StringBuilder(capacity: 32);
			bool 			skip			= true;
			for (int iter = 0; iter < list.Count; iter++)
			{
				if (false == skip)
				{
					stringBuilder.AppendLine();
				}

				skip	= false;
				stringBuilder.AppendFormat("[{0}]: {1}", iter, list[iter]);
			}
			return stringBuilder.ToString();
		}

		internal static string GetPrintableString<K, V>(this IDictionary<K, V> dictionary)
		{
			if (null == dictionary)
			{
				return "NULL";
			}

			StringBuilder 	stringBuilder	= new StringBuilder(capacity: 32);
			bool 			skip			= true;
			foreach (KeyValuePair<K, V> kvPair in dictionary)
			{
				if (false == skip)
				{
					stringBuilder.AppendLine();
				}

				skip	= false;
				stringBuilder.AppendFormat("{0}: {1}", kvPair.Key, kvPair.Value);
			}
			return stringBuilder.ToString();
		}

		#endregion
	}
}