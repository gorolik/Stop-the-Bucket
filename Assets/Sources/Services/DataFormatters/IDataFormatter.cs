namespace Sources.Services.DataFormatters
{
    public interface IDataFormatter
    {
        string Serialize(object obj);
        T Deserialize<T>(string data);
    }
}