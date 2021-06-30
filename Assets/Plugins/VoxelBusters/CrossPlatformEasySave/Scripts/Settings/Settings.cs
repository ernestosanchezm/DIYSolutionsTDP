using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using VoxelBusters.External.UnityEngineUtils;

using SerializationOptionType = VoxelBusters.Serialization.SerializationOption.SerializationOptionType;

namespace VoxelBusters.Serialization
{
	[System.Serializable]
	public struct Settings
	{
		#region Fields

		[SerializeField]
		private			int 						m_bufferSize;
		[SerializeField, EnumMaskField(typeof(SerializationMethodOptions))]
		private			SerializationMethodOptions	m_serializationMethod;
		[SerializeField]
		private			StorageTarget				m_storageTarget;

		#endregion

		#region Properties

		public int BufferSize
		{
			get
			{
				return m_bufferSize;
			}
			set
			{
				// validate request
				if (0 == value)
				{
					Debug.Log("[Serialization] Buffer size value provided is invalid.");
					return;
				}

				// assign value
				m_bufferSize	= value;
			}
		}

		public SerializationMethodOptions SerializationMethod
		{
			get
			{
				return m_serializationMethod;
			}
			set
			{
				// assign value
				m_serializationMethod	= value;
			}
		}

		public StorageTarget StorageTarget
		{
			get
			{
				return m_storageTarget;
			}
			set
			{
				// validate request
				if (StorageTarget.Unknown == value)
				{
					Debug.Log("[Serialization] Storage target value provided is invalid.");
					return;
				}

				// assign value
				m_storageTarget	= value;
			}
		}

		#endregion

		#region Create methods

		public static Settings Create(SerializationOption[] options, bool useDefaultValues)
		{
			Settings newSettings;
			if (useDefaultValues)
			{
				newSettings		= SerializationSettings.Copy();
			}
			else
			{
				newSettings		= new Settings() 
				{
					m_bufferSize 			= 0,
					m_serializationMethod	= SerializationMethodOptions.Default,
					m_storageTarget 		= StorageTarget.Unknown,
				};
			}
			newSettings.CopyValuesFromSerializationOptions(options);

			return newSettings;
		}

		#endregion

		#region Public methods

		public SerializationOption[] ToOptions()
		{
			return new SerializationOption[] 
			{
				new SerializationOption(SerializationOptionType.BufferSize, 			m_bufferSize),
				new SerializationOption(SerializationOptionType.SerializationMethod,	m_serializationMethod),
				new SerializationOption(SerializationOptionType.Storage, 				m_storageTarget),
			};
		}

		#endregion

		#region Private methods

		private void CopyValuesFromSerializationOptions(SerializationOption[] options)
		{
			foreach (SerializationOption option in options)
			{
				switch (option.Type)
				{
					case SerializationOptionType.BufferSize:
						BufferSize 			= (int)option.Value;
						break;

					case SerializationOptionType.SerializationMethod:
						SerializationMethod	= (SerializationMethodOptions)option.Value;
						break;

					case SerializationOptionType.Storage:
						StorageTarget		= (StorageTarget)option.Value;
						break;

					default:
						break;
				}
			}
		}

		#endregion
	}
}