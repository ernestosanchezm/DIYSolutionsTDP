using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_Collider2DDataProvider : SerializationDataProvider<UnityEngine.Collider2D>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Collider2D obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty("density", obj.density);
			writer.WriteProperty("isTrigger", obj.isTrigger);
			writer.WriteProperty("usedByEffector", obj.usedByEffector);
			writer.WriteProperty("usedByComposite", obj.usedByComposite);
			writer.WriteProperty("offset", obj.offset);
			writer.WriteProperty("sharedMaterial", obj.sharedMaterial);
			
			// write parent property values
			SerializationDataProvider<UnityEngine.Behaviour>.Default.Serialize(obj, writer, context);
		}
		
		public override UnityEngine.Collider2D CreateInstance(IObjectReader reader, SerializationContext context)
		{
			// special case: Component data provider contains instructions to create instances of its derived types
			return (UnityEngine.Collider2D)SerializationDataProvider<UnityEngine.Component>.Default.CreateInstance(reader, context);
		}
		
		public override UnityEngine.Collider2D Deserialize(UnityEngine.Collider2D obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.density = reader.ReadProperty<float>("density");
			obj.isTrigger = reader.ReadProperty<bool>("isTrigger");
			obj.usedByEffector = reader.ReadProperty<bool>("usedByEffector");
			obj.usedByComposite = reader.ReadProperty<bool>("usedByComposite");
			obj.offset = reader.ReadProperty<UnityEngine.Vector2>("offset");
			obj.sharedMaterial = reader.ReadProperty<UnityEngine.PhysicsMaterial2D>("sharedMaterial");
			
			// read parent property values
			return (UnityEngine.Collider2D)SerializationDataProvider<UnityEngine.Behaviour>.Default.Deserialize(obj, reader, context);
		}
		
		#endregion
	}
}