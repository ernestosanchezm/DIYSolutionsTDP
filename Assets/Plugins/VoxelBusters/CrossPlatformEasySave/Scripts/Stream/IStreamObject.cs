using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Type = System.Type;

namespace VoxelBusters.Serialization
{
	public interface IStreamObject 
	{
		#region Properties

		long StartPosition
		{
			get;
		}

		long DataPosition
		{
			get;
		}

		long TotalLength
		{
			get;
		}

		Type DeclaredType
		{
			get;
		}

		SerializedPropertyType Type
		{
			get;
		}

		#endregion

		#region Methods

		IStreamObject FindProperty(string name);
		IStreamObject Next();
		bool HasNext();

		#endregion
	}
}