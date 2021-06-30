using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.Serialization;
using System;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_TransformDataProvider : SerializationDataProvider<UnityEngine.Transform>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Transform obj, IObjectWriter writer, SerializationContext context)
		{
			// get parent guid
			string parentGuid = null;
			Transform parent = obj.parent;
			if (parent != null)
			{
				SceneObjectIdentifierStoreManager.TryGetGameObjectGuid(parent.gameObject, out parentGuid);
			}

			// write declared property values
			writer.WriteProperty(KeyStore.kParentObjectGuid, parentGuid);
			writer.WriteProperty("localPosition", obj.localPosition);
			writer.WriteProperty("localRotation", obj.localRotation);
			writer.WriteProperty("localScale", obj.localScale);
			writer.WriteProperty("siblingIndex", obj.GetSiblingIndex());

			// write parent property values
			SerializationDataProvider<UnityEngine.Component>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.Transform CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.Transform)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.Transform Deserialize(UnityEngine.Transform obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			string 		parentGuid	= reader.ReadProperty<String>(KeyStore.kParentObjectGuid);
			if (parentGuid != null)
			{
				GameObject	parentGO;
				if (SceneObjectIdentifierStoreManager.TryGetGameObjectWithGuid(parentGuid, out parentGO))
				{
					obj.SetParent(parentGO.transform, false);
				}
			}

			obj.localPosition = reader.ReadProperty<UnityEngine.Vector3>("localPosition");
			obj.localRotation = reader.ReadProperty<UnityEngine.Quaternion>("localRotation");
			obj.localScale = reader.ReadProperty<UnityEngine.Vector3>("localScale");
			obj.SetSiblingIndex(reader.ReadProperty<int>("siblingIndex"));
			  
			// read parent properties
			return (UnityEngine.Transform)SerializationDataProvider<UnityEngine.Component>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}