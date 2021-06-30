using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public interface ISerializationDataProvider
	{
		#region Methods

		void Serialize(object obj, IObjectWriter writer, SerializationContext context);

		object CreateInstance(IObjectReader reader, SerializationContext context);
		object Deserialize(object obj, IObjectReader reader, SerializationContext context);

		#endregion
	}

	public interface ISerializationDataProvider<T> : ISerializationDataProvider
	{
		#region Methods

		void Serialize(T obj, IObjectWriter writer, SerializationContext context);

		new T CreateInstance(IObjectReader reader, SerializationContext context);
		T Deserialize(T obj, IObjectReader reader, SerializationContext context);

		#endregion
	}
}