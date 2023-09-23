using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.Installers.GlobalInstallers
{
    public class GameBootstrapperInstaller : MonoInstaller
    {
        [SerializeField] private GameBootstrapper _gameBootstrapperPrefab;
        
        public override void InstallBindings() => 
            BindGameBootstrapper();

        private void BindGameBootstrapper()
        {
            GameBootstrapper gameBootstrapper = Container
                .InstantiatePrefab(_gameBootstrapperPrefab)
                .GetComponent<GameBootstrapper>();
            
            Container.Bind<IInitializable>()
                .FromInstance(gameBootstrapper);
        }
    }
}