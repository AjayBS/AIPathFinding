using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour {
    [SerializeField]
    private List<Transform> path;
    private int currentPathIndex = 0;
    private bool allowMovement = false;
    [SerializeField]
    private SnakeTile head;
    [SerializeField]
    private SnakeTile tail;
    Vector3 nextPos;
    [SerializeField]
    private GameObject snakePrefab;

    public int currentSize;
    public int maxSize;

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
                if (head.transform.position == this.path[currentPathIndex].transform.position)
                {
                    currentPathIndex++;
                    
                }
                    

                if (currentPathIndex < path.Count)
                {
                    nextPos = Vector3.MoveTowards(head.transform.position, path[currentPathIndex].transform.position, Time.deltaTime * 10);
                    GameObject temp = (GameObject)Instantiate(snakePrefab, nextPos, transform.rotation);
                    head.SetNext(temp.GetComponent<SnakeTile>());
                    head = temp.GetComponent<SnakeTile>();
                    if(currentSize >= maxSize)
                    {
                        TailFunction();
                    }
                    else
                    {
                        currentSize++;
                    }
                }                   
                else
                {                   
                    allowMovement = false;
                    GetComponent<GameManager>().SetStartAndEndNode(path[currentPathIndex - 1]);
                    currentPathIndex = 0;
                }
                
            }
        }
	}

    public SnakeTile GetHead()
    {
        return head;
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
        head.transform.position = path[0].transform.position;

        maxSize++;
    }

    void TailFunction()
    {
        SnakeTile tempsSnake = tail;
        tail = tail.GetNext();
        tempsSnake.RemoveTail();
    }
}
