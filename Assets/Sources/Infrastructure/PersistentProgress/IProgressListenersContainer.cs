using System.Collections.Generic;
using UnityEngine;

namespace Sources.Infrastructure.PersistentProgress
{
    public interface IProgressListenersContainer
    {
        IEnumerable<ISavedProgressReader> SavedProgressReaders { get; }
        IEnumerable<ISavedProgressUpdater> SavedProgressUpdaters { get; }
        void RegisterGameObject(GameObject gameObject);
    }
}