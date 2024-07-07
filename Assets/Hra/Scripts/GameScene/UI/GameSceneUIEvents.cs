using System;

public static class GameSceneUIEvents
{
    public static event Action<GameScreenType> OnGameScreenOpened;
    public static void OnGameScreenOpenedInvoke(GameScreenType gameScreenType)
    {
        OnGameScreenOpened?.Invoke(gameScreenType);
    }

    public static Action<GameScreenType> OnGameScreenClosed;
    public static void OnGameScreenClosedInvoke(GameScreenType gameScreenType)
    {
        OnGameScreenClosed?.Invoke(gameScreenType);
    }
}
