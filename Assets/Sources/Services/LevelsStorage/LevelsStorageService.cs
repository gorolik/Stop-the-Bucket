using System.Collections.Generic;
using Sources.Services.StaticData;
using Sources.StaticData.Levels;
using UnityEngine;

namespace Sources.Services.LevelsStorage
{
    public class LevelsStorageService : ILevelsStorageService
    {
        private readonly IStaticDataService _staticData;
        
        private readonly List<LevelData> _levelsData = new List<LevelData>();

        public IReadOnlyList<LevelData> LevelsData => _levelsData;

        public LevelsStorageService(IStaticDataService staticData) => 
            _staticData = staticData;

        public void Load()
        {
            _levelsData.Clear();
            
            LevelClustersStorage clustersStorage = _staticData.GetClustersStorage();
            
            LevelSettings[] levelSettings = GetLevels();
            
            int levelClusterId = 0;

            for (int i = 0; i < levelSettings.Length; i++)
            {
                int previousLevels =  GetPreviousLevelsCount(levelClusterId, clustersStorage);
                int levelNumberInCluster = i - previousLevels + 1;
                ClusterType cluster = GetCluster(levelNumberInCluster, clustersStorage, ref levelClusterId);
                
                LevelSettings settings = levelSettings[i];

                LevelData data = new LevelData(settings, cluster, i);
                
                _levelsData.Add(data);
            }
        }

        private LevelSettings[] GetLevels()
        {
            List<LevelSettings> levelsSettings = new List<LevelSettings>();

            LevelClustersStorage clustersStorage = _staticData.GetClustersStorage();
            
            foreach (LevelClusterData cluster in clustersStorage.LevelClusters)
            {
                for (int i = 0; i < cluster.LevelsCount; i++)
                {
                    float percent = i / (float)cluster.LevelsCount;
                    float currentMaxSpeed = Mathf.Lerp(cluster.StartMaxSpeed, cluster.EndMaxSpeed, percent);
                    float currentAcceleration = Mathf.Lerp(cluster.StartAcceleration, cluster.EndAcceleration, percent);
                    
                    levelsSettings.Add(new LevelSettings(cluster.LineRange, currentMaxSpeed, currentAcceleration));
                }
            }

            return levelsSettings.ToArray();
        }

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