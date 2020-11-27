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
    Health playerHealth;

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

        if (isLevelScene)
        {
            playerHealth = GameObject.FindGameObjectWithTag(Settings.TagPlayer).GetComponent<Health>();
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

    private void Update()
    {
        if (!isLevelScene)
        {
            return;
        }

        int maxAmount = Settings.PlayerMaxHealth;
        int currentProgress = playerHealth.GetCurrentHealth();
        // number between 0 and 1
        float progressFraction = Mathf.Clamp((float)currentProgress / (float)maxAmount, 0, 1);
        float widthToSet = Settings.progressHPBarWidth * progressFraction;
        HPprogressBarImage.rectTransform.sizeDelta = new Vector2(widthToSet, HPprogressBarImage.rectTransform.sizeDelta.y);
    }
}
