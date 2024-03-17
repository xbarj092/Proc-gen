using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle
{
    public Vector2[] Vertices { get; private set; }
    public Edge[] Edges { get; private set; }

    public Triangle(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        Vertices = new Vector2[] { p1, p2, p3 };
        Edges = new Edge[]
        {
            new Edge(p1, p2),
            new Edge(p2, p3),
            new Edge(p3, p1)
        };
    }

    private Vector2 Circumcenter()
    {
        // fuck barycentrický souøadnice
        float A = Vertices[1].x - Vertices[0].x;
        float B = Vertices[1].y - Vertices[0].y;
        float C = Vertices[2].x - Vertices[0].x;
        float D = Vertices[2].y - Vertices[0].y;
        float E = A * (Vertices[0].x + Vertices[1].x) + B * (Vertices[0].y + Vertices[1].y);
        float F = C * (Vertices[0].x + Vertices[2].x) + D * (Vertices[0].y + Vertices[2].y);
        float G = 2 * (A * (Vertices[2].y - Vertices[1].y) - B * (Vertices[2].x - Vertices[1].x));

        float circumcenterX = (D * E - B * F) / G;
        float circumcenterY = (A * F - C * E) / G;

        return new Vector2(circumcenterX, circumcenterY);
    }

    private float SquaredDistance(Vector2 p1, Vector2 p2)
    {
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;
        return dx * dx + dy * dy;
    }

    public bool IsPointInsideCircumcircle(Vector2 point)
    {
        Vector2 circumcenter = Circumcenter();
        float radiusSquared = SquaredDistance(Vertices[0], circumcenter);
        float distanceSquared = SquaredDistance(circumcenter, point);
        return distanceSquared <= radiusSquared;
    }

    public bool HasEdge(Edge edge)
    {
        foreach (Edge e in Edges)
        {
            if (e.Equals(edge))
            {
                return true;
            }
        }
        return false;
    }

    public bool HasVertexOf(Triangle triangle)
    {
        foreach (Vector2 vertex in Vertices)
        {
            foreach (Vector2 otherVertex in triangle.Vertices)
            {
                if (vertex.Equals(otherVertex))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void DrawEdges(Edge edge)
    {
        Debug.Log(edge);
        LineRenderer lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, new Vector3(edge.Point1.x, 0, edge.Point1.y));
        lineRenderer.SetPosition(1, new Vector3(edge.Point2.x, 0, edge.Point2.y));
    }
}
