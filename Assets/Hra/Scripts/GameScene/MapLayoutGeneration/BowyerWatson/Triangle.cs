using UnityEngine;

namespace MapGenerator
{
    internal class Triangle
    {
        /// <summary>
        /// Gets the vertices of the triangle.
        /// </summary>
        internal Vector2[] Vertices { get; private set; }

        /// <summary>
        /// Gets the edges of the triangle.
        /// </summary>
        internal Edge[] Edges { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> class with the specified vertices.
        /// </summary>
        /// <param name="p1">The first vertex of the triangle.</param>
        /// <param name="p2">The second vertex of the triangle.</param>
        /// <param name="p3">The third vertex of the triangle.</param>
        internal Triangle(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            Vertices = new Vector2[] { p1, p2, p3 };
            Edges = new Edge[]
            {
            new(p1, p2),
            new(p2, p3),
            new(p3, p1)
            };
        }

        /// <summary>
        /// Calculates the circumcenter of the triangle.
        /// </summary>
        /// <returns>The circumcenter of the triangle.</returns>
        private Vector2 Circumcenter()
        {
            // fuck barycentric coords
            float deltaX01 = Vertices[1].x - Vertices[0].x;
            float deltaY01 = Vertices[1].y - Vertices[0].y;
            float deltaX02 = Vertices[2].x - Vertices[0].x;
            float deltaY02 = Vertices[2].y - Vertices[0].y;
            float constant1 = deltaX01 * (Vertices[0].x + Vertices[1].x) + deltaY01 * (Vertices[0].y + Vertices[1].y);
            float constant2 = deltaX02 * (Vertices[0].x + Vertices[2].x) + deltaY02 * (Vertices[0].y + Vertices[2].y);
            float denominator = 2 * (deltaX01 * (Vertices[2].y - Vertices[1].y) - deltaY01 * (Vertices[2].x - Vertices[1].x));

            float circumcenterX = (deltaY02 * constant1 - deltaY01 * constant2) / denominator;
            float circumcenterY = (deltaX01 * constant2 - deltaX02 * constant1) / denominator;

            return new Vector2(circumcenterX, circumcenterY);
        }

        /// <summary>
        /// Calculates the squared distance between two points.
        /// </summary>
        /// <param name="p1">The first point.</param>
        /// <param name="p2">The second point.</param>
        /// <returns>The squared distance between the two points.</returns>
        private float SquaredDistance(Vector2 p1, Vector2 p2)
        {
            float dx = p2.x - p1.x;
            float dy = p2.y - p1.y;
            return dx * dx + dy * dy;
        }

        /// <summary>
        /// Determines whether the specified point lies inside the circumcircle of the triangle.
        /// </summary>
        /// <param name="point">The point to test.</param>
        /// <returns>True if the point lies inside the circumcircle, otherwise false.</returns>
        internal bool IsPointInsideCircumcircle(Vector2 point)
        {
            Vector2 circumcenter = Circumcenter();
            float radiusSquared = SquaredDistance(Vertices[0], circumcenter);
            float distanceSquared = SquaredDistance(circumcenter, point);
            return distanceSquared <= radiusSquared;
        }

        /// <summary>
        /// Determines whether the triangle has the specified edge.
        /// </summary>
        /// <param name="edge">The edge to check.</param>
        /// <returns>True if the triangle has the edge, otherwise false.</returns>
        internal bool HasEdge(Edge edge)
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

        /// <summary>
        /// Determines whether the triangle shares a vertex with another triangle.
        /// </summary>
        /// <param name="triangle">The other triangle to compare vertices with.</param>
        /// <returns>True if the triangle shares a vertex with the other triangle, otherwise false.</returns>
        internal bool HasVertexOf(Triangle triangle)
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
    }
}
