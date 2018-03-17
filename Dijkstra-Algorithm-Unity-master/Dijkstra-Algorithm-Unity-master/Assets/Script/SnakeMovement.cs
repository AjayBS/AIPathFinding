using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour {
    [SerializeField]
    private List<Transform> path;
    private int currentPathIndex = 0;
    private bool allowMovement = false;
    private Node currentNode;

    private Node previousNode;

    private float secondsSpent = 2.0f;

    float startTime;

    // Use this for initialization
    void Start () {
        if (path == null)
        {
            path = new List<Transform>();
        }        
    }
	
	// Update is called once per frame
	void Update () {
		if(allowMovement)
        {
            if(this.path.Count > 0)
            {           
               if (Time.time - startTime > 0.52)
               {
                    currentPathIndex++;
                    if (currentPathIndex < path.Count)
                    {
                        currentNode = this.path[currentPathIndex].GetComponent<Node>();
                        Renderer rend = currentNode.GetComponent<Renderer>();
                        rend.material.color = Color.yellow;
                        startTime = Time.time;

                        Node previousNode = this.path[currentPathIndex - 1].GetComponent<Node>();
                        Renderer prevRend = previousNode.GetComponent<Renderer>();
                        prevRend.material.color = Color.white;
                    }
                    else
                    {
                        allowMovement = false;
                        previousNode = currentNode;
                        GetComponent<GameManager>().SetStartAndEndNode(path[currentPathIndex - 1]);
                        currentPathIndex = 0;
                    }
                        
               }
            }
        }
	}

    public void SetPath(List<Transform> i_path)
    {
        this.path.Clear();
        foreach(Transform p in i_path)
        {
            this.path.Add(p);
        }
       
        currentPathIndex = 0;
        allowMovement = true;
        startTime = Time.time;
        currentNode = this.path[0].GetComponent<Node>();
        Renderer rend = currentNode.GetComponent<Renderer>();
        rend.material.color = Color.yellow;

    }
}
