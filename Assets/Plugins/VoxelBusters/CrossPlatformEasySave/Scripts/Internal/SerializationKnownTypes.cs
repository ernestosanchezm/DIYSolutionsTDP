using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using VoxelBusters.Utils;

using Type = System.Type;
using TypeCode = System.TypeCode;
using NotImplementedException = System.NotImplementedException;

namespace VoxelBusters.Serialization
{
	internal class SerializationKnownTypes 
	{
		#region Constants

		internal	const		long 								kNullId					= 0;
		internal	const		int									kReservedId				= 2047;

		#endregion

		#region Static fields

		private		static 		TwoWayDictionary<long, Assembly>	assemblyCollection;
		private		static 		TwoWayDictionary<long, Type>		typeCollection;

		#endregion

		#region Properties

		internal static Type NullType
		{
			get;
			private set;
		}

		internal static Type BoolType
		{
			get;
			private set;
		}

		internal static Type CharType
		{
			get;
			private set;
		}

		internal static Type SByteType
		{
			get;
			private set;
		}

		internal static Type ByteType
		{
			get;
			private set;
		}

		internal static Type Int16Type
		{
			get;
			private set;
		}

		internal static Type UInt16Type
		{
			get;
			private set;
		}

		internal static Type Int32Type
		{
			get;
			private set;
		}

		internal static Type UInt32Type
		{
			get;
			private set;
		}

		internal static Type Int64Type
		{
			get;
			private set;
		}

		internal static Type UInt64Type
		{
			get;
			private set;
		}

		internal static Type SingleType
		{
			get;
			private set;
		}

		internal static Type DoubleType
		{
			get;
			private set;
		}

		internal static Type DecimalType
		{
			get;
			private set;
		}

		internal static Type StringType
		{
			get;
			private set;
		}

		internal static Type SystemObjectType
		{
			get;
			private set;
		}

        internal static Type IEnumerableType
        {
            get;
            private set;
        }

        internal static Type UnityObjectType
		{
			get;
			private set;
		}

		internal static Type GameObjectType
		{
			get;
			private set;
		}

		internal static Type ComponentType
		{
			get;
			private set;
		}

		#endregion

		#region Static constructors

		static SerializationKnownTypes()
		{
			// set properties
			NullType			= null;
			BoolType			= typeof(bool);
			CharType			= typeof(char);
			SByteType			= typeof(sbyte);
			ByteType			= typeof(byte);
			Int16Type	 		= typeof(short);
			UInt16Type			= typeof(ushort);
			Int32Type			= typeof(int);
			UInt32Type			= typeof(uint);
			Int64Type			= typeof(long);
			UInt64Type			= typeof(ulong);
			SingleType			= typeof(float);
			DoubleType			= typeof(double); 
			DecimalType 		= typeof(decimal);
			StringType			= typeof(string);
			StringType			= typeof(string);
			SystemObjectType	= typeof(System.Object);
            IEnumerableType     = typeof(IEnumerable);

            UnityObjectType     = typeof(UnityEngine.Object);
			GameObjectType		= typeof(UnityEngine.GameObject);
			ComponentType		= typeof(Component);

			CreateCollection();
		}

		#endregion

		#region Assembly methods

		public static bool TryGetAssemblyId(Assembly assembly, out long assemblyId)
		{
			return assemblyCollection.TryGetKey(assembly, out assemblyId);
		}

		public static bool TryGetAssembly(long id, out Assembly assembly)
		{
			return assemblyCollection.TryGetValue(id, out assembly);
		}

		#endregion

		#region Type methods

		public static bool TryGetTypeId(Type type, out long typeId)
		{
			return typeCollection.TryGetKey(type, out typeId);
		}

		public static bool TryGetType(long id, out Type type)
		{
			return typeCollection.TryGetValue(id, out type);
		}

		#endregion

		#region Private static methods

		private static void CreateCollection()
		{
			// build known type collection
			assemblyCollection		= new TwoWayDictionary<long, Assembly>(capacity: 4);
			// assembly index [1..63]
			assemblyCollection.Add(1, 	BoolType.Assembly);
			assemblyCollection.Add(2, 	typeof(SerializationManager).Assembly);
			assemblyCollection.Add(3, 	typeof(GameObject).Assembly);

			// build known type collection
			typeCollection			= new TwoWayDictionary<long, Type>(capacity: 128);
			// system types [64..256]
			typeCollection.Add(64, 	BoolType);
			typeCollection.Add(65, 	CharType);
			typeCollection.Add(66, 	SByteType);
			typeCollection.Add(67, 	ByteType);
			typeCollection.Add(68, 	Int16Type);
			typeCollection.Add(69, 	UInt16Type);
			typeCollection.Add(70, 	Int32Type);
			typeCollection.Add(71, 	UInt32Type);
			typeCollection.Add(72, 	Int64Type);
			typeCollection.Add(73,	UInt64Type);
			typeCollection.Add(74, 	SingleType);
			typeCollection.Add(75, 	DoubleType);
			typeCollection.Add(76, 	DecimalType);
			typeCollection.Add(77, 	StringType);
			typeCollection.Add(78,	SystemObjectType);
			typeCollection.Add(79,	typeof(System.DateTime));
			typeCollection.Add(80,	typeof(System.Guid));
			typeCollection.Add(81,	typeof(System.DateTime));
			typeCollection.Add(82,	typeof(System.TimeSpan));
			typeCollection.Add(83,	typeof(Hashtable));
			typeCollection.Add(84,	typeof(ArrayList));
			typeCollection.Add(85,	typeof(Stack));
			typeCollection.Add(86,	typeof(Queue));

			// unity object types [256..1028]
			typeCollection.Add(256,	UnityObjectType);
			typeCollection.Add(257,	GameObjectType);
			typeCollection.Add(258,	ComponentType);
			typeCollection.Add(259,	typeof(Transform));
			typeCollection.Add(260,	typeof(RectTransform));
			typeCollection.Add(261,	typeof(Behaviour));
			typeCollection.Add(262,	typeof(MonoBehaviour));
			typeCollection.Add(263,	typeof(Camera));
			typeCollection.Add(264,	typeof(Material));
			typeCollection.Add(265,	typeof(Renderer));
			typeCollection.Add(266,	typeof(MeshRenderer));
			typeCollection.Add(267,	typeof(SkinnedMeshRenderer));
			typeCollection.Add(268,	typeof(TrailRenderer));
			typeCollection.Add(269,	typeof(LineRenderer));
			typeCollection.Add(270,	typeof(SpriteRenderer));
			typeCollection.Add(271,	typeof(Texture));
			typeCollection.Add(272,	typeof(Texture2D));
			typeCollection.Add(273,	typeof(Sprite));
			typeCollection.Add(274,	typeof(MeshFilter));
			typeCollection.Add(275,	typeof(Mesh));
			typeCollection.Add(276,	typeof(Skybox));
			typeCollection.Add(277,	typeof(Shader));
			typeCollection.Add(278,	typeof(TextAsset));
			typeCollection.Add(279,	typeof(Rigidbody2D));
			typeCollection.Add(280,	typeof(Collider2D));
			typeCollection.Add(281,	typeof(CircleCollider2D));
			typeCollection.Add(282,	typeof(PolygonCollider2D));
			typeCollection.Add(283,	typeof(BoxCollider2D));
			typeCollection.Add(284,	typeof(EdgeCollider2D));
			typeCollection.Add(285,	typeof(CapsuleCollider2D));
			typeCollection.Add(286,	typeof(CompositeCollider2D));
			typeCollection.Add(287,	typeof(Rigidbody));
			typeCollection.Add(289,	typeof(Collider));
			typeCollection.Add(290,	typeof(SphereCollider));
			typeCollection.Add(291,	typeof(CapsuleCollider));
			typeCollection.Add(292,	typeof(BoxCollider));
			typeCollection.Add(293,	typeof(MeshCollider));
			typeCollection.Add(294,	typeof(WheelCollider));
			typeCollection.Add(295,	typeof(PhysicsMaterial2D));
			typeCollection.Add(296,	typeof(PhysicMaterial));
			typeCollection.Add(297,	typeof(AnimationClip));
			typeCollection.Add(298,	typeof(Animator));
			typeCollection.Add(299,	typeof(RuntimeAnimatorController));
			typeCollection.Add(300,	typeof(AudioListener));
			typeCollection.Add(301,	typeof(AudioSource));
			typeCollection.Add(302,	typeof(AudioClip));
			typeCollection.Add(303,	typeof(Cubemap));
			typeCollection.Add(304,	typeof(TextMesh));
			typeCollection.Add(305,	typeof(Light));
			typeCollection.Add(306,	typeof(Effector2D));
			typeCollection.Add(307,	typeof(AreaEffector2D));
			typeCollection.Add(308,	typeof(PointEffector2D));
			typeCollection.Add(309,	typeof(PlatformEffector2D));
			typeCollection.Add(310,	typeof(SurfaceEffector2D));
			typeCollection.Add(311,	typeof(Color));
			typeCollection.Add(312,	typeof(Color32));
			typeCollection.Add(313,	typeof(Rect));
			typeCollection.Add(314,	typeof(Vector2));
			typeCollection.Add(315,	typeof(Vector3));
			typeCollection.Add(316,	typeof(Vector4));
			typeCollection.Add(317,	typeof(Quaternion));
			typeCollection.Add(318,	typeof(LayerMask));
			typeCollection.Add(319,	typeof(Matrix4x4));

			// custom types [1029..2055]

		}

		#endregion
	}
}