using Sources.Infrastructure.PersistentProgress.Services.DataSavers;
using Sources.Infrastructure.PersistentProgress.Structure;
using Sources.Services.DataFormatters;

namespace Sources.Infrastructure.PersistentProgress.Services
{
    public class PersistentProgressService : IPersistentProgressService
    {
        private readonly IPersistentProgressContainer _progressContainer;
        private readonly IProgressListenersContainer _progressListenersContainer;
        private readonly IDataFormatter _dataFormatter;
        private readonly IDataSaver _dataSaver;

        public PersistentProgressService(IPersistentProgressContainer progressContainer,
            IProgressListenersContainer progressListenersContainer, IDataFormatter dataFormatter, IDataSaver dataSaver)
        {
            _progressContainer = progressContainer;
            _progressListenersContainer = progressListenersContainer;
            _dataFormatter = dataFormatter;
            _dataSaver = dataSaver;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgressUpdater progressUpdater in _progressListenersContainer.SavedProgressUpdaters)
                progressUpdater.UpdateProgress(_progressContainer.PlayerProgress);

            string data = _dataFormatter.Serialize(_progressContainer.PlayerProgress);
            _dataSaver.Save(data);
        }

        public PlayerProgress LoadProgress() => 
            _dataFormatter.Deserialize<PlayerProgress>(_dataSaver.GetData());
    }
}