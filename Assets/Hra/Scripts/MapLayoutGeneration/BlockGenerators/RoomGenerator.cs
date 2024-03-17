using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private Room _roomPrefab;
    [SerializeField] private GameObject _floorPrefab;

    private List<Room> _placedRooms = new List<Room>();
    public List<Room> PlacedRooms
    {
        get { return _placedRooms; }
    }

    public void GenerateRooms(int dungeonSizeX, int dungeonSizeY, int amountOfRooms, AStar aStar)
    {
        for (int i = 0; i < amountOfRooms; i++)
        {
            int minEvenValue = 2;
            int maxEvenValue = 6;

            int randomEvenX = Random.Range(minEvenValue / 2, maxEvenValue / 2 + 1) * 2;
            int randomEvenZ = Random.Range(minEvenValue / 2, maxEvenValue / 2 + 1) * 2;
            Room newRoom = _roomPrefab;
            newRoom.transform.localScale = new Vector3(randomEvenX + 1, 1, randomEvenZ + 1);
            int positionX, positionY, numberOfTries = 0;
            Vector3 transform = Vector3.zero;
            do
            {
                numberOfTries++;
                if (numberOfTries > 100)
                {
                    break;
                }

                positionX = Random.Range(5, dungeonSizeX + 5);
                positionY = Random.Range(5, dungeonSizeY + 5);
                transform = new Vector3(positionX, 0, positionY);
            } while (Physics.CheckBox(transform, (newRoom.transform.localScale + Vector3.one) / 2));

            if (numberOfTries <= 100)
            {
                newRoom = Instantiate(newRoom, transform, Quaternion.identity);
                GameObject floor = Instantiate(_floorPrefab, new Vector3(transform.x, -0.5f, transform.z), Quaternion.identity);
                floor.transform.localScale = new Vector3(newRoom.transform.localScale.x, 0.05f, newRoom.transform.localScale.z);
                _placedRooms.Add(newRoom);
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
        }
    }

    public void BuildRooms(AStar aStar)
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

            for (int x = startXMin; x <= startXMax; x++)
            {
                for (int y = startYMin; y <= startYMax; y++)
                {
                    if (x == startXMin || x == startXMax || y == startYMin || y == startYMax)
                    {
                        PathNode roomNode = aStar.GetGrid().GetGridObject(x, y);
                        if (roomNode != null && IsOuterNode(x, y, startXMin, startXMax, startYMin, startYMax))
                        {
                            Vector3 tilePosition = new Vector3(x, 0, y);
                            if (x == startXMin && y == startYMin) // Bottom-left corner
                            {
                                BuildSideWall(room, tilePosition, new Vector3(-0.5f, 0, 0), new Vector3(0.05f, 1, 1));
                                BuildSideWall(room, tilePosition, new Vector3(0, 0, -0.5f), new Vector3(1, 1, 0.05f));
                            }
                            else if (x == startXMin && y == startYMax) // Top-left corner
                            {
                                BuildSideWall(room, tilePosition, new Vector3(-0.5f, 0, 0), new Vector3(0.05f, 1, 1));
                                BuildSideWall(room, tilePosition, new Vector3(0, 0, 0.5f), new Vector3(1, 1, 0.05f));
                            }
                            else if (x == startXMax && y == startYMin) // Bottom-right corner
                            {
                                BuildSideWall(room, tilePosition, new Vector3(0.5f, 0, 0), new Vector3(0.05f, 1, 1));
                                BuildSideWall(room, tilePosition, new Vector3(0, 0, -0.5f), new Vector3(1, 1, 0.05f));
                            }
                            else if (x == startXMax && y == startYMax) // Top-right corner
                            {
                                BuildSideWall(room, tilePosition, new Vector3(0.5f, 0, 0), new Vector3(0.05f, 1, 1));
                                BuildSideWall(room, tilePosition, new Vector3(0, 0, 0.5f), new Vector3(1, 1, 0.05f));
                            }
                            else if (x == startXMin || x == startXMax) // Left or Right side
                            {
                                BuildSideWall(room, tilePosition, (x == startXMin) ? new Vector3(-0.5f, 0, 0) : new Vector3(0.5f, 0, 0), new Vector3(0.05f, 1, 1));
                            }
                            else if (y == startYMin || y == startYMax) // Bottom or Top side
                            {
                                BuildSideWall(room, tilePosition, (y == startYMin) ? new Vector3(0, 0, -0.5f) : new Vector3(0, 0, 0.5f), new Vector3(1, 1, 0.05f));
                            }
                        }
                    }
                }
            }

            Destroy(room.gameObject);
        }
    }

    private bool IsOuterNode(int x, int y, int startXMin, int startXMax, int startYMin, int startYMax)
    {
        return x == startXMin || x == startXMax || y == startYMin || y == startYMax;
    }

    private void BuildSideWall(Room room, Vector3 tilePosition, Vector3 wallOffset, Vector3 wallScale)
    {
        Vector3 wallPosition = tilePosition + wallOffset;
        Instantiate(room, wallPosition, Quaternion.identity).transform.localScale = wallScale;
    }
}
