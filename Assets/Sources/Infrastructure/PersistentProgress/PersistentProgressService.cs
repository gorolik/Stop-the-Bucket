using Sources.Infrastructure.PersistentProgress.Structure;

namespace Sources.Infrastructure.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}