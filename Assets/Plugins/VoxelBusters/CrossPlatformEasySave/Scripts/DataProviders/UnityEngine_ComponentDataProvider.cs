using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_ComponentDataProvider : SerializationDataProvider<UnityEngine.Component>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Component obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			string gameObjectGuid;
			SceneObjectIdentifierStoreManager.TryGetGameObjectGuid(obj.gameObject, out gameObjectGuid);
			string componentGuid;
			SceneObjectIdentifierStoreManager.TryGetComponentGuid(obj, out componentGuid);

			writer.WriteProperty(KeyStore.kGameObjectGuid, gameObjectGuid);
			writer.WriteProperty(KeyStore.kComponentGuid, componentGuid);

			// write parent property values
			SerializationDataProvider<UnityEngine.Object>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.Component CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// find owner object, create if it doesn't exist
			string 		gameObjectGuid	= reader.ReadProperty<string>(KeyStore.kGameObjectGuid);
			GameObject	gameObject 		= null;
			if (gameObjectGuid != null)
			{
				SceneObjectIdentifierStoreManager.TryGetGameObjectWithGuid(gameObjectGuid, out gameObject);
			}
			gameObject	= gameObject ?? SerializationUtility.CreateGameObjectWithGuid(gameObjectGuid);

			// find original component, create if it doesn't exist
			string 		componentGuid 	= reader.ReadProperty<string>(KeyStore.kComponentGuid);
			Component	component 		= null;
			if (componentGuid != null)
			{
				SceneObjectIdentifierStoreManager.TryGetComponentWithGuid(componentGuid, gameObject, out component);
			}
			component	= component ?? SerializationUtility.AddComponentWithGuid(gameObject, reader.GetObjectType(), componentGuid);

			return component;
		}
		
		public override UnityEngine.Component Deserialize(UnityEngine.Component obj, IObjectReader reader, SerializationContext context)
		{
			// read parent property values
			return (UnityEngine.Component)SerializationDataProvider<UnityEngine.Object>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}