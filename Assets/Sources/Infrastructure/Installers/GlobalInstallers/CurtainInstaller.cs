using Sources.Behaviour.UI;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.Installers.GlobalInstallers
{
    public class CurtainInstaller : MonoInstaller
    {
        [SerializeField] private Curtain _curtainPrefab;

        public override void InstallBindings() => 
            CreateAndBindCurtain();

        private void CreateAndBindCurtain()
        {
            Curtain curtain = Container.InstantiatePrefab(_curtainPrefab).GetComponent<Curtain>();
            
            Container.Bind<Curtain>()
                .FromInstance(curtain)
                .AsSingle();
        }
    }
}
