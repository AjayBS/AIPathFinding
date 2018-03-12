using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    public bool walkable;
    public Vector3 worldPosition;

    public Node(bool i_walkable, Vector3 i_worldPos)
    {
        walkable = i_walkable;
        worldPosition = i_worldPos;
    }
}
