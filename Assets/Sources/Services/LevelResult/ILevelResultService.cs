namespace Sources.Services.LevelResult
{
    public interface ILevelResultService
    {
        bool IsWin(float bucketHeight);
        int GetStarsCount(float bucketHeight);
    }
}