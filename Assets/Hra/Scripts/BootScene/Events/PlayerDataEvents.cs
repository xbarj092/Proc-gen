using System;

public static class PlayerDataEvents
{
    public static event Action<LevellingData> OnLevellingDataChanged;
    public static void OnLevellingDataChangedInvoke(LevellingData levellingData)
    {
        OnLevellingDataChanged?.Invoke(levellingData);
    }

    public static event Action<TransformData> OnTransformDataChanged;
    public static void OnTransformDataChangedInvoke(TransformData transformData)
    {
        OnTransformDataChanged?.Invoke(transformData);
    }
}
