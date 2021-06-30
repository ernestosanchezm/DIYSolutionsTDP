using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	/// <summary>
	/// SerializationOption is an object used to pass option which can alter the behaviour of serialization process.
	/// </summary>
	/// <description>
	/// You don't use this struct directly, but create them while using serialization functions.
	/// </description>
	public struct SerializationOption 
	{
		#region Properties

		internal SerializationOptionType Type
		{
			get;
			private set;
		}

		internal object Value
		{
			get;
			private set;
		}

		#endregion

		#region Constructors

		internal SerializationOption(SerializationOptionType type, object value)
		{
			// set property value
			Type 	= type;
			Value	= value;
		}

		#endregion

		#region Nested types

		internal enum SerializationOptionType
		{
			BufferSize,
			SerializationMethod,
			Storage,
		}

		#endregion
	}
}