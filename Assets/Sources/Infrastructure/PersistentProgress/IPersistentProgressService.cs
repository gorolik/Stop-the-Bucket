using Sources.Infrastructure.PersistentProgress.Structure;

namespace Sources.Infrastructure.PersistentProgress
{
    public interface IPersistentProgressService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}