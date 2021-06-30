using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	internal class IOServices 
	{
		#region Directory operations

		internal static void CreateDirectory(string path, bool replace)
		{
			try
			{
				if (DirectoryExists(path))
				{
					if (!replace)
					{
						return;
					}

					DeleteDirectory(path, true);
				}

				Directory.CreateDirectory(path);
			}
			catch (IOException)
			{}
		}

		internal static bool DirectoryExists(string path)
		{
			return Directory.Exists(path);
		}

		internal static void DeleteDirectory(string path, bool recursive)
		{
			try
			{
				Directory.Delete(path, recursive);
			}
			catch (DirectoryNotFoundException)
			{}
		}

		#endregion

		#region File operations

		internal static bool FileExists(string path)
		{
			return File.Exists(path);
		}

		internal static void MoveFile(string pathA, string pathB)
		{
			try
			{
				DeleteFile(pathB);
				File.Move(pathA, pathB);
			}
			catch (IOException)
			{
			}
		}

		internal static void DeleteFile(string path)
		{
			try
			{
				File.Delete(path);
			}
			catch (FileNotFoundException)
			{
			}
		}

		#endregion

		#region Path operations

		internal static string CombinePath(string path1, string path2)
		{
			return Path.Combine(path1, path2);
		}

		internal static string GetRandomFileName()
		{
			return Path.GetRandomFileName();
		}

		internal static string GetFileName(string path)
		{
			return Path.GetFileName(path);
		}

		internal static string GetFileNameWithoutExtension(string path)
		{
			return Path.GetFileNameWithoutExtension(path);
		}

		#endregion
	}
}