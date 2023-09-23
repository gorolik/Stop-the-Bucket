using System.Collections.Generic;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public interface IGameFactory
    {
        IEnumerable<IGameStartListener> GameStartListeners { get; }
        void CreateBucket();
        void Cleanup();
        void CreateSuccessLine(Camera camera);
    }
}