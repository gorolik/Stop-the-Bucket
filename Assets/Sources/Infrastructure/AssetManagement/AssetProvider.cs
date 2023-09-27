using UnityEngine;

namespace Sources.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject GetGameObjectByPath(string path) =>
            Resources.Load<GameObject>(path);
    }
}