using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_ObjectDataProvider : SerializationDataProvider<UnityEngine.Object>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Object obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
            if (false == (obj is Component))
            {
			    writer.WriteProperty("name", obj.name);
            }
			writer.WriteProperty("hideFlags", obj.hideFlags);
		}
		
		public override UnityEngine.Object CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new UnityEngine.Object();
		}
		
		public override UnityEngine.Object Deserialize(UnityEngine.Object obj, IObjectReader reader, SerializationContext context)
		{
            // read declared property values
            if (false == (obj is Component))
            {
                obj.name = reader.ReadProperty<string>("name");
            }
			obj.hideFlags = reader.ReadProperty<UnityEngine.HideFlags>("hideFlags");
			
			return obj;
		}
		
		#endregion
	}
}