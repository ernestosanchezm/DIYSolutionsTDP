using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Attribute = System.Attribute;

namespace VoxelBusters.External.UnityEngineUtils
{
	public class AssetCollectionFolderAttribute : Attribute
	{
		#region Properties

		public string FolderPath
		{
			get;
			private set;
		}

		#endregion

		#region Constructors

		public AssetCollectionFolderAttribute(string folderPath)
		{
			// set property
			FolderPath	= folderPath;
		}

		#endregion
	}
}