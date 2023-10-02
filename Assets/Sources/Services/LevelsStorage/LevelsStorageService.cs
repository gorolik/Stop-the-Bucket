using System.Collections.Generic;
using Sources.Services.StaticData;
using Sources.StaticData.Levels;

namespace Sources.Services.LevelsStorage
{
    public class LevelsStorageService : ILevelsStorageService
    {
        private readonly IStaticDataService _staticData;
        
        private readonly List<LevelData> _levelsData = new List<LevelData>();

        public LevelsStorageService(IStaticDataService staticData) => 
            _staticData = staticData;

        public void Load()
        {
            _levelsData.Clear();

            LevelsSettingsStorage levelsSettingsStorage = _staticData.GetLevelsSettingsStorage();
            LevelClustersStorage clustersStorage = _staticData.GetClustersStorage();

            int levelClusterId = 0;

            for (int i = 0; i < levelsSettingsStorage.LevelsSettings.Count; i++)
            {
                int previousLevels =  GetPreviousLevelsCount(levelClusterId, clustersStorage);
                int levelNumberInCluster = i - previousLevels + 1;
                ClusterType cluster = GetCluster(levelNumberInCluster, clustersStorage, ref levelClusterId);
                
                LevelSettings settings = levelsSettingsStorage.LevelsSettings[i];

                LevelData data = new LevelData(settings, cluster, i);
                
                _levelsData.Add(data);
            }
        }

        public IReadOnlyList<LevelData> LevelsData =>
            _levelsData;

        private ClusterType GetCluster(int levelNumberInCluster, LevelClustersStorage clustersStorage, ref int levelClusterId)
        {
            levelClusterId = GetLevelClusterId(levelNumberInCluster, clustersStorage, levelClusterId);
            ClusterType cluster = clustersStorage.LevelClusters[levelClusterId].Type;
            
            return cluster;
        }

        private int GetLevelClusterId(int levelNumberInCluster, LevelClustersStorage clustersStorage, int levelClusterId)
        {
            if (levelNumberInCluster > clustersStorage.LevelClusters[levelClusterId].LevelsCount)
                if (clustersStorage.LevelClusters[levelClusterId + 1] != null)
                    levelClusterId++;
            
            return levelClusterId;
        }

        private int GetPreviousLevelsCount(int levelClusterId, LevelClustersStorage clustersStorage)
        {
            int previousLevels = 0;
            
            for (int j = 0; j < levelClusterId; j++)
                previousLevels += clustersStorage.LevelClusters[j].LevelsCount;
            
            return previousLevels;
        }
    }
}