using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class CreditsSceneScroller : MonoBehaviour
{
    [SerializeField]
    private float speed = 50f;

    void Update()
    {
        Vector3 currPosition = transform.position;
        transform.position = new Vector3(currPosition.x, currPosition.y + speed * Time.deltaTime, currPosition.z);
    }
}
