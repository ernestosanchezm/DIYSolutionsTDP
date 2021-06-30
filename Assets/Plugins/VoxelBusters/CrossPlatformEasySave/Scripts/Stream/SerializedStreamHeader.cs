using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	internal class SerializedStreamHeader 
	{
		#region Properties

		public byte SerializedVersion
		{
			get;
			private set;
		}

		public SerializationMethodOptions SerializationMethod
		{
			get;
			private set;
		}

		public long TotalLength
		{
			get;
			set;
		}

		#endregion

		#region Constructors

		internal SerializedStreamHeader(byte serializedVersion, SerializationMethodOptions serializationMethod)
		{
			// set properties
			SerializedVersion 		= serializedVersion;
			SerializationMethod 	= serializationMethod;
		}

		#endregion
	}
}