using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using Debug = UnityEngine.Debug;

namespace VoxelBusters.Serialization.Examples
{
	public class PrimitiveValueSerializationExample : MonoBehaviour 
	{
		#region Fields

		[SerializeField]
		private			InputField 		m_inputField;

		#endregion

		#region Button callback methods

		public void SerializeIntValue()
		{
			SerializationManager.BeginSerializeGroup("test");
			SerializationManager.Serialize(key: "intValue", 	value: int.Parse(m_inputField.text));
			SerializationManager.Serialize(key: "stringValue",	value: m_inputField.text);
			SerializationManager.Serialize(key: "vec2", 		value: Vector2.down);
			SerializationManager.Serialize(key: "intArray", 	value: new int[] {1, 2});
			SerializationManager.EndSerializeGroup();
		}

		public void DeserializeIntValue()
		{
			SerializationManager.BeginDeserializeGroup("test");
			Debug.Log("intValue :" + SerializationManager.Deserialize<int>("intValue"));
			Debug.Log("stringValue :" + SerializationManager.Deserialize<string>("stringValue"));
			Debug.Log("vec2 :" + SerializationManager.Deserialize<Vector2>("vec2"));
			Debug.Log("intArray :" + SerializationManager.Deserialize<int[]>("intArray").GetPrintableString());
			SerializationManager.EndDeserializeGroup();
		}

		public void SerializeStringValue()
		{
			SerializationManager.Serialize(key: "stringValue", value: m_inputField.text);
		}

		public void DeserializeStringValue()
		{
			string	value		= SerializationManager.Deserialize<string>("stringValue");
			m_inputField.text 	= value.ToString();
			Debug.Log("String value after deserialize: " + m_inputField.text);
		}

		#endregion
	}
}