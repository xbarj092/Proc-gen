using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MapGenerator
{
    internal struct RoomConnection
    {
        internal Room StartRoom;
        internal Room EndRoom;
        internal float Distance;
    }

    internal class PrimsAlg
    {
        private Dictionary<Room, List<RoomConnection>> _roomDistances = new();
        private List<RoomConnection> _roomConnections = new();

        internal List<RoomConnection> CreateTriMesh(List<Triangle> triangles, List<Room> placedRooms)
        {
            foreach (Room room in placedRooms)
            {
                foreach (Triangle triangle in triangles.Where(tri => tri.Vertices.Any(vertice =>
                    vertice.x == room.transform.position.x && vertice.y == room.transform.position.z)))
                {
                    foreach (Edge edge in triangle.Edges)
                    {
                        RoomConnection roomConnection = RoomConnectionBuilder(edge, placedRooms);
                        if (roomConnection.EndRoom != room &&
                            (!_roomDistances.ContainsKey(room) ||
                             !_roomDistances[room].Any(room => room.EndRoom == roomConnection.EndRoom)))
                        {
                            _roomDistances[room] = _roomDistances.TryGetValue(room, out var list) ?
                                list.Append(roomConnection).ToList() : new List<RoomConnection> { roomConnection };
                        }
                    }
                }
            }

            CalculateSkeleton();
            MakeSomeLoops();
            return _roomConnections;
        }

        internal void CalculateSkeleton()
        {
            Dictionary<Room, List<RoomConnection>> remainingRooms = new Dictionary<Room, List<RoomConnection>>(_roomDistances);
            List<Room> visitedRooms = new List<Room>();
            Room currentRoom = _roomDistances.Keys.First();
            visitedRooms.Add(currentRoom);

            int connectedCount = 1;
            while (connectedCount < _roomDistances.Count)
            {
                List<Room> occupiedRooms = new List<Room>();
                foreach (Room room in remainingRooms.Keys)
                {
                    bool toRemove = true;
                    foreach (RoomConnection roomConnection in remainingRooms[room])
                    {
                        if (!visitedRooms.Contains(roomConnection.EndRoom))
                        {
                            toRemove = false;
                        }
                    }

                    if (toRemove)
                    {
                        occupiedRooms.Add(room);
                    }
                }

                foreach (Room occupiedRoom in occupiedRooms)
                {
                    remainingRooms.Remove(occupiedRoom);
                }

                List<RoomConnection> roomConnections = _roomDistances[currentRoom];
                roomConnections = roomConnections.OrderBy(connection => connection.Distance).ToList();
                RoomConnection currentConnection = roomConnections.FirstOrDefault(connection => !visitedRooms.Contains(connection.EndRoom) && visitedRooms.Contains(connection.StartRoom) && remainingRooms.ContainsKey(connection.StartRoom));

                if (currentConnection.EndRoom == null)
                {
                    remainingRooms.Remove(currentRoom);
                    foreach (Room room in _roomDistances.Keys)
                    {
                        if (visitedRooms.Contains(room))
                        {
                            roomConnections.AddRange(_roomDistances[room].Where(connection => !visitedRooms.Contains(connection.EndRoom)));
                        }
                    }

                    roomConnections = roomConnections.OrderBy(connection => connection.Distance).ToList();
                    currentConnection = roomConnections.FirstOrDefault(connection => !visitedRooms.Contains(connection.EndRoom) && visitedRooms.Contains(connection.StartRoom) && remainingRooms.ContainsKey(connection.StartRoom));

                    Room availableRoom = currentConnection.EndRoom;

                    if (availableRoom != null)
                    {
                        currentRoom = availableRoom;
                        visitedRooms.Add(currentConnection.EndRoom);
                        _roomConnections.Add(currentConnection);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    visitedRooms.Add(currentConnection.EndRoom);
                    _roomConnections.Add(currentConnection);
                    currentRoom = currentConnection.EndRoom;
                    connectedCount++;
                }
            }
        }

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

        private void MakeSomeLoops()
        {
            foreach (Room room in _roomDistances.Keys)
            {
                foreach (RoomConnection roomConnection in _roomDistances[room])
                {
                    if (!_roomConnections.Contains(roomConnection) && !_roomConnections.Any(connection => connection.StartRoom == roomConnection.EndRoom && connection.EndRoom == roomConnection.StartRoom))
                    {
                        if (UnityEngine.Random.Range(0, 10) == 1)
                        {
                            _roomConnections.Add(roomConnection);
                        }
                    }
                }
            }
        }
    }
}
