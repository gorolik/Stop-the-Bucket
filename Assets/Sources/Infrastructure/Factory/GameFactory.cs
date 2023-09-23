using System.Collections.Generic;
using Sources.Behaviour;
using Sources.Infrastructure.AssetManagement;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetProvider _assets;
        
        private readonly List<IGameStartListener> _gameStartListeners = new List<IGameStartListener>();

        public IEnumerable<IGameStartListener> GameStartListeners => _gameStartListeners;

        public GameFactory(DiContainer container, IAssetProvider assets)
        {
            _container = container;
            _assets = assets;
        }

        public void CreateBucket()
        {
            InstantiateObject(AssetsPath.BucketPath, Vector2.zero);
        }

        public void CreateSuccessLine(Camera camera)
        {
            GameObject successLineObject = InstantiateObject(AssetsPath.SuccessLinePath, Vector2.zero);

            SuccessLine successLine = successLineObject.GetComponent<SuccessLine>();
            successLine.Construct(camera);
            
            _container.Bind<SuccessLine>().FromInstance(successLine);
        }

        public void Cleanup()
        {
            _gameStartListeners.Clear();
        }
        
        private GameObject InstantiateObject(string path, Vector2 position)
        {
            GameObject createdObject = _container.InstantiatePrefab(_assets.GetObjectByPath(path), position, quaternion.identity, null);

            TryRegisterObject(createdObject);
            
            return createdObject;
        }

        private void TryRegisterObject(GameObject createdObject)
        {
            foreach (IGameStartListener gameStartListener in createdObject.GetComponentsInChildren<IGameStartListener>())
                _gameStartListeners.Add(gameStartListener);
        }
    }
}