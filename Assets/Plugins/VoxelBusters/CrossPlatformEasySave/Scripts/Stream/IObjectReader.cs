using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Type = System.Type;

namespace VoxelBusters.Serialization
{
	public interface IObjectReader 
	{
		#region Read methods

		object ReadProperty(Type type);
		object ReadProperty(string name, Type type);
		T ReadProperty<T>();
		T ReadProperty<T>(string name);

		int ReadArrayLength(int dimension);

		Type GetObjectType();
		bool HasNext();

		#endregion
	}
}