using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class MoonDustProgressBar : MonoBehaviour
{
    Image MoonDustProgressBarImage;
    bool isLevelScene;

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
        isLevelScene = currentScene.name.StartsWith("Level");

        MoonDustProgressBarImage = GameObject.FindGameObjectWithTag(Settings.TagMoonDustBar).GetComponent<Image>();
        if (isLevelScene)
        {
            MoonDustProgressBarImage.enabled = true;
        }
        else
        {
            MoonDustProgressBarImage.enabled = false;
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
}
