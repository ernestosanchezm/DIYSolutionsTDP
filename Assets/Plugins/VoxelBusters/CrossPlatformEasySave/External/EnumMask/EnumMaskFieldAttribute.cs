using System.Collections;
using UnityEngine;

using Type = System.Type;

namespace VoxelBusters.External.UnityEngineUtils
{
	public class EnumMaskFieldAttribute : PropertyAttribute 
	{
		#region Properties

		public Type EnumType 
		{ 
			get; 
			private set; 
		}

		#endregion

		#region Constructors

		private EnumMaskFieldAttribute()
		{}

		public EnumMaskFieldAttribute(Type type)
		{
			EnumType	= type;
		}

		#endregion
	}
}