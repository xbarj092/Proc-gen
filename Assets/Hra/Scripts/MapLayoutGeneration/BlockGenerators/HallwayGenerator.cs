using System.Collections.Generic;
using UnityEngine;

public class HallwayGenerator : MonoBehaviour
{
    private List<List<PathNode>> _placedHallways = new List<List<PathNode>>();

    public void GenerateHallways(List<Triangle> triangles, List<Room> placedRooms, AStar aStar, PrimsAlg primsAlg, GameObject hallwayPrefab)
    {
        List<RoomConnection> hallways = primsAlg.CreateTriMesh(triangles, placedRooms);
        foreach (RoomConnection roomConnection in hallways)
        {
            PathNode startNode = aStar.GetGrid().GetGridObject(roomConnection.StartRoom.transform.position);
            PathNode endNode = aStar.GetGrid().GetGridObject(roomConnection.EndRoom.transform.position);
            Vector3 startRoomPosition = roomConnection.StartRoom.transform.position;
            Vector3 startRoomScale = roomConnection.StartRoom.transform.localScale;
            Vector3 endRoomPosition = roomConnection.EndRoom.transform.position;
            Vector3 endRoomScale = roomConnection.EndRoom.transform.localScale;

            aStar.GetGrid().GetXY(startRoomPosition, out int startX, out int startY);
            aStar.GetGrid().GetXY(endRoomPosition, out int endX, out int endY);

            int minXStart = Mathf.FloorToInt(startX - (startRoomScale.x - 1) / 2);
            int maxXStart = Mathf.CeilToInt(startX + (startRoomScale.x - 1) / 2);
            int minYStart = Mathf.FloorToInt(startY - (startRoomScale.z - 1) / 2);
            int maxYStart = Mathf.CeilToInt(startY + (startRoomScale.z - 1) / 2);

            int minXEnd = Mathf.FloorToInt(endX - (endRoomScale.x - 1) / 2);
            int maxXEnd = Mathf.CeilToInt(endX + (endRoomScale.x - 1) / 2);
            int minYEnd = Mathf.FloorToInt(endY - (endRoomScale.z - 1) / 2);
            int maxYEnd = Mathf.CeilToInt(endY + (endRoomScale.z - 1) / 2);

            PathNode closestStartNode = null;
            PathNode closestEndNode = null;
            float minDistance = float.MaxValue;

            for (int xStart = minXStart; xStart <= maxXStart; xStart++)
            {
                for (int yStart = minYStart; yStart <= maxYStart; yStart++)
                {
                    startNode = aStar.GetGrid().GetGridObject(xStart, yStart);

                    if (startNode != null)
                    {
                        for (int xEnd = minXEnd; xEnd <= maxXEnd; xEnd++)
                        {
                            for (int yEnd = minYEnd; yEnd <= maxYEnd; yEnd++)
                            {
                                endNode = aStar.GetGrid().GetGridObject(xEnd, yEnd);

                                if (endNode != null)
                                {
                                    float currentDistance = Vector2.Distance(new Vector2(xStart, yStart), new Vector2(xEnd, yEnd));
                                    if (currentDistance < minDistance)
                                    {
                                        closestStartNode = startNode;
                                        closestEndNode = endNode;
                                        minDistance = currentDistance;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            List<PathNode> path = aStar.FindPath(closestStartNode.X, closestStartNode.Y, closestEndNode.X, closestEndNode.Y);

            if (path == null || path.Count <= 0)
            {
                Debug.LogError("Path is null or empty");
                continue;
            }

            foreach (PathNode pathNode in path)
            {
                GameObject hallway = Instantiate(hallwayPrefab, new Vector3(pathNode.X, 0, pathNode.Y), Quaternion.identity);
                hallway.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        foreach (Room room in placedRooms)
        {
            foreach (Collider collider in Physics.OverlapBox(room.transform.position, new Vector3(room.transform.localScale.x, room.transform.localScale.y, room.transform.localScale.z) / 2.1f))
            {
                if (collider.gameObject.CompareTag("Hallway"))
                {
                    Destroy(collider.gameObject);
                }
            }
        }
    }
}
