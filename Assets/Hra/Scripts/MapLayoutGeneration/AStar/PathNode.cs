using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType
{
    None = 0,
    Room = 1,
    Hallway = 2
}

[Serializable]
public class PathNode
{
    public NodeType NodeType = NodeType.None;
    private Grid<PathNode> _grid;
    public int X;
    public int Y;

    public int GCost;
    public int HCost;
    public int FCost;

    public PathNode CameFromNode;

    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        _grid = grid;
        X = x;
        Y = y;
    }

    public void CalculateFCost()
    {
        FCost = GCost + HCost;
    }
}
