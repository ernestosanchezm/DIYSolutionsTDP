using UnityEngine;

using SystemGuid = System.Guid;
using Type = System.Type;

namespace VoxelBusters.Serialization
{
	internal class Metadata<T>
    {
		#region Fields

		[SerializeField]
		private 	T 			m_object;
		[SerializeField]
		private 	string 		m_guid;

		#endregion
        
		#region Properties

        public T Object
        {
            get
            {
                return (T)m_object;
            }
        }

        public string Guid
        {
            get
            {
                return m_guid;
            }
        }

		public Type ObjectType
		{
			get
			{
				return m_object.GetType();
			}
		}

		#endregion

		#region Constructors

        public Metadata(T metaObject)
        {
			// set values
            m_object 	= metaObject;
			m_guid		= SystemGuid.NewGuid().ToString("N");
        }

		public Metadata(T metaObject, string guid)
        {
			// assign values
            m_object 	= metaObject;
            m_guid 		= guid;
        }

		#endregion
    }
}