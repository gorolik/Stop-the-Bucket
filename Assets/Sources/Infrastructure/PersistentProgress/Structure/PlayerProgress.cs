using System;
using System.Collections.Generic;
using Sources.StaticData.Levels;

namespace Sources.Infrastructure.PersistentProgress.Structure
{
    [Serializable]
    public class PlayerProgress
    {
        public CompletedLevel[] CompletedLevels;
        public ClusterType SelectedCluster;
        public bool TutorialComplete;

        public PlayerProgress(CompletedLevel[] completedLevels, ClusterType selectedCluster, bool tutorialComplete)
        {
            CompletedLevels = completedLevels;
            SelectedCluster = selectedCluster;
            TutorialComplete = tutorialComplete;
        }

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