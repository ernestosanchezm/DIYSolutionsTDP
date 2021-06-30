using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_GameObjectDataProvider : SerializationDataProvider<UnityEngine.GameObject>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.GameObject obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			string gameObjectGuid;
			SceneObjectIdentifierStoreManager.TryGetGameObjectGuid(obj, out gameObjectGuid);

			writer.WriteProperty(KeyStore.kGameObjectGuid, gameObjectGuid);
			writer.WriteProperty("activeSelf", obj.activeSelf);
			writer.WriteProperty("layer", obj.layer);
			writer.WriteProperty("isStatic", obj.isStatic);
			writer.WriteProperty("tag", obj.tag);

			// serialize other related properties, if specified
			if (context.Supports(SerializationMethodOptions.GameObjectSerializeAttachedComponents))
			{
				writer.WriteProperty("components", obj.GetComponents<Component>());
			}
			else if (context.Supports(SerializationMethodOptions.GameObjectSerializeTransform))
			{
				writer.WriteProperty("transform", obj.transform);
			}
			// check whether children properties need to be serialized
			if (context.Supports(SerializationMethodOptions.GameObjectSerializeChildrens))
			{
				Transform		transform	= obj.transform;
				int 			childCount	= transform.childCount;
				GameObject[]	childrens	= new GameObject[childCount];
				for (int iter = 0; iter < childCount; iter++)
				{
					childrens[iter] 		= transform.GetChild(iter).gameObject;
				}
				writer.WriteProperty("childrens", childrens);
			}

			// write parent property values
			SerializationDataProvider<UnityEngine.Object>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.GameObject CreateInstance(IObjectReader reader, SerializationContext context)
		{
			GameObject	gameObject 		= null;
			string		gameObjectGuid	= reader.ReadProperty<string>();
			if (gameObjectGuid != null)
			{
				SceneObjectIdentifierStoreManager.TryGetGameObjectWithGuid(gameObjectGuid, out gameObject);
			}
			gameObject					= gameObject ?? SerializationUtility.CreateGameObjectWithGuid(gameObjectGuid);

			return gameObject;
		}
		
		public override UnityEngine.GameObject Deserialize(UnityEngine.GameObject obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.SetActive(value: reader.ReadProperty<bool>("activeSelf"));
			obj.layer 	 = reader.ReadProperty<int>("layer");
			obj.isStatic = reader.ReadProperty<bool>("isStatic");
			obj.tag 	 = reader.ReadProperty<string>("tag");

			// read optional properties
			if (context.Supports(SerializationMethodOptions.GameObjectSerializeAttachedComponents))
			{
				reader.ReadProperty<Component[]>("components");
			}
			else if (context.Supports(SerializationMethodOptions.GameObjectSerializeTransform))
			{
				reader.ReadProperty<Transform>("transform");
			}
			// read children
			if (context.Supports(SerializationMethodOptions.GameObjectSerializeChildrens))
			{
				reader.ReadProperty<GameObject[]>("childrens");
			}

			// read parent property values
			return (UnityEngine.GameObject)SerializationDataProvider<UnityEngine.Object>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}