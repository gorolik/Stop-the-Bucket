using Sources.Infrastructure.PersistentProgress.Structure;

namespace Sources.Infrastructure.PersistentProgress
{
    public interface ISavedProgressUpdater : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}