using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;

using ArgumentNullException = System.ArgumentNullException;
using NotSupportedException = System.NotSupportedException;

namespace VoxelBusters.Serialization
{
    internal struct DeclaredMemberInfo 
	{
		#region Fields

		private		MemberInfo		m_member;
		private		MemberTypes		m_memberType;

		#endregion

		#region Properties

		public bool IsSerializable
		{
			get;
			private set;
		}

		public bool IsObsolete
		{
			get
			{
				return ReflectionServices.IsObsolete(m_member);
			}
		}

		public bool IsStatic
		{
			get
			{
				switch (m_memberType)
				{
					case MemberTypes.Field:
						return GetField().IsStatic;

					case MemberTypes.Property:
						MethodInfo setter = GetProperty().GetSetMethod();
						return (setter != null && setter.IsStatic);

					default:
						throw new NotSupportedException();
				}
			}
		}

		public string Name
		{
			get
			{
				return m_member.Name;
			}
		}

		public Type UnderlyingType
		{
			get
			{
				switch (m_memberType)
				{
					case MemberTypes.Field:
						return GetField().FieldType;

					case MemberTypes.Property:
						return GetProperty().PropertyType;

					default:
						throw new NotSupportedException();
				}
			}
		}

		public Type DeclaringType
		{
			get
			{
				return m_member.DeclaringType;
			}
		}

		public Type ReflectedType
		{
			get
			{
				return m_member.ReflectedType;
			}
		}

		#endregion

		#region Constructors

		private DeclaredMemberInfo(MemberInfo member, MemberTypes memberType)
		{
			// set values
			m_member 		= member;
			m_memberType 	= memberType;
			IsSerializable	= IsSerializableMember(member, memberType);
		}

		#endregion

		#region Create methods

		public static DeclaredMemberInfo Create(MemberInfo member)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}

			MemberTypes	memberType	= (member is FieldInfo) ? MemberTypes.Field : MemberTypes.Property;
			return new DeclaredMemberInfo(member, memberType);
		}

		#endregion

		#region Static methods

		private static bool IsSerializableMember(MemberInfo member, MemberTypes memberType)
		{
			switch (memberType)
			{
				case MemberTypes.Field:
					return !((FieldInfo)member).IsNotSerialized;

				case MemberTypes.Property:
					PropertyInfo property	= (PropertyInfo)member;
					return (property.CanRead && property.CanWrite && (property.GetIndexParameters().Length == 0));

				default:
					return false;
			}
		}

		#endregion

		#region Getter/Setter methods

		public object GetValue(object obj)
		{
			// validate passed object
			if (obj == null && !IsStatic)
			{
				throw new ArgumentNullException("obj");
			}

			// get value from available source
			switch (m_memberType)
			{
				case MemberTypes.Field:
					return GetField().GetValue(obj);

				case MemberTypes.Property:
					return GetProperty().GetValue(obj, null);

				default:
					return null;
			}
		}

		public void SetValue(object obj, object value, object[] index = null)
		{
			switch (m_memberType)
			{
				case MemberTypes.Field:
					GetField().SetValue(obj, value);
					break;

				case MemberTypes.Property:
					GetProperty().SetValue(obj, value, index);
					break;

				default:
					break;
			}
		}

		#endregion

		#region System.Object methods

		public override string ToString()
		{
			return string.Format("[DeclaredMemberInfo: Name={0}, IsSerializable={1}, UnderlyingType={2}]", Name, IsSerializable, UnderlyingType);
		}

		#endregion

		#region Private methods

		private PropertyInfo GetProperty()
		{
			return (PropertyInfo)m_member;
		}

		private FieldInfo GetField()
		{
			return (FieldInfo)m_member;
		}
		                     
		#endregion
	}
}