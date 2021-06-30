using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TypeCode = System.TypeCode;

namespace VoxelBusters.Serialization
{
	public class SerializationTypeCode 
	{
		#region Constants

		internal const	byte	Boolean 	= (byte)TypeCode.Boolean;
		internal const	byte 	Char 		= (byte)TypeCode.Char;
		internal const	byte 	SByte 		= (byte)TypeCode.SByte;
		internal const	byte 	Byte 		= (byte)TypeCode.Byte;
		internal const	byte 	Int16 		= (byte)TypeCode.Int16;
		internal const	byte 	UInt16 		= (byte)TypeCode.UInt16;
		internal const	byte 	Int32 		= (byte)TypeCode.Int32;
		internal const	byte 	UInt32 		= (byte)TypeCode.UInt32;
		internal const	byte 	Int64 		= (byte)TypeCode.Int64;
		internal const	byte 	UInt64 		= (byte)TypeCode.UInt64;
		internal const	byte 	Single		= (byte)TypeCode.Single;
		internal const	byte 	Double		= (byte)TypeCode.Double;
		internal const	byte 	Decimal		= (byte)TypeCode.Decimal;
		internal const	byte 	String		= (byte)TypeCode.String;
		internal const	byte 	Object		= (byte)TypeCode.Object;

		#endregion
	}
}