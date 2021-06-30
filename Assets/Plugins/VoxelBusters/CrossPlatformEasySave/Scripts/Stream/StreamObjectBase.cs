using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Type = System.Type;

namespace VoxelBusters.Serialization
{
	internal abstract class StreamObjectBase : IStreamObject 
	{
		#region Abstract methods

		public abstract IStreamObject FindProperty(string name);
		public abstract IStreamObject Next();
		public abstract bool HasNext();

		#endregion

		#region IStreamObject implementation

		public long StartPosition
		{
			get;
			internal set;
		}

		public long DataPosition
		{
			get;
			internal set;
		}

		public long TotalLength
		{
			get;
			internal set;
		}

		public Type DeclaredType
		{
			get;
			internal set;
		}

		public SerializedPropertyType Type
		{
			get;
			internal set;
		}

		#endregion
	}
}