using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace VoxelBusters.Serialization
{
    internal class EditorApplicationModificationProcessor
    {
        #region Delegates

        public delegate void HierarchyChangedCallback();

        #endregion

        #region Static events

        public static event HierarchyChangedCallback hierarchyChanged;

        #endregion

        #region Static constructors

        static EditorApplicationModificationProcessor()
        {
#if UNITY_2018_2_OR_NEWER
            EditorApplication.hierarchyChanged += OnHierarchyWindowChanged;
#else
            EditorApplication.hierarchyWindowChanged += OnHierarchyWindowChanged;
#endif
        }

        #endregion

        #region Callback methods

		private static void OnHierarchyWindowChanged()
		{
			if (hierarchyChanged != null)
			{
				hierarchyChanged();
			}
		}

        #endregion
	}
}