using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization.Benchmark
{
	public struct SampleStruct
	{
		#region Properties

		public int IntVar 
		{ 
			get; 
			set; 
		}

		public float FloatVar 
		{ 
			get; 
			set; 
		}

		public string StringVar 
		{ 
			get; 
			set; 
		}

		#endregion

		#region Constructors

		public SampleStruct(int intVar, float floatVar, string stringVar)
		{
			// set properties
			IntVar 		= intVar;
			FloatVar	= floatVar;
			StringVar 	= stringVar;
		}

		#endregion
	}

	public class SampleClass
	{
		#region Properties

		public int IntVar 
		{ 
			get; 
			set; 
		}

		public float FloatVar 
		{ 
			get; 
			set; 
		}

		public string StringVar 
		{ 
			get; 
			set; 
		}

		#endregion

		#region Constructors

		public SampleClass(int intVar, float floatVar, string stringVar)
		{
			// set properties
			IntVar 		= intVar;
			FloatVar	= floatVar;
			StringVar 	= stringVar;
		}

		#endregion
	}

	[System.Serializable]
	public struct CustomVec3
	{
		#region Properties
		public float X 
		{ 
			get; 
			set; 
		}

		public float Y 
		{ 
			get; 
			set; 
		}

		public float Z 
		{ 
			get; 
			set; 
		}

		#endregion

		#region Constructors

		public CustomVec3(float x, float y, float z)
		{
			// set properties
			X = x;
			Y = y;
			Z = z;
		}

		#endregion

		#region Object methods

		public override string ToString()
		{
			return string.Format("[CustomVec3: X={0}, Y={1}, Z={2}]", X, Y, Z);
		}

		#endregion
	}


	[System.Serializable]
	public class Person : System.IEquatable<Person>
	{
		#region Properties

		public virtual int Age 
		{ 
			get; 
			set; 
		}

		public virtual string FirstName 
		{ 
			get; 
			set; 
		}

		public virtual string LastName 
		{ 
			get; 
			set; 
		}

		public virtual Sex Sex 
		{ 
			get; 
			set; 
		}

		#endregion

		#region IEquatable implementation

		public bool Equals(Person other)
		{
			if (null == other)
				return false;

			return Age == other.Age && FirstName == other.FirstName && LastName == other.LastName && Sex == other.Sex;
		}

		#endregion

		#region Object methods

		public override string ToString()
		{
			return string.Format("[Person: Age={0}, FirstName={1}, LastName={2}, Sex={3}]", Age, FirstName, LastName, Sex);
		}

		#endregion
	}

	[System.Serializable]
	public enum Sex : sbyte
	{
		Unknown, Male, Female,
	}
}