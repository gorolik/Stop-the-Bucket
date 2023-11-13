using UnityEngine;

namespace Sources.Infrastructure.GameInstallSettings
{
    public class InstallData : MonoBehaviour
    {
        [SerializeField] private TargetPlatform _targetPlatform;

        public TargetPlatform TargetPlatform => _targetPlatform;
    }
}