using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTile : MonoBehaviour {
    public Node parent;
    public Node previous;
    public Node current;
    bool followParent;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FollowParent()
    {
        followParent = true;
    }

    public void SetParent(Node i_parent)
    {
        parent = i_parent;
    }

    public void SetPrevious(Node i_previous)
    {
        previous = i_previous;
    }

    public void SetCurrent(Node i_current)
    {
        current = i_current;
    }

    public void MoveToNext()
    {

    }
}
