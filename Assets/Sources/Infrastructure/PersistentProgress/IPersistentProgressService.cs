using Sources.Infrastructure.PersistentProgress.Structure;

namespace Sources.Infrastructure.PersistentProgress
{
    public interface IPersistentProgressService
    {
        PlayerProgress PlayerProgress { get; set; }
    }
}