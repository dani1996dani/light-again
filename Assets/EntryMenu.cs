using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class EntryMenu : MonoBehaviour
{
    private SceneChanger sceneChanger;

    private void Awake()
    {
        sceneChanger = GameObject.FindGameObjectWithTag(Settings.TagSceneChanger).GetComponent<SceneChanger>();
    }

    public void StartGame()
    {
        sceneChanger.GoToLevel(Settings.SceneNameLevel1);
    }
}
