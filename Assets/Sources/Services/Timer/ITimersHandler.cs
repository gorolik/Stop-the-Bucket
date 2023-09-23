namespace Sources.Services.Timer
{
    public interface ITimersHandler
    {
        TimersHandler.Timer StartNewTimer(float seconds);
        void StopTimer(TimersHandler.Timer timer);
        void ResumeTimer(TimersHandler.Timer timer);
    }
}