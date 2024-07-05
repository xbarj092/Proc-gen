using UnityEngine;

namespace MapGenerator
{
    /// <summary>
    /// Represents an edge defined by two points in 2D space.
    /// </summary>
    internal class Edge
    {
        /// <summary>
        /// Gets the first endpoint of the edge.
        /// </summary>
        internal Vector2 Point1 { get; private set; }

        /// <summary>
        /// Gets the second endpoint of the edge.
        /// </summary>
        internal Vector2 Point2 { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Edge"/> class with the specified endpoints.
        /// </summary>
        /// <param name="point1">The first endpoint of the edge.</param>
        /// <param name="point2">The second endpoint of the edge.</param>
        internal Edge(Vector2 point1, Vector2 point2)
        {
            Point1 = point1;
            Point2 = point2;
        }

        /// <summary>
        /// Checks whether the edge contains the specified point.
        /// </summary>
        /// <param name="point">The point to check.</param>
        /// <returns>True if the edge contains the point, otherwise false.</returns>
        internal bool HasPoint(Vector2 point)
        {
            return Point1.Equals(point) || Point2.Equals(point);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Edge"/> is equal to the current <see cref="Edge"/>.
        /// </summary>
        /// <param name="other">The <see cref="Edge"/> to compare with the current <see cref="Edge"/>.</param>
        /// <returns>True if the specified <see cref="Edge"/> is equal to the current <see cref="Edge"/>; otherwise, false.</returns>
        internal bool Equals(Edge other)
        {
            return (Point1.Equals(other.Point1) && Point2.Equals(other.Point2)) ||
                   (Point1.Equals(other.Point2) && Point2.Equals(other.Point1));
        }

        /// <summary>
        /// Returns the hash code for this <see cref="Edge"/>.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return Point1.GetHashCode() ^ Point2.GetHashCode();
        }
    }
}
