using Sources.Behaviour;

namespace Sources.Services.SceneData
{
    public interface ISceneDataService
    {
        LevelSceneData LevelSceneData { get; }
        void Load();
    }
}