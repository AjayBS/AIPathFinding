using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortestPath : MonoBehaviour
{

    private GameObject[] nodes;
    
    /// <summary>
    /// Finding the shortest path and return in a List
    /// </summary>
    /// <param name="start">The start point</param>
    /// <param name="end">The end point</param>
    /// <returns>A List of transform for the shortest path</returns>
    public List<Transform> findShortestPath(Transform start, Transform end)
    {

        nodes = GameObject.FindGameObjectsWithTag("Node");

        List<Transform> result = new List<Transform>();
        Transform node = AStar(start, end);

        // While there's still previous node, we will continue.
        while (node != null)
        {
            result.Add(node);
            Node currentNode = node.GetComponent<Node>();
            node = currentNode.getParentNode();
        }

        // Reverse the list so that it will be from start to end.
        result.Reverse();
        return result;
    }

    /// <summary>
    /// Dijkstra Algorithm to find the shortest path
    /// </summary>
    /// <param name="start">The start point</param>
    /// <param name="end">The end point</param>
    /// <returns>The end node</returns>
    private Transform DijkstrasAlgo(Transform start, Transform end)
    {
        double startTime = Time.realtimeSinceStartup;

        // Nodes that are unexplored
        List<Transform> unexplored = new List<Transform>();

        // We add all the nodes we found into unexplored.
        foreach (GameObject obj in nodes)
        {
            Node n = obj.GetComponent<Node>();
            if (n.isWalkable())
            {
                n.resetNode();
                unexplored.Add(obj.transform);
            }
        }

        // Set the starting node weight to 0;
        Node startNode = start.GetComponent<Node>();
        startNode.setWeight(0);

        while (unexplored.Count > 0)
        {
            // Sort the explored by their weight in ascending order.
            unexplored.Sort((x, y) => x.GetComponent<Node>().getWeight().CompareTo(y.GetComponent<Node>().getWeight()));

            // Get the lowest weight in unexplored.
            Transform current = unexplored[0];

            // Note: This is used for games, as we just want to reduce compuation, better way will be implementing A*
            /*
            // If we reach the end node, we will stop.
            if(current == end)
            {   
                return end;
            }*/

            //Remove the node, since we are exploring it now.
            unexplored.Remove(current);

            Node currentNode = current.GetComponent<Node>();
            List<Transform> neighbours = currentNode.getNeighbourNode();
            foreach (Transform neighNode in neighbours)
            {
                Node node = neighNode.GetComponent<Node>();

                // We want to avoid those that had been explored and is not walkable.
                if (unexplored.Contains(neighNode) && node.isWalkable())
                {
                    // Get the distance of the object.
                    float distance = Vector3.Distance(neighNode.position, current.position);
                    distance = currentNode.getWeight() + distance;

                    // If the added distance is less than the current weight.
                    if (distance < node.getWeight())
                    {
                        // We update the new distance as weight and update the new path now.
                        node.setWeight(distance);
                        node.setParentNode(current);
                    }
                }
            }

        }

        double endTime = (Time.realtimeSinceStartup - startTime);
        print("Compute time: " + endTime);

        print("Path completed!");

        return end;
    }

    private Transform AStar(Transform start, Transform end)
    {
        double startTime = Time.realtimeSinceStartup;
        List<Transform> openSet = new List<Transform>();
        List<Transform> closedSet = new List<Transform>();
        start.GetComponent<Node>().setWeight(0);
        openSet.Add(start);

        while (openSet.Count > 0)
        {
            Transform current = openSet[0];
            Node currentNode = current.GetComponent<Node>();

            for (int i = 1; i < openSet.Count; i++)
            {
                Node openSetNode = openSet[i].GetComponent<Node>();
                if (openSetNode.getfCost() < currentNode.getfCost() || openSetNode.getfCost() == currentNode.getfCost() && openSetNode.gethCost() < currentNode.gethCost())
                {
                    currentNode = openSetNode;
                }
                // if(openSet[i])
            }

            openSet.Remove(current);
            closedSet.Add(current);

            if (current == end)
            {
                double endTime = (Time.realtimeSinceStartup - startTime);
                print("Compute time: " + endTime);

                print("Path completed!");
                return end;
            }

            foreach (Transform neighbour in currentNode.getNeighbourNode())
            {
                Node neighbourNode = neighbour.GetComponent<Node>();
                if (!neighbourNode.isWalkable() || closedSet.Contains(neighbour))
                    continue;
                float newMovementCostToNeighbour = currentNode.getWeight() + GetDistance(current, neighbour);
                if (newMovementCostToNeighbour < neighbourNode.getWeight() || !openSet.Contains(neighbour))
                {
                    neighbour.GetComponent<Node>().setWeight(newMovementCostToNeighbour);
                    neighbour.GetComponent<Node>().sethCost(GetDistance(neighbour, end));
                    neighbour.GetComponent<Node>().setParentNode(current);

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }

            //for (int i = 1; i < openSet.Count; i++)
            //{
            //    if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
            //        currentNode = openSet[i];
            //}

            //openSet.Remove(currentNode);
            //closedSet.Add(currentNode);

            //if (currentNode == targetNode)
            //{
            //    RetracePath(startNode, targetNode);
            //    return;
            //}

            //foreach (Node neighbour in grid.GetNeighbours(currentNode))
            //{
            //    if (!neighbour.walkable || closedSet.Contains(neighbour))
            //    {
            //        continue;
            //    }

            //    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
            //    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
            //    {
            //        neighbour.gCost = newMovementCostToNeighbour;
            //        neighbour.hCost = GetDistance(neighbour, targetNode);
            //        neighbour.parent = currentNode;

            //        if (!openSet.Contains(neighbour))
            //            openSet.Add(neighbour);
            //    }
            //}


        }

        return end;
    }

    private float GetDistance(Transform currentNode, Transform neighbourNode)
    {
        float distance = Vector3.Distance(currentNode.position, neighbourNode.position);
        return distance;
    }

}
