using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public abstract class SerializationDataProvider<T> : ISerializationDataProvider, ISerializationDataProvider<T>
	{
		#region Static fields

		private 	static 		SerializationDataProvider<T> 	defaultInstance;

		#endregion

		#region Static properties

		public static SerializationDataProvider<T> Default
		{
			get
			{
				if (defaultInstance == null)
				{
                    defaultInstance = CreateDefaultInstance();
				}

				return defaultInstance;
			}
		}

		#endregion

		#region Abstract methods

		public abstract void Serialize(T obj, IObjectWriter writer, SerializationContext context);

		public abstract T CreateInstance(IObjectReader reader, SerializationContext context);
		public abstract T Deserialize(T obj, IObjectReader reader, SerializationContext context);

		#endregion

		#region ISerializationDataProvider implementation

		void ISerializationDataProvider.Serialize(object obj, IObjectWriter writer, SerializationContext context)
		{
			Serialize((T)obj, writer, context);
		}

		object ISerializationDataProvider.CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return (object)CreateInstance(reader, context);
		}

		object ISerializationDataProvider.Deserialize(object obj, IObjectReader reader, SerializationContext context)
		{
			return (object)Deserialize((T)obj, reader, context);
		}

		#endregion

        #region Private methods

        private static SerializationDataProvider<T> CreateDefaultInstance()
        {
            return SerializationDataProviderServices.LoadFromCacheOrCreateDataProvider<T>();
        }

        #endregion
	}
}