using Sources.Infrastructure.PersistentProgress.Structure;

namespace Sources.Infrastructure.PersistentProgress
{
    public interface IPersistentProgressContainer
    {
        PlayerProgress PlayerProgress { get; set; }
    }
}