namespace Sources.Infrastructure.PersistentProgress.Services.DataSavers
{
    public interface IDataSaver
    {
        string GetData();
        void Save(string data);
    }
}