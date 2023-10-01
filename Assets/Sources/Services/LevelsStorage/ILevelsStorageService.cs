using System.Collections.Generic;
using Sources.StaticData.Levels;

namespace Sources.Services.LevelsStorage
{
    public interface ILevelsStorageService
    {
        void Load();
        IReadOnlyList<LevelData> LevelsData { get; }
    }
}