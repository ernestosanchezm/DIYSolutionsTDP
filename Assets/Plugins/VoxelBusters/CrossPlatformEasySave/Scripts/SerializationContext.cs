using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using VoxelBusters.External.SystemUtils;
using VoxelBusters.External.Utils;

using Type = System.Type;
using ArgumentNullException = System.ArgumentNullException;

namespace VoxelBusters.Serialization
{
	public class SerializationContext
	{
		#region Defaults

		private class Defaults
		{
			internal	const		string 						kUndefinedStringValue				= null;
			internal	const		int 						kObjectCacheInitialCapacity			= 4;
			internal 	const		long						kObjectIdStartsFrom					= (SerializationKnownTypes.kReservedId + 1);
		}

		#endregion

		#region Properties

		internal SerializationMode SerializationMode
		{
			get;
			private set;
		}

		public byte SerializedVersion
		{
			get;
			internal set;
		}

		public SerializationMethodOptions SerializationMethod
		{
			get;
			internal set;
		}

		public StorageTarget StorageTarget
		{
			get;
			internal set;
		}

		public string Tag
		{
			get;
			internal set;
		}

		#endregion

		#region Constructors

		internal SerializationContext(SerializationMode serializationMode, Settings settings, string tag)
			: this(serializationMode, settings.SerializationMethod, settings.StorageTarget, tag)
		{}

		internal SerializationContext(SerializationMode serializationMode,  StorageTarget storageTarget, string tag)
			: this(serializationMode, SerializationMethodOptions.Default, storageTarget, tag)
		{}

		internal SerializationContext(SerializationMode serializationMode, SerializationMethodOptions serializationMethod, 
		                              StorageTarget storageTarget, string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag is null.");
			}

			// set properties
			SerializedVersion		= 0;
			SerializationMode		= serializationMode;
			SerializationMethod		= serializationMethod;
			StorageTarget			= storageTarget;
			Tag						= tag;
		}

		~SerializationContext()
		{}

		#endregion

		#region Public methods

		public bool Supports(SerializationMethodOptions option)
		{
			return SerializationMethod.Contains(option);
		}

		#endregion

		#region Internal methods

		internal StorageTarget GetStorageTargetForCurrentPlatform()
		{
			if (StorageTarget.LocalDisk == StorageTarget)
			{
				if (false == SerializationEnvironment.IsFileStreamSupported())
				{
					return StorageTarget.PlayerPrefs;
				}
			}
			return StorageTarget;
		}

		#endregion
	}
}