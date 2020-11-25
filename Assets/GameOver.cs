
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class GameOver : MonoBehaviour
{
    bool isLevelScene;
    Canvas gameOverCanvas;
    SceneChanger sceneChanger;

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

        gameOverCanvas = GameObject.FindGameObjectWithTag(Settings.TagGameOverMenu).GetComponent<Canvas>();
        gameOverCanvas.enabled = false;

        sceneChanger = GameObject.FindGameObjectWithTag(Settings.TagSceneChanger).GetComponent<SceneChanger>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Settings.isGameActive  && isLevelScene && !gameOverCanvas.enabled)
        {
            gameOverCanvas.enabled = true;
        }
    }

    public void RestartLevel()
    {
        sceneChanger.GoToLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
