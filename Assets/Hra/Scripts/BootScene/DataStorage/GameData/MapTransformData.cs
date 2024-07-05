using System;
using System.Collections.Generic;

[Serializable]
public class MapTransformData
{
    public List<TransformData> WallTransformData;
    public List<TransformData> FloorTransformData;

    public MapTransformData(List<TransformData> wallTransformData, List<TransformData> floorTransformData)
    {
        WallTransformData = wallTransformData;
        FloorTransformData = floorTransformData;
    }
}
