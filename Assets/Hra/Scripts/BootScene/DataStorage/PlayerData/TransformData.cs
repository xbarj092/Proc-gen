using System;
using System.Numerics;

[Serializable]
public class TransformData
{
    public float[] Position;
    public float[] Rotation;

    public TransformData(Vector3 position, Vector3 rotation)
    {
        Position = new float[3];
        Position[0] = position.X;
        Position[1] = position.Y;
        Position[2] = position.Z;

        Rotation = new float[3];
        Position[0] = rotation.X;
        Position[1] = rotation.Y;
        Position[2] = rotation.Z;
    }
}
