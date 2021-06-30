using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Type = System.Type;

namespace VoxelBusters.Serialization
{
	public class TypeNotSupportedException : System.Exception 
	{
		#region Constructors

		public TypeNotSupportedException(Type type)
			: base("")
		{}

		#endregion
	}
}