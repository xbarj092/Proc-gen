using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    private Grid<PathNode> _grid;
    private List<PathNode> _openList;
    private List<PathNode> _closedList;

    public AStar(int width, int height)
    {
        _grid = new Grid<PathNode>(width, height, 1, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }

    public Grid<PathNode> GetGrid()
    {
        return _grid;
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = _grid.GetGridObject(startX, startY);
        PathNode endNode = _grid.GetGridObject(endX, endY);

        _openList = new List<PathNode> { startNode };
        _closedList = new List<PathNode>();

        for (int x = 0; x < _grid.GetWidth(); x++)
        {
            for (int y = 0; y < _grid.GetHeight(); y++)
            {
                PathNode pathNode = _grid.GetGridObject(x, y);
                pathNode.GCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.CameFromNode = null;
            }
        }

        startNode.GCost = 0;
        startNode.HCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (_openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(_openList);
            if (currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            _openList.Remove(currentNode);
            _closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (!_closedList.Contains(neighbourNode))
                {
                    int tentativeGCost = currentNode.GCost + CalculateDistanceCost(currentNode, neighbourNode);
                    if (tentativeGCost < neighbourNode.GCost)
                    {
                        neighbourNode.CameFromNode = currentNode;
                        neighbourNode.GCost = tentativeGCost;
                        neighbourNode.HCost = CalculateDistanceCost(neighbourNode, endNode);
                        neighbourNode.CalculateFCost();

                        if (!_openList.Contains(neighbourNode))
                        {
                            _openList.Add(neighbourNode);
                        }
                    }
                }
            }
        }

        return null;
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();
        if (currentNode.X - 1 >= 0) neighbourList.Add(GetNode(currentNode.X - 1, currentNode.Y));
        if (currentNode.X + 1 < _grid.GetWidth()) neighbourList.Add(GetNode(currentNode.X + 1, currentNode.Y));
        if (currentNode.Y - 1 >= 0) neighbourList.Add(GetNode(currentNode.X, currentNode.Y - 1));
        if (currentNode.Y + 1 < _grid.GetHeight()) neighbourList.Add(GetNode(currentNode.X, currentNode.Y + 1));
        return neighbourList;
    }

    private PathNode GetNode(int x, int y)
    {
        return _grid.GetGridObject(x, y);
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new() { endNode };
        PathNode currentNode = endNode;
        while (currentNode.CameFromNode != null)
        {
            path.Add(currentNode.CameFromNode);
            currentNode.NodeType = NodeType.Hallway;
            currentNode = currentNode.CameFromNode;
        }

        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(PathNode startNode, PathNode endNode)
    {
        int xDistance = Mathf.Abs(startNode.X - endNode.X);
        int yDistance = Mathf.Abs(startNode.Y - endNode.Y);

        int straightCost = 10;
        int diagonalCost = 21;

        int roomDistanceCost = 10;
        int hallwayDistanceCost = 1;
        int noneDistanceCost = 5;

        int additionalCost = 0;
        if (startNode.NodeType == NodeType.Room)
            additionalCost += roomDistanceCost;
        else if (startNode.NodeType == NodeType.Hallway)
            additionalCost += hallwayDistanceCost;
        else if (startNode.NodeType == NodeType.None)
            additionalCost += noneDistanceCost;

        if (endNode.NodeType == NodeType.Room)
            additionalCost += roomDistanceCost;
        else if (endNode.NodeType == NodeType.Hallway)
            additionalCost += hallwayDistanceCost;
        else if (endNode.NodeType == NodeType.None)
            additionalCost += noneDistanceCost;

        int remaining = Mathf.Abs(xDistance - yDistance);
        return (diagonalCost * Mathf.Min(xDistance, yDistance) + straightCost * remaining) + additionalCost;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodes)
    {
        PathNode lowestFCostNode = pathNodes[0];
        for (int i = 0; i < pathNodes.Count; i++)
        {
            if (pathNodes[i].FCost < lowestFCostNode.FCost)
            {
                lowestFCostNode = pathNodes[i];
            }
        }
        return lowestFCostNode;
    }
}
