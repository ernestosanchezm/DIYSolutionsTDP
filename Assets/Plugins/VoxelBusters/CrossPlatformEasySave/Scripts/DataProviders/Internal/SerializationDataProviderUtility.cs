using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
	public static class SerializationDataProviderUtility 
	{
		#region Collection methods

		public static DictionaryEntry[] ToArray(this IDictionary dictionary)
		{
			DictionaryEntry[]		entryArray = new DictionaryEntry[dictionary.Count];
			IDictionaryEnumerator	enumerator	= dictionary.GetEnumerator();

			int	iter = 0;
			while (enumerator.MoveNext())
			{
				entryArray[iter++]	= enumerator.Entry;
			}
			return entryArray;
		}

        public static object[] ToArray(this IEnumerator enumerator)
        {
            List<object> itemList = new List<object>(capacity: 4);
            while (enumerator.MoveNext())
            {
                itemList.Add(enumerator.Current);
            }

            return itemList.ToArray();
        }

        #endregion

    }
}