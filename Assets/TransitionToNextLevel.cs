using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class TransitionToNextLevel : MonoBehaviour
{
    SceneChanger sceneChanger;

    private void Start()
    {
        sceneChanger = GameObject.FindGameObjectWithTag(Settings.TagSceneChanger).GetComponent<SceneChanger>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == Settings.TagPlayer)
        {
            sceneChanger.GoToLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
