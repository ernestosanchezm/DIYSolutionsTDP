using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	internal interface IStreamDataConfiguration 
	{
		#region Properties

		HeaderStructure Header
		{
			get;
		}

		FooterStructure Footer
		{
			get;
		}

		#endregion
	}
}