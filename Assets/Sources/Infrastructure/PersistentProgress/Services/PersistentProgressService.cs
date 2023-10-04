using Sources.Infrastructure.PersistentProgress.Structure;
using UnityEngine;

namespace Sources.Infrastructure.PersistentProgress.Services
{
    public abstract class PersistentProgressService : IPersistentProgressService
    {
        protected readonly IPersistentProgressContainer ProgressContainer;
        protected readonly IProgressListenersContainer ProgressListenersContainer;

        public PersistentProgressService(IPersistentProgressContainer progressContainer,
            IProgressListenersContainer progressListenersContainer)
        {
            ProgressContainer = progressContainer;
            ProgressListenersContainer = progressListenersContainer;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgressUpdater progressUpdater in ProgressListenersContainer.SavedProgressUpdaters)
                progressUpdater.UpdateProgress(ProgressContainer.PlayerProgress);
            Debug.Log("saving");
            Save();
        }

        public abstract PlayerProgress LoadProgress();

        protected abstract void Save();
    }
}