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
                LevelSettings settings = levelsSettingsStorage.LevelsSettings[i];

                int previousLevels = 0;
                for (int j = 0; j < levelClusterId; j++) 
                    previousLevels += clustersStorage.LevelClusters[j].LevelsCount;
                int levelNumberInCluster = i - previousLevels + 1;

                if (levelNumberInCluster > clustersStorage.LevelClusters[levelClusterId].LevelsCount)
                    if(clustersStorage.LevelClusters[levelClusterId + 1] != null)
                        levelClusterId++;

                ClusterType cluster = clustersStorage.LevelClusters[levelClusterId].Type;
                
                LevelData data = new LevelData(settings, cluster, i);
                _levelsData.Add(data);
            }
        }

        public IReadOnlyList<LevelData> LevelsData =>
            _levelsData;
    }
}