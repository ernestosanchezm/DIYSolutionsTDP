using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public class VoxelBusters_Serialization_Benchmark_PersonDataProvider : SerializationDataProvider<VoxelBusters.Serialization.Benchmark.Person>
	{
		#region SerializationDataProvider abstract members implementation
		
		public override void Serialize(VoxelBusters.Serialization.Benchmark.Person obj, IObjectWriter writer, SerializationContext context)
		{
			// write declared property values
			writer.WriteProperty(obj.Age);
			writer.WriteProperty(obj.FirstName);
			writer.WriteProperty(obj.LastName);
			writer.WriteProperty(obj.Sex);
		}
		
		public override VoxelBusters.Serialization.Benchmark.Person CreateInstance(IObjectReader reader, SerializationContext context)
		{
			return new VoxelBusters.Serialization.Benchmark.Person();
		}
		
		public override VoxelBusters.Serialization.Benchmark.Person Deserialize(VoxelBusters.Serialization.Benchmark.Person obj, IObjectReader reader, SerializationContext context)
		{
			// read declared property values
			obj.Age = reader.ReadProperty<int>();
			obj.FirstName = reader.ReadProperty<string>();
			obj.LastName = reader.ReadProperty<string>();
			obj.Sex = reader.ReadProperty<VoxelBusters.Serialization.Benchmark.Sex>();
			
			return obj;
		}
		
		#endregion
	}
}