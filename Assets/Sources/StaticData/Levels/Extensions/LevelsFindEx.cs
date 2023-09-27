namespace Sources.StaticData.Levels.Extensions
{
    public static class LevelsFindEx
    {
        public static LevelData GetLevelDataByClusterAndId(this LevelsData levelsData, ClusterType cluster, int id) =>
            GetLevelDataById(GetLevelsClusterByType(levelsData, cluster), id);
        
        public static LevelsCluster GetLevelsClusterByType(this LevelsData levelsData, ClusterType type)
        {
            foreach (LevelsCluster cluster in levelsData.LevelsClusters)
                if (cluster.ClusterType == type)
                    return cluster;
            
            return null;
        }

        public static LevelData GetLevelDataById(this LevelsCluster levelsCluster, int id) => 
            levelsCluster.LevelsData[id];
    }
}