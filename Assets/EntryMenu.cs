using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class EntryMenu : MonoBehaviour
{
    private SceneChanger sceneChanger;

    Scene currentScene;
    bool isEntryMenu;
    Canvas entryMenuCanvas;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene;
        isEntryMenu = currentScene.name.StartsWith("EntryScreen");

        entryMenuCanvas = GameObject.FindGameObjectWithTag(Settings.TagEntryMenuCanvas).GetComponent<Canvas>();
        if (isEntryMenu)
        {
            entryMenuCanvas.enabled = true;
        }
        else
        {
            entryMenuCanvas.enabled = false;
        }

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        sceneChanger = GameObject.FindGameObjectWithTag(Settings.TagSceneChanger).GetComponent<SceneChanger>();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        sceneChanger.GoToLevel(Settings.SceneNameLevel1);
        Settings.isGameActive = true;
    }
}
