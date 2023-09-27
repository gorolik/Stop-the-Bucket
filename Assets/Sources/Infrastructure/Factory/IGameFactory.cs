using System.Collections.Generic;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public interface IGameFactory
    {
        IEnumerable<IGameStartListener> GameStartListeners { get; }
        void CreateBucket(float maxSpeed, float acceleration);
        void CreateSuccessLine(Camera camera, float height);
        void CreatePeople();
        void CreateMainMenuHud();
        void Cleanup();
    }
}