﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace VoxelBusters.Serialization
{
	[CustomPropertyDrawer(typeof(ResourceObject))]
	public class ResourceObjectDrawer : MetadataDrawerBase
	{}
}