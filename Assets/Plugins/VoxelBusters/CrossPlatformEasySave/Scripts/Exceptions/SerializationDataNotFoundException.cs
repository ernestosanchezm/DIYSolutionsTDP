using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class SerializationDataNotFoundException : SerializationException 
	{
		#region Constructors

		public SerializationDataNotFoundException() 
            : this(message: "Serialization data not found.")
		{}

		public SerializationDataNotFoundException(string message) 
            : base(message)
		{}

		#endregion
	}
}