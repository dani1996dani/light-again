using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class EntryMenu : MonoBehaviour
{

    
    bool isEntryMenu;
    Canvas entryMenuCanvas;
    MoonDustProgress moonDustProgressController;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetMenuStateOnInit();
    }

    private void Start()
    {
        SetMenuStateOnInit();
    }

    void SetMenuStateOnInit()
    {
        Scene currentScene = SceneManager.GetActiveScene();
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

    public void StartGame()
    {
        SceneChanger sceneChanger = GameObject.FindGameObjectWithTag(Settings.TagSceneChanger).GetComponent<SceneChanger>();
        Time.timeScale = 1;
        sceneChanger.GoToLevel(Settings.SceneNameLevel1);
        Settings.isGameActive = true;

        moonDustProgressController = GameObject.FindGameObjectWithTag(Settings.TagGameSettings).GetComponent<MoonDustProgress>();
        moonDustProgressController.ResetMoonDustAmount();
    }
}
