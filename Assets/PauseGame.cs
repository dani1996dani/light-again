using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts;

public class PauseGame : MonoBehaviour
{
    
    bool isLevelScene;
    Canvas pauseMenuCanvas;

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

        pauseMenuCanvas = GameObject.FindGameObjectWithTag(Settings.TagPauseMenu).GetComponent<Canvas>();
        pauseMenuCanvas.enabled = false;
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
        if(Settings.isGameActive && Input.GetKeyDown(KeyCode.Escape) && isLevelScene)
        {
            ToggleGamePaused();
        }
    }

    public void ToggleGamePaused()
    {
        Settings.isGamePaused = !Settings.isGamePaused;
        if (Settings.isGamePaused)
        {
            pauseMenuCanvas.enabled = true;
            Time.timeScale = 0;
        } else
        {
            pauseMenuCanvas.enabled = false;
            Time.timeScale = 1;
        }
    }
}
