using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AStar
{
    private static Dictionary<Point, Node> nodes;

    // Creates a node for each tile in the game
    private static void CreateNodes()
    {
        // Instantiates the dictionary
        nodes = new Dictionary<Point, Node>();

        // Run through all the tiles in the game
        foreach (TileScript tile in LevelManager.Instance.Tiles.Values)
        {
            // Add the node to the node dictionary
            nodes.Add(tile.GridPosition, new Node(tile));
        }
    }

    public static void GetPath(Point start, Point goal)
    {
        if (nodes == null)
        {
            CreateNodes();
        }

        HashSet<Node> openList = new HashSet<Node>();

        HashSet<Node> closedList = new HashSet<Node>();

        Stack<Node> finalPath = new Stack<Node>();

        Node currentNode = nodes[start];

        openList.Add(currentNode);

        while (openList.Count > 0)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Point neighbourPos = new Point(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y);

                    if (LevelManager.Instance.InBounds(neighbourPos) && LevelManager.Instance.Tiles[neighbourPos].Walkable && neighbourPos != currentNode.GridPosition)
                    {
                        int gCost = 0;

                        if (Mathf.Abs(x - y) == 1)
                        {
                            gCost = 10;
                        }
                        else
                        {
                            if (!ConnectedDiagonally(currentNode, nodes[neighbourPos]))
                            {
                                continue;
                            }
                            gCost = 14;
                        }

                        Node neighbour = nodes[neighbourPos];

                        if (openList.Contains(neighbour))
                        {
                            if (currentNode.G + gCost < neighbour.G)
                            {
                                neighbour.CalcValues(currentNode, nodes[goal], gCost);
                            }
                        }
                        else if (!closedList.Contains(neighbour))
                        {
                            openList.Add(neighbour);
                            neighbour.CalcValues(currentNode, nodes[goal], gCost);
                        }
                    }
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (openList.Count > 0)
            {
                // Sorts the list by F value and selects the first one on it
                currentNode = openList.OrderBy(n => n.F).First();
            }

            if (currentNode == nodes[goal])
            {
                while (currentNode.GridPosition != start)
                {
                    finalPath.Push(currentNode);
                    currentNode = currentNode.Parent;
                }
                break;
            }

        }
    }

    private static bool ConnectedDiagonally(Node currentNode, Node neighbour)
    {
        Point direction = neighbour.GridPosition - currentNode.GridPosition;

        Point first = new Point(currentNode.GridPosition.X + direction.X, currentNode.GridPosition.Y);

        Point second = new Point(currentNode.GridPosition.X, currentNode.GridPosition.Y + direction.Y);

        if (LevelManager.Instance.InBounds(first) && !LevelManager.Instance.Tiles[first].Walkable) {
            return false;
        }

        if (LevelManager.Instance.InBounds(second) && !LevelManager.Instance.Tiles[second].Walkable)
        {
            return false;
        }

        return true;
    }
}
