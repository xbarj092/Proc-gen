using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MapGenerator
{
    /// <summary>
    /// Represents a connection between two rooms.
    /// </summary>
    internal struct RoomConnection
    {
        internal Room StartRoom;
        internal Room EndRoom;
        internal float Distance;
    }

    /// <summary>
    /// Represents a class implementing Prim's algorithm for generating minimum spanning tree.
    /// </summary>
    internal class PrimsAlg
    {
        private Dictionary<Room, List<RoomConnection>> _roomDistances = new();
        private List<RoomConnection> _roomConnections = new();

        /// <summary>
        /// Creates a mesh using Prim's algorithm.
        /// </summary>
        /// <param name="triangles">The list of triangles.</param>
        /// <param name="placedRooms">The list of placed rooms.</param>
        /// <returns>The list of room connections comprising the mesh.</returns>
        internal List<RoomConnection> CreateTriMesh(List<Triangle> triangles, List<Room> placedRooms)
        {
            GetRoomDistances(triangles, placedRooms);
            CalculateSkeleton();
            MakeSomeLoops();
            return _roomConnections;
        }

        /// <summary>
        /// Calculates room distances based on the provided list of triangles and placed rooms.
        /// </summary>
        /// <param name="triangles">The list of triangles to analyze.</param>
        /// <param name="placedRooms">The list of placed rooms.</param>
        private void GetRoomDistances(List<Triangle> triangles, List<Room> placedRooms)
        {
            foreach (Room room in placedRooms)
            {
                foreach (Triangle triangle in RelevantTriangles(triangles, room))
                {
                    foreach (Edge edge in triangle.Edges)
                    {
                        RoomConnection roomConnection = RoomConnectionBuilder(edge, placedRooms);
                        if (IsSuitableForAdding(roomConnection, room))
                        {
                            _roomDistances[room] = UpdatedRoomConnections(roomConnection, room);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Calculates the skeleton of the mesh.
        /// </summary>
        internal void CalculateSkeleton()
        {
            Dictionary<Room, List<RoomConnection>> remainingRooms = new Dictionary<Room, List<RoomConnection>>(_roomDistances);
            List<Room> visitedRooms = new List<Room>();
            Room currentRoom = _roomDistances.Keys.First();
            visitedRooms.Add(currentRoom);

            int connectedCount = 1;
            while (connectedCount < _roomDistances.Count)
            {
                foreach (Room occupiedRoom in GetOccupiedRooms(remainingRooms, visitedRooms))
                {
                    remainingRooms.Remove(occupiedRoom);
                }

                List<RoomConnection> roomConnections = _roomDistances[currentRoom];
                roomConnections = roomConnections.OrderBy(connection => connection.Distance).ToList();
                RoomConnection currentConnection = GetCurrentRoomConnection(roomConnections, visitedRooms, remainingRooms);

                if (currentConnection.EndRoom == null)
                {
                    remainingRooms.Remove(currentRoom);
                    AddRoomConnections(roomConnections, visitedRooms);

                    roomConnections = roomConnections.OrderBy(connection => connection.Distance).ToList();
                    currentConnection = GetCurrentRoomConnection(roomConnections, visitedRooms, remainingRooms);

                    Room availableRoom = currentConnection.EndRoom;

                    if (availableRoom == null)
                    {
                        break;
                    }

                    currentRoom = availableRoom;
                }
                else
                {
                    currentRoom = currentConnection.EndRoom;
                    connectedCount++;
                }

                visitedRooms.Add(currentConnection.EndRoom);
                _roomConnections.Add(currentConnection);
            }
        }

        /// <summary>
        /// Adds room connections from the remaining rooms to the visited rooms.
        /// </summary>
        /// <param name="roomConnections">The list of room connections to add to.</param>
        /// <param name="visitedRooms">The list of visited rooms.</param>
        private void AddRoomConnections(List<RoomConnection> roomConnections, List<Room> visitedRooms)
        {
            foreach (Room room in _roomDistances.Keys)
            {
                if (visitedRooms.Contains(room))
                {
                    roomConnections.AddRange(_roomDistances[room].Where(connection => !visitedRooms.Contains(connection.EndRoom)));
                }
            }
        }

        /// <summary>
        /// Gets the current room connection based on the provided list of room connections,
        /// visited rooms, and remaining rooms.
        /// </summary>
        /// <param name="roomConnections">The list of room connections to search within.</param>
        /// <param name="visitedRooms">The list of visited rooms.</param>
        /// <param name="remainingRooms">The dictionary of remaining rooms.</param>
        /// <returns>The current room connection.</returns>
        private RoomConnection GetCurrentRoomConnection(List<RoomConnection> roomConnections, 
            List<Room> visitedRooms, Dictionary<Room, List<RoomConnection>> remainingRooms)
        {
            return roomConnections.FirstOrDefault(connection => !visitedRooms.Contains(connection.EndRoom) && 
                visitedRooms.Contains(connection.StartRoom) && remainingRooms.ContainsKey(connection.StartRoom));
        }

        /// <summary>
        /// Gets the list of rooms occupied by connections in the given dictionary.
        /// </summary>
        /// <param name="remainingRooms">The dictionary of remaining rooms.</param>
        /// <param name="visitedRooms">The list of visited rooms.</param>
        /// <returns>The list of occupied rooms.</returns>
        private List<Room> GetOccupiedRooms(Dictionary<Room, List<RoomConnection>> remainingRooms, List<Room> visitedRooms)
        {
            List<Room> occupiedRooms = new List<Room>();
            foreach (Room room in remainingRooms.Keys)
            {
                if (IsToRemove(remainingRooms, visitedRooms, room))
                {
                    occupiedRooms.Add(room);
                }
            }

            return occupiedRooms;
        }

        /// <summary>
        /// Determines whether a room should be removed based on the remaining rooms and visited rooms.
        /// </summary>
        /// <param name="remainingRooms">The dictionary of remaining rooms and their connections.</param>
        /// <param name="visitedRooms">The list of visited rooms.</param>
        /// <param name="room">The room to evaluate.</param>
        /// <returns>True if the room should be removed; otherwise, false.</returns>
        private bool IsToRemove(Dictionary<Room, List<RoomConnection>> remainingRooms, List<Room> visitedRooms, Room room)
        {
            foreach (RoomConnection roomConnection in remainingRooms[room])
            {
                if (!visitedRooms.Contains(roomConnection.EndRoom))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Builds a room connection based on an edge and a list of placed rooms.
        /// </summary>
        /// <param name="edge">The edge to use for constructing the room connection.</param>
        /// <param name="placedRooms">The list of placed rooms.</param>
        /// <returns>The constructed room connection.</returns>
        private RoomConnection RoomConnectionBuilder(Edge edge, List<Room> placedRooms)
        {
            return new RoomConnection()
            {
                StartRoom = placedRooms.FirstOrDefault(room => room.transform.position.x == edge.Point1.x &&
                    room.transform.position.z == edge.Point1.y),
                EndRoom = placedRooms.FirstOrDefault(room => room.transform.position.x == edge.Point2.x &&
                    room.transform.position.z == edge.Point2.y),
                Distance = Vector2.Distance(edge.Point1, edge.Point2)
            };
        }

        /// <summary>
        /// Adds random loops between rooms in the mesh.
        /// </summary>
        private void MakeSomeLoops()
        {
            foreach (Room room in _roomDistances.Keys)
            {
                foreach (RoomConnection roomConnection in _roomDistances[room])
                {
                    if (!_roomConnections.Contains(roomConnection) && !_roomConnections.Any(connection => connection.StartRoom == roomConnection.EndRoom && connection.EndRoom == roomConnection.StartRoom))
                    {
                        if (Random.Range(0, 10) == 1)
                        {
                            _roomConnections.Add(roomConnection);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Updates the list of room connections for a room after adding a new connection.
        /// </summary>
        /// <param name="roomConnection">The new room connection.</param>
        /// <param name="room">The room to update connections for.</param>
        /// <returns>The updated list of room connections.</returns>
        private List<RoomConnection> UpdatedRoomConnections(RoomConnection roomConnection, Room room)
        {
            return _roomDistances.TryGetValue(room, out var list) ? list.Append(roomConnection).ToList() :
                new List<RoomConnection> { roomConnection };
        }

        /// <summary>
        /// Retrieves relevant triangles for a given room.
        /// </summary>
        /// <param name="triangles">The list of triangles to search.</param>
        /// <param name="room">The room to find relevant triangles for.</param>
        /// <returns>The relevant triangles for the given room.</returns>
        private IEnumerable<Triangle> RelevantTriangles(List<Triangle> triangles, Room room)
        {
            return triangles.Where(tri => tri.Vertices.Any(vertice => vertice.x ==
                room.transform.position.x && vertice.y == room.transform.position.z));
        }

        /// <summary>
        /// Determines if a room connection is suitable for adding to the mesh.
        /// </summary>
        /// <param name="roomConnection">The room connection to evaluate.</param>
        /// <param name="room">The room to check against.</param>
        /// <returns>True if the room connection is suitable for adding; otherwise, false.</returns>
        private bool IsSuitableForAdding(RoomConnection roomConnection, Room room)
        {
            return roomConnection.EndRoom != room && (!_roomDistances.ContainsKey(room) ||
                !_roomDistances[room].Any(room => room.EndRoom == roomConnection.EndRoom));
        }
    }
}
