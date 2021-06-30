using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	internal enum TypeSchemaKind : byte
	{
		Unknown,
		Value,
		Generic,
		Object,
		Array,
	}
}