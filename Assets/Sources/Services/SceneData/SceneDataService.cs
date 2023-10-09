using System.Linq;
using Sources.StaticData.Levels;
using Sources.Behaviour;
using Sources.Services.StaticData;
using UnityEngine;

namespace Sources.Services.SceneData
{
    public class SceneDataService : ISceneDataService
    {
        private const string _levelSceneDataObjectTag = "SceneData";
        
        private readonly IStaticDataService _staticData;

        public LevelSceneData LevelSceneData { get; private set; }
        public LevelData LevelData { get; private set; }
        public ClusterViewData ClusterViewData { get; private set; }

        public SceneDataService(IStaticDataService staticData) => 
            _staticData = staticData;

        public void Load() => 
            LevelSceneData = GameObject.FindGameObjectWithTag(_levelSceneDataObjectTag).GetComponent<LevelSceneData>();

        public void Init(LevelData levelData)
        {
            LevelData = levelData;
            ClusterViewData = _staticData.GetClustersStorage().LevelClusters
                .FirstOrDefault(x => x.Type == levelData.Cluster)
                .ViewData;
        }
    }
}