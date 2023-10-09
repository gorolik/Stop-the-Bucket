using System.Collections.Generic;
using Sources.Behaviour;
using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.PersistentProgress;
using Sources.Services.StaticData;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IProgressListenersContainer _progressListenersContainer;

        private readonly List<GameObject> _createdObjects = new List<GameObject>();
        private readonly List<IGameStartListener> _gameStartListeners = new List<IGameStartListener>();

        public IEnumerable<IGameStartListener> GameStartListeners => _gameStartListeners;

        public GameFactory(DiContainer container, IAssetProvider assets, IStaticDataService staticData, 
            IProgressListenersContainer progressListenersContainer)
        {
            _container = container;
            _assets = assets;
            _staticData = staticData;
            _progressListenersContainer = progressListenersContainer;
        }

        public void CreateBucket(float maxSpeed, float acceleration)
        {
            float bucketHeight = _staticData.GetGameSettings().BucketHeight;

            GameObject bucketObject = InstantiateObject(AssetsPath.BucketPath, Vector2.up * bucketHeight);

            Bucket bucket = bucketObject.GetComponent<Bucket>();
            bucket.Init(maxSpeed, acceleration);
        }

        public void CreateSuccessLine(Camera camera, float height)
        {
            float successLineHeight = _staticData.GetGameSettings().PeopleHeight + height;

            GameObject successLineObject = InstantiateObject(AssetsPath.SuccessLinePath, Vector2.up * successLineHeight);

            SuccessLine successLine = successLineObject.GetComponent<SuccessLine>();
            successLine.Construct(camera);

            _container.Bind<SuccessLine>()
                .FromInstance(successLine);
        }

        public void CreatePeople(Sprite sprite)
        {
            float peopleHeight = _staticData.GetGameSettings().PeopleHeight;

            GameObject peopleObject = InstantiateObject(AssetsPath.PeoplePath, Vector2.up * peopleHeight);

            People people = peopleObject.GetComponent<People>();
            people.Init(sprite);
        }

        public void CreateMainMenuHud() =>
            InstantiateObject(AssetsPath.MainMenuPath, Vector2.zero);

        public void Cleanup()
        {
            _container.Unbind<SuccessLine>();
            
            foreach (GameObject gameObject in _createdObjects)
                if (gameObject)
                    Object.Destroy(gameObject);

            _createdObjects.Clear();
            _gameStartListeners.Clear();
        }

        private GameObject InstantiateObject(string path, Vector2 position)
        {
            GameObject createdObject = Object.Instantiate(_assets.GetGameObjectByPath(path), position, quaternion.identity);
            _container.InjectGameObject(createdObject);

            TryRegisterObject(createdObject);

            return createdObject;
        }

        private void TryRegisterObject(GameObject createdObject)
        {
            _createdObjects.Add(createdObject);

            foreach (IGameStartListener gameStartListener in createdObject.GetComponents<IGameStartListener>())
                _gameStartListeners.Add(gameStartListener);

            _progressListenersContainer.RegisterGameObject(createdObject);
        }
    }
}