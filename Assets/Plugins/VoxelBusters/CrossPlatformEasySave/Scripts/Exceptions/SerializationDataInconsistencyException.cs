using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class SerializationDataInconsistencyException : SerializationException 
	{
		#region Constructors

		public SerializationDataInconsistencyException() 
            : this(string.Empty)
		{}

		public SerializationDataInconsistencyException(string message) 
            : base(message)
		{}

		#endregion
	}
}