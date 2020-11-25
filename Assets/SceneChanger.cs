using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class SceneChanger : MonoBehaviour
{
    private Image blackSreenImage;
    private bool shouldFadeIn;
    private bool shouldFadeOut;
    private float fadeInVelocity;
    private float fadeOutVelocity;
    private float neglegableOffset = 0.1f;
    private float fadeInTarget = 0.0f;
    private float fadeOutTarget = 1.0f;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        blackSreenImage = GetComponentInChildren<Image>();
        shouldFadeIn = true;
        shouldFadeOut = false;
        blackSreenImage.color = new Color(0, 0, 0, 1);
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
        if(blackSreenImage == null)
        {
            blackSreenImage = GetComponentInChildren<Image>();
        }

        if (shouldFadeIn)
        {

            float newAlphaValue = Mathf.SmoothDamp(blackSreenImage.color.a, fadeInTarget, ref fadeInVelocity, Settings.SceneFadeTime);
            blackSreenImage.color = new Color(0, 0, 0, newAlphaValue);
            if (Mathf.Abs(newAlphaValue - fadeInTarget) <= neglegableOffset)
            {
                blackSreenImage.color = new Color(0, 0, 0, fadeInTarget);
                shouldFadeIn = false;
            }
        }

        if (shouldFadeOut)
        {

            float newAlphaValue = Mathf.SmoothDamp(blackSreenImage.color.a, fadeOutTarget, ref fadeOutVelocity, Settings.SceneFadeTime);
            blackSreenImage.color = new Color(0, 0, 0, newAlphaValue);
            if (Mathf.Abs(newAlphaValue - neglegableOffset) >= fadeOutTarget)
            {
                blackSreenImage.color = new Color(0, 0, 0, fadeOutTarget);
                shouldFadeOut = false;
            }
        }
    }

    public void GoToLevel(string levelName)
    {
        IEnumerator coroutine = LevelTransfer(levelName);
        StartCoroutine(coroutine);
    }

    public void GoToLevel(int sceneIndex)
    {
        string levelName = SceneNameFromIndex(sceneIndex);
        IEnumerator coroutine = LevelTransfer(levelName);
        StartCoroutine(coroutine);
    }

    private IEnumerator LevelTransfer(string levelName)
    {
        shouldFadeOut = true;
        Settings.isGamePaused = true;
        yield return new WaitForSeconds(Settings.SceneFadeTime * 2);
        SceneManager.LoadScene(levelName);
        Settings.isGamePaused = false;
    }

    private string SceneNameFromIndex(int BuildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }
}
