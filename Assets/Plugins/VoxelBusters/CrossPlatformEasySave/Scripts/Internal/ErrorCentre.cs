using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Type = System.Type;
using Exception = System.Exception;

namespace VoxelBusters.Serialization
{
	internal class ErrorCentre 
	{
		#region Common exception methods

        internal static Exception Exception(string message)
        {
            return new SerializationException(message);
        }

		internal static Exception StreamClosedException()
		{
			return new SerializationException("Stream is closed.");
		}

		internal static Exception EOFException()
		{
			return new SerializationException("Reached end of file.");
		}

        internal static Exception DataInconsistencyException(string message = null)
		{
            message = message ?? "We couldn't parse the file in expected format.";
            return new SerializationDataInconsistencyException(message);
		}

        internal static Exception TypeNotCompatibleException(Type type, Type savedType)
		{
            return new SerializationException(string.Format("Given type: {0} is not compatible with saved type: {1}.", type, savedType));
		}

		internal static Exception EndOfEnumeratorException()
		{
			return new SerializationException("We have reached end of enumerator.");
		}

		internal static Exception SearchByNameIsNotSupportedException()
		{
			return new SerializationException("Search using name is not supported because object was serialized directly without including property name.");
		}

		internal static Exception PropertyNotHandledException(SerializedPropertyType type)
		{
			return new SerializationException(string.Format("Couldn't find implementation for property of type {0}.", type));
		}

		internal static Exception ArrayRankNotSupportedException(int rank)
		{
			return new SerializationException(string.Format("Array with rank {0} is not supported.", rank));
		}

		internal static Exception OperationNotSupportedException()
		{
			return new SerializationException("This operation is not supported.");
		}

		internal static Exception SerializationCallInconsitencyException()
		{
			return new SerializationException("System has identified an open deserialization block. You need to close the deserialization block before proceeding with serialization.");
		}

		internal static Exception DeserializationCallInconsitencyException()
		{
			return new SerializationException("System has identified an open serialization block. You need to close the serialization block before proceeding with deserialization.");
		}

        internal static Exception DataNotFoundException()
        {
            return new SerializationDataNotFoundException();
        }

		#endregion
	}
}