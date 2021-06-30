using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_PhysicMaterialDataProvider : SerializationDataProvider<UnityEngine.PhysicMaterial>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.PhysicMaterial obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("dynamicFriction", obj.dynamicFriction);
			writer.WriteProperty("staticFriction", obj.staticFriction);
			writer.WriteProperty("bounciness", obj.bounciness);
			writer.WriteProperty("frictionCombine", obj.frictionCombine);
			writer.WriteProperty("bounceCombine", obj.bounceCombine);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Object>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.PhysicMaterial CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new UnityEngine.PhysicMaterial();
		}
		
		public override UnityEngine.PhysicMaterial Deserialize(UnityEngine.PhysicMaterial obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.dynamicFriction = reader.ReadProperty<float>();
			obj.staticFriction = reader.ReadProperty<float>();
			obj.bounciness = reader.ReadProperty<float>();
			obj.frictionCombine = reader.ReadProperty<UnityEngine.PhysicMaterialCombine>();
			obj.bounceCombine = reader.ReadProperty<UnityEngine.PhysicMaterialCombine>();
			
			// read parent property values
			return (UnityEngine.PhysicMaterial)SerializationDataProvider<UnityEngine.Object>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}