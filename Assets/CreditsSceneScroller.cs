using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class CreditsSceneScroller : MonoBehaviour
{
    [SerializeField]
    private float speed = 50f;

    private void Start()
    {
        //StartCoroutine("GoToMainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currPosition = transform.position;
        transform.position = new Vector3(currPosition.x, currPosition.y + speed * Time.deltaTime, currPosition.z);
    }

    //IEnumerator GoToMainMenu()
    //{
    //    yield return new WaitForSeconds(40.0f);
    //    SceneChanger sceneChanger = GameObject.FindGameObjectWithTag(Settings.TagSceneChanger).GetComponent<SceneChanger>();
    //    sceneChanger.GoToLevel(0);
    //}
}
