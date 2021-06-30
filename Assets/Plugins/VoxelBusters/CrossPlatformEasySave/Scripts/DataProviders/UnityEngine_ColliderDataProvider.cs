using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_ColliderDataProvider : SerializationDataProvider<UnityEngine.Collider>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Collider obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("enabled", obj.enabled);
			writer.WriteProperty("isTrigger", obj.isTrigger);
			writer.WriteProperty("contactOffset", obj.contactOffset);
			bool isSharedMaterial = (obj.sharedMaterial != null);
			PhysicMaterial material = isSharedMaterial ? obj.sharedMaterial : obj.material;
			writer.WriteProperty("isSharedMaterial", isSharedMaterial);
			writer.WriteProperty("material", material);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Component>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.Collider CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.Collider)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.Collider Deserialize(UnityEngine.Collider obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.enabled = reader.ReadProperty<bool>("enabled");
			obj.isTrigger = reader.ReadProperty<bool>("isTrigger");
			obj.contactOffset = reader.ReadProperty<float>("contactOffset");
			bool isSharedMaterial = reader.ReadProperty<bool>("isSharedMaterial");
			PhysicMaterial material = reader.ReadProperty<UnityEngine.PhysicMaterial>("material");
			if (isSharedMaterial)
			{
				obj.sharedMaterial = material;
			}
			else
			{
				obj.material = material;
			}

			// read parent property values
			return (UnityEngine.Collider)SerializationDataProvider<UnityEngine.Component>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}