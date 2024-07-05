using System;

public static class GameDataEvents
{
    public static event Action<MapTransformData> OnMapTransformDataChanged;
    public static void OnMapTransformDataChangedInvoke(MapTransformData mapTransformData)
    {
        OnMapTransformDataChanged?.Invoke(mapTransformData);
    }
}
