using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject player;
    private Transform startNode;
    private Transform endNode;

    private GameObject[] nodes;
    public GameObject fruit;
   

    // Use this for initialization
    void Start () {        
        
           
    }

  

    // Update is called once per frame
    void Update () {
        
    }

    void ChooseRandomStartNode()
    {
        int maxSize = nodes.Length;
        int randomNumber = UnityEngine.Random.Range(0, maxSize);
        startNode = nodes[randomNumber].transform;
    }

    void ChooseRandomEndNode()
    {
        int maxSize = nodes.Length;
        int randomNumber = UnityEngine.Random.Range(0, maxSize);
        endNode = nodes[randomNumber].transform;
        fruit.transform.position = endNode.position;
        while (startNode == endNode)
        {
            randomNumber = UnityEngine.Random.Range(0, maxSize);
            endNode = nodes[randomNumber].transform;
            fruit.transform.position = endNode.position;
        }
       
    }

    public void AfterGridGenerated()
    {
        nodes = GameObject.FindGameObjectsWithTag("Node");
        ChooseRandomStartNode();
        ChooseRandomEndNode();
        FindPath();
    }

    internal void SetStartAndEndNode(Transform i_node)
    {
        startNode = i_node;
        SetInactiveNodes();
        ChooseRandomEndNode();
        FindPath();
    }

    private void SetInactiveNodes()
    {
        GetComponent<GenerateGrid>().SetAllAsActive();
        SnakeTile currentHead =  GetComponent<SnakeMovement>().GetHead();
        while(currentHead != null)
        {
            GetComponent<GenerateGrid>().SetInactiveNode(currentHead.transform.position);
            currentHead = currentHead.GetNext();
        }
    }

    private void FindPath()
    {
        // Execute Shortest Path.
        ShortestPath finder = gameObject.GetComponent<ShortestPath>();
        List<Transform> paths = finder.findShortestPath(startNode, endNode);

       

        GetComponent<SnakeMovement>().SetPath(paths);
        // Colour the node red.
        foreach (Transform path in paths)
        {            
            path.GetComponent<Node>().setWeight(int.MaxValue);
            path.GetComponent<Node>().sethCost(0);
            path.GetComponent<Node>().resetNode();
        }
    }

   
}
