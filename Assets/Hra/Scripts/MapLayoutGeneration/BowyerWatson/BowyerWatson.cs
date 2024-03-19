using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenerator
{
    /// <summary>
    /// Class responsible for generating a triangular mesh using the Bowyer-Watson algorithm.
    /// </summary>
    internal class BowyerWatson
    {
        /// <summary>
        /// Generates a triangular mesh based on the given placed rooms.
        /// </summary>
        /// <param name="placedRooms">The list of placed rooms.</param>
        /// <returns>The list of triangles comprising the triangular mesh.</returns>
        internal List<Triangle> GenerateTriangularMesh(List<Room> placedRooms)
        {
            return Triangulate(GetPointList(placedRooms), 100, 100);
        }

        /// <summary>
        /// Triangulates a set of points within the given bounding box.
        /// </summary>
        /// <param name="pointList">The list of points to triangulate.</param>
        /// <param name="maxX">The maximum x-coordinate of the bounding box.</param>
        /// <param name="maxY">The maximum y-coordinate of the bounding box.</param>
        /// <returns>The list of triangles resulting from the triangulation.</returns>
        private List<Triangle> Triangulate(List<Vector2> pointList, float maxX, float maxY)
        {
            List<Triangle> triangulation = new List<Triangle>();
            Triangle superTriangle = GenerateSuperTriangle(maxX, maxY);
            triangulation.Add(superTriangle);

            foreach (Vector2 point in pointList)
            {
                List<Triangle> badTriangles = GetBadTriangles(triangulation, point);
                HashSet<Edge> polygon = GetPolygon(badTriangles);
                RemoveBadTriangles(badTriangles, triangulation);
                AddNewTriangles(polygon, triangulation, point);
            }

            GetRidOfSuperTriangleConnections(triangulation, superTriangle);
            return triangulation;
        }

        /// <summary>
        /// Retrieves the polygon formed by the given set of bad triangles.
        /// </summary>
        /// <param name="badTriangles">The list of bad triangles.</param>
        /// <returns>The polygon formed by the bad triangles.</returns>
        private HashSet<Edge> GetPolygon(List<Triangle> badTriangles)
        {
            HashSet<Edge> polygon = new();

            foreach (Triangle triangle in badTriangles)
            {
                foreach (Edge edge in triangle.Edges)
                {
                    if (!IsShared(badTriangles, triangle, edge))
                    {
                        polygon.Add(edge);
                    }
                }
            }

            return polygon;
        }

        /// <summary>
        /// Adds new triangles to the triangulation based on the provided polygon and point.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        /// <param name="triangulation">The current triangulation.</param>
        /// <param name="point">The new point.</param>
        private void AddNewTriangles(HashSet<Edge> polygon, List<Triangle> triangulation, Vector2 point)
        {
            foreach (Edge edge in polygon)
            {
                Triangle newTriangle = new(edge.Point1, edge.Point2, point);
                triangulation.Add(newTriangle);
            }
        }

        /// <summary>
        /// Removes triangles from the triangulation that share vertices with the super triangle.
        /// </summary>
        /// <param name="triangulation">The list of triangles in the triangulation.</param>
        /// <param name="superTriangle">The super triangle.</param>
        private void GetRidOfSuperTriangleConnections(List<Triangle> triangulation, Triangle superTriangle)
        {
            for (int i = triangulation.Count - 1; i >= 0; i--)
            {
                if (triangulation[i].HasVertexOf(superTriangle))
                {
                    triangulation.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Removes bad triangles from the triangulation.
        /// </summary>
        /// <param name="badTriangles">The list of bad triangles to remove.</param>
        /// <param name="triangulation">The list of triangles in the triangulation.</param>
        private void RemoveBadTriangles(List<Triangle> badTriangles, List<Triangle> triangulation)
        {
            foreach (Triangle triangle in badTriangles)
            {
                triangulation.Remove(triangle);
            }
        }

        /// <summary>
        /// Generates a super triangle based on the maximum x and y coordinates.
        /// </summary>
        /// <param name="maxX">The maximum x-coordinate.</param>
        /// <param name="maxY">The maximum y-coordinate.</param>
        /// <returns>The generated super triangle.</returns>
        private Triangle GenerateSuperTriangle(float maxX, float maxY)
        {
            float superSize = Math.Max(maxX - 0, maxY - 0) * 2;
            Vector2 p1 = new(0 - superSize, 0 - superSize);
            Vector2 p2 = new(0, superSize);
            Vector2 p3 = new(superSize, 0 - superSize);
            return new Triangle(p1, p2, p3);
        }

        /// <summary>
        /// Retrieves bad triangles from the triangulation based on the given point.
        /// </summary>
        /// <param name="triangulation">The list of triangles in the triangulation.</param>
        /// <param name="point">The point to check.</param>
        /// <returns>The list of bad triangles.</returns>
        private List<Triangle> GetBadTriangles(List<Triangle> triangulation, Vector2 point)
        {
            List<Triangle> badTriangles = new();
            foreach (Triangle triangle in triangulation)
            {
                if (triangle.IsPointInsideCircumcircle(point))
                {
                    badTriangles.Add(triangle);
                }
            }

            return badTriangles;
        }

        /// <summary>
        /// Checks if an edge is shared among the bad triangles.
        /// </summary>
        /// <param name="badTriangles">The list of bad triangles.</param>
        /// <param name="triangle">The current triangle.</param>
        /// <param name="edge">The edge to check.</param>
        /// <returns>True if the edge is shared; otherwise, false.</returns>
        private bool IsShared(List<Triangle> badTriangles, Triangle triangle, Edge edge)
        {
            foreach (Triangle otherTriangle in badTriangles)
            {
                if (otherTriangle != triangle && otherTriangle.HasEdge(edge))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Retrieves a list of points from the given list of placed rooms.
        /// </summary>
        /// <param name="placedRooms">The list of placed rooms.</param>
        /// <returns>The list of points representing the rooms' positions.</returns>
        private List<Vector2> GetPointList(List<Room> placedRooms)
        {
            List<Vector2> pointList = new();
            foreach (Room room in placedRooms)
            {
                pointList.Add(new Vector2(room.transform.position.x, room.transform.position.z));
            }
            return pointList;
        }
    }
}
