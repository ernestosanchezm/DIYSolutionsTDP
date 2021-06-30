using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	internal class SerializationRecord
	{
		#region Properties

		public string Name
		{
			get;
			private set;
		}

		public StorageTarget StorageTarget
		{
			get;
			private set;
		}

		#endregion

		#region Constructors

		public SerializationRecord(string name, StorageTarget target)
		{
			// set properties
			Name				= name;
			StorageTarget		= target;
		}

		public SerializationRecord(string name, string data)
		{
			// set properties
			Name				= name;
			StorageTarget		= (StorageTarget)int.Parse(data);
		}

		#endregion

		#region Public methods

		public string GetData()
		{
			return ((int)StorageTarget).ToString();
		}

		#endregion
	}
}