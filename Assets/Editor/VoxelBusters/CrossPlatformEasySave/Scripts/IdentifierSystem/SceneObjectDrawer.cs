using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace VoxelBusters.Serialization
{
	[CustomPropertyDrawer(typeof(SceneObject))]
	public class SceneObjectDrawer : MetadataDrawerBase
	{}
}