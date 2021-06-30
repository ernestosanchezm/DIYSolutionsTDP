using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class SerializationInvalidCastException : SerializationException
	{
		#region Constructors

		public SerializationInvalidCastException() 
            : base("")
		{}

		#endregion
	}
}