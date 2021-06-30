using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Type = System.Type;

namespace VoxelBusters.Serialization
{
	internal class StreamMember : StreamObjectBase 
	{
		#region StreamObjectBase implementation

		public override IStreamObject FindProperty(string name)
		{
			throw ErrorCentre.OperationNotSupportedException();
		}

		public override IStreamObject Next()
		{
			throw ErrorCentre.OperationNotSupportedException();
		}

		public override bool HasNext()
		{
			throw ErrorCentre.OperationNotSupportedException();
		}

		#endregion
	}
}