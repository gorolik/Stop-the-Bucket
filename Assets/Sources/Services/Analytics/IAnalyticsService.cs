namespace Sources.Services.Analytics
{
    public interface IAnalyticsService
    {
        void Init(AnalyticsEnvironment environment);
        void LevelPassedFirst(int level);
        void LevelPassed(int level, int stars);
    }
}