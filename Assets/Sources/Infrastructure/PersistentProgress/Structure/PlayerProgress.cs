using System;
using System.Collections.Generic;

namespace Sources.Infrastructure.PersistentProgress.Structure
{
    [Serializable]
    public class PlayerProgress
    {
        public CompletedLevel[] CompletedLevels;

        public PlayerProgress()
        {
            CompletedLevels = Array.Empty<CompletedLevel>();
        }

        public void AddCompletedLevel(CompletedLevel level)
        {
            Array.Resize(ref CompletedLevels, CompletedLevels.Length + 1);
            CompletedLevels[CompletedLevels.Length - 1] = level;
        }
    }
}