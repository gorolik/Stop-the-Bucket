using System.Collections.Generic;
using Sources.Infrastructure.PersistentProgress;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public interface IGameFactory
    {
        IEnumerable<IGameStartListener> GameStartListeners { get; }
        IEnumerable<ISavedProgressUpdater> SavedProgressUpdaters { get; }
        IEnumerable<ISavedProgressReader> SavedProgressReaders { get; }
        void CreateBucket(float maxSpeed, float acceleration);
        void CreateSuccessLine(Camera camera, float height);
        void CreatePeople();
        void CreateMainMenuHud();
        void Cleanup();
    }
}