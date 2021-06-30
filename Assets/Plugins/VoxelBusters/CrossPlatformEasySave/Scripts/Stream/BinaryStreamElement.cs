using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	internal enum BinaryStreamElement : byte
	{
		Invalid	= 0,
		Header,
		Data,
		Assembly,
		Type,
		Primitive,
		String,
		Enum,
		ObjectRef,
		ResourceRef,
		Value,
		ValueWithSchema,
		Object,
		ObjectWithSchema,
		ArrayOfPrimitive,
		ArrayOfString,
		Array,
		NotSupported,
		Null,
		Footer,
		End,
	}
}