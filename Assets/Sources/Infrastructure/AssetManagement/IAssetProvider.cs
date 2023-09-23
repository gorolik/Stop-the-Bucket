using UnityEngine;

namespace Sources.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        Object GetObjectByPath(string path);
    }
}