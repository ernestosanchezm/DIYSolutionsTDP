using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using Type = System.Type;
using Array = System.Array;

namespace VoxelBusters.Serialization
{
	internal class ResourceObjectIdentifierStore : ResourceObjectIdentifierStoreBase 
	{
		#region Fields

		private		Dictionary<Type, ResourceObject[]> 	m_groupedObjects;

		#endregion

		#region Unity methods

		private void OnEnable()
		{
			// prepare store
			RegroupObjects();
		}

		#endregion

		#region Private methods

		private void RegroupObjects()
		{
			m_groupedObjects	= m_resourceMetaList.GroupBy((item) => item.ObjectType).ToDictionary(element => element.Key, element => element.ToArray());
		}

		#endregion

		#region Query methods

		// Find the SceneObjectMetadata reference for the given gameobject
		public override ResourceObject Find(Object asset)
		{
			ResourceObject[] targetObjects;
			if (m_groupedObjects.TryGetValue(asset.GetType(), out targetObjects))
			{
				return Array.Find(targetObjects, (item) => (asset == item.Object));
			}

			return null;
		}

		public override ResourceObject Find(string guid)
		{
			var enumerator = m_groupedObjects.GetEnumerator();
			while (enumerator.MoveNext())
			{
				ResourceObject[] objects = enumerator.Current.Value;
				return Array.Find(objects, (item) => (guid == item.Guid));
			}

			return null;
		}

		public ResourceObject Find(string guid, Type type)
		{
			ResourceObject[] targetObjects;
			if (m_groupedObjects.TryGetValue(type, out targetObjects))
			{
				return Array.Find(targetObjects, (item) => (guid == item.Guid));
			}

			return null;
		}

		#endregion

		#region Editor methods

		#if UNITY_EDITOR
		private bool IsBlacklistedType(Type type)
		{
			return (type == typeof(MonoScript)) || (type == typeof(SceneAsset));
		}

		// Sanitize the whole database
		internal override void Refresh()
		{
			#if SERIALIZATION_DEBUG
			Debug.Log("[Serialization] Updating resource id store for scene " + this.name, this);
			#endif

			// clear old values
			m_resourceMetaList.Clear();

			// get the name and its the scene guid
			string 		path 			= AssetDatabase.GUIDToAssetPath(this.name);
			string[] 	dependencies 	= AssetDatabase.GetDependencies(path);

			// iterate through each dependency and check if its interested asset type
			foreach (string dependencyPath in dependencies)
			{
				Type  	type			= AssetDatabase.GetMainAssetTypeAtPath(dependencyPath);
				// add if we want to track this asset type
				if (false == IsBlacklistedType(type))
				{
					string		assetGuid		= AssetDatabase.AssetPathToGUID(dependencyPath);
					Object		asset			= AssetDatabase.LoadMainAssetAtPath(dependencyPath);
					Add(asset, assetGuid);

					string[] 	subAssetGuids;
					Object[] 	subAssets		= AssetDatabaseServices.LoadAllSubAssetsAtPath(dependencyPath, out subAssetGuids);
					int			subAssetCount 	= subAssets.Length;
					for (int iter = 0; iter < subAssetCount; iter++)
					{
						Add(subAssets[iter], subAssetGuids[iter]);
					}
				}
			}

			EditorUtility.SetDirty(this);
			AssetDatabase.SaveAssets();
		}
		#endif

		#endregion
	}
}