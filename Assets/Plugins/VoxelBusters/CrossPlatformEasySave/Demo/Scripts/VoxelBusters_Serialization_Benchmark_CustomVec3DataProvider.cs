using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class VoxelBusters_Serialization_Benchmark_CustomVec3DataProvider : SerializationDataProvider<VoxelBusters.Serialization.Benchmark.CustomVec3>
	{
		#region SerializationDataProvider abstract members implementation

		public override void Serialize(VoxelBusters.Serialization.Benchmark.CustomVec3 obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty(obj.X);
			writer.WriteProperty(obj.Y);
			writer.WriteProperty(obj.Z);
		}
		
		public override VoxelBusters.Serialization.Benchmark.CustomVec3 CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new VoxelBusters.Serialization.Benchmark.CustomVec3();
		}
		
		public override VoxelBusters.Serialization.Benchmark.CustomVec3 Deserialize(VoxelBusters.Serialization.Benchmark.CustomVec3 obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.X = reader.ReadProperty<float>();
			obj.Y = reader.ReadProperty<float>();
			obj.Z = reader.ReadProperty<float>();
			
			return obj;
		}
		
		#endregion
	}
}