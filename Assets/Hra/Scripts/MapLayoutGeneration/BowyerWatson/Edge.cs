using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge
{
    public Vector2 Point1 { get; private set; }
    public Vector2 Point2 { get; private set; }

    public Edge(Vector2 point1, Vector2 point2)
    {
        Point1 = point1;
        Point2 = point2;
    }

    public bool HasPoint(Vector2 point)
    {
        return Point1.Equals(point) || Point2.Equals(point);
    }

    public bool Equals(Edge other)
    {
        return (Point1.Equals(other.Point1) && Point2.Equals(other.Point2)) ||
               (Point1.Equals(other.Point2) && Point2.Equals(other.Point1));
    }

    public override int GetHashCode()
    {
        return Point1.GetHashCode() ^ Point2.GetHashCode();
    }
}
