using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCharacter
{
    public void MirrorGameObjectBasedOnDirection(GameObject receivedGameObject, Vector3 direction)
    {
        if (direction.x > 0 && receivedGameObject.transform.localScale.x != 1)
        {
            receivedGameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        if (direction.x < 0 && receivedGameObject.transform.localScale.x != -1)
        {
            receivedGameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
