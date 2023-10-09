using System;
using System.Collections.Generic;

namespace Sources.Infrastructure.PersistentProgress.Structure
{
    [Serializable]
    public class PlayerProgress
    {
        public CompletedLevel[] CompletedLevels;

        public PlayerProgress(CompletedLevel[] completedLevels) => 
            CompletedLevels = completedLevels;

        public void AddCompletedLevel(CompletedLevel level)
        {
            Array.Resize(ref CompletedLevels, CompletedLevels.Length + 1);
            CompletedLevels[CompletedLevels.Length - 1] = level;
        }

        public int GetPlayerStarsCount()
        {
            int stars = 0;

            foreach (CompletedLevel completedLevel in CompletedLevels) 
                stars += completedLevel.Stars;

            return stars;
        }
    }
}