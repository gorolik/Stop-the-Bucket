namespace Sources.StaticData.Levels
{
    public class LevelData
    {
        public LevelSettings Settings { get; }
        public ClusterType Cluster { get; }
        public int Id { get; }

        public LevelData(LevelSettings settings, ClusterType cluster, int id)
        {
            Settings = settings;
            Cluster = cluster;
            Id = id;
        }
    }
}