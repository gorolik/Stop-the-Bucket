using System.Collections.Generic;
using Sources.Behaviour;
using Sources.Behaviour.Bucket;
using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.PersistentProgress;
using Sources.Services.StaticData;
using Sources.StaticData.Peoples;
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
        private readonly List<IGameEndListener> _gameEndListeners = new List<IGameEndListener>();

        public IEnumerable<IGameStartListener> GameStartListeners => _gameStartListeners;
        public IEnumerable<IGameEndListener> GameEndListeners => _gameEndListeners;

        public GameFactory(DiContainer container, IAssetProvider assets, IStaticDataService staticData, 
            IProgressListenersContainer progressListenersContainer)
        {
            _container = container;
            _assets = assets;
            _staticData = staticData;
            _progressListenersContainer = progressListenersContainer;
        }

        public void CreateLevelRoot()
        {
            GameObject levelRootObject = InstantiateObject(AssetsPath.LevelRootPath, Vector2.zero);

            Level levelRoot = levelRootObject.GetComponent<Level>();
            levelRoot.Init();
        }

        public void CreateBucket(float maxSpeed, float acceleration)
        {
            float bucketHeight = _staticData.GetGameSettings().BucketHeight;

            GameObject bucketObject = InstantiateObject(AssetsPath.BucketPath, Vector2.up * bucketHeight);

            BucketFalling bucketFalling = bucketObject.GetComponent<BucketFalling>();
            bucketFalling.Init(maxSpeed, acceleration);

            BucketCatcher bucketCatcher = bucketObject.GetComponent<BucketCatcher>();
            
            _container.Bind<BucketFalling>()
                .FromInstance(bucketFalling);
            
            _container.Bind<BucketCatcher>()
                .FromInstance(bucketCatcher);
        }

        public void CreateSuccessLine(Camera camera, float height)
        {
            float successLineHeight = _staticData.GetGameSettings().PeopleHeight + height;

            GameObject successLineObject = InstantiateObject(AssetsPath.SuccessLinePath, Vector2.up * successLineHeight);

            FullWeightLine fullWeightLine = successLineObject.GetComponent<FullWeightLine>();
            fullWeightLine.Construct(camera);
        }

        public void CreatePeople(PeopleData data, Camera camera)
        {
            float peopleHeight = _staticData.GetGameSettings().PeopleHeight;

            GameObject peopleObject = InstantiateObject(AssetsPath.PeoplePath, Vector2.up * peopleHeight);

            People people = peopleObject.GetComponent<People>();
            people.Init(data, camera);
            
            _container.Bind<People>()
                .FromInstance(people);
        }

        public void CreateMainMenuHud() =>
            InstantiateObject(AssetsPath.MainMenuPath, Vector2.zero);

        public void Cleanup()
        {
            _container.Unbind<People>();
            _container.Unbind<FullWeightLine>();
            _container.Unbind<BucketFalling>();
            _container.Unbind<BucketCatcher>();

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
            
            foreach (IGameEndListener gameEndListener in createdObject.GetComponents<IGameEndListener>())
                _gameEndListeners.Add(gameEndListener);

            _progressListenersContainer.RegisterGameObject(createdObject);
        }
    }
}