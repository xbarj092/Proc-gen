using System;

public static class DataEvents
{
    public static event Action<LevellingData> OnLevellingDataChanged;
    public static void OnLevellingDataChangedInvoke(LevellingData levellingData)
    {
        OnLevellingDataChanged?.Invoke(levellingData);
    }
}
