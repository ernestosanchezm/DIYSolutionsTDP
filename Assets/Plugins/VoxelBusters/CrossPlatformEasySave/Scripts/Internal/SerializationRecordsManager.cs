using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	internal class SerializationRecordsManager 
	{
		#region Defaults

		private class Defaults 
		{
			internal 	const 		string 		kSerializationLogs		= "serialization-logs";
		}

		#endregion

		#region Static fields

		private 	static 	Dictionary<string, SerializationRecord>	records = null;

		#endregion

		#region Static constructors

		static SerializationRecordsManager()
		{
			// set properties
			records	= LoadRecords();
		}

		#endregion

		#region History methods

		internal static void LogSerialization(SerializationRecord record)
		{
			// add entry
			records[record.Name] 	= record;
			Save();
		}

		internal static bool ContainsRecord(string name)
		{
			return records.ContainsKey(name);
		}

		internal static bool TryGetRecord(string name, out SerializationRecord record)
		{
			return records.TryGetValue(name, out record);
		}

		internal static SerializationRecordsEnumerator GetEnumerator()
		{
			return new SerializationRecordsEnumerator(records.GetEnumerator());
		}

		internal static void Delete(string name)
		{
			records.Clear();
			Save();
		}

		internal static void DeleteAll()
		{
			PlayerPrefs.DeleteKey(Defaults.kSerializationLogs);
		}

		#endregion

		#region Private static methods

		private static void Save()
		{
			StringBuilder	stringBuilder 	= new StringBuilder(capacity: 24);
			if (records != null)
			{
				bool					appendSeperator	= false;
				IDictionaryEnumerator 	enumeraor 		= records.GetEnumerator();
				while (enumeraor.MoveNext())
				{
					SerializationRecord record	= (SerializationRecord)enumeraor.Value;
					if (appendSeperator)
					{	
						stringBuilder.Append(',');
					}
					stringBuilder.AppendFormat("{0}:{1}", record.Name, record.GetData());
					appendSeperator				= true;
				}
			}

			PlayerPrefs.SetString(Defaults.kSerializationLogs, stringBuilder.ToString());
		}

		private static Dictionary<string, SerializationRecord> LoadRecords()
		{
			string			dataStr		= PlayerPrefs.GetString(Defaults.kSerializationLogs, null);
			if (!string.IsNullOrEmpty(dataStr))
			{
				string[]	dataParts	= dataStr.Split(',');
				int 		count 		= dataParts.Length;

				Dictionary<string, SerializationRecord> newCollection = new Dictionary<string, SerializationRecord>(capacity: Mathf.Max(count, 4));
				for (int iter = 0; iter < dataParts.Length; iter++)
				{
					string[]	entryParts	= dataParts[iter].Split(':');
					string 		key 		= entryParts[0];
					newCollection.Add(key, new SerializationRecord(key, entryParts[1]));
				}

				return newCollection;
			}

			return new Dictionary<string, SerializationRecord>(capacity: 4);
		}

		#endregion
	}
}