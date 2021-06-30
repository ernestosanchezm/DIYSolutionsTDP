#define ENABLE_BINARY_FORMATTER_TEST
//#define USE_FILE_STREAM

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Profiling;

using Debug = UnityEngine.Debug;

namespace VoxelBusters.Serialization.Benchmark
{
	public class Benchmark : MonoBehaviour 
	{
		#region Constants

		private		const 	int 	kWarmUpIteration 	= 1;
		private		const 	int 	kRunIteration 		= 1000;

		#endregion

		#region Static fields

		private 	static 	int 	iteration 			= 1;
		private		static 	bool 	dryRun 				= true;
		private		static 	bool	run					= false;

		#endregion

		#region Callback methods

		public void OnRunButtonPressed()
		{
			run	= true;
		}

		#endregion

		#region Unity methods

		private void Update()
		{ 
			if (run)
			{
				Run();
				run = false;
			}
		}

		#endregion

		#region Static methods

		public static void Run()
		{
			var p = new Person
			{
				Age = 99999,
				FirstName = "Windows",
				LastName = "Server",
				Sex = Sex.Male,
			};
			IList<Person> l = Enumerable.Range(100, 100).Select(x => new Person { Age = x, FirstName = "Windows", LastName = "Server", Sex = Sex.Female }).ToArray();

			var integer = 1;
			var v3 = new CustomVec3(x: 12345.12345f, y: 3994.35226f, z: 325125.52426f);
			IList<CustomVec3> v3List = Enumerable.Range(1, 100).Select(_ => new CustomVec3(x: 12345.12345f, y: 3994.35226f, z: 325125.52426f)).ToArray();
			string str	= "akhfoapngkoabnkgvgnfo naskdnakdnkasnfkn ;lpolapna;lkn klanlknklj32poeur9 2uriooiqbljkasnlk nalkfnalksfbnlkabnglkansflkfn2u290kjafbkjhsiuhriqholmnwcolmnvcpoqr0";

			dryRun 		= true;
			iteration 	= kWarmUpIteration;
			Debug.Log("Warming-up");
			TestCPES("warm-up1", p); 
			#if ENABLE_BINARY_FORMATTER_TEST
			TestBinaryFormatter("warm-up2", p);
			#endif

			dryRun 		= false;
			iteration 	= kRunIteration;
			Debug.Log("Testing");

			#pragma warning disable
			Debug.Log(string.Format("Small Object(int,string,string,enum) {0} iterations", iteration)); 
			var A1 		= TestCPES("A1", p);
			#if ENABLE_BINARY_FORMATTER_TEST
			var A2		= TestBinaryFormatter("A2", p);
			#endif

			Debug.Log(string.Format("Large Array(SmallObject[1000]) {0} iterations", iteration)); 
			var B1 		= TestCPES("B1", l);
			#if ENABLE_BINARY_FORMATTER_TEST
			var B2		= TestBinaryFormatter("B2", l);
			#endif

			Debug.Log("Additional Benchmarks");
			Debug.Log(string.Format("String(1) {0} iterations", iteration));
			var C1 		= TestCPES("C1", str);
			#if ENABLE_BINARY_FORMATTER_TEST
			var C2 		= TestBinaryFormatter("C2", str);
			#endif

			Debug.Log(string.Format("Int32(1) {0} iterations", iteration));
			var D1 		= TestCPES("D1", integer);
			#if ENABLE_BINARY_FORMATTER_TEST
			var D2 		= TestBinaryFormatter("D2", integer);
			#endif

			Debug.Log(string.Format("Vector3(float, float, float) {0} iterations", iteration)); 
			var E1 		= TestCPES("E1", v3);
			#if ENABLE_BINARY_FORMATTER_TEST
			var E2 		= TestBinaryFormatter("E2", v3);
			#endif

			Debug.Log(string.Format("Vector3[100] {0} iterations", iteration)); 
			var F1 		= TestCPES("F1", v3List);
			#if ENABLE_BINARY_FORMATTER_TEST
			var F2 		= TestBinaryFormatter("F2", v3List);
			#endif
			#pragma warning restore
		}

        #pragma warning disable
		public static void TestInstantiate(Benchmark instance)
		{
			using (new Measure("Struct"))
			{
				for (int i = 0; i < iteration; i++)
				{
					SampleStruct newStruct = new SampleStruct(1, 2f, "hello");
				}
			}

			using (new Measure("Class"))
			{
				for (int i = 0; i < iteration; i++)
				{
					SampleClass newClass = new SampleClass(1, 2f, "hello");
				}
			}

			using (new Measure("Dict"))
			{
				for (int i = 0; i < iteration; i++)
				{
					Dictionary<string, SampleClass> dict = new Dictionary<string, SampleClass>(10);
					for (int iter = 0; iter < 10; iter++)
					{
						dict.Add(iter.ToString(), new SampleClass(1, 2f, "hello"));
					}
				}
			}

			using (new Measure("List"))
			{
				for (int i = 0; i < iteration; i++)
				{
					IList<SampleClass> list = new List<SampleClass>(10);
					for (int iter = 0; iter < 10; iter++)
					{
						list.Add(new SampleClass(1, 2f, "hello"));
					}
				}
			}

			using (new Measure("Array"))
			{
				for (int i = 0; i < iteration; i++)
				{
					SampleClass[] list = new SampleClass[10];
					for (int iter = 0; iter < 10; iter++)
					{
						list[iter] = new SampleClass(1, 2f, "hello");
					}
				}
			}

			using (new Measure("Method.Invoke"))
			{
				for (int i = 0; i < iteration; i++)
				{
					SampleClass[] list = new SampleClass[10];
					for (int iter = 0; iter < 10; iter++)
					{
						IntAdd(1, 2);
					}
				}
			}

			using (new Measure("Action.Invoke"))
			{
				for (int i = 0; i < iteration; i++)
				{
					SampleClass[] list = new SampleClass[10];
					for (int iter = 0; iter < 10; iter++)
					{
						Action<int, int> action = instance.Sum;
						action.Invoke(1, 2);
					}
				}
			}

			using (new Measure("Delegate.Invoke"))
			{
				for (int i = 0; i < iteration; i++)
				{
					SampleClass[] list = new SampleClass[10];
					for (int iter = 0; iter < 10; iter++)
					{
						Method2Params action = instance.Sum;
						action.Invoke(1, 2);
					}
				}
			}

			using (new Measure("Boxing"))
			{
				for (int i = 0; i < iteration; i++)
				{
					SampleClass[] list = new SampleClass[10];
					for (int iter = 0; iter < 10; iter++)
					{
						instance.SumObject((object)1, (object)2);
					}
				}
			}

			using (new Measure("Hack"))
			{
				for (int i = 0; i < iteration; i++)
				{
					SampleClass[] list = new SampleClass[10];
					for (int iter = 0; iter < 10; iter++)
					{
						GenericAdd(1, 2);
					}
				}
			}
		}
        #pragma warning restore

		public delegate void Method2Params(int a, int b);

        #pragma warning disable
		private void Sum(int a, int b)
		{
			int c = a + b;
		}

		private void SumObject(object a, object b)
		{
			int c = (int)a + (int)b;
		}

		private static void GenericAdd<T>(T a, T b)
		{
			object method = (Action<int, int>)IntAdd;
			((Action<T, T>)method).Invoke(a, b);
		}

		private static void IntAdd(int a, int b)
		{
			int c = a + b;
		}

		private static void FloatAdd(float a, float b)
		{
			float c = a + b;
		}
        #pragma warning restore

		public static T TestCPES<T>(string label, T original)
		{
			T 			copy 		= default(T);
			#if USE_FILE_STREAM
			string		savePath 	= Path.Combine(Application.persistentDataPath, label + ".binary");
			#endif
			Settings	settings	= SerializationSettings.Copy();
			byte[] 		rawData 	= null;

			using (new Measure("CPS - Serialize"))
			{
				for (int i = 0; i < iteration; i++)
				{
					SerializationContext	context			= new SerializationContext(SerializationMode.Serialize, settings, label);
					#if USE_FILE_STREAM
					Stream 					stream 			= new FileStream(savePath, FileMode.Create);
					#else
					Stream 					stream 			= new MemoryStream(capacity: settings.BufferSize);
					#endif
					ObjectWriter			objectWriter	= new ObjectWriter(stream, context);
					objectWriter.WriteProperty(original);
					objectWriter.Close();

					if (stream is MemoryStream)
					{
						rawData = ((MemoryStream)stream).ToArray();
					}
				}
			}

			using (new Measure("Deserialize"))
			{
				for (int i = 0; i < iteration; i++)
				{
					SerializationContext	context			= new SerializationContext(SerializationMode.Deserialize, settings.StorageTarget, label);
					#if USE_FILE_STREAM
					Stream 					stream 			= new FileStream(savePath, FileMode.Open);
					#else
					Stream 					stream 			= new MemoryStream(rawData);
					#endif
					ObjectReader			objectReader	= new ObjectReader(stream , context);
					copy = objectReader.ReadProperty<T>();
					objectReader.Close();
				}
			}

			return copy;
		}

		public static T TestBinaryFormatter<T>(string label, T original)
		{
			T 			copy 		= default(T);
			#if USE_FILE_STREAM
			string		savePath 	= Path.Combine(Application.persistentDataPath, label + ".binary");
			#endif
			Settings	settings	= SerializationSettings.Copy();
			byte[] 		rawData 	= null;

			using (new Measure("Binary - Serialize"))
			{
				for (int i = 0; i < iteration; i++)
				{
					BinaryFormatter 		formatter 	= new BinaryFormatter();
					#if USE_FILE_STREAM
					Stream 					stream 		= new FileStream(savePath, FileMode.Create);
					#else
					Stream 					stream 		= new MemoryStream(capacity: settings.BufferSize);
					#endif
					formatter.Serialize(stream, original);
					stream.Close();

					if (stream is MemoryStream)
					{
						rawData = ((MemoryStream)stream).ToArray();
					}
				}
			}

			using (new Measure("Deserialize"))
			{
				for (int i = 0; i < iteration; i++)
				{
					BinaryFormatter formatter 	= new BinaryFormatter();
					#if USE_FILE_STREAM
					Stream 			stream 		= new FileStream(savePath, FileMode.Open);
					#else
					MemoryStream 	stream 		= new MemoryStream(rawData);
					#endif
					copy 						= (T)formatter.Deserialize(stream);
					stream.Close();
				}
			}

			return copy;
		}

		static string ToHumanReadableSize(long size)
		{
			return ToHumanReadableSize(new Nullable<long>(size));
		}

		static string ToHumanReadableSize(long? size)
		{
			if (size == null) return "NULL";

			double bytes = size.Value;

			if (bytes <= 1024) return bytes.ToString("f2") + " B";

			bytes = bytes / 1024;
			if (bytes <= 1024) return bytes.ToString("f2") + " KB";

			bytes = bytes / 1024;
			if (bytes <= 1024) return bytes.ToString("f2") + " MB";

			bytes = bytes / 1024;
			if (bytes <= 1024) return bytes.ToString("f2") + " GB";

			bytes = bytes / 1024;
			if (bytes <= 1024) return bytes.ToString("f2") + " TB";

			bytes = bytes / 1024;
			if (bytes <= 1024) return bytes.ToString("f2") + " PB";

			bytes = bytes / 1024;
			if (bytes <= 1024) return bytes.ToString("f2") + " EB";

			bytes = bytes / 1024;
			return bytes + " ZB";
		}

		#endregion

		#region Nested types

		struct Measure : System.IDisposable
		{
			string label;
			Stopwatch s;

			public Measure(string label)
			{
				this.label = label;
				System.GC.Collect(2, GCCollectionMode.Forced);
				this.s = Stopwatch.StartNew();
			}

			public void Dispose()
			{
				s.Stop();
				if (!dryRun)
				{
					Debug.Log(string.Format("{0}   {1}ms", label, s.Elapsed.TotalMilliseconds));
				}

				System.GC.Collect(2, GCCollectionMode.Forced);
			}
		}

		#endregion
	}
}