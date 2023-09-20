using Sources.Game;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Bucket _bucket;
        [SerializeField] private SuccessLine _successLine;
        
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(_mainCamera).AsSingle();
            Container.Bind<SuccessLine>().FromInstance(_successLine).AsSingle();
            Container.Bind<Bucket>().FromInstance(_bucket).AsSingle();
        }
    }
}