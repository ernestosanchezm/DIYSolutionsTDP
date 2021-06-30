using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using UnityEngine;
using VoxelBusters.External.Utils;

using Type = System.Type;
using Array = System.Array;
using UnityObject = UnityEngine.Object;
using Encoding = System.Text.Encoding;
using ArgumentNullException = System.ArgumentNullException;
using NullReferenceException = System.NullReferenceException;
using IDisposable = System.IDisposable;

namespace VoxelBusters.Serialization
{
	public class ObjectWriter : IObjectWriter, IDisposable
	{
		#region Constants

		internal 		const	long					kObjectIdStartsFrom				= (SerializationKnownTypes.kReservedId + 1);
		internal 		const	long					kValueTypeReferenceId			= 0;

		#endregion

		#region Static fields

		private			static 	ProxyObjectWriter		proxyObjectWriter				= new ProxyObjectWriter();

		#endregion

		#region Fields

		private			IStreamWriter	 				m_streamWriter;
		private			SerializationContext			m_context;
		private			ObjectIDGenerator<object>		m_idGenerator;
		private			Dictionary<Type, TypeSchema> 	m_typeSchemaMap;
		private			DataOrderKind[]					m_writeStack;

		private			SerializedStreamHeader			m_streamHeader;
		private			int								m_objectLevel;
		private			int								m_maxObjectLevel;
		private			int 							m_rootMemberCount;
		private			bool							m_canWriteName;

		private			bool							m_disposed;

        #endregion

        #region Internal properties

        internal Stream Stream
        {
            get
            {
                return m_streamWriter.BaseStream;
            }
        }

        #endregion

        #region Events

        public event SerializeEndCallback onSerializeEnd; 

		#endregion

		#region Constructors

		internal ObjectWriter(Stream stream, SerializationContext context)
			: this(stream, Encoding.UTF8, context)
		{}

		internal ObjectWriter(Stream stream, Encoding encoding, SerializationContext context)
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
			if (SerializationMode.Serialize != context.SerializationMode)
			{
				throw ErrorCentre.Exception("Context is not configured properly.");
			}

			// set properties
			m_streamWriter				= new BinaryStreamWriter(stream, encoding);
			#if SERIALIZATION_DEBUG
			DebugWriter debugWriter 	= new DebugWriter();
			debugWriter.underlyingWriter= m_streamWriter;
			m_streamWriter 				= debugWriter;
			#endif
			m_context 					= context;
			context.SerializedVersion	= SerializationSettings.SerializedVersion;

			m_idGenerator				= null;
			m_typeSchemaMap				= null;
			m_maxObjectLevel			= SerializationSettings.DepthLimit + 1;
			m_writeStack				= new DataOrderKind[m_maxObjectLevel];

			m_streamHeader 				= new SerializedStreamHeader(serializedVersion: m_context.SerializedVersion,
			                                                         serializationMethod: m_context.SerializationMethod);
			m_objectLevel	 			= -1;
			m_rootMemberCount			= 0;
			m_disposed					= false;

			PrepareForSerialize();
		}

		~ObjectWriter()
		{
			m_streamWriter	= null;
			m_context		= null;
		}

		#endregion

		#region Public methods

		public void Close()
		{
            Dispose(disposing: true);
		}

		#endregion

		#region State methods

		private void PrepareForSerialize()
		{
			// serialize
			OnBeforeSerializeStart();
			WriteHeader();
			OnSerializeStart();
		}

		private void OnBeforeSerializeStart()
		{}

		private void OnSerializeStart()
		{
			BeginData();
		}

		private void OnBeforeSerializeEnd()
		{
			EndData();

			// update root object count in data block
			HeaderStructure header = StreamDataConfiguration.GetConfiguration(m_context.SerializedVersion).Header;
			m_streamWriter.Seek((header.FixedSize + 1), System.IO.SeekOrigin.Begin);
			m_streamWriter.WriteInt32(m_rootMemberCount);
		}

		#endregion

		#region IObjectWriter implementation

		public void WriteProperty(string name, bool value)
		{
			EnsureWriteOrder(hasName: (name != null));

			RegisterWriteAction();
			WritePropertyNameInternal(name);
			WriteBooleanInternal(value);
		}

		public void WriteProperty(string name, char value)
		{
			EnsureWriteOrder(hasName: (name != null));

			RegisterWriteAction();
			WritePropertyNameInternal(name);
			WriteCharInternal(value);
		}

		public void WriteProperty(string name, sbyte value)
		{
			EnsureWriteOrder(hasName: (name != null));

			RegisterWriteAction();
			WritePropertyNameInternal(name);
			WriteSByteInternal(value);
		}

		public void WriteProperty(string name, byte value)
		{
			EnsureWriteOrder(hasName: (name != null));

			RegisterWriteAction();
			WritePropertyNameInternal(name);
			WriteByteInternal(value);
		}

		public void WriteProperty(string name, short value)
		{
			EnsureWriteOrder(hasName: (name != null));

			RegisterWriteAction();
			WritePropertyNameInternal(name);
			WriteInt16Internal(value);
		}

		public void WriteProperty(string name, ushort value)
		{
			EnsureWriteOrder(hasName: (name != null));

			RegisterWriteAction();
			WriteUInt16Internal(value);
		}

		public void WriteProperty(string name, int value)
		{
			EnsureWriteOrder(hasName: (name != null));

			RegisterWriteAction();
			WritePropertyNameInternal(name);
			WriteInt32Internal(value);
		}

		public void WriteProperty(string name, uint value)
		{
			EnsureWriteOrder(hasName: (name != null));

			RegisterWriteAction();
			WritePropertyNameInternal(name);
			WriteUInt32Internal(value);
		}

		public void WriteProperty(string name, long value)
		{
			EnsureWriteOrder(hasName: (name != null));

			RegisterWriteAction();
			WritePropertyNameInternal(name);
			WriteInt64Internal(value);
		}

		public void WriteProperty(string name, ulong value)
		{
			EnsureWriteOrder(hasName: (name != null));

			RegisterWriteAction();
			WritePropertyNameInternal(name);
			WriteUInt64Internal(value);
		}

		public void WriteProperty(string name, float value)
		{
			EnsureWriteOrder(hasName: (name != null));

			RegisterWriteAction();
			WritePropertyNameInternal(name);
			WriteSingleInternal(value);
		}

		public void WriteProperty(string name, double value)
		{
			EnsureWriteOrder(hasName: (name != null));

			RegisterWriteAction();
			WritePropertyNameInternal(name);
			WriteDoubleInternal(value);
		}

		public void WriteProperty(string name, decimal value)
		{
			EnsureWriteOrder(hasName: (name != null));

			RegisterWriteAction();
			WritePropertyNameInternal(name);
			WriteDecimalInternal(value);
		}

		public void WriteProperty(string name, string value)
		{
			EnsureWriteOrder(hasName: (name != null));

			RegisterWriteAction();
			WritePropertyNameInternal(name);
			WriteStringInternal(value);
		}

        public void WriteProperty(string name, object value)
        {
            WriteProperty(name, value, SerializationKnownTypes.SystemObjectType);
        }

        public void WriteProperty(string name, object value, Type type)
		{
			EnsureWriteOrder(hasName: (name != null));

			RegisterWriteAction();
			WritePropertyNameInternal(name);
			WriteGenericPropertyInternal(value, type);
		}

		public void WriteProperty<T>(string name, T value)
		{
			EnsureWriteOrder(hasName: (name != null));

			RegisterWriteAction();
			WritePropertyNameInternal(name);
			WriteGenericPropertyInternal<T>(value);
		}

		public void WriteProperty(bool value)
		{
			EnsureWriteOrder(hasName: false);

			RegisterWriteAction();
			WriteBooleanInternal(value);
		}

		public void WriteProperty(char value)
		{
			EnsureWriteOrder(hasName: false);

			RegisterWriteAction();
			WriteCharInternal(value);
		}

		public void WriteProperty(sbyte value)
		{
			EnsureWriteOrder(hasName: false);

			RegisterWriteAction();
			WriteSByteInternal(value);
		}

		public void WriteProperty(byte value)
		{
			EnsureWriteOrder(hasName: false);

			RegisterWriteAction();
			WriteByteInternal(value);
		}

		public void WriteProperty(short value)
		{
			EnsureWriteOrder(hasName: false);

			RegisterWriteAction();
			WriteInt16Internal(value);
		}

		public void WriteProperty(ushort value)
		{
			EnsureWriteOrder(hasName: false);

			RegisterWriteAction();
			WriteUInt16Internal(value);
		}

		public void WriteProperty(int value)
		{
			EnsureWriteOrder(hasName: false);

			RegisterWriteAction();
			WriteInt32Internal(value);
		}

		public void WriteProperty(uint value)
		{
			EnsureWriteOrder(hasName: false);

			RegisterWriteAction();
			WriteUInt32Internal(value);
		}

		public void WriteProperty(long value)
		{
			EnsureWriteOrder(hasName: false);

			RegisterWriteAction();
			WriteInt64Internal(value);
		}

		public void WriteProperty(ulong value)
		{
			EnsureWriteOrder(hasName: false);

			RegisterWriteAction();
			WriteUInt64Internal(value);
		}

		public void WriteProperty(float value)
		{
			EnsureWriteOrder(hasName: false);

			RegisterWriteAction();
			WriteSingleInternal(value);
		}

		public void WriteProperty(double value)
		{
			EnsureWriteOrder(hasName: false);

			RegisterWriteAction();
			WriteDoubleInternal(value);
		}

		public void WriteProperty(decimal value)
		{
			EnsureWriteOrder(hasName: false);

			RegisterWriteAction();
			WriteDecimalInternal(value);
		}

		public void WriteProperty(string value)
		{
			EnsureWriteOrder(hasName: false);

			RegisterWriteAction();
			WriteStringInternal(value);
		}

        public void WriteProperty(object value)
        {
            WriteProperty(value, SerializationKnownTypes.SystemObjectType);
        }

        public void WriteProperty(object value, Type type)
		{
			EnsureWriteOrder(hasName: false);

			RegisterWriteAction();
			WriteGenericPropertyInternal(value, type);
		}

		public void WriteProperty<T>(T value)
		{
			EnsureWriteOrder(hasName: false);

			RegisterWriteAction();
			WriteGenericPropertyInternal<T>(value);
		}
			
		public void WriteArrayLength(int dimension, int value)
		{
			m_streamWriter.WriteInt32(value);
		}

		private void WritePropertyNameInternal(string name)
		{
			DataOrderKind writeKind = GetActiveWriteKind();
			if (DataOrderKind.KeyValuePair == writeKind)
			{
				m_streamWriter.WriteString(name);
			}
		}

		#endregion

		#region Write header methods

		private void WriteHeader()
		{
			// fixed properties
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Header); 			// token (1B)
			m_streamWriter.WriteByte(m_streamHeader.SerializedVersion);				// version (1B)
			m_streamWriter.WriteInt32((int)m_streamHeader.SerializationMethod);		// method (4B)
		}

		#endregion

		#region Data methods

		private void BeginData()
		{
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Data);
			m_streamWriter.WriteInt32(0);
			SetDepthLevel(value: 0);
			SetWriteKind(DataOrderKind.Unknown);
		}

		private void EndData()
		{
			if (m_objectLevel != 0)
			{
				throw ErrorCentre.DataInconsistencyException("Found inconsistency in object level.");
			}

            WriteFooter();
		}

		#endregion

		#region Footer methods

		// Footer structure
		// token (1B)
		// length (8B)
		// eof token (1B)
		private void WriteFooter()
		{
			// calculate length
//			long currentLength 	= m_streamWriter.Length;
//			long totalLength	= currentLength + 10;
//
//			// write
//			m_streamWriter.WriteByte((byte)BinaryStreamElement.Footer);
//			m_streamWriter.WriteInt64(totalLength);
			m_streamWriter.WriteByte((byte)BinaryStreamElement.End);
		}

		#endregion

		#region Write type methods

		private void WriteAssemblyInternal(Assembly assembly, long assemblyId)
		{
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Assembly);
			m_streamWriter.WriteInt32((int)assemblyId);
			m_streamWriter.WriteString(ReflectionServices.GetAssemblyFullName(assembly));
		}

		private void WriteTypeInternal(Type type, long typeId)
		{
			// type instances entering this block are untracked ones
			// so in order to rebuild type, we need to save its corresponding assembly information
			Assembly 		assembly	 = type.Assembly;
			long 			assemblyId	 = RegisterAssembly(assembly);

			// for type serialization, we break complex types (list, dict, array) to simple types
			// while doing this, we need to ensure even dependents types are known to the system
			string	 		name 		= null;
			TypeSchemaKind 	schemaKind 	= TypeSchemaKind.Unknown;
			int 			argCount	= 0;
			long[]			argIds		= null;
            int             rank        = -1;
            long            elementId   = -1;

			if (TypeServices.IsGenericType(type))
			{
				schemaKind				= TypeSchemaKind.Generic;
				name 					= TypeServices.GetTypeFullName(type.GetGenericTypeDefinition());	

				// build arg id's
				Type[]	arguments		= type.GetGenericArguments();
				argCount				= arguments.Length;
				argIds					= new long[argCount];
				for (int iter = 0; iter < argCount; iter++)
				{
					Type	argType		= arguments[iter];
					argIds[iter] 		= RegisterType(argType);
				}
			}
			else if (TypeServices.IsArrayType(type))
			{
                schemaKind              = TypeSchemaKind.Array;

                Type elementType;
                SerializationUtilityInternal.GetArrayProperties(type, out elementType, out rank);
                elementId               = RegisterType(elementType);
            }
            else
			{
				schemaKind				= TypeServices.IsValueType(type) ? TypeSchemaKind.Value : TypeSchemaKind.Object;
				name 					= TypeServices.GetTypeFullName(type);	
			}

			// write type metadata
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Type);	// token
			m_streamWriter.WriteInt32((int)typeId);						// type id
			m_streamWriter.WriteInt32((int)assemblyId);					// assemblyId
			m_streamWriter.WriteString(name);							// name
			m_streamWriter.WriteByte((byte)schemaKind);					// schema kind

            // array specific meta information
            if (elementId != -1)
            {
                m_streamWriter.WriteInt32((int)rank);
                m_streamWriter.WriteInt32((int)elementId);
            }

            // generic type meta information
            if (0 != argCount)
			{
				m_streamWriter.WriteInt32(argCount);					// generic arg count
				for (int iter = 0; iter < argCount; iter++)
				{
					m_streamWriter.WriteInt32((int)argIds[iter]);		// generic arg typeId's
				}
			}
		}

		private long RegisterAssembly(Assembly assembly)
		{
			// check whether assembly is known type
			long 	assemblyId;
			if (false == SerializationKnownTypes.TryGetAssemblyId(assembly, out assemblyId))
			{
				// register type
				bool 	isNew;
				assemblyId		= GetIdGenerator().GetId(assembly, out isNew);

				if (isNew)
				{
					WriteAssemblyInternal(assembly, assemblyId);
				}
			}

			return assemblyId;
		}

		private long RegisterType(Type type)
		{
			// check whether type is known type
			long 	typeId;
			if (false == SerializationKnownTypes.TryGetTypeId(type, out typeId))
			{
				// register type
				bool 	isNew;
				typeId			= GetIdGenerator().GetId(type, out isNew);

				if (isNew)
				{
                    WriteTypeInternal(type, typeId);
                    // add type to tracker
				}
			}

			return typeId;
		}

		#endregion

		#region Write primitives methods

		// primitive property structure
		// stream-token (1B)
		// typecode (1B)
		// value
		protected void WriteBooleanInternal(bool value)
		{
			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Primitive);
			m_streamWriter.WriteByte(SerializationTypeCode.Boolean);
			m_streamWriter.WriteBoolean(value); 
		}

		protected void WriteCharInternal(char value)
		{
			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Primitive);
			m_streamWriter.WriteByte(SerializationTypeCode.Char);
			m_streamWriter.WriteChar(value); 
		}

		protected void WriteSByteInternal(sbyte value)
		{
			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Primitive);
			m_streamWriter.WriteByte(SerializationTypeCode.SByte);
			m_streamWriter.WriteSByte(value); 
		}

		protected void WriteByteInternal(byte value)
		{
			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Primitive);
			m_streamWriter.WriteByte(SerializationTypeCode.Byte);
			m_streamWriter.WriteByte(value); 
		}

		protected void WriteInt16Internal(short value)
		{
			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Primitive);
			m_streamWriter.WriteByte(SerializationTypeCode.Int16);
			m_streamWriter.WriteInt16(value); 
		}

		protected void WriteUInt16Internal(ushort value)
		{
			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Primitive);
			m_streamWriter.WriteByte(SerializationTypeCode.UInt16);
			m_streamWriter.WriteUInt16(value); 
		}

		protected void WriteInt32Internal(int value)
		{
			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Primitive);
			m_streamWriter.WriteByte(SerializationTypeCode.Int32);
			m_streamWriter.WriteInt32(value); 
		}

		protected void WriteUInt32Internal(uint value)
		{
			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Primitive);
			m_streamWriter.WriteByte(SerializationTypeCode.UInt32);
			m_streamWriter.WriteUInt32(value); 
		}

		protected void WriteInt64Internal(long value)
		{
			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Primitive);
			m_streamWriter.WriteByte(SerializationTypeCode.Int64);
			m_streamWriter.WriteInt64(value); 
		}

		protected void WriteUInt64Internal(ulong value)
		{
			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Primitive);
			m_streamWriter.WriteByte(SerializationTypeCode.UInt64);
			m_streamWriter.WriteUInt64(value); 
		}

		protected void WriteSingleInternal(float value)
		{
			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Primitive);
			m_streamWriter.WriteByte(SerializationTypeCode.Single);
			m_streamWriter.WriteSingle(value); 
		}

		protected void WriteDoubleInternal(double value)
		{
			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Primitive);
			m_streamWriter.WriteByte(SerializationTypeCode.Double);
			m_streamWriter.WriteDouble(value); 
		}

		protected void WriteDecimalInternal(decimal value)
		{
			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Primitive);
			m_streamWriter.WriteByte(SerializationTypeCode.Decimal);
			m_streamWriter.WriteDecimal(value);  
		}

		protected void WritePrimitiveInternal<T>(T value, Type type)
		{
			byte	typeCode	= TypeServices.GetTypeCode(type);

			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Primitive);
			m_streamWriter.WriteByte(typeCode);
			BinaryConverter.WritePrimitiveInternal(value, m_streamWriter);
		}

		protected void WritePrimitiveInternal(object value, Type type)
		{
			byte	typeCode	= TypeServices.GetTypeCode(type);

			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Primitive);
			m_streamWriter.WriteByte(typeCode);
			BinaryConverter.WritePrimitiveInternal(value, type, m_streamWriter);
		}

		#endregion

		#region Write methods

		protected void WriteStringInternal(string value)
		{
			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.String);
			m_streamWriter.WriteString(value);  
		}

		protected void WriteEnumInternal(object value, Type type)
		{
			// register type
			long 	typeId		= RegisterType(type);

			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Enum);
			m_streamWriter.WriteInt32((int)typeId);
			if (EnumSaveAsStringValue())
			{
				int valueIndex	= SerializationUtilityInternal.FindEnumValueIndex(value.ToString(), type);
				m_streamWriter.WriteInt32(valueIndex); 
			}
			else
			{
				BinaryConverter.WritePrimitiveInternal(value, type, m_streamWriter);
			}
		}

		protected void WriteObjectReferenceInternal(long objectReferenceId, Type type)
		{
			// register type
			long 	typeId		= RegisterType(type);

			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.ObjectRef);
			m_streamWriter.WriteInt32((int)typeId);
			m_streamWriter.WriteInt32((int)objectReferenceId); 
		}

		protected void WriteResourceReferenceInternal(string objectGuid, long objectReferenceId, Type type)
		{
			// register type
			long 	typeId		= RegisterType(type);

			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.ResourceRef);
			m_streamWriter.WriteInt32((int)typeId);
			m_streamWriter.WriteString(objectGuid); 
			m_streamWriter.WriteInt32((int)objectReferenceId); 
		}

		protected void WriteNotSupportedObjectInternal(Type type)
		{
			// register type
			long 	typeId		= RegisterType(type);

			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.NotSupported);
			m_streamWriter.WriteInt32((int)typeId);
		}

		protected void WriteNullObjectInternal(Type type)
		{
			// register type
			long 	typeId		= RegisterType(type);

			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Null);
			m_streamWriter.WriteInt32((int)typeId);
		}

		#endregion

		#region Write array methods

		protected void WriteArrayInternal(Array array, long objectId, Type type)
		{
			// prepare for serializaion
			SetDepthLevel(value: (m_objectLevel + 1));
			SetWriteKind(value: DataOrderKind.ArrayKind);

			// get array properties
			Type	elementType;
			int		rank;
            SerializationUtilityInternal.GetArrayProperties(type, out elementType, out rank);

			// write array data
			if (TypeServices.IsPrimitiveType(elementType))
			{
				WritePrimitiveArrayInternal(array, rank, objectId, type, elementType);
			}
			else if (SerializationKnownTypes.StringType == elementType)
			{
				WriteStringArrayInternal(array, rank, objectId);
			}
			else
			{
				WriteObjectArrayInternal(array, rank, objectId, type, elementType);
			}

			// reset state
			SetDepthLevel(value: (m_objectLevel - 1));
		}

		protected void WritePrimitiveArrayInternal(Array array, int rank, long objectId, Type arrayType, Type elementType)
		{
			byte	typeCode	= TypeServices.GetTypeCode(elementType);

			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.ArrayOfPrimitive);
			m_streamWriter.WriteByte(typeCode);
			m_streamWriter.WriteInt32(rank);
			m_streamWriter.WriteInt32((int)objectId);
			ArrayConverter.WritePrimitiveArray(array, elementType, rank, m_streamWriter);
		}

		protected void WriteStringArrayInternal(Array array, int rank, long objectId)
		{
			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.ArrayOfString);
			m_streamWriter.WriteInt32(rank);
			m_streamWriter.WriteInt32((int)objectId);
			ArrayConverter.WriteStringArray(array, rank, m_streamWriter);
		}

		protected void WriteObjectArrayInternal(Array array, int rank, long objectId, Type type, Type elementType)
		{
			// register type
			long 	elementTypeId		= RegisterType(elementType);

			// write property data
			m_streamWriter.WriteByte((byte)BinaryStreamElement.Array);
			m_streamWriter.WriteInt32((int)elementTypeId);
			m_streamWriter.WriteInt32(rank);
			m_streamWriter.WriteInt32((int)objectId);

            // write internal properties using data provider
            // sometimes you can't find data provider capable of handling this type
            // in such cases, we use proxy object to handle serialization
            // proxy objects are not as efficient as actual data providers, mainly because values returned are boxed
            ISerializationDataProvider dataProvider = SerializationDataProviderServices.LoadFromCacheOrCreateDataProvider(type);
            if (null == dataProvider)
            {
                dataProvider            = SerializationDataProviderServices.GetProxyObjectDataProvider(type);
            }
            dataProvider.Serialize(array, this, m_context);
		}

		#endregion

		#region Write object methods

		protected void WriteObjectInternal(object value, long objectId, Type type)
		{
			// update depth
			SetDepthLevel(value: (m_objectLevel + 1));

            // find data provider available for given type
            // use an fake data provider in case if we are not able to find one
            ISerializationDataProvider dataProvider = SerializationDataProviderServices.LoadFromCacheOrCreateDataProvider(type);
            if (null == dataProvider)
            {
                dataProvider = SerializationDataProviderServices.GetProxyObjectDataProvider(type);
            }

            // check whether schema needs to be serialized along with object data
            TypeSchema	typeSchema	= GetTypeSchema(type);
			if (null == typeSchema)
			{
				WriteObjectDataWithSchemaInternal(value, objectId, type, dataProvider);
			}
			else
			{
				WriteObjectDataInternal(value, objectId, type, dataProvider);
            }

			// reset state
			ResetOnWriteObjectFinished();
		}

		private void WriteObjectDataWithSchemaInternal(object value, long objectId, Type type, ISerializationDataProvider dataProvider)
        {
			// register type
			long		typeId		= RegisterType(type);
			
			// build type schema information using dry run mechanism
			proxyObjectWriter.PrepareForWrite();
			dataProvider.Serialize(value, proxyObjectWriter, m_context);

			// create and save type schema 
			TypeSchema	typeSchema	= new TypeSchema(type);
			typeSchema.SetMembers(proxyObjectWriter.MemberCount, proxyObjectWriter.MemberNames);
			AddTypeSchema(type, typeSchema);

			// update writer properties
			SetWriteKind(value: DataOrderKind.ArrayKind);

			// write header
			if (objectId > 0)
			{
				m_streamWriter.WriteByte((byte)BinaryStreamElement.ObjectWithSchema);
				m_streamWriter.WriteInt32((int)typeId);
				m_streamWriter.WriteInt32((int)objectId);
			}
			else
			{
				m_streamWriter.WriteByte((byte)BinaryStreamElement.ValueWithSchema);
				m_streamWriter.WriteInt32((int)typeId);
			}
			m_streamWriter.WriteInt32(typeSchema.MemberCount);

			bool 	hasMemberNames	= (typeSchema.MemberNames != null);
			m_streamWriter.WriteBoolean(hasMemberNames);
			if (hasMemberNames)
			{
				for (int iter = 0; iter < typeSchema.MemberCount; iter++)
				{
					m_streamWriter.WriteString(typeSchema.MemberNames[iter]);
				}
			}    

			// write child properties
			dataProvider.Serialize(value, this, m_context);
		}

		private void WriteObjectDataInternal(object value, long objectId, Type type, ISerializationDataProvider dataProvider)
		{
			// register type
			long		typeId		= RegisterType(type);

			// set writer properties
			SetWriteKind(value: DataOrderKind.ArrayKind);

			// write header
			if (objectId > 0)
			{
				m_streamWriter.WriteByte((byte)BinaryStreamElement.Object);
				m_streamWriter.WriteInt32((int)typeId);
				m_streamWriter.WriteInt32((int)objectId);
			}
			else
			{
				m_streamWriter.WriteByte((byte)BinaryStreamElement.Value);
				m_streamWriter.WriteInt32((int)typeId);
			}

			// write child properties
			dataProvider.Serialize(value, this, m_context);
		}

		protected void WriteObjectInternal<T>(T value, long objectId, Type type)
		{
			// update depth
			SetDepthLevel(value: (m_objectLevel + 1));

            // find data provider suitable for this given generic type
            ISerializationDataProvider<T> dataProvider = SerializationDataProvider<T>.Default;

            // check whether schema needs to be serialized along with object data
            // based on state, we will determine the properties that needs to be serialized
            TypeSchema	typeSchema	= GetTypeSchema(type);
			if (null == typeSchema)
			{
                if (dataProvider != null)
                {
                    WriteObjectDataWithSchemaInternal(value, objectId, type, dataProvider);
                }
                else
                {
                    WriteObjectDataWithSchemaInternal(value, objectId, type, SerializationDataProviderServices.GetProxyObjectDataProvider(type));
                }
            }
			else
			{
                if (dataProvider != null)
                {
                    WriteObjectDataInternal(value, objectId, type, dataProvider);
                }
                else
                {
                    WriteObjectDataInternal(value, objectId, type, SerializationDataProviderServices.GetProxyObjectDataProvider(type));
                }
            }

			// reset state
			ResetOnWriteObjectFinished();
		}

        protected void WriteObjectDataWithSchemaInternal<T>(T value, long objectId, Type type, ISerializationDataProvider<T> dataProvider)
		{
			// register type
			long		typeId		= RegisterType(type);

			// build type schema information using dry run mechanism
			proxyObjectWriter.PrepareForWrite();
			dataProvider.Serialize(value, proxyObjectWriter, m_context);

			TypeSchema	typeSchema	= new TypeSchema(type);
			typeSchema.SetMembers(proxyObjectWriter.MemberCount, proxyObjectWriter.MemberNames);
			AddTypeSchema(type, typeSchema);

			// update writer properties
			SetWriteKind(value: DataOrderKind.ArrayKind);

			// write header
			if (objectId > 0)
			{
				m_streamWriter.WriteByte((byte)BinaryStreamElement.ObjectWithSchema);
				m_streamWriter.WriteInt32((int)typeId);
				m_streamWriter.WriteInt32((int)objectId);
			}
			else
			{
				m_streamWriter.WriteByte((byte)BinaryStreamElement.ValueWithSchema);
				m_streamWriter.WriteInt32((int)typeId);
			}
			bool 	hasMemberNames	= (typeSchema.MemberNames != null);
			m_streamWriter.WriteInt32(typeSchema.MemberCount);
			m_streamWriter.WriteBoolean(hasMemberNames);
			if (hasMemberNames)
			{
				for (int iter = 0; iter < typeSchema.MemberCount; iter++)
				{
					m_streamWriter.WriteString(typeSchema.MemberNames[iter]);
				}
			}    

			// write child properties
			dataProvider.Serialize(value, this, m_context);
		}

		private void WriteObjectDataInternal<T>(T value, long objectId, Type type, ISerializationDataProvider<T> dataProvider)
        {
			// register type
			long		typeId		= RegisterType(type);

			// set writer properties
			SetWriteKind(value: DataOrderKind.ArrayKind);

			// write header
			if (objectId > 0)
			{
				m_streamWriter.WriteByte((byte)BinaryStreamElement.Object);
				m_streamWriter.WriteInt32((int)typeId);
				m_streamWriter.WriteInt32((int)objectId);
			}
			else
			{
				m_streamWriter.WriteByte((byte)BinaryStreamElement.Value);
				m_streamWriter.WriteInt32((int)typeId);
			}

			// write child properties
			dataProvider.Serialize(value, this, m_context);
		}

		private void ResetOnWriteObjectFinished()
		{
			// reset state
			SetDepthLevel(value: (m_objectLevel - 1));
		}

		private DataOrderKind GetWriteOrderForTypeSchema(TypeSchema typeSchema)
		{
			if (null == typeSchema.MemberNames)
			{
				return DataOrderKind.ArrayKind;
			}
			else
			{
				return DataOrderKind.KeyValuePair;
			}
		}

		#endregion

		#region Write generic methods

		protected void WriteGenericPropertyInternal<T>(T value)
		{
			Type 	type = typeof(T);

			// resolve generic request to type specific handler
			// we breakdown to 2 sections
			// #1 value type
			// #2 object type
			if (TypeServices.IsValueType(type))
			{
				if (TypeServices.IsPrimitiveType(type))
				{
					WritePrimitiveInternal(value, type);
					return;
				}
				else if (TypeServices.IsEnumType(type))
				{
					WriteEnumInternal((object)value, type);
					return;
				}
				else if (SerializationUtilityInternal.CanSerialize(type, m_context))
				{
					WriteObjectInternal(value: value, objectId: kValueTypeReferenceId, type: type);
					return;
				}
			}
			else
			{
				// type information passed via generic constraints do not necessarily point to the correct type
				// so we need to have an additional check to find out actual type of the value
                object  underlyingValue = (object)value;
                bool    isNullValue     = IsObjectNull(underlyingValue, type);
                Type	underlyingType	= isNullValue ? type : value.GetType();
				if (type != underlyingType)
                {
                    WriteGenericPropertyInternal(underlyingValue, underlyingType);
                    return;
                }

				// string handling
				if (SerializationKnownTypes.StringType == type)
				{
					WriteStringInternal((string)(object)value);
					return;
				}

				// check whether object passed is null
				// all non-string null objects are handled differently
				if (isNullValue)
				{
					WriteNullObjectInternal(type: type);
					return;
				}

				// register this object to handle circular referencing
				bool 	isNewObject;
				long 	objectId 		= GetIdGenerator().GetId((object)value, out isNewObject);
				if (isNewObject)
				{
					// array type
					if (TypeServices.IsArrayType(type))
					{
						WriteArrayInternal((Array)(object)value, objectId, type);
						return;
					}

					// object type
					// special case: unity asset's existing within project will be saved by id
					// so we need to check whether given object is a resource object or not
					if (value is UnityObject)
					{
						string resourceGuid;
						if (IsResourceObject(value as UnityObject, type, out resourceGuid))
						{
							WriteResourceReferenceInternal(objectGuid: resourceGuid, objectReferenceId: objectId,  type: type);
							return;
						}
					}
					
					WriteObjectInternal(value, objectId, type);
					return;
				}
				else
				{
					WriteObjectReferenceInternal(objectId, type);
					return;
				}
			}

			WriteNotSupportedObjectInternal(type);
			return;
		}

		protected void WriteGenericPropertyInternal(object value, Type type)
		{
            // get underlying type
            bool isNull = IsObjectNull(value, type);
            type        = isNull ? type : value.GetType();

			// resolve generic request to type specific handler
			// we breakdown to 2 sections
			// #1 value type
			// #2 object type
			if (TypeServices.IsValueType(type))
			{
				if (TypeServices.IsPrimitiveType(type))
				{
					WritePrimitiveInternal(value, type);
					return;
				}
				else if (TypeServices.IsEnumType(type))
				{
					WriteEnumInternal(value, type);
					return;
				}
				// custom data types
				else if (SerializationUtilityInternal.CanSerialize(type, m_context))
				{
					WriteObjectInternal(value: value, objectId: kValueTypeReferenceId, type: type);
					return;
				}
			}
			else
			{
				// string type
				if (SerializationKnownTypes.StringType == type)
				{
					WriteStringInternal((string)value);
					return;
				}

				// check whether object passed is null
				// non-string null objects are handled differently
				if (isNull)
				{
					WriteNullObjectInternal(type);
					return;
				}

				// register this object to handle circular referencing
				bool 	isNewObject;
				long 	objectId 		= GetIdGenerator().GetId((object)value, out isNewObject);
				if (isNewObject)
				{
					// array type
					if (TypeServices.IsArrayType(type))
					{
						WriteArrayInternal((Array)value, objectId, type);
						return;
					}
					
					// object type
					// special case: unity asset's existing within project will be saved by id
					// so we need to check whether given object is a resource object or not
					if (value is UnityObject)
					{
						string resourceGuid;
						if (IsResourceObject(value as UnityObject, type, out resourceGuid))
						{
							WriteResourceReferenceInternal(objectGuid: resourceGuid, objectReferenceId: objectId, type: type);
							return;
						}
					}
					
					WriteObjectInternal(value, objectId, type);
					return;
				}
				else
				{
					WriteObjectReferenceInternal(objectId, type);
					return;
				}
			}

			WriteNotSupportedObjectInternal(type);
			return;
		}

		#endregion

		#region State methods

		private void SetDepthLevel(int value)
		{
			if (value > m_maxObjectLevel)
			{
				throw ErrorCentre.Exception("Serialization exceeds max depth limit.");
			}
			// reset active state value
			if (value < m_objectLevel)
			{
				m_writeStack[m_objectLevel] = DataOrderKind.Unknown;
			}

			// set new values
			m_objectLevel	= value;
		}

		private void SetWriteKind(DataOrderKind value)
		{
			if (DataOrderKind.Unknown == value)
			{
				if (m_objectLevel > 0)
				{
					throw ErrorCentre.Exception("Please set valid write order.");
				}
			}
			m_writeStack[m_objectLevel] = value;
		}

		internal DataOrderKind GetActiveWriteKind()
		{
			return m_writeStack[m_objectLevel];
		}

		#endregion

		#region Id methods
	
		private ObjectIDGenerator<object> GetIdGenerator()
		{
			if (null == m_idGenerator)
			{
				m_idGenerator 	= new ObjectIDGenerator<object>(capacity: 4, idStartsFrom: kObjectIdStartsFrom);
			}

			return m_idGenerator;
		}

		#endregion

		#region Type schema methods

		private TypeSchema GetTypeSchema(Type type)
		{
			if (null == m_typeSchemaMap)
			{
				m_typeSchemaMap = new Dictionary<Type, TypeSchema>(capacity: 4);
				return null;
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

		#region Write order methods

		private void EnsureWriteOrder(bool hasName)
		{
			// root level write mode wont be known until request is received
			DataOrderKind writeKind = GetActiveWriteKind();
			if (0 == m_objectLevel)
			{
				if (DataOrderKind.Unknown == writeKind)
				{
					writeKind		= hasName ? DataOrderKind.KeyValuePair : DataOrderKind.ArrayKind;
					SetWriteKind(writeKind);

					// append this information 
					m_streamWriter.WriteByte((byte)writeKind);
				}
			}

			// check for exceptions
			if (DataOrderKind.KeyValuePair == writeKind)
			{
				if (false == hasName)
				{
					throw ErrorCentre.Exception("Invalid property name.");
				}
			}
		}

		private void RegisterWriteAction()
		{
			if (0 == m_objectLevel)
			{
				// update counter
				m_rootMemberCount++;
			}
		}

		#endregion

		#region Private methods

		private bool EnumSaveAsStringValue()
		{
			return m_context.SerializationMethod.Contains(SerializationMethodOptions.EnumSaveAsStringValue);
		}

		private bool IsResourceObject(Object obj, Type type, out string guid)
		{
			// set default value
			guid = null;

			// check whether specified object is resource type
			if (false == m_context.Supports(SerializationMethodOptions.DisableAssetSerializationById))
			{
				if (ResourceObjectIdentifierStoreManager.TryGetObjectGuid(obj, out guid))
				{
					return true;
				}
			}
			return false;
		}

        private bool IsObjectNull(object value, Type type)
        {
            return value == null || value.Equals(null);
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
                m_disposed  = true;

                // instructions to be executed before disposing object
                OnBeforeSerializeEnd();
                if (onSerializeEnd != null)
                {
                    onSerializeEnd(m_context.Tag, m_context);
                }

                // dispose writer
                m_streamWriter.Close();
            }
		}

		#endregion
	}
}