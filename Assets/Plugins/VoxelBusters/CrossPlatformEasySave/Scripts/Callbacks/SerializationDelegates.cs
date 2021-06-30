using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public delegate void SerializeEndCallback(string name, SerializationContext context);
	public delegate void DeserializeEndCallback(string name, SerializationContext context);
}