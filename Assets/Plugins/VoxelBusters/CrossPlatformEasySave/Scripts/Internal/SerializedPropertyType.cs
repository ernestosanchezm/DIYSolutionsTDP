using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	[System.Serializable]
	public enum SerializedPropertyType : byte
	{
		Unknown = 0,
		NotSupported,
		Primitive,
		String,
		Enum,
		ObjectRef,
		ResourceRef,
		Null,
		Value = 1 << 4,
		Object,
		Generic,
		ArrayOfPrimitive = 1 << 5,
		ArrayOfString,
		ArrayOfEnum,
		ArrayOfValue,
		ArrayOfObject,
		ArrayOfGeneric,
	}
}