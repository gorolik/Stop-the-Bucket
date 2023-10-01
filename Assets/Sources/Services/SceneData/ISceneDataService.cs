using Sources.Behaviour;
using Sources.StaticData.Levels;

namespace Sources.Services.SceneData
{
    public interface ISceneDataService
    {
        LevelSceneData LevelSceneData { get; }
        LevelData LevelData { get; }
        void Load();
        void Init(LevelData levelData);
    }
}