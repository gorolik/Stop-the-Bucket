using System.Collections.Generic;
using Sources.Infrastructure.PersistentProgress;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public interface IGameFactory
    {
        IEnumerable<IGameStartListener> GameStartListeners { get; }
        IEnumerable<IGameEndListener> GameEndListeners { get; }
        void CreateBucket(float maxSpeed, float acceleration);
        void CreateSuccessLine(Camera camera, float height);
        void CreatePeople(Sprite sprite);
        void CreateMainMenuHud();
        void Cleanup();
    }
}