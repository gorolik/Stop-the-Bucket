using UnityEngine;

namespace Sources.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        GameObject GetGameObjectByPath(string path);
    }
}