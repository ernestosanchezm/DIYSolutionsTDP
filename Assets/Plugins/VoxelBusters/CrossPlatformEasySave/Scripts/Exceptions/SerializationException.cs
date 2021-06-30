using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class SerializationException : System.Exception 
	{
		#region Constructors

		public SerializationException(string message) 
            : base(message)
		{}

		#endregion
	}
}