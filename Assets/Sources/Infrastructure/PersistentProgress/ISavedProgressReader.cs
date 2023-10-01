using Sources.Infrastructure.PersistentProgress.Structure;

namespace Sources.Infrastructure.PersistentProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}