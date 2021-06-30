using UnityEngine;

#if UNITY_EDITOR	
using UnityEditor;
#endif

namespace VoxelBusters.External.UnityEngineUtils
{
	public class ApplicationUtils
	{
		#region Platform methods

		public static string GetActivePlatformAsString()
		{
#if UNITY_EDITOR
			return ConvertRuntimePlatformToString(platform: ConvertBuildTargetToRuntimePlatform(EditorUserBuildSettings.activeBuildTarget));
#else
			return ConvertRuntimePlatformToString(platform: Application.platform);
#endif
		}

		public static RuntimePlatform GetActivePlatform()
		{
#if UNITY_EDITOR
			return ConvertBuildTargetToRuntimePlatform(EditorUserBuildSettings.activeBuildTarget);
#else
			return Application.platform;
#endif
		}

		#endregion

		#region Converter methods
	
#if UNITY_EDITOR
		public static RuntimePlatform ConvertBuildTargetToRuntimePlatform(BuildTarget buildTarget)
		{
			switch (buildTarget)
			{
				case BuildTarget.iOS:
					return RuntimePlatform.IPhonePlayer;

				case BuildTarget.Android:
					return RuntimePlatform.Android;

#if UNITY_2017_3_OR_NEWER
                case BuildTarget.StandaloneOSX:
#else
                case BuildTarget.StandaloneOSXUniversal:
                case BuildTarget.StandaloneOSXIntel:
                case BuildTarget.StandaloneOSXIntel64:
#endif
                    return RuntimePlatform.OSXPlayer;

				case BuildTarget.WebGL:
					return RuntimePlatform.WebGLPlayer;

				case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
					return RuntimePlatform.WindowsPlayer;

                case BuildTarget.WSAPlayer:
                    return RuntimePlatform.WSAPlayerX86;

				case BuildTarget.tvOS:
					return RuntimePlatform.tvOS;

				default:
					throw new System.NotSupportedException(message: string.Format("Failed to convert build target: {0} to typeof(RuntimePlatform).", buildTarget));
			}
		}
#endif

		private static string ConvertRuntimePlatformToString(RuntimePlatform platform)
		{
			switch (platform)
			{
				case RuntimePlatform.Android:
					return "Android";

				case RuntimePlatform.IPhonePlayer:
					return "iOS";

				case RuntimePlatform.WebGLPlayer:
					return "WebGL";

				case RuntimePlatform.WindowsPlayer:
                    return "Windows";
                    
                case RuntimePlatform.WSAPlayerARM:
                case RuntimePlatform.WSAPlayerX64:
                case RuntimePlatform.WSAPlayerX86:
					return "WSAPlayer";

				case RuntimePlatform.OSXPlayer:
					return "OSX";

				default:
					return null;
			}
		}

		#endregion
	}
}