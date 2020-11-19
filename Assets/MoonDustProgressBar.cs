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
    MoonDustProgress moonDustProgressController;
    private int maxWidth = 180;

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
        moonDustProgressController = GameObject.FindGameObjectWithTag(Settings.TagGameSettings).GetComponent<MoonDustProgress>();
    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        if (!isLevelScene)
        {
            return;
        }

        int maxAmount = Settings.MoonDustMaxAmount;
        int currentProgress = moonDustProgressController.GetCurrentAmount();
        // number between 0 and 1
        float progressFraction = (float)currentProgress / (float)maxAmount;
        float widthToSet = maxWidth * progressFraction;
        MoonDustProgressBarImage.rectTransform.sizeDelta = new Vector2(widthToSet, MoonDustProgressBarImage.rectTransform.sizeDelta.y);
    }
}
