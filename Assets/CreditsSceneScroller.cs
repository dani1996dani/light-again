using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsSceneScroller : MonoBehaviour
{
    [SerializeField]
    private float speed = 50f;

    // Update is called once per frame
    void Update()
    {
        Vector3 currPosition = transform.position;
        transform.position = new Vector3(currPosition.x, currPosition.y + speed * Time.deltaTime, currPosition.z);
    }
}
