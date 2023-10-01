using System;
using System.Collections.Generic;

namespace Sources.Infrastructure.PersistentProgress.Structure
{
    [Serializable]
    public class PlayerProgress
    {
        public List<CompletedLevel> CompletedLevels = new List<CompletedLevel>();
    }
}