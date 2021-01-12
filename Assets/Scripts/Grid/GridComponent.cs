using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridComponent : MonoBehaviour
{
    public float FieldSize;
    public int x;
    public int z;

    private Vector3 offset;
    public List<KeyValuePair<Field, List<FieldGridCoordinate>>> Spawns { get; set; }
    public Field[,] grid { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        offset = transform.position;
        InitGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(144f / 256f, 238f / 256f, 144f / 256f, 0.5f);
        offset = transform.position;
        //Draw horizontal lines
        for (int i = 0; i <= x; i++)
        {
            Gizmos.DrawLine(offset + new Vector3(FieldSize * i, 0, 0), offset + new Vector3(FieldSize * i, 0, FieldSize * z));
        }
        //Draw vertical lines
        for (int i = 0; i <= z; i++)
        {
            Gizmos.DrawLine(offset + new Vector3(0, 0, FieldSize * i), offset + new Vector3(FieldSize * x, 0, FieldSize * i));
        }
    }

    private void InitGrid()
    {
        Spawns = new List<KeyValuePair<Field, List<FieldGridCoordinate>>>();
        grid = new Field[x, z];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++)
            {
                grid[i, j] = new Field(i, j, FieldSize, offset);
            }
        }
    }

    public void RecalculatePathAfterPlacement(object sender, CoordinateEventArgs args)
    {
        for (int i = 0; i < Spawns.Count; i++)
        {
            if (Spawns[i].Value.Any(field => field.Equals(args.changedCoordinate)))
            {
                Spawns[i] = new KeyValuePair<Field, List<FieldGridCoordinate>>(Spawns[i].Key, CalculatePath(Spawns[i].Key.gridCoordinate));
            }
        }
    }
    public void RecalculatePathAfterDestroy(object sender, CoordinateEventArgs args)
    {
        for (int i = 0; i < Spawns.Count; i++)
        {
            if (!Spawns[i].Value.Any(field => field.Equals(args.changedCoordinate)))
            {
                Spawns[i] = new KeyValuePair<Field, List<FieldGridCoordinate>>(Spawns[i].Key, CalculatePath(Spawns[i].Key.gridCoordinate));
            }
        }
    }

    public void CalculatePath()
    {
        for (int i = 0; i < Spawns.Count; i++)
        {
            Spawns[i] = new KeyValuePair<Field, List<FieldGridCoordinate>>(Spawns[i].Key, CalculatePath(Spawns[i].Key.gridCoordinate));
        }
    }

    public List<FieldGridCoordinate> CalculatePath(FieldGridCoordinate startField)
    {
        List<Node> nodesToCheck = new List<Node>();
        List<Node> checkedNodes = new List<Node>();
        nodesToCheck.Add(new Node(startField.X, startField.Z)
        {
            Cost = 0
        });

        //InitGraph(nodesToCheck);
        while (nodesToCheck.Count > 0)
        {
            int minCostNode = IndexOfMin(nodesToCheck);
            Node tmp = new Node(0,0);
            tmp = nodesToCheck[minCostNode];
            nodesToCheck.Remove(tmp);
            checkedNodes.Add(tmp);
            if (grid[tmp.coordinate.X, tmp.coordinate.Z].IsGoal)
            {
                return CreatePath(tmp);
            }
            else
            {
                List<Node> neighbors = new List<Node>();
                Node tmpNeighbor;
                // Get left neighbor
                if (tmp.coordinate.X > 0)
                {
                    tmpNeighbor = new Node(tmp.coordinate.X - 1, tmp.coordinate.Z);
                    if (!grid[tmpNeighbor.coordinate.X, tmpNeighbor.coordinate.Z].IsEnvironment && !checkedNodes.Any(node => node.coordinate.Equals(tmpNeighbor.coordinate)))
                    {
                        neighbors.Add(tmpNeighbor);
                    }
                }
                // Get right neighbor
                if (tmp.coordinate.X < x - 1)
                {
                    tmpNeighbor = new Node(tmp.coordinate.X + 1, tmp.coordinate.Z);
                    if (!grid[tmpNeighbor.coordinate.X, tmpNeighbor.coordinate.Z].IsEnvironment && !checkedNodes.Any(node => node.coordinate.Equals(tmpNeighbor.coordinate)))
                    {
                        neighbors.Add(tmpNeighbor);
                    }
                }
                // Get lower neighbor
                if (tmp.coordinate.Z > 0)
                {
                    tmpNeighbor = new Node(tmp.coordinate.X, tmp.coordinate.Z - 1);
                    if (!grid[tmpNeighbor.coordinate.X, tmpNeighbor.coordinate.Z].IsEnvironment && !checkedNodes.Any(node => node.coordinate.Equals(tmpNeighbor.coordinate)))
                    {
                        neighbors.Add(tmpNeighbor);
                    }
                }
                // Get upper neighbor
                if (tmp.coordinate.Z < z - 1)
                {
                    tmpNeighbor = new Node(tmp.coordinate.X, tmp.coordinate.Z + 1);
                    if (!grid[tmpNeighbor.coordinate.X, tmpNeighbor.coordinate.Z].IsEnvironment && !checkedNodes.Any(node => node.coordinate.Equals(tmpNeighbor.coordinate)))
                    {
                        neighbors.Add(tmpNeighbor);
                    }
                }

                foreach (Node neighbor in neighbors)
                {
                    int pathCost = tmp.Cost + grid[neighbor.coordinate.X, neighbor.coordinate.Z].GetPathCost();
                    if (nodesToCheck.Any(node => node.coordinate.Equals(neighbor.coordinate)))
                    {
                        tmpNeighbor = nodesToCheck.Find(n => n.coordinate.Equals(neighbor.coordinate));
                        if (tmpNeighbor.Cost > pathCost)
                        {
                            tmpNeighbor.Cost = pathCost;
                            tmpNeighbor.Previous = tmp;
                        }
                    }
                    else
                    {
                        neighbor.Previous = tmp;
                        neighbor.Cost = pathCost;
                        nodesToCheck.Add(neighbor);
                    }

                }
            }
        }
        Debug.LogError("Couldn'f find a valid path from [" + startField.X + "," + startField.Z + "]");
        return null;
    }
    private int IndexOfMin(List<Node> self)
    {
        int min = self[0].Cost;
        int minIndex = 0;

        for (int i = 1; i < self.Count; ++i)
        {
            if (self[i].Cost < min)
            {
                min = self[i].Cost;
                minIndex = i;
            }
        }
        
        return minIndex;
    }
    private List<FieldGridCoordinate> CreatePath(Node goal)
    {
        List<FieldGridCoordinate> path = new List<FieldGridCoordinate>();
        path.Add(goal.coordinate);

        Node previous = goal.Previous;
        while (previous.Previous != null)
        {
            path.Add(previous.coordinate);
            previous = previous.Previous;
        }
        path.Reverse();
        /*foreach(FieldGridCoordinate field in path)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            go.transform.localScale = new Vector3(0.5f, 0.5f,0.5f);
            go.transform.position = grid[field.X, field.Z].GetMiddlePoint() + new Vector3(0,1,0);
        }*/
        return path;
    }

    private void InitGraph(List<Node> graph)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++)
            {
                if (!grid[x, z].IsEnvironment)
                {
                    Node tmp = new Node(x, z);
                    graph.Add(tmp);
                }
            }
        }
    }

    public Field GetNearestField(Vector3 pos)
    {
        pos -= offset;
        pos /= FieldSize;
        return grid[(int)pos.x, (int)pos.z];
    }

    internal Vector3 GetNearestGridPosition(Vector3 mousePosition)
    {
        mousePosition -= offset;
        int zPosition, xPosition;
        //set x coordinate
        if (mousePosition.x < 0)
        {
            xPosition = 0;
        }
        else if (mousePosition.x > (x * FieldSize))
        {
            xPosition = x - 1;
        }
        else
        {
            xPosition = (int)(mousePosition.x / FieldSize);
        }
        //set z coordiante
        if (mousePosition.z < 0)
        {
            zPosition = 0;
        }
        else if (mousePosition.z > z * FieldSize)
        {
            zPosition = z - 1;
        }
        else
        {
            zPosition = (int)(mousePosition.z / FieldSize);
        }

        return grid[xPosition, zPosition].GetMiddlePoint();
    }
    private class Node
    {
        public FieldGridCoordinate coordinate;
        public int Cost = int.MaxValue;
        public Node Previous = null;

        public Node(int x, int z)
        {
            coordinate = new FieldGridCoordinate(x, z);
        }
    }

}
