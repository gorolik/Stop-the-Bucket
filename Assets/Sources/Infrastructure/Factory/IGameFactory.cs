using System.Collections.Generic;
using Sources.Infrastructure.PersistentProgress;
using Sources.StaticData.Peoples;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public interface IGameFactory
    {
        IEnumerable<IGameStartListener> GameStartListeners { get; }
        IEnumerable<IGameEndListener> GameEndListeners { get; }
        void CreateLevelRoot();
        void CreateBucket(float maxSpeed, float acceleration);
        void CreateSuccessLine(Camera camera, float height);
        void CreatePeople(PeopleData data);
        void CreateMainMenuHud();
        void Cleanup();
    }
}