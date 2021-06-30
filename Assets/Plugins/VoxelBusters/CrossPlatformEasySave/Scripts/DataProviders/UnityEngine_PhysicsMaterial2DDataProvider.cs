using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_PhysicsMaterial2DDataProvider : SerializationDataProvider<UnityEngine.PhysicsMaterial2D>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.PhysicsMaterial2D obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("bounciness", obj.bounciness);
			writer.WriteProperty("friction", obj.friction);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Object>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.PhysicsMaterial2D CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new UnityEngine.PhysicsMaterial2D();
		}
		
		public override UnityEngine.PhysicsMaterial2D Deserialize(UnityEngine.PhysicsMaterial2D obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.bounciness = reader.ReadProperty<float>();
			obj.friction = reader.ReadProperty<float>();
			
			// read parent property values
			return (UnityEngine.PhysicsMaterial2D)SerializationDataProvider<UnityEngine.Object>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}