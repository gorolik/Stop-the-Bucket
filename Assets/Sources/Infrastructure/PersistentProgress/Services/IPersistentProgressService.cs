using Sources.Infrastructure.PersistentProgress.Structure;

namespace Sources.Infrastructure.PersistentProgress.Services
{
    public interface IPersistentProgressService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}