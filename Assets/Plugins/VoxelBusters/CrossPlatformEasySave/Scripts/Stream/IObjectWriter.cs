using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Type = System.Type;

namespace VoxelBusters.Serialization
{
	public interface IObjectWriter 
	{
		#region Write methods

		void WriteProperty(string name, bool value);
		void WriteProperty(string name, char value);
		void WriteProperty(string name, sbyte value);
		void WriteProperty(string name, byte value);
		void WriteProperty(string name, short value);
		void WriteProperty(string name, ushort value);
		void WriteProperty(string name, int value);
		void WriteProperty(string name, uint value);
		void WriteProperty(string name, long value);
		void WriteProperty(string name, ulong value);
		void WriteProperty(string name, float value);
		void WriteProperty(string name, double value);
		void WriteProperty(string name, decimal value);
		void WriteProperty(string name, string value);
        void WriteProperty(string name, object value);
        void WriteProperty(string name, object value, Type type);
		void WriteProperty<T>(string name, T value);

		void WriteProperty(bool value);
		void WriteProperty(char value);
		void WriteProperty(sbyte value);
		void WriteProperty(byte value);
		void WriteProperty(short value);
		void WriteProperty(ushort value);
		void WriteProperty(int value);
		void WriteProperty(uint value);
		void WriteProperty(long value);
		void WriteProperty(ulong value);
		void WriteProperty(float value);
		void WriteProperty(double value);
		void WriteProperty(decimal value);
		void WriteProperty(string value);
        void WriteProperty(object value);
        void WriteProperty(object value, Type type);
		void WriteProperty<T>(T value);

		void WriteArrayLength(int dimension, int value);

		#endregion
	}
}