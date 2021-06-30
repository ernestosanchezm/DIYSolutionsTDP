using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.Serialization
{
    internal class AOTStubs
    {
        #region Constructors

        static AOTStubs()
        {
            AddDataProviders();
        }

        #endregion

        #region Private methods

        private static void AddDataProviders()
        {
            // array types
            SerializationDataProviderServices.AddDataProvider(typeof(GameObject[]),     typeof(Generic1dArrayDataProvider<GameObject>));
            SerializationDataProviderServices.AddDataProvider(typeof(Component[]),      typeof(Generic1dArrayDataProvider<Component>));
            SerializationDataProviderServices.AddDataProvider(typeof(Material[]),       typeof(Generic1dArrayDataProvider<Material>));

            SerializationDataProviderServices.AddDataProvider(typeof(Vector2[]),        typeof(Generic1dArrayDataProvider<Vector2>));
            SerializationDataProviderServices.AddDataProvider(typeof(Vector3[]),        typeof(Generic1dArrayDataProvider<Vector3>));
            SerializationDataProviderServices.AddDataProvider(typeof(Vector4[]),        typeof(Generic1dArrayDataProvider<Vector4>));
            SerializationDataProviderServices.AddDataProvider(typeof(Color[]),          typeof(Generic1dArrayDataProvider<Color>));
            SerializationDataProviderServices.AddDataProvider(typeof(Color32[]),        typeof(Generic1dArrayDataProvider<Color32>));
        }

        #endregion
    }
}