using UnityEngine;
using System.Collections.Generic;

public class Agent : MonoBehaviour
{

    IDictionary<Vector3, Vector3> nodeParents = new Dictionary<Vector3, Vector3>();
    public IDictionary<Vector3, bool> walkablePositions;
    public IDictionary<Vector3, string> obstacles;
    IDictionary<Vector3, Sprite> prevSprite = new Dictionary<Vector3, Sprite>();

    NodeNetwork nodeNetwork;
    IList<Vector3> path;

    bool solutionVisible;
    string prevAlgo;

    Camera camera;

    bool moveCube = false;
    int i;

    // Use this for initialization
    void Start()
    {
        camera = FindObjectOfType<Camera>();
        nodeNetwork = GameObject.Find("NodeNetwork").GetComponent<NodeNetwork>();
        obstacles = GameObject.Find("NodeNetwork").GetComponent<NodeNetwork>().obstacles;
        walkablePositions = nodeNetwork.walkablePositions;
    }

    // Update is called once per frame
    void Update()
    {

        camera.transform.position = new Vector3(this.transform.position.x, 10, this.transform.position.z);

        //Hacky way to move the cube along the path.
        if (moveCube)
        {
            float speed = 25 / Weight(path[i]);
            float step = Time.deltaTime * speed;
            transform.position = Vector3.MoveTowards(transform.position, path[i], step);
            if (transform.position.Equals(path[i]) && i >= 0)
                i--;
            if (i < 0)
                moveCube = false;
        }
    }

    int EuclideanEstimate(Vector3 node, Vector3 goal)
    {
        return (int)Mathf.Sqrt(Mathf.Pow(node.x - goal.x, 2) +
            Mathf.Pow(node.y - goal.y, 2) +
            Mathf.Pow(node.z - goal.z, 2));
    }

    int ManhattanEstimate(Vector3 node, Vector3 goal)
    {
        return (int)(Mathf.Abs(node.x - goal.x) +
            Mathf.Abs(node.y - goal.y) +
            Mathf.Abs(node.z - goal.z));
    }

    int HeuristicCostEstimate(Vector3 node, Vector3 goal, string heuristic)
    {
        switch (heuristic)
        {
            case "euclidean":
                return EuclideanEstimate(node, goal);
            case "manhattan":
                return ManhattanEstimate(node, goal);
        }

        return -1;
    }

    int Weight(Vector3 node)
    {
        if (obstacles.Keys.Contains(node))
        {
            if (obstacles[node] == "slow")
            {
                return 3;
            }
            else if (obstacles[node] == "verySlow")
            {
                return 5;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            return 1;
        }
    }

    //Breadth first search of graph.
    //Populates IList<Vector3> path with a valid solution to the goalPosition.
    //Returns the goalPosition if a solution is found.
    //Returns the startPosition if no solution is found.
    Vector3 FindShortestPathBFS(Vector3 startPosition, Vector3 goalPosition)
    {
        Queue<Vector3> queue = new Queue<Vector3>();
        HashSet<Vector3> exploredNodes = new HashSet<Vector3>();
        queue.Enqueue(startPosition);

        while (queue.Count != 0)
        {
            Vector3 currentNode = queue.Dequeue();
            if (currentNode == goalPosition)
            {
                return currentNode;
            }

            IList<Vector3> nodes = GetWalkableNodes(currentNode);

            foreach (Vector3 node in nodes)
            {
                if (!exploredNodes.Contains(node))
                {
                    //Mark the node as explored
                    exploredNodes.Add(node);

                    //Store a reference to the previous node
                    nodeParents.Add(node, currentNode);

                    //Add this to the queue of nodes to examine
                    queue.Enqueue(node);
                }
            }
        }

        return startPosition;
    }

    bool CanMove(Vector3 nextPosition)
    {
        return (walkablePositions.ContainsKey(nextPosition) ? walkablePositions[nextPosition] : false);
    }

    public void DisplayShortestPath(string algorithm)
    {

        if (solutionVisible && algorithm == prevAlgo)
        {
            foreach (Vector3 node in path)
            {
                nodeNetwork.nodeReference[node].GetComponent<SpriteRenderer>().sprite = prevSprite[node];
            }

            solutionVisible = false;
            return;
        }

        nodeParents = new Dictionary<Vector3, Vector3>();
        path = FindShortestPath(algorithm);

        if (path == null)
            return;

        Sprite exploredTile = Resources.Load<Sprite>("path 1");
        Sprite victoryTile = Resources.Load<Sprite>("victory 1");
        Sprite dijkstraTile = Resources.Load<Sprite>("dijkstra");

        foreach (Vector3 node in path)
        {

            prevSprite[node] = nodeNetwork.nodeReference[node].GetComponent<SpriteRenderer>().sprite;

            if (algorithm == "DFS")
            {
                nodeNetwork.nodeReference[node].GetComponent<SpriteRenderer>().sprite = victoryTile;
            }
            else if (algorithm == "BFS")
            {
                nodeNetwork.nodeReference[node].GetComponent<SpriteRenderer>().sprite = exploredTile;
            }
            else if (algorithm == "AStarEuclid")
            {
                nodeNetwork.nodeReference[node].GetComponent<SpriteRenderer>().sprite = victoryTile;
            }
            else if (algorithm == "AStarManhattan")
            {
                nodeNetwork.nodeReference[node].GetComponent<SpriteRenderer>().sprite = dijkstraTile;
            }
            else
            {
                nodeNetwork.nodeReference[node].GetComponent<SpriteRenderer>().sprite = dijkstraTile;
            }
        }

        nodeNetwork.nodeReference[path[0]].GetComponent<SpriteRenderer>().sprite = victoryTile;

        i = path.Count - 1;

        solutionVisible = true;
        prevAlgo = algorithm;
    }

    public void MoveCube()
    {
        moveCube = true;
    }

    IList<Vector3> FindShortestPath(string algorithm)
    {

        IList<Vector3> path = new List<Vector3>();
        Vector3 goal;

        goal = FindShortestPathBFS(this.transform.localPosition, GameObject.Find("Goal").transform.localPosition);

        if (goal == this.transform.localPosition || !nodeParents.ContainsKey(nodeParents[goal]))
        {
            //No solution was found.
            return null;
        }

        Vector3 curr = goal;
        while (curr != this.transform.localPosition)
        {
            path.Add(curr);
            curr = nodeParents[curr];
        }

        return path;
    }

    IList<Vector3> GetWalkableNodes(Vector3 curr)
    {

        IList<Vector3> walkableNodes = new List<Vector3>();

        IList<Vector3> possibleNodes = new List<Vector3>() {
            new Vector3 (curr.x + 1, curr.y, curr.z),
            new Vector3 (curr.x - 1, curr.y, curr.z),
            new Vector3 (curr.x, curr.y, curr.z + 1),
            new Vector3 (curr.x, curr.y, curr.z - 1),
            new Vector3 (curr.x + 1, curr.y, curr.z + 1),
            new Vector3 (curr.x + 1, curr.y, curr.z - 1),
            new Vector3 (curr.x - 1, curr.y, curr.z + 1),
            new Vector3 (curr.x - 1, curr.y, curr.z - 1)
        };

        foreach (Vector3 node in possibleNodes)
        {
            if (CanMove(node))
            {
                walkableNodes.Add(node);
            }
        }

        return walkableNodes;
    }
}