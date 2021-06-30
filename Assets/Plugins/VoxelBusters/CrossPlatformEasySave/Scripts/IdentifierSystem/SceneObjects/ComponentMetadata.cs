using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	[System.Serializable]
	internal class ComponentMetadata : Metadata<Component>
	{
		#region Constructors

		public ComponentMetadata(Component component) 
			: base(component)
		{}

		public ComponentMetadata(Component component, string guid) 
			: base(component, guid)
		{}

		#endregion
	}
}