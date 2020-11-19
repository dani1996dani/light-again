using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class HPProgressBar : MonoBehaviour
{
    Image HPprogressBarImage;
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

        HPprogressBarImage = GameObject.FindGameObjectWithTag(Settings.TagHPBar).GetComponent<Image>();
        if (isLevelScene)
        {
            HPprogressBarImage.enabled = true;
        }
        else
        {
            HPprogressBarImage.enabled = false;
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
