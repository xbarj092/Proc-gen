using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class HallwayGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _hallwayPrefab;
    [SerializeField] private GameObject _hallwayFloorPrefab;

    private List<List<PathNode>> _paths = new List<List<PathNode>>();
    private List<PathNode> _edgeNodes = new List<PathNode>();

    public void GenerateHallways(List<Triangle> triangles, List<Room> placedRooms, AStar aStar, PrimsAlg primsAlg)
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

            _paths.Add(aStar.FindPath(closestStartNode.X, closestStartNode.Y, closestEndNode.X, closestEndNode.Y));
        }

        foreach (List<PathNode> path in _paths)
        {
            _edgeNodes.AddRange(new List<PathNode>() { path.ElementAt(path.Count - 2), path.ElementAt(1)});

            foreach (PathNode pathNode in path)
            {
                PathNode leftNode = null;
                PathNode rightNode = null;
                PathNode topNode = null;
                PathNode bottomNode = null;

                foreach (PathNode neighbourNode in aStar.GetNeighbourList(pathNode))
                {
                    if (neighbourNode.NodeType == NodeType.Hallway)
                    {
                        if (neighbourNode.X == pathNode.X)
                        {
                            if (neighbourNode.Y == pathNode.Y - 1)
                            {
                                bottomNode = neighbourNode;
                            }
                            else if (neighbourNode.Y == pathNode.Y + 1)
                            {
                                topNode = neighbourNode;
                            }
                        }
                        else if (neighbourNode.Y == pathNode.Y)
                        {
                            if (neighbourNode.X == pathNode.X - 1)
                            {
                                leftNode = neighbourNode;
                            }
                            else if (neighbourNode.X == pathNode.X + 1)
                            {
                                rightNode = neighbourNode;
                            }
                        }
                    }
                }

                // TODO - destroy ovelapping hallways
                // TODO - create room entrances
                if (topNode != null && bottomNode != null)
                {
                    if (rightNode == null && leftNode == null)
                    {
                        InstantiateHallway(pathNode, new Vector3(0.5f, 0, 0), new Vector3(0.05f, 1, 1));
                        InstantiateHallway(pathNode, new Vector3(-0.5f, 0, 0), new Vector3(0.05f, 1, 1));
                    }
                    else if (leftNode == null && rightNode != null)
                    {
                        InstantiateHallway(pathNode, new Vector3(-0.5f, 0, 0), new Vector3(0.05f, 1, 1));
                    }
                    else if (leftNode != null && rightNode == null)
                    {
                        InstantiateHallway(pathNode, new Vector3(0.5f, 0, 0), new Vector3(0.05f, 1, 1));
                    }
                }
                else if (topNode != null && bottomNode == null)
                {
                    if (leftNode == null && rightNode != null)
                    {
                        InstantiateHallway(pathNode, new Vector3(-0.5f, 0, 0), new Vector3(0.05f, 1, 1));
                    }
                    else if (leftNode != null && rightNode == null)
                    {
                        InstantiateHallway(pathNode, new Vector3(0.5f, 0, 0), new Vector3(0.05f, 1, 1));
                    }
                    else if (leftNode == null && rightNode == null)
                    {
                        InstantiateHallway(pathNode, new Vector3(0.5f, 0, 0), new Vector3(0.05f, 1, 1));
                        InstantiateHallway(pathNode, new Vector3(-0.5f, 0, 0), new Vector3(0.05f, 1, 1));
                    }

                    InstantiateHallway(pathNode, new Vector3(0, 0, -0.5f), new Vector3(1, 1, 0.05f));
                }
                else if (topNode == null && bottomNode != null)
                {
                    if (leftNode == null && rightNode != null)
                    {
                        InstantiateHallway(pathNode, new Vector3(-0.5f, 0, 0), new Vector3(0.05f, 1, 1));
                    }
                    else if (leftNode != null && rightNode == null)
                    {
                        InstantiateHallway(pathNode, new Vector3(0.5f, 0, 0), new Vector3(0.05f, 1, 1));
                    }
                    else if (leftNode == null && rightNode == null)
                    {
                        InstantiateHallway(pathNode, new Vector3(0.5f, 0, 0), new Vector3(0.05f, 1, 1));
                        InstantiateHallway(pathNode, new Vector3(-0.5f, 0, 0), new Vector3(0.05f, 1, 1));
                    }

                    InstantiateHallway(pathNode, new Vector3(0, 0, 0.5f), new Vector3(1, 1, 0.05f));
                }
                else if (topNode == null && bottomNode == null)
                {
                    if (leftNode != null && rightNode == null)
                    {
                        InstantiateHallway(pathNode, new Vector3(0.5f, 0, 0), new Vector3(0.05f, 1, 1));
                    }
                    else if (leftNode == null && rightNode != null)
                    {
                        InstantiateHallway(pathNode, new Vector3(-0.5f, 0, 0), new Vector3(0.05f, 1, 1));
                    }

                    InstantiateHallway(pathNode, new Vector3(0, 0, -0.5f), new Vector3(1, 1, 0.05f));
                    InstantiateHallway(pathNode, new Vector3(0, 0, 0.5f), new Vector3(1, 1, 0.05f));
                }

                InstantiateHallway(pathNode, new Vector3(0, -0.5f, 0), new Vector3(1, 0.05f, 1));
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

    public void MakeRoomEntrances(AStar aStar)
    {
        foreach (PathNode node in _edgeNodes)
        {
            Vector3 worldPos = aStar.GetGrid().GetWorldPosition(node.X, node.Y);

            DestroyAdjacentRoom(worldPos, Vector3.left);
            DestroyAdjacentRoom(worldPos, Vector3.right);
            DestroyAdjacentRoom(worldPos, Vector3.forward);
            DestroyAdjacentRoom(worldPos, Vector3.back);
        }
    }

    private void DestroyAdjacentRoom(Vector3 position, Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(position, direction * 0.5f, out hit, 0.5f))
        {
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Room"))
            {
                Debug.DrawRay(position, direction * hit.distance, Color.green, 100);
                Debug.Log("object position -" + hit.collider.transform.position);
                Debug.Log(direction.ToString() + " Destroying room at: " + hit.collider.transform.position);
                Destroy(hit.collider.gameObject);
            }
        }
    }

    private void InstantiateHallway(PathNode pathNode, Vector3 hallwayOffset, Vector3 hallwayScale)
    {
        Vector3 wallPosition = new Vector3(pathNode.X, 0, pathNode.Y) + hallwayOffset;
        Instantiate(_hallwayPrefab, wallPosition, Quaternion.identity).transform.localScale = hallwayScale;
    }
}
