using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class UnityEngine_Matrix4x4DataProvider : SerializationDataProvider<UnityEngine.Matrix4x4>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(UnityEngine.Matrix4x4 obj, VoxelBusters.Serialization.IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty(obj.m00);
			writer.WriteProperty(obj.m10);
			writer.WriteProperty(obj.m20);
			writer.WriteProperty(obj.m30);
			writer.WriteProperty(obj.m01);
			writer.WriteProperty(obj.m11);
			writer.WriteProperty(obj.m21);
			writer.WriteProperty(obj.m31);
			writer.WriteProperty(obj.m02);
			writer.WriteProperty(obj.m12);
			writer.WriteProperty(obj.m22);
			writer.WriteProperty(obj.m32);
			writer.WriteProperty(obj.m03);
			writer.WriteProperty(obj.m13);
			writer.WriteProperty(obj.m23);
			writer.WriteProperty(obj.m33);
		}
		
		public override UnityEngine.Matrix4x4 CreateInstance(VoxelBusters.Serialization.IObjectReader reader, SerializationContext context)
		{
			return new UnityEngine.Matrix4x4();
		}
		
		public override UnityEngine.Matrix4x4 Deserialize(UnityEngine.Matrix4x4 obj, VoxelBusters.Serialization.IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.m00 = reader.ReadProperty<float>();
			obj.m10 = reader.ReadProperty<float>();
			obj.m20 = reader.ReadProperty<float>();
			obj.m30 = reader.ReadProperty<float>();
			obj.m01 = reader.ReadProperty<float>();
			obj.m11 = reader.ReadProperty<float>();
			obj.m21 = reader.ReadProperty<float>();
			obj.m31 = reader.ReadProperty<float>();
			obj.m02 = reader.ReadProperty<float>();
			obj.m12 = reader.ReadProperty<float>();
			obj.m22 = reader.ReadProperty<float>();
			obj.m32 = reader.ReadProperty<float>();
			obj.m03 = reader.ReadProperty<float>();
			obj.m13 = reader.ReadProperty<float>();
			obj.m23 = reader.ReadProperty<float>();
			obj.m33 = reader.ReadProperty<float>();
			
			return obj;
		}
		
		#endregion
	}
}