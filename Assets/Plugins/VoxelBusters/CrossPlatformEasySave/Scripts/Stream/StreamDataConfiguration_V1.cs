using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	internal class StreamDataConfiguration_V1 : IStreamDataConfiguration 
	{
		#region IStreamDataConfiguration implementation

		public HeaderStructure Header
		{
			get;
			private set;
		}

		public FooterStructure Footer
		{
			get;
			private set;
		}

		#endregion

		#region Constructors

		public StreamDataConfiguration_V1()
		{
			// initialise
			Header				= new HeaderStructure() 
			{
				FixedSize		= 6,
			};
		}

		#endregion
	}
}