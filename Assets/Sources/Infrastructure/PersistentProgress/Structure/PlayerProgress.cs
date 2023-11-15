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
        public float MusicVolume;
        public float SoundsVolume;

        public PlayerProgress(CompletedLevel[] completedLevels, ClusterType selectedCluster, bool tutorialComplete, float musicVolume, float soundsVolume)
        {
            CompletedLevels = completedLevels;
            SelectedCluster = selectedCluster;
            TutorialComplete = tutorialComplete;
            MusicVolume = musicVolume;
            SoundsVolume = soundsVolume;
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