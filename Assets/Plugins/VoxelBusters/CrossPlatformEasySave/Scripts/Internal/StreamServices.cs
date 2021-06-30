using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NotSupportedException = System.NotSupportedException;

namespace VoxelBusters.Serialization
{
	internal class StreamServices 
	{
		#region Static methods

		internal static void RemoveStreamData(SerializationRecord record)
		{
			switch (record.StorageTarget)
			{
				case StorageTarget.PlayerPrefs:
					PlayerPrefsStream.Delete(record.Name);
					break;

				case StorageTarget.LocalDisk:
					string path = SerializationManager.GetFilePath(record.Name, Constants.kFileExtension);
					IOServices.DeleteFile(path);
					break;

				default:
					throw new NotSupportedException();
			}
		}

		#endregion
	}
}