using Sources.Infrastructure.PersistentProgress.Structure;

namespace Sources.Infrastructure.PersistentProgress
{
    public interface ISaveLoadService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}