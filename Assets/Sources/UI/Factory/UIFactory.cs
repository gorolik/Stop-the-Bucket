using Sources.Infrastructure.AssetManagement;
using Sources.Services.StaticData;
using Sources.UI.Windows;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Sources.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;

        public Transform UIRoot { get; private set; }

        public UIFactory(DiContainer container, IAssetProvider assetProvider, IStaticDataService staticData)
        {
            _container = container;
            _assetProvider = assetProvider;
            _staticData = staticData;
        }

        public void CreateUIRoot() => 
            UIRoot = Object.Instantiate(_assetProvider.GetGameObjectByPath(AssetsPath.UIRootPath)).transform;

        public void CreateMainMenu() => 
            InstantiateByWindowId(WindowId.MainMenu);

        public void CreateChooseLevelMenu() => 
            InstantiateByWindowId(WindowId.ChooseLevel);
        
        private void InstantiateByWindowId(WindowId id)
        {
            WindowBase window = Object.Instantiate(_staticData.GetWindowById(id).Prefab, UIRoot);
            _container.InjectGameObject(window.gameObject);
        }
    }
}