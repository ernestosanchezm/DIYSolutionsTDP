using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.External.UnityEngineUtils;

namespace VoxelBusters.Serialization
{
	// The main objective of this class is to create the ideal environment required for serialization operation
	// This includes initialisation of static classes, caching data providers etc
	internal static class SerializationEnvironment 
	{
		#region Properties

        internal static bool IsAOT
        {
            get;
            private set;
        }

		internal static string DataPath
		{
			get;
			private set;
		}

		internal static string TempDataPath
		{
			get;
			private set;
		}

		#endregion

		#region Constructors

		static SerializationEnvironment()
		{
			// prepare environment
			DataPath 		= IOServices.CombinePath(path1: Application.persistentDataPath, path2: SerializationSettings.SaveFolderName);
			TempDataPath	= IOServices.CombinePath(path1: DataPath, path2: "Temp");
            IsAOT           = IsAOTPlatform();

            // manually invoke static class constructutors which prefetches required info
            typeof(SerializationKnownTypes).StaticConstructor();
			typeof(SerializationGlobalCache).StaticConstructor();
			typeof(SerializationRecordsManager).StaticConstructor();
            typeof(AOTStubs).StaticConstructor();

            SerializationDataProviderServices.SearchAllDataProviderTypes();
		}

		#endregion

		#region Static methods

		internal static bool IsFileStreamSupported()
		{
			return (ApplicationUtils.GetActivePlatform() != RuntimePlatform.WebGLPlayer);
		}

        internal static bool IsAOTPlatform()
        {
            RuntimePlatform platform = ApplicationUtils.GetActivePlatform();
            if ((RuntimePlatform.IPhonePlayer == platform) ||
                (RuntimePlatform.Android == platform))
            {
                return true;
            }
        
            return false;
        }

		#endregion
	}
}