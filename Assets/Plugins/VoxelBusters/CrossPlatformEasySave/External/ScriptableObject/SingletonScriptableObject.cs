using UnityEngine;
using System.Collections;

using Type = System.Type;

namespace VoxelBusters.External.UnityEngineUtils
{
	public abstract class SingletonScriptableObject<T> : ScriptableObject, ISaveAssetCallback where T : ScriptableObject
	{
		#region Constants

		private		const		string 		kDefaultPathFormat 			= "Assets/Resources/SharedData/{0}.asset";

		#endregion

		#region Static Fields

		private		static		T			instance					= null;

		#endregion

		#region Static Properties

		public static T Instance
		{
			get 
			{ 
				if (instance == null)
				{
					string	path	= GetAssetPath(typeof(T));
					instance		= ScriptableObjectUtils.LoadAssetAtPath<T>(path);

					#if UNITY_EDITOR
					if (instance == null)
					{
						instance	= ScriptableObjectUtils.Create<T>(path);
					}
					#endif
				}

				return instance;
			}
			private set
			{
				instance	= value;
			}
		}

		#endregion

		#region Static methods

		internal static string GetAssetPath(Type type)
		{
			object[]	attributes	= type.GetCustomAttributes(typeof(AssetCollectionFolderAttribute), inherit: false);
			if (attributes.Length > 0)
			{
				string	folderPath	= ((AssetCollectionFolderAttribute)attributes[0]).FolderPath;
				return string.Format("{0}/{1}.asset", folderPath, type.Name);
			}

			return string.Format(kDefaultPathFormat, type.Name);
		}

		#endregion

		#region Unity Callbacks

		protected virtual void Reset()
		{}

		protected virtual void OnEnable()
		{
			if (instance == null)
			{
				instance	= this as T;
			}
		}

		protected virtual void OnDisable()
		{}

		protected virtual void OnDestroy()
		{}

		#endregion

		#region Editor Methods

		#if UNITY_EDITOR
		public void Save()
		{
			this.SaveChanges();
		}
		#endif

		#endregion

		#region ISaveAssetCallback Implementation

		public virtual void OnBeforeSave()
		{}

		#endregion
	}
}