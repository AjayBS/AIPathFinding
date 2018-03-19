using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTile : MonoBehaviour {
    private SnakeTile next;

    public void SetNext(SnakeTile IN)
    {
        next = IN;
    }

    public SnakeTile GetNext()
    {
        return next;
    }

    public void RemoveTail()
    {
        Destroy(this.gameObject);
    }
	
}
