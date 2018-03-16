using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour {
    [SerializeField]
    private List<Transform> path;
    private int currentPathIndex = 0;
    private bool allowMovement = false;
    [SerializeField]
    private GameObject player;

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
                if (player.transform.position == this.path[currentPathIndex].transform.position)
                    currentPathIndex++;

                if (currentPathIndex < path.Count)
                    player.transform.position = Vector3.MoveTowards(player.transform.position, path[currentPathIndex].transform.position, Time.deltaTime * 10);
                else
                {                   
                    allowMovement = false;
                    GetComponent<GameManager>().SetStartAndEndNode(path[currentPathIndex - 1]);
                    currentPathIndex = 0;
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
        player.transform.position = path[0].transform.position;
    }
}
