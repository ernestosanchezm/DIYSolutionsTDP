using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	internal class StreamDataConfiguration 
	{
		#region Properties

		private static IStreamDataConfiguration v1 = new StreamDataConfiguration_V1();

		#endregion

		#region Internal methods

		internal static IStreamDataConfiguration GetConfiguration(int version)
		{
			switch (version)
			{
				case 1:
				default:
					return v1;
			}
		}

		#endregion

	}
}