using UnityEngine;

namespace Sources.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public Object GetObjectByPath(string path) =>
            Resources.Load(path);
    }
}