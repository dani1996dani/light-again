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
    Health entitysHealth;
    [SerializeField]
    private bool isPlayer = false;
    Canvas mainCanvas;

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

        if (isPlayer)
        {
            HPprogressBarImage = GameObject.FindGameObjectWithTag(Settings.TagHPBar).GetComponent<Image>();
        } else
        {
            HPprogressBarImage = GameObject.FindGameObjectWithTag(Settings.TagBossHPBar).GetComponent<Image>();
        }

        mainCanvas = gameObject.GetComponentInChildren<Canvas>();


        if (isLevelScene)
        {
            mainCanvas.enabled = true;
            HPprogressBarImage.enabled = true;
        }
        else
        {
            mainCanvas.enabled = false;
            HPprogressBarImage.enabled = false;
        }

        if (isLevelScene)
        {
            if (isPlayer)
            {
                entitysHealth = GameObject.FindGameObjectWithTag(Settings.TagPlayer).GetComponent<Health>();
            } else
            {
                entitysHealth = GameObject.FindGameObjectWithTag(Settings.TagOwlmanBoss).GetComponent<Health>();
            }
            
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
        int maxAmount;
        if (isPlayer)
        {
            maxAmount = Settings.PlayerMaxHealth;
        } else
        {
            maxAmount = Settings.OwlmanBossMaxHealth;
        }
        int currentProgress = entitysHealth.GetCurrentHealth();
        // number between 0 and 1
        float progressFraction = Mathf.Clamp((float)currentProgress / (float)maxAmount, 0, 1);
        float widthToSet = Settings.progressHPBarWidth * progressFraction;
        HPprogressBarImage.rectTransform.sizeDelta = new Vector2(widthToSet, HPprogressBarImage.rectTransform.sizeDelta.y);
    }
}
