using Sources.Infrastructure.PersistentProgress.Structure;

namespace Sources.Infrastructure.PersistentProgress
{
    public class PersistentProgressContainer : IPersistentProgressContainer
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}