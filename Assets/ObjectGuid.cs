using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGuid : MonoBehaviour
{
    int guid;
    
    void Start()
    {
        guid = Guid.NewGuid().GetHashCode();
    }

    public int GetGuid()
    {
        return this.guid;
    }
}
