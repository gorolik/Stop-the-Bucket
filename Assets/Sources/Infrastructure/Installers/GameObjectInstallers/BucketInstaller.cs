using Sources.Behaviour;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.Installers.GameObjectInstallers
{
    public class BucketInstaller : MonoInstaller
    {
        [SerializeField] private Bucket _bucket;
        [SerializeField] private BucketCatcher _bucketCatcher;
        
        private SuccessLine _successLine;
        
        [Inject]
        public void Construct(SuccessLine successLine)
        {
            _successLine = successLine;
        }
        
        public override void InstallBindings()
        {
            Container.Bind<SuccessLine>().FromInstance(_successLine).AsSingle();
            Container.Bind<Bucket>().FromInstance(_bucket).AsSingle();
            Container.Bind<BucketCatcher>().FromInstance(_bucketCatcher).AsSingle();
        }
    }
}
