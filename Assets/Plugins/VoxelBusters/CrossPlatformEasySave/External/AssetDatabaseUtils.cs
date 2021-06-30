#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace VoxelBusters.External.UnityEditorUtils
{
	public class AssetDatabaseUtils 
	{
		#region Folder methods

		public static void CreateFolder(string path)
		{
			// remove file name from path
			string 		fileName	= Path.GetFileName(path);
			if (!string.IsNullOrEmpty(fileName))
			{
				path	= path.Replace("/" + fileName, string.Empty);
			}

			// create folder
			string[]	pathComponents	= path.Split('/');
			string		currentPath		= pathComponents[0];
			if (string.Compare(currentPath, "Assets") != 0)
			{
				throw new System.Exception("Path should be relative to Assets folder.");
			}

			int pIter = 1;
			while (pIter < pathComponents.Length)
			{
				string newPath = (currentPath + "/" + pathComponents[pIter]);
				if (!AssetDatabase.IsValidFolder(newPath))
				{
					AssetDatabase.CreateFolder(currentPath, pathComponents[pIter]);
				}

				currentPath	= newPath;
				pIter++;
			}
		}

		#endregion
	}
}
#endif