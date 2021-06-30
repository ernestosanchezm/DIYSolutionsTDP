using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Type = System.Type;

namespace VoxelBusters.Serialization
{
	internal class TypeSchema 
	{
		#region Properties

		public Type Type
		{
			get;
			private set;
		}

		public int MemberCount
		{
			get;
			private set;
		}

		public string[] MemberNames
		{
			get;
			private set;
		}

		#endregion

		#region Construtors

		public TypeSchema(Type type)
		{
			// set properties
			Type = type;
		}

		#endregion

		#region Public methods

		public void SetMembers(int memberCount, string[] memberNames)
		{
			// set properties
			MemberCount		= memberCount;
			MemberNames		= memberNames;
		}

		#endregion
	}
}