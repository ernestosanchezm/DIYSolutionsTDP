using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace VoxelBusters.Serialization.Examples
{
	[System.Serializable]
	public class Sample 
	{
		#region Fields

		public IShape[] 					shapes;
		public Dictionary<string, int> 		randomCollection;
		public List<string> 				stringList;

		#endregion

		#region Constructors

		public Sample()
		{}

		#endregion

		#region Public methods

		public void SetRandomValues()
		{
			// set properties
			shapes	= new IShape[] 
			{
				new Circle() 
				{ 
					radius	= Random.Range(1, 1000) 
				},
				new Square() 
				{ 
					length	= Random.Range(1, 1000) 
				},
				new Rectangle() 
				{ 
					length	= Random.Range(1, 1000), 
					breadth	= Random.Range(1, 1000) 
				},
			};
			randomCollection = new Dictionary<string, int>();
			for (int iter = 0; iter < Random.Range(1, 5); iter++)
			{
				randomCollection[IOServices.GetRandomFileName()]	= Random.Range(1, 1000);
			}

			stringList = new List<string>();
			for (int iter = 0; iter < Random.Range(5, 10); iter++)
			{
				stringList.Add(IOServices.GetRandomFileName());
			}
		}

		#endregion

		#region System.Object implementation

		public override string ToString()
		{
			StringBuilder textBuilder = new StringBuilder();
			textBuilder.AppendFormat("[Sample] Shapes: {0}\n", shapes.GetPrintableString());
			textBuilder.AppendFormat("RandomCollection: {0}\n", randomCollection.GetPrintableString());
			textBuilder.AppendFormat("StringList: {0}", stringList.GetPrintableString());

			return textBuilder.ToString();
		}

		#endregion
	}

	public interface IShape 
	{
		#region Methods

		float GetTotalArea();

		#endregion
	}

	[System.Serializable]
	public class Circle : IShape 
	{
		#region Fields

		public		int 		radius;

		#endregion

		#region IShape methods

		public float GetTotalArea()
		{
			return (2 * Mathf.PI * radius);
		}

		#endregion

		#region System.Object implementation

		public override string ToString()
		{
			return string.Format("[Circle] Radius: {0}", radius);
		}

		#endregion
	}

	[System.Serializable]
	public class Square : IShape 
	{
		#region Fields

		public 		float 		length;

		#endregion

		#region IShape methods

		public float GetTotalArea()
		{
			return (length * length);
		}

		#endregion

		#region System.Object implementation

		public override string ToString()
		{
			return string.Format("[Square] Length: {0}", length);
		}

		#endregion
	}

	[System.Serializable]
	public class Rectangle : IShape 
	{
		#region Fields

		public 		float 		length;
		public 		float 		breadth;
					
		#endregion

		#region IShape methods

		public float GetTotalArea()
		{
			return (length * breadth);
		}

		#endregion

		#region System.Object implementation

		public override string ToString()
		{
			return string.Format("[Rectangle] Length: {0} Breadth: {1}", length, breadth);
		}

		#endregion
	}
}