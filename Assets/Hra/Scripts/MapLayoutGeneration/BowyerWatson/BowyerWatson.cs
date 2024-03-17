using System;
using System.Collections.Generic;
using UnityEngine;

public class BowyerWatson
{
    public List<Triangle> GenerateTriangularMesh(List<Room> placedRooms)
    {
        List<Vector2> pointList = new();
        foreach (Room room in placedRooms)
        {
            pointList.Add(new Vector2(room.transform.position.x, room.transform.position.z));
        }

        return Triangulate(pointList, 100, 100);
    }

    private List<Triangle> Triangulate(List<Vector2> pointList, float maxX, float maxY)
    {
        List<Triangle> triangulation = new List<Triangle>();

        float superSize = Math.Max(maxX - 0, maxY - 0) * 2;
        Vector2 p1 = new Vector2(0 - superSize, 0 - superSize);
        Vector2 p2 = new Vector2(0, superSize);
        Vector2 p3 = new Vector2(superSize, 0 - superSize);
        Triangle superTriangle = new Triangle(p1, p2, p3);
        triangulation.Add(superTriangle);

        foreach (Vector2 point in pointList)
        {
            List<Triangle> badTriangles = new List<Triangle>();
            foreach (Triangle triangle in triangulation)
            {
                if (triangle.IsPointInsideCircumcircle(point))
                {
                    badTriangles.Add(triangle);
                }
            }

            HashSet<Edge> polygon = new HashSet<Edge>();
            foreach (Triangle triangle in badTriangles)
            {
                foreach (Edge edge in triangle.Edges)
                {
                    bool isShared = false;
                    foreach (Triangle otherTriangle in badTriangles)
                    {
                        if (otherTriangle != triangle && otherTriangle.HasEdge(edge))
                        {
                            isShared = true;
                            break;
                        }
                    }
                    if (!isShared)
                    {
                        polygon.Add(edge);
                    }
                }
            }

            foreach (Triangle triangle in badTriangles)
            {
                triangulation.Remove(triangle);
            }

            foreach (Edge edge in polygon)
            {
                Triangle newTriangle = new Triangle(edge.Point1, edge.Point2, point);
                triangulation.Add(newTriangle);
            }
        }

        for (int i = triangulation.Count - 1; i >= 0; i--)
        {
            if (triangulation[i].HasVertexOf(superTriangle))
            {
                triangulation.RemoveAt(i);
            }
        }

        return triangulation;
    }
}
