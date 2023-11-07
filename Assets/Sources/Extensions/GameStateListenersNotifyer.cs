using Sources.Infrastructure;

namespace Sources.Extensions
{
    public static class GameStateListenersNotifyer
    {
        public static void NotifyGameStartListeners(this IGameStartListener[] gameStartListeners)
        {
            foreach (IGameStartListener gameStartListener in gameStartListeners)
                gameStartListener.OnGameStarted();
        }
        
        public static void NotifyGameEndListeners(this IGameEndListener[] gameEndListeners)
        {
            foreach (IGameEndListener gameEndListener in gameEndListeners)
                gameEndListener.OnGameEnded();
        }
    }
}