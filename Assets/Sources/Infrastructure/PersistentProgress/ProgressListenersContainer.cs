using System.Collections.Generic;
using UnityEngine;

namespace Sources.Infrastructure.PersistentProgress
{
    public class ProgressListenersContainer : IProgressListenersContainer
    {
        private readonly List<ISavedProgressReader> _savedProgressReaders = new List<ISavedProgressReader>();
        private readonly List<ISavedProgressUpdater> _savedProgressUpdaters = new List<ISavedProgressUpdater>();
        
        public IEnumerable<ISavedProgressReader> SavedProgressReaders => _savedProgressReaders;
        public IEnumerable<ISavedProgressUpdater> SavedProgressUpdaters => _savedProgressUpdaters;

        public void RegisterGameObject(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in
                     gameObject.GetComponents<ISavedProgressReader>())
            {
                _savedProgressReaders.Add(progressReader);

                if (progressReader is ISavedProgressUpdater)
                    _savedProgressUpdaters.Add(progressReader as ISavedProgressUpdater);
            }
        }
    }
}