using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenerator
{
    /// <summary>
    /// Generates rooms for the dungeon.
    /// </summary>
    internal class RoomGenerator
    {
        private List<Room> _placedRooms = new List<Room>();
        /// <summary>
        /// Gets the list of rooms placed in the dungeon.
        /// </summary>
        internal List<Room> PlacedRooms
        {
            get { return _placedRooms; }
        }

        internal event Action<Room, Vector3> OnRoomInstantiated;
        internal event Action<Room, Vector3, Vector3> OnRoomWallInstantiated;
        internal event Action<GameObject, Vector3> OnRoomFloorInstantiated;
        internal event Action<GameObject> OnRoomDestroyed;

        /// <summary>
        /// Generates rooms within the dungeon area.
        /// </summary>
        /// <param name="dungeonSizeX">The width of the dungeon area.</param>
        /// <param name="dungeonSizeY">The height of the dungeon area.</param>
        /// <param name="amountOfRooms">The number of rooms to generate.</param>
        /// <param name="aStar">The AStar instance used for pathfinding.</param>
        internal void GenerateRooms(int dungeonSizeX, int dungeonSizeY, int amountOfRooms, AStar aStar, Room roomPrefab, GameObject roomFloorPrefab)
        {
            GenerateBaseRooms(aStar, roomPrefab, roomFloorPrefab);
            for (int i = 0; i < amountOfRooms; i++)
            {
                Room newRoom = roomPrefab;
                newRoom.transform.localScale = GetRoomScale();
                int positionX, positionY, numberOfTries = 0;
                Vector3 transform = Vector3.zero;
                do
                {
                    numberOfTries++;
                    if (numberOfTries > 100)
                    {
                        break;
                    }

                    positionX = UnityEngine.Random.Range(5, dungeonSizeX + 5);
                    positionY = UnityEngine.Random.Range(5, dungeonSizeY + 5);
                    transform = new Vector3(positionX, 0, positionY);
                } while (Physics.CheckBox(transform, (newRoom.transform.localScale + Vector3.one) / 2));

                PlaceRoom(numberOfTries, transform, newRoom, aStar, roomFloorPrefab);
            }
        }

        private void GenerateBaseRooms(AStar aStar, Room roomPrefab, GameObject roomFloorPrefab)
        {
            GenerateRoom(aStar, roomPrefab, roomFloorPrefab, 10, 10);
            GenerateRoom(aStar, roomPrefab, roomFloorPrefab, 60, 60);
        }

        private void GenerateRoom(AStar aStar, Room roomPrefab, GameObject roomFloorPrefab, int x, int y)
        {
            Room newRoom = roomPrefab;
            newRoom.transform.localScale = GetRoomScale();
            Vector3 transform = new(x, 0, y);
            PlaceRoom(0, transform, newRoom, aStar, roomFloorPrefab);
        }

        /// <summary>
        /// Gets the scale of a new room.
        /// </summary>
        /// <returns>The scale of the new room.</returns>
        private Vector3 GetRoomScale()
        {
            int randomEvenX = UnityEngine.Random.Range(1, 4) * 2;
            int randomEvenZ = UnityEngine.Random.Range(1, 4) * 2;
            return new Vector3(randomEvenX + 1, 1, randomEvenZ + 1);
        }

        /// <summary>
        /// Places the room within the dungeon area if the number of placement attempts does not exceed the limit.
        /// </summary>
        /// <param name="numberOfTries">The number of attempts made to place the room.</param>
        /// <param name="transform">The position to place the room.</param>
        /// <param name="newRoom">The room to place.</param>
        /// <param name="aStar">The AStar instance used for pathfinding.</param>
        private void PlaceRoom(int numberOfTries, Vector3 transform, Room newRoom, AStar aStar, GameObject roomFloorPrefab)
        {
            if (numberOfTries <= 100)
            {
                OnRoomInstantiated?.Invoke(newRoom, transform);
                roomFloorPrefab.transform.localScale = new Vector3(newRoom.transform.localScale.x, 0.05f, newRoom.transform.localScale.z);
                OnRoomFloorInstantiated?.Invoke(roomFloorPrefab, new Vector3(transform.x, -0.5f, transform.z));
                SetRoomNodes(newRoom, aStar);
            }
        }

        /// <summary>
        /// Sets the nodes of the A* grid to represent the placement of the room.
        /// </summary>
        /// <param name="newRoom">The newly placed room.</param>
        /// <param name="aStar">The AStar instance used for pathfinding.</param>
        private void SetRoomNodes(Room newRoom, AStar aStar)
        {
            Vector3 roomPosition = newRoom.transform.position;
            Vector3 roomScale = newRoom.transform.localScale;

            int minX = Mathf.FloorToInt(roomPosition.x - roomScale.x / 2.1f);
            int maxX = Mathf.CeilToInt(roomPosition.x + roomScale.x / 2.1f);
            int minY = Mathf.FloorToInt(roomPosition.z - roomScale.z / 2.1f);
            int maxY = Mathf.CeilToInt(roomPosition.z + roomScale.z / 2.1f);

            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    PathNode pathNode = aStar.GetGrid().GetGridObject(x, y);

                    if (pathNode != null)
                    {
                        pathNode.NodeType = NodeType.Room;
                        aStar.GetGrid().SetGridObject(x, y, pathNode);
                    }
                }
            }
        }

        /// <summary>
        /// Builds the walls of each room within the dungeon area.
        /// </summary>
        /// <param name="aStar">The AStar instance used for pathfinding.</param>
        internal void BuildRooms(AStar aStar)
        {
            foreach (Room room in _placedRooms)
            {
                Vector3 roomPosition = room.transform.position;
                Vector3 roomScale = room.transform.localScale;

                aStar.GetGrid().GetXY(roomPosition, out int roomX, out int roomY);

                int startXMin = Mathf.FloorToInt(roomX - (roomScale.x - 1) / 2);
                int startXMax = Mathf.CeilToInt(roomX + (roomScale.x - 1) / 2);
                int startYMin = Mathf.FloorToInt(roomY - (roomScale.z - 1) / 2);
                int startYMax = Mathf.CeilToInt(roomY + (roomScale.z - 1) / 2);

                BuildRoomWalls(aStar, room, startXMin, startXMax, startYMin, startYMax);

                OnRoomDestroyed?.Invoke(room.gameObject);
            }
        }

        /// <summary>
        /// Builds the walls surrounding each room.
        /// </summary>
        /// <param name="aStar">The AStar instance used for pathfinding.</param>
        /// <param name="room">The room to build walls for.</param>
        /// <param name="startXMin">The minimum X coordinate of the room.</param>
        /// <param name="startXMax">The maximum X coordinate of the room.</param>
        /// <param name="startYMin">The minimum Y coordinate of the room.</param>
        /// <param name="startYMax">The maximum Y coordinate of the room.</param>
        private void BuildRoomWalls(AStar aStar, Room room, int startXMin, int startXMax, int startYMin, int startYMax)
        {
            for (int x = startXMin; x <= startXMax; x++)
            {
                for (int y = startYMin; y <= startYMax; y++)
                {
                    if (x == startXMin || x == startXMax || y == startYMin || y == startYMax)
                    {
                        BuildOuterWalls(aStar, room, x, y, startXMin, startXMax, startYMin, startYMax);
                    }
                }
            }
        }

        /// <summary>
        /// Builds the outer walls around a room based on its position in the dungeon area.
        /// </summary>
        /// <param name="aStar">The AStar instance used for pathfinding.</param>
        /// <param name="room">The room to build walls for.</param>
        /// <param name="x">The X coordinate of the current tile.</param>
        /// <param name="y">The Y coordinate of the current tile.</param>
        /// <param name="startXMin">The minimum X coordinate of the room.</param>
        /// <param name="startXMax">The maximum X coordinate of the room.</param>
        /// <param name="startYMin">The minimum Y coordinate of the room.</param>
        /// <param name="startYMax">The maximum Y coordinate of the room.</param>
        private void BuildOuterWalls(AStar aStar, Room room, int x, int y, int startXMin, int startXMax, int startYMin, int startYMax)
        {
            PathNode roomNode = aStar.GetGrid().GetGridObject(x, y);
            if (roomNode != null && IsOuterNode(x, y, startXMin, startXMax, startYMin, startYMax))
            {
                Vector3 tilePosition = new(x, 0, y);
                if (x == startXMin || x == startXMax || y == startYMin || y == startYMax)
                {
                    if (x == startXMin && y == startYMin) // Bottom-left corner
                    {
                        BuildCornerWalls(room, tilePosition, new Vector3(-0.5f, 0, 0), new Vector3(0, 0, -0.5f));
                    }
                    else if (x == startXMin && y == startYMax) // Top-left corner
                    {
                        BuildCornerWalls(room, tilePosition, new Vector3(-0.5f, 0, 0), new Vector3(0, 0, 0.5f));
                    }
                    else if (x == startXMax && y == startYMin) // Bottom-right corner
                    {
                        BuildCornerWalls(room, tilePosition, new Vector3(0.5f, 0, 0), new Vector3(0, 0, -0.5f));
                    }
                    else if (x == startXMax && y == startYMax) // Top-right corner
                    {
                        BuildCornerWalls(room, tilePosition, new Vector3(0.5f, 0, 0), new Vector3(0, 0, 0.5f));
                    }
                    else if (x == startXMin || x == startXMax) // Left or Right side
                    {
                        BuildSideWall(room, tilePosition, (x == startXMin) ? new Vector3(-0.5f, 0, 0) : 
                            new Vector3(0.5f, 0, 0), new Vector3(0.05f, 1, 1));
                    }
                    else if (y == startYMin || y == startYMax) // Bottom or Top side
                    {
                        BuildSideWall(room, tilePosition, (y == startYMin) ? new Vector3(0, 0, -0.5f) : 
                            new Vector3(0, 0, 0.5f), new Vector3(1, 1, 0.05f));
                    }
                }
            }
        }

        /// <summary>
        /// Builds walls at the corner of a room based on the given position and offsets.
        /// </summary>
        /// <param name="room">The room to build corner walls for.</param>
        /// <param name="position">The position where the corner walls will be built.</param>
        /// <param name="offset1">The first offset for the corner walls.</param>
        /// <param name="offset2">The second offset for the corner walls.</param>
        private void BuildCornerWalls(Room room, Vector3 position, Vector3 offset1, Vector3 offset2)
        {
            BuildSideWall(room, position, offset1, new Vector3(0.05f, 1, 1));
            BuildSideWall(room, position, offset2, new Vector3(1, 1, 0.05f));
        }

        /// <summary>
        /// Determines if the given tile is an outer node of the room.
        /// </summary>
        /// <param name="x">The X coordinate of the tile.</param>
        /// <param name="y">The Y coordinate of the tile.</param>
        /// <param name="startXMin">The minimum X coordinate of the room.</param>
        /// <param name="startXMax">The maximum X coordinate of the room.</param>
        /// <param name="startYMin">The minimum Y coordinate of the room.</param>
        /// <param name="startYMax">The maximum Y coordinate of the room.</param>
        /// <returns>True if the tile is an outer node of the room, otherwise false.</returns>
        private bool IsOuterNode(int x, int y, int startXMin, int startXMax, int startYMin, int startYMax)
        {
            return x == startXMin || x == startXMax || y == startYMin || y == startYMax;
        }

        /// <summary>
        /// Builds a wall for the specified room at the given tile position with the provided wall offset and scale.
        /// </summary>
        /// <param name="room">The room to build the wall for.</param>
        /// <param name="tilePosition">The position of the tile where the wall will be built.</param>
        /// <param name="wallOffset">The offset from the tile position to place the wall.</param>
        /// <param name="wallScale">The scale of the wall.</param>
        private void BuildSideWall(Room room, Vector3 tilePosition, Vector3 wallOffset, Vector3 wallScale)
        {
            Vector3 wallPosition = tilePosition + wallOffset;
            OnRoomWallInstantiated?.Invoke(room, wallPosition, wallScale);
        }
    }
}
