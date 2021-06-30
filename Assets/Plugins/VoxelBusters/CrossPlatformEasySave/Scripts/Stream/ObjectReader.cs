using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using UnityEngine;

using Array = System.Array;
using Type = System.Type;
using Enum = System.Enum;
using Encoding = System.Text.Encoding;
using ArgumentNullException = System.ArgumentNullException;
using IDisposable = System.IDisposable;

namespace VoxelBusters.Serialization
{
	public class ObjectReader : IObjectReader, IDisposable
	{
		#region Fields

		private			IStreamReader	 				m_streamReader;
		private			SerializationContext			m_context;
		private			Dictionary<long, object> 		m_objectIdMap;
		private			Dictionary<long, Type> 			m_typeIdMap;
		private			Dictionary<long, Assembly> 		m_assemblyIdMap;
		private			Dictionary<Type, TypeSchema>	m_typeSchemaMap;

		private			StreamObject					m_dataObject;
		private			StreamObject					m_activeObject;
		private			StreamObject[]					m_readStack;
		private			int								m_readLevel;
		private			int 							m_rootObjectCount;

		private			bool							m_disposed;

		#endregion

        #region Internal properties

        internal Stream Stream
        {
            get
            {
                return m_streamReader.BaseStream;
            }
        }

        #endregion

		#region Events

		public event DeserializeEndCallback onDeserializeEnd; 

		#endregion

		#region Constructors

		public ObjectReader(Stream stream, SerializationContext context)
			: this(stream, Encoding.UTF8, context)
		{}

		public ObjectReader(Stream stream, Encoding encoding, SerializationContext context)
		{
			// check for exceptions
			if (null == stream)
			{
				throw new ArgumentNullException("stream");
			}
			if (null == encoding)
			{
				throw new ArgumentNullException("encoding");
			}
			if (null == context)
			{
				throw new ArgumentNullException("context");
			}
			if (SerializationMode.Deserialize != context.SerializationMode)
			{
				throw ErrorCentre.Exception("Context is not configured for deserialization process.");
			}

			// set properties
			m_streamReader 		= new BinaryStreamReader(stream, encoding);
			m_context 			= context;

			m_disposed			= false;

			PrepareForDeserialize();
		}

		~ObjectReader()
		{
			m_streamReader 		= null;
			m_context 			= null;
		}

		#endregion

		#region Public methods

		public void Close()
		{
            Dispose(disposing: true);
		}

		#endregion

		#region State methods

		private void PrepareForDeserialize()
		{
			// validate data
			if (BinaryStreamElement.Header != (BinaryStreamElement)m_streamReader.Peek())
			{
				throw ErrorCentre.DataInconsistencyException();
			}
			SerializedStreamHeader	streamHeader	= ReadHeader();

			// update configuration 
			m_context.SerializedVersion		= streamHeader.SerializedVersion;
			m_context.SerializationMethod 	= streamHeader.SerializationMethod;

			Parse();
			OnBeforeDeserialize();
		}

		private void OnBeforeDeserialize()
		{
			PushObject(m_dataObject);
		}

		private void Parse()
		{
			bool loop = true;
			while (loop)
			{
				BinaryStreamElement token = (BinaryStreamElement)m_streamReader.ReadByte();
				switch (token)
				{
					case BinaryStreamElement.Data:
						m_dataObject = ParseData();
						break;

					case BinaryStreamElement.Footer:
						ReadFooter();
						break;

					case BinaryStreamElement.End:
						loop = false;
						break;

					default:
						throw ErrorCentre.Exception("Couldn't find method to handle token " + token);
				}
			}
		}

		protected virtual void OnBeforeDeserializeEnd()
		{}

		#endregion

		#region IObjectReader implementation

		public object ReadProperty(string name, Type type)
		{
			try
			{
                IStreamObject 	child	= m_activeObject.FindProperty(name);
				if (TypeServices.CheckTypeCompatiblity(type, child.DeclaredType))
				{
					return ReadPropertyInternal(child);
				}

				throw ErrorCentre.TypeNotCompatibleException(type, child.DeclaredType);
			}
			catch (SerializationException exception)
			{
                Debug.LogWarning(string.Format("Could not read property with name: {0}. Please check description for more information. Error: {1}", name, exception.Message));
                return TypeServices.GetDefault(type);
			}
		}

		public object ReadProperty(Type type)
		{
			try
			{
				IStreamObject 	child	= m_activeObject.Next();
				if (TypeServices.CheckTypeCompatiblity(type, child.DeclaredType))
				{
					return ReadPropertyInternal(child);
				}

				throw ErrorCentre.TypeNotCompatibleException(type, child.DeclaredType);
			}
			catch (SerializationException exception)
			{
                Debug.LogWarning(string.Format("Could not read property. Please check description for more information. Error: {0}", exception.Message));
                return TypeServices.GetDefault(type);
			}
		}

		public T ReadProperty<T>(string name)
		{
			try
			{
				IStreamObject 	child 			= m_activeObject.FindProperty(name);
				Type			type			= typeof(T);
				Type			underlyingType	= child.DeclaredType;
				if (type == underlyingType)
				{
					return ReadPropertyInternal<T>(child);
				}
				else if (TypeServices.CheckTypeCompatiblity(type, underlyingType))
				{
					return (T)ReadPropertyInternal(child);
				}

				throw ErrorCentre.TypeNotCompatibleException(type, child.DeclaredType);
			}
			catch (SerializationException exception)
			{
                Debug.LogWarning(string.Format("Could not read property with name: {0}. Please check description for more information. Error: {1}", name, exception.Message));
				return default(T);
			}
		}

		public T ReadProperty<T>()
		{
			try
			{
				IStreamObject 	child 			= m_activeObject.Next();
				Type			type			= typeof(T);
				Type			underlyingType	= child.DeclaredType;
				if (type == underlyingType)
				{
					return ReadPropertyInternal<T>(child);
				}
				else if (TypeServices.CheckTypeCompatiblity(type, underlyingType))
				{
					return (T)ReadPropertyInternal(child);
				}

				throw ErrorCentre.TypeNotCompatibleException(type, child.DeclaredType);
			}
			catch (SerializationException exception)
			{
                Debug.LogWarning(string.Format("Could not read property. Please check description for more information. Error: {0}", exception.Message));
                return default(T);
			}
		}

		public int ReadArrayLength(int dimension)
		{
			return m_streamReader.ReadInt32();
		}

		public Type GetObjectType()
		{
			return m_activeObject.DeclaredType;
		}

		public bool HasNext()
		{
			return m_activeObject.HasNext();
		}

		private object ReadPropertyInternal(IStreamObject streamObject)
		{
			m_streamReader.Position = streamObject.DataPosition;
			switch (streamObject.Type)
			{
				case SerializedPropertyType.Primitive:
					return BinaryConverter.ReadPrimitiveInternal(type: streamObject.DeclaredType, streamReader: m_streamReader);

				case SerializedPropertyType.String:
					return m_streamReader.ReadString();

				case SerializedPropertyType.Enum:
					return ReadEnumPropertyInternal(type: streamObject.DeclaredType);

				case SerializedPropertyType.ObjectRef:
					return ReadObjectReferencePropertyInternal(type: streamObject.DeclaredType);

				case SerializedPropertyType.ResourceRef:
					return ReadResourceReferencePropertyInternal(type: streamObject.DeclaredType);

				case SerializedPropertyType.NotSupported:
				case SerializedPropertyType.Null:
					return TypeServices.GetDefault(type: streamObject.DeclaredType);

				case SerializedPropertyType.Value:
				case SerializedPropertyType.Object:
					return ReadObject((StreamObject)streamObject);

				case SerializedPropertyType.ArrayOfPrimitive:
					return ReadArrayOfPrimitive((StreamObject)streamObject);

				case SerializedPropertyType.ArrayOfString:
					return ReadArrayOfString((StreamObject)streamObject);

				case SerializedPropertyType.ArrayOfObject:
					return ReadArray((StreamObject)streamObject);

				default:	
					throw ErrorCentre.PropertyNotHandledException(streamObject.Type);
			}
		}

		private T ReadPropertyInternal<T>(IStreamObject streamObject)
		{
			m_streamReader.Position = streamObject.DataPosition;
			switch (streamObject.Type)
			{
				case SerializedPropertyType.Primitive:
					return BinaryConverter.ReadPrimitiveInternal<T>(type: streamObject.DeclaredType, streamReader: m_streamReader);

				case SerializedPropertyType.String:
					return (T)(object)m_streamReader.ReadString();

				case SerializedPropertyType.Enum:
					return ReadEnumPropertyInternal<T>(type: streamObject.DeclaredType);

				case SerializedPropertyType.ObjectRef:
					return (T)ReadObjectReferencePropertyInternal(type: streamObject.DeclaredType);

				case SerializedPropertyType.ResourceRef:
					return (T)ReadResourceReferencePropertyInternal(type: streamObject.DeclaredType);

				case SerializedPropertyType.NotSupported:
				case SerializedPropertyType.Null:
					return default(T);

				case SerializedPropertyType.Value:
				case SerializedPropertyType.Object:
					return ReadObject<T>((StreamObject)streamObject);

				case SerializedPropertyType.ArrayOfPrimitive:
					return (T)ReadArrayOfPrimitive((StreamObject)streamObject);

				case SerializedPropertyType.ArrayOfString:
					return (T)ReadArrayOfString((StreamObject)streamObject);

				case SerializedPropertyType.ArrayOfObject:
					return (T)ReadArray((StreamObject)streamObject);

				default:	
					throw ErrorCentre.PropertyNotHandledException(streamObject.Type);
			}
		}

		#endregion

		#region Read header methods

		private SerializedStreamHeader ReadHeader()
		{
			// read header properties
			m_streamReader.ReadByte();
			byte 						version 		= m_streamReader.ReadByte();
			SerializationMethodOptions 	method 			= (SerializationMethodOptions)m_streamReader.ReadInt32();

			return new SerializedStreamHeader(version, method);
		}

		#endregion

		#region Read data methods

		private StreamObject ParseData()
		{
			int		memberCount		= m_streamReader.ReadInt32();
			if (memberCount > 0)
			{
				// find all the root objects
				// please note object count is not available for root level objects
				// entries in this section can be organised in key value pair or as array elements arranged sequentially
				DataOrderKind	kind	= (DataOrderKind)m_streamReader.ReadByte();
				switch (kind)
				{
					case DataOrderKind.KeyValuePair: 
						string[]		keys 		= new string[memberCount];
						IStreamObject[]	values		= new IStreamObject[memberCount];
						int 			kvIter		= 0;
						while (kvIter < memberCount)
						{
							keys[kvIter] 			= m_streamReader.ReadString();
							values[kvIter]			= ParseMember();
							kvIter++;
						}
						StreamObject	streamObject= new StreamObject(0);
						streamObject.Set(memberCount, keys, values);

						return streamObject;

					case DataOrderKind.ArrayKind:
						IStreamObject[]	elements	= new IStreamObject[memberCount];
						int 			elementIter	= 0;
						while (elementIter < memberCount)
						{
							elements[elementIter++]	= ParseMember();
						}
						StreamObject	streamArray	= new StreamObject(0);
						streamArray.Set(memberCount, elements);

						return streamArray;

					default:
						#if SERIALIZATION_DEBUG
						Debug.Log("Couldn't find data objects");
						#endif
						break;
				}
			}

			return null;
		}

		private IStreamObject ParseMember()
		{
			long 				startPosition 	= m_streamReader.Position;

			StreamObjectBase	member 			= null;
			BinaryStreamElement binaryElement;
			bool				loop			= true;

			while (loop)
			{
				binaryElement	= (BinaryStreamElement)m_streamReader.ReadByte();
				switch (binaryElement)
				{
					case BinaryStreamElement.Assembly:
						ReadAssembly();
						break;

					case BinaryStreamElement.Type:
						ReadType();
						break;

					case BinaryStreamElement.Primitive:
						member	= ParsePrimitive();
						loop	= false;
						break;

					case BinaryStreamElement.String:
						member 	= ParseString();
						loop	= false;
						break;

					case BinaryStreamElement.Enum:
						member 	= ParseEnum();
						loop	= false;
						break;

					case BinaryStreamElement.ObjectRef:
						member 	= ParseObjectReference();
						loop	= false;
						break;

					case BinaryStreamElement.ResourceRef:
						member 	= ParseResourceReference();
						loop	= false;
						break;

					case BinaryStreamElement.NotSupported:
						member 	= ParseNotSupported();
						loop	= false;
						break;

					case BinaryStreamElement.Null:
						member 	= ParseNull();
						loop	= false;
						break;

					case BinaryStreamElement.ValueWithSchema:
						member 	= ParseObjectWithSchema(isObject: false);
						loop	= false;
						break;

					case BinaryStreamElement.Value:
						member 	= ParseObject(isObject: false);
						loop	= false;
						break;

					case BinaryStreamElement.ObjectWithSchema:
						member 	= ParseObjectWithSchema(isObject: true);
						loop	= false;
						break;

					case BinaryStreamElement.Object:
						member 	= ParseObject(isObject: true);
						loop	= false;
						break;

					case BinaryStreamElement.ArrayOfPrimitive:
						member 	= ParseArrayOfPrimitive();
						loop	= false;
						break;

					case BinaryStreamElement.ArrayOfString:
						member 	= ParseArrayOfString();
						loop	= false;
						break;

					case BinaryStreamElement.Array:
						member 	= ParseArray();
						loop	= false;
						break;

					case BinaryStreamElement.Footer:
					case BinaryStreamElement.End:
						loop	= false;
						break;

					default:
						throw ErrorCentre.Exception("Couldn't find parse function for token " + binaryElement);
				}
			}

			if (member == null)
			{
				throw ErrorCentre.DataInconsistencyException();
			}

			// update position and length info
			member.StartPosition 	= startPosition;
			member.TotalLength 		= (m_streamReader.Position - startPosition);
			
			return member;
		}

		private StreamMember ParsePrimitive()
		{
			byte			typeCode	= m_streamReader.ReadByte();

			StreamMember	member 		= new StreamMember();
			member.DeclaredType 		= TypeServices.GetTypeFromTypeCode(typeCode);
			member.Type 				= SerializedPropertyType.Primitive;
			member.DataPosition			= m_streamReader.Position;
			m_streamReader.Seek(TypeServices.SizeOfPrimitiveType(typeCode), SeekOrigin.Current);

			return member;
		}

		private StreamMember ParseString()
		{
			StreamMember	member 		= new StreamMember();
			member.DeclaredType 		= SerializationKnownTypes.StringType;
			member.Type 				= SerializedPropertyType.String;
			member.DataPosition			= m_streamReader.Position;
			m_streamReader.SkipString();

			return member;
		}

		private StreamMember ParseEnum()
		{
			long 			typeId 		= m_streamReader.ReadInt32();
			Type 			type 		= ResolveType(typeId);

			StreamMember	member 		= new StreamMember();
			member.DeclaredType			= type;
			member.Type 				= SerializedPropertyType.Enum;
			member.DataPosition			= m_streamReader.Position;
			if (EnumSaveAsStringValue())
			{
				m_streamReader.SkipString();
			}
			else
			{
				m_streamReader.Seek(TypeServices.SizeOfPrimitiveType(type), SeekOrigin.Current);
			}

			return member;
		}

		private StreamMember ParseObjectReference()
		{
			long 			typeId 		= m_streamReader.ReadInt32();
			Type 			type 		= ResolveType(typeId);

			StreamMember	member 		= new StreamMember();
			member.DeclaredType			= type;
			member.Type 				= SerializedPropertyType.ObjectRef;
			member.DataPosition			= m_streamReader.Position;
			m_streamReader.SkipInt32();

			return member;
		}

		private StreamMember ParseResourceReference()
		{
			long 			typeId 		= m_streamReader.ReadInt32();
			Type 			type 		= ResolveType(typeId);

			StreamMember	member 		= new StreamMember();
			member.DeclaredType			= type;
			member.Type 				= SerializedPropertyType.ResourceRef;
			member.DataPosition			= m_streamReader.Position;
			m_streamReader.SkipString();
			m_streamReader.SkipInt32();

			return member;
		}

		private StreamMember ParseNotSupported()
		{
			long 			typeId 		= m_streamReader.ReadInt32();
			Type 			type 		= ResolveType(typeId);

			StreamMember	member 		= new StreamMember();
			member.DeclaredType			= type;
			member.Type 				= SerializedPropertyType.NotSupported;
			member.DataPosition			= m_streamReader.Position;

			return member;
		}

		private StreamMember ParseNull()
		{
			long 			typeId 		= m_streamReader.ReadInt32();
			Type 			type 		= ResolveType(typeId);

			StreamMember	member 		= new StreamMember();
			member.DeclaredType			= type;
			member.Type 				= SerializedPropertyType.Null;
			member.DataPosition			= m_streamReader.Position;

			return member;
		}

		private StreamObject ParseObjectWithSchema(bool isObject)
		{
			// read type information
			long 		typeId 		= m_streamReader.ReadInt32();
			Type 		type 		= ResolveType(typeId);

			// read object reference id used to track circular references
			long 					objectId 		= 0;
			SerializedPropertyType	propertyType 	= SerializedPropertyType.Value;
			if (isObject)
			{
				objectId 			= m_streamReader.ReadInt32();
				propertyType		= SerializedPropertyType.Object;	
			}

			// read member details
			// note that member names are provided optionally by the user
			int 		memberCount = m_streamReader.ReadInt32();
 			string[] 	memberNames = null;
			if (m_streamReader.ReadBoolean())
			{
				memberNames			= new string[memberCount];
				for (int iter = 0; iter < memberCount; iter++)
				{
					memberNames[iter] = m_streamReader.ReadString();
				}
			}

			// cache data start position
			long		dataPosition= m_streamReader.Position;

			// save schema
			TypeSchema	schema		= new TypeSchema(type);
			schema.SetMembers(memberCount, memberNames);
			AddTypeSchema(type, schema);

			// read members
			IStreamObject[]	members = new IStreamObject[memberCount];
			int 			miter 	= 0;
			while (miter < memberCount)
			{
				members[miter++] 	= ParseMember();
			}

			// create stream object
			StreamObject	streamObject 	= new StreamObject(objectId);
			streamObject.DeclaredType		= type;
			streamObject.Type				= propertyType;
			streamObject.DataPosition		= dataPosition;
			streamObject.Set(memberCount, memberNames, members);
			return streamObject;
		}

		private StreamObject ParseObject(bool isObject)
		{
			// read type information
			long 		typeId 		= m_streamReader.ReadInt32();
			Type 		type 		= ResolveType(typeId);

			// read object reference id used to track circular references
			long 					objectId 		= 0;
			SerializedPropertyType	propertyType 	= SerializedPropertyType.Value;
			if (isObject)
			{
				objectId 			= m_streamReader.ReadInt32();
				propertyType		= SerializedPropertyType.Object;	
			}

			// cache data start position
			long		dataPosition= m_streamReader.Position;

			// read members
			TypeSchema		schema	= GetTypeSchema(type);
			IStreamObject[]	members = new IStreamObject[schema.MemberCount];
			int 			miter 	= 0;
			while (miter < schema.MemberCount)
			{
				members[miter++] 	= ParseMember();
			}

			// create stream object
			StreamObject 	streamObject	= new StreamObject(objectId);
			streamObject.DeclaredType		= type;
			streamObject.Type				= propertyType;
			streamObject.DataPosition		= dataPosition;
			streamObject.Set(schema.MemberCount, schema.MemberNames, members);

			return streamObject;
		}

		private StreamObject ParseArrayOfPrimitive()
		{
			// read type info
			byte 	typeCode		= m_streamReader.ReadByte(); 
			int 	rank			= m_streamReader.ReadInt32();
			Type	elementType 	= TypeServices.GetTypeFromTypeCode(typeCode);
            Type	arrayType		= SerializationUtilityInternal.CreateArrayType(elementType, rank);

			// read object reference id
			long 	objectId		= m_streamReader.ReadInt32();

			// parse elements
			long	dataPosition	= m_streamReader.Position;
            if (SerializationUtilityInternal.GetJaggedArrayRank() == rank)
            {
                int length0 = m_streamReader.ReadInt32();
                for (int iter = 0; iter < length0; iter++)
                {
                    int length1 = m_streamReader.ReadInt32();
                    m_streamReader.Seek(length1 * TypeServices.SizeOfPrimitiveType(typeCode), SeekOrigin.Current);
                }
            }
            else if (1 == rank)
            {
                int length0 = m_streamReader.ReadInt32();
				m_streamReader.Seek(length0 * TypeServices.SizeOfPrimitiveType(typeCode), SeekOrigin.Current);
			}
			else
			{
				int count = 1;
				for (int iter = 0; iter < rank; iter++)
				{
					count *= m_streamReader.ReadInt32();
				}
				m_streamReader.Seek(count * TypeServices.SizeOfPrimitiveType(typeCode), SeekOrigin.Current);
			}

			// create stream object
			StreamObject	streamArray = new StreamObject(objectId);
			streamArray.DeclaredType	= arrayType;
			streamArray.Type			= SerializedPropertyType.ArrayOfPrimitive;
			streamArray.DataPosition	= dataPosition;

			return streamArray;
		}

		private StreamObject ParseArrayOfString()
		{
			// read type info
			int 	rank			= m_streamReader.ReadInt32();
            Type    arrayType       = SerializationUtilityInternal.CreateArrayType(SerializationKnownTypes.StringType, rank);

            // read object reference id
            long    objectId        = m_streamReader.ReadInt32();

			// parse elements
			long	dataPosition	= m_streamReader.Position;
            if (SerializationUtilityInternal.GetJaggedArrayRank() == rank)
			{
                int length0 = m_streamReader.ReadInt32();
                for (int iter = 0; iter < length0; iter++)
                {
                    int length1 = m_streamReader.ReadInt32();
                    for (int iter2 = 0; iter2 < length1; iter2++)
                    {
                        m_streamReader.SkipString();
                    }
                }
			}
			else if (1 == rank)
			{
                int length0 = m_streamReader.ReadInt32();
                for (int iter = 0; iter < length0; iter++)
                {
                    m_streamReader.SkipString();
                }
            }
			else
			{
				int count = 1;
				for (int iter = 0; iter < rank; iter++)
				{
					count *= m_streamReader.ReadInt32();
				}
				for (int iter = 0; iter < count; iter++)
				{
					m_streamReader.SkipString();
				}
			}

			// create stream object
			StreamObject	streamArray = new StreamObject(objectId);
			streamArray.DeclaredType	= arrayType;			
			streamArray.Type			= SerializedPropertyType.ArrayOfString;
			streamArray.DataPosition	= dataPosition;

			return streamArray;
		}

		private StreamObject ParseArray()
		{
			// read type
			Type	elementType		= ResolveType(typeId: (long)m_streamReader.ReadInt32());
			int 	rank			= m_streamReader.ReadInt32();
            Type	arrayType		= SerializationUtilityInternal.CreateArrayType(elementType, rank);

			// read reference id
			long 	objectId		= m_streamReader.ReadInt32();

			// read array elements
			long				dataPosition	= m_streamReader.Position;
			List<IStreamObject> memberList 		= null;
            if (SerializationUtilityInternal.GetJaggedArrayRank() == rank)
            {
				int	length0	= m_streamReader.ReadInt32();
				memberList 	= new List<IStreamObject>(length0);
				for (int iter = 0; iter < length0; iter++)
				{
					int length1 = m_streamReader.ReadInt32();
					for (int iter2 = 0; iter2 < length1; iter2++)
					{
						memberList.Add(ParseMember());
					}
				}
			}
            else if (1 == rank)
            {
                int length0 = m_streamReader.ReadInt32();
                memberList  = new List<IStreamObject>(length0);
                for (int iter = 0; iter < length0; iter++)
                {
                    memberList.Add(ParseMember());
                }
            }
            else
            {
				int	count	= 1;
				for (int iter = 0; iter < rank; iter++)
				{
					count *= m_streamReader.ReadInt32();
				}
				memberList	= new List<IStreamObject>(count);
				for (int iter = 0; iter < count; iter++)
				{
					memberList.Add(ParseMember());
				}
			}

			// create stream object
			StreamObject	streamArray	= new StreamObject(objectId);
			streamArray.DeclaredType	= arrayType;			
			streamArray.Type			= SerializedPropertyType.ArrayOfObject;
			streamArray.DataPosition	= dataPosition;
			streamArray.Set(memberList.Count, memberList);

			return streamArray;
		}

		#endregion

		#region Read type methods

		private void ReadAssembly()
		{
			// read properties
			long		assemblyId		= m_streamReader.ReadInt32();
			Assembly	assembly 		= ReflectionServices.CreateAssembly(name: m_streamReader.ReadString());

			// save info
			AddAssembly(assembly, assemblyId);
		}

		private void ReadType()
		{
			// read type properties
			long			typeId		= m_streamReader.ReadInt32();
			Assembly		assembly	= ResolveAssembly(assemblyId: m_streamReader.ReadInt32());
			string			name		= m_streamReader.ReadString();
			TypeSchemaKind	schemaKind	= (TypeSchemaKind)m_streamReader.ReadByte();
			Type			type;

			if (TypeSchemaKind.Generic == schemaKind)
			{
				int 		argCount	= m_streamReader.ReadInt32();
				Type[]		argTypes	= new Type[argCount];
				for (int iter = 0; iter < argCount; iter++)
				{
					Type	currentType	= ResolveType(typeId: m_streamReader.ReadInt32());
					if (null == currentType)
					{
						throw ErrorCentre.DataInconsistencyException();
					}
					argTypes[iter]		= currentType;
				}

				// create type
				Type		definition	= ReflectionServices.CreateType(assembly, name);
				type					= definition.MakeGenericType(argTypes);
			}
            else if (TypeSchemaKind.Array == schemaKind)
            {
                int         rank        = m_streamReader.ReadInt32();
                Type        elementType = ResolveType(typeId: m_streamReader.ReadInt32());

                // create type
                type                    = SerializationUtilityInternal.CreateArrayType(elementType, rank);
            }
            else
			{
				type					= ReflectionServices.CreateType(assembly, name);
			}

			// register type
			AddType(type, typeId);
		}

		private void AddAssembly(Assembly assembly, long assemblyId)
		{
			if (null == m_assemblyIdMap)
			{
				m_assemblyIdMap	= new Dictionary<long, Assembly>(capacity: 2);
			}

			m_assemblyIdMap.Add(assemblyId, assembly);
		}

		private void AddType(Type type, long typeId)
		{
			if (null == m_typeIdMap)
			{
				m_typeIdMap 	= new Dictionary<long, Type>(capacity: 4);
			}

			m_typeIdMap.Add(typeId, type);
		}

		private Assembly ResolveAssembly(long assemblyId)
		{
			Assembly assembly;
			if (false == SerializationKnownTypes.TryGetAssembly(assemblyId, out assembly))
			{
				m_assemblyIdMap.TryGetValue(assemblyId, out assembly);
			}

			return assembly;
		}

		private Type ResolveType(long typeId)
		{
			Type type;
			if (false == SerializationKnownTypes.TryGetType(typeId, out type))
			{
				m_typeIdMap.TryGetValue(typeId, out type);
			}

			return type;
		}

		#endregion

		#region Read footer methods

		private void ReadFooter()
		{
		}

		#endregion

		#region Read member methods

		private T ReadEnumPropertyInternal<T>(Type type)
		{
			if (EnumSaveAsStringValue())
			{
				int 	enumValueIndex 	= m_streamReader.ReadInt32();
				return ((T[])Enum.GetValues(type))[enumValueIndex];
			}
			else
			{
				return (T)BinaryConverter.ReadPrimitiveInternal(type, streamReader: m_streamReader);
			}
		}

		private object ReadEnumPropertyInternal(Type type)
		{
			if (EnumSaveAsStringValue())
			{
				int 	enumValueIndex 	= m_streamReader.ReadInt32();
				return Enum.GetValues(type).GetValue(enumValueIndex);
			}
			else
			{
				return BinaryConverter.ReadPrimitiveInternal(type, streamReader: m_streamReader);
			}
		}

		private object ReadObjectReferencePropertyInternal(Type type)
		{
			long	referenceId 	= (long)m_streamReader.ReadInt32();

			// resolve reference id to object
			bool	exists;
			object	referenceObject	= ResolveObjectReference(referenceId, out exists);
			if (false == exists)
			{
				throw ErrorCentre.DataInconsistencyException();
			}

			return referenceObject;
		}

		private object ReadResourceReferencePropertyInternal(Type type)
		{
			string	guid 			= m_streamReader.ReadString();
			long	referenceId 	= (long)m_streamReader.ReadInt32();

			// find object corresponding to saved guid
			Object	resourceObject;
			if (false == ResourceObjectIdentifierStoreManager.TryGetObjectWithGuid(guid, type, out resourceObject))
			{
                string  errorMessage    = string.Format("Couldn't find resource of type {0} with guid: {1}", type, guid);
				throw ErrorCentre.DataInconsistencyException(errorMessage);
			}

			// register resource object
			RegisterObject(resourceObject, referenceId);

			return resourceObject;
		}

		#endregion

		#region Read object methods

		private object ReadObject(StreamObject streamObject)
		{
			// check object
			if (null == streamObject)
			{
				throw new ArgumentNullException("streamObject");
			}

            // find data provider appropriate to this given type
            // use proxy data provider, incase if you don't find one
            ISerializationDataProvider dataProvider = SerializationDataProviderServices.LoadFromCacheOrCreateDataProvider(source: streamObject.DeclaredType);
            if (null == dataProvider)
            {
                dataProvider = SerializationDataProviderServices.GetProxyObjectDataProvider(type: streamObject.DeclaredType);
            }

            return ReadObject(streamObject, dataProvider);
        }

        private object ReadObject(StreamObject streamObject, ISerializationDataProvider dataProvider)
        {
            // update read stack
            PushObject(streamObject);

            // read properties using data provider
            object value = dataProvider.CreateInstance(reader: this, context: m_context);
            if (streamObject.ObjectId > 0)
            {
                RegisterObject(value, streamObject.ObjectId);
            }
            value = dataProvider.Deserialize(obj: value, reader: this, context: m_context);

            // reset stack
            PopActiveStreamObject();

            return value;
        }

        private T ReadObject<T>(StreamObject streamObject)
		{
			// check object
			if (null == streamObject)
			{
				throw new ArgumentNullException("streamObject");
			}

            // find data provider appropriate to this given type
            ISerializationDataProvider<T> dataProvider = SerializationDataProvider<T>.Default;
            if (dataProvider != null)
            {
                // update read stack
                PushObject(streamObject);

    			// read value
    			T		value	= dataProvider.CreateInstance(reader: this, context: m_context);
    			if (streamObject.ObjectId > 0)
    			{
    				RegisterObject(value, streamObject.ObjectId);
    			}
    			value 			= dataProvider.Deserialize(obj: value, reader: this, context: m_context);

    			// reset stack
    			PopActiveStreamObject();

                return value;
            }

            // fallback case wherein read operation is handled using proxy object
            return (T)ReadObject(streamObject, SerializationDataProviderServices.GetProxyObjectDataProvider(type: streamObject.DeclaredType));
        }

        private object ReadArrayOfPrimitive(StreamObject streamObject)
		{
			// check object
			if (null == streamObject)
			{
                throw new ArgumentNullException("streamObject");
			}
			// update read stack
			PushObject(streamObject);

			// read array elements
            Type    arrayType   = streamObject.DeclaredType;
            Type	elementType;
            int     rank;
            SerializationUtilityInternal.GetArrayProperties(arrayType, out elementType, out rank);

			Array	array	    = ArrayConverter.ReadPrimitiveArray(elementType, rank, m_streamReader);
			RegisterObject(array, streamObject.ObjectId);

			// reset stack
			PopActiveStreamObject();

			return array;
		}

		private object ReadArrayOfString(StreamObject streamObject)
		{
			// check object
			if (null == streamObject)
			{
                throw new ArgumentNullException("streamObject");
			}
			// update read stack
			PushObject(streamObject);

			// read array elements
            Type    arrayType   = streamObject.DeclaredType;
            Type	elementType;
            int		rank;
            SerializationUtilityInternal.GetArrayProperties(arrayType, out elementType, out rank);

			Array	array	    = ArrayConverter.ReadStringArray(rank, m_streamReader);
			RegisterObject(array, streamObject.ObjectId);

			// reset stack
			PopActiveStreamObject();

			return array;
		}

		private object ReadArray(StreamObject streamObject)
		{
			// check object
			if (null == streamObject)
			{
                throw new ArgumentNullException("streamObject");
			}
			// update read stack
			PushObject(streamObject);

            // find data provider compatible with given array
            // generally we create a runtime type using generic template which can deserialize array without boxing values
            // however AOT platforms restict creation of runtime types
            // in such cases we need to use proxy data provider which can serialize array, but downfall is values get boxed
            Type                        arrayType       = streamObject.DeclaredType;
            ISerializationDataProvider  dataProvider    = SerializationDataProviderServices.LoadFromCacheOrCreateDataProvider(source: arrayType);
            if (null == dataProvider)
            {
                dataProvider    = SerializationDataProviderServices.GetProxyObjectDataProvider(arrayType);
            }

            // read properties using data provider
            object  value	    = dataProvider.CreateInstance(reader: this, context: m_context);
			RegisterObject(value, streamObject.ObjectId);
			value 		        = dataProvider.Deserialize(obj: value, reader: this, context: m_context);

			// reset stack
			PopActiveStreamObject();

			return value;
		}

		#endregion

		#region Type schema methods

		private TypeSchema GetTypeSchema(Type type)
		{
			if (null == m_typeSchemaMap)
			{
				throw ErrorCentre.Exception("Type schema map not found.");
			}

			TypeSchema targetData;
			m_typeSchemaMap.TryGetValue(type, out targetData);

			return targetData;
		}

		private void AddTypeSchema(Type type, TypeSchema data)
		{
			// initialise schema container
			if (null == m_typeSchemaMap)
			{
				m_typeSchemaMap = new Dictionary<Type, TypeSchema>(capacity: 4);
			}

			// add item
			m_typeSchemaMap.Add(type, data);
		}

		#endregion

		#region Reference methods

		private void RegisterObject(object obj, long objectReferenceId)
		{
			if (null == m_objectIdMap)
			{
				m_objectIdMap = new Dictionary<long, object>(capacity: 4);
			}

			m_objectIdMap.Add(objectReferenceId, obj);
		}

		private object ResolveObjectReference(long objectReferenceId, out bool exists)
		{
			object	target	= null;
			if (null != m_objectIdMap)
			{
				if (false == m_objectIdMap.TryGetValue(objectReferenceId, out target))
				{
					// TODO handle missing references
				}
			}

			exists = (target != null);
			return target;
		}

		#endregion

		#region Read methods

		private void PushObject(StreamObject streamObject)
		{
			// add previous object to stack
			if (null != m_activeObject)
			{
				if (null == m_readStack)
				{
					m_readStack = new StreamObject[SerializationSettings.DepthLimit];
				}
				m_readStack[m_readLevel]	= m_activeObject;
			}

			// TODO replace with level specified in object
			if (m_dataObject == streamObject)
			{
				m_readLevel = 0;
			}
			else
			{
				m_readLevel++;
			}
			SetActiveReadObject(streamObject);
		}

		private void SetActiveReadObject(StreamObject streamObject)
		{
			// update active object
			m_activeObject			= streamObject;
			m_streamReader.Position	= streamObject.DataPosition;
		}

		private void PopActiveStreamObject()
		{
			if (m_readLevel == 0)
			{
				throw ErrorCentre.DataInconsistencyException();
			}

			// update references
			m_readLevel--;
			SetActiveReadObject(m_readStack[m_readLevel]);
			m_readStack[m_readLevel]	= null;
		}

		#endregion

		#region Private methods

		private bool EnumSaveAsStringValue()
		{
			return m_context.SerializationMethod.Contains(SerializationMethodOptions.EnumSaveAsStringValue);
		}

        #endregion

        #region IDisposable implementation

        public void Dispose()
		{
			Dispose(disposing: true);
		}

		public void Dispose(bool disposing)
		{
            if (m_disposed)
            {
                return;
            }

			if (disposing)
            {
                m_disposed      = true;

                // execute steps before releasing objects
                OnBeforeDeserializeEnd();
                if (onDeserializeEnd != null)
                {
                    onDeserializeEnd(m_context.Tag, m_context);
                }

                // dispose writer
                m_streamReader.Close();
                m_streamReader  = null;
            }
		}

		#endregion
	}
}