using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	internal class HeaderStructure
	{
		#region Properties

		public long AttributeOffset
		{
			get;
			set;
		}

		public long FixedSize
		{
			get;
			set;
		}

		#endregion
	}
}