using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class CreditsSceneScrollEnd : MonoBehaviour
{
    bool didTransitionToMainMenu = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!didTransitionToMainMenu)
        {
            SceneChanger sceneChanger = GameObject.FindGameObjectWithTag(Settings.TagSceneChanger).GetComponent<SceneChanger>();
            sceneChanger.GoToLevel(0);
            didTransitionToMainMenu = true;
        }
    }
}
