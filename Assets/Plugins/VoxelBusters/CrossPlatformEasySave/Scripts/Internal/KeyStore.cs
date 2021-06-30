using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	internal class KeyStore 
	{
		#region String literals

		// stream data blocks
		internal 	const 	string 		kHeader							= "$header";
		internal 	const 	string 		kData							= "$data";
		internal 	const 	string 		kSharedData						= "$sharedData";
		internal 	const 	string 		kTypeDescription				= "$typeDescription";

		// stream header section
		internal 	const 	string 		kSerializedVersion 				= "$serializedVersion";
		internal 	const 	string 		kSerializationMethod 			= "$serializationMethod";
		internal 	const 	string 		kHeaderLength 					= "$headerLength";
		internal 	const 	string 		kTotalLength 					= "$totalLength";
		internal 	const 	string 		kCRC 							= "$crc";
		internal 	const 	string 		kDataAddress 					= "$dataAddress";
		internal 	const 	string 		kSharedDataAddress 				= "$sharedDataAddress";
		internal 	const 	string 		kTypeDescriptionAddress 		= "$typeDescriptionAddress";

		// type descriptor section
		internal 	const 	string 		kAssemblyElement				= "$assembly";
		internal 	const 	string 		kTypeElement 					= "$type";
		internal 	const 	string 		kTypes							= "$types";
		internal 	const 	string 		kTypeId 						= "$typeId";
		internal 	const 	string 		kGenericArgumentTypeIds 		= "$genArgTypeIds";
		internal 	const 	string 		kSchemaType						= "$schemaType";
		internal 	const 	string 		kSchemaMembers					= "$members";

		// generic keys
		internal 	const 	string 		kName							= "$name";
		internal 	const 	string 		kValue 							= "$value";
		internal 	const 	string 		kCount 							= "$count";
		internal 	const 	string 		kReferenceId 					= "$referenceId";
		internal 	const 	string 		kEnumValueIndex 				= "$enumIndex";
		internal 	const 	string 		kInternal 						= "$internal";
		internal 	const 	string 		kOffset							= "$offset";

		// array attributes	
		internal 	const 	string 		kArrayElementTypeId 			= "$elementTypeId";
		internal 	const 	string 		kArrayElements		 			= "$elements";
		internal 	const 	string 		kArrayRank	 					= "$rank";
		internal 	const 	string 		kArrayLengthFormat 				= "$length{0}";
		internal 	const 	string 		k1dArrayElementNameFormat 		= "[{0}]";
		internal 	const 	string 		k2dArrayElementNameFormat 		= "[{0},{1}]";
		internal 	const 	string 		kJaggedArrayElementNameFormat 	= k1dArrayElementNameFormat;

		// unity objects
		internal 	const	string		kGameObjectGuid					= "gameObjectGuid";
		internal 	const	string		kParentObjectGuid				= "parentObjectGuid";
		internal 	const	string		kComponentGuid					= "componentGuid";

		#endregion
	}
}