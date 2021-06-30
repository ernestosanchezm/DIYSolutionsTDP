using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace VoxelBusters.Serialization
{
	[InitializeOnLoad]
	internal class PluginManager 
	{
        #region Static constructors

        static PluginManager()
        {
            // register for editor application callback
            EditorApplication.delayCall += OnInitialise;
        }

		#endregion

		#region Private static methods

		private static void OnInitialise()
		{
			// create tag reserved for the plugin
			SerializationUtilityInternal.CreateTag(Constants.kSceneObjectIdStoreTagName);
		}

		#endregion
	}
}