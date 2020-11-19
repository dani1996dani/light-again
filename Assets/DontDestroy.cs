using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    void Start()
    {
        Object[] globalResources = Resources.LoadAll("Prefabs/Global");
        foreach(GameObject resource in globalResources)
        {
            GameObject sceneGameObject = GameObject.FindGameObjectWithTag(resource.tag);
            if (sceneGameObject != null && sceneGameObject.activeSelf)
            {
                DontDestroyOnLoad(sceneGameObject);
            }
        }
    }

    private void OnSceneChange(Scene oldScene, Scene newScene)
    {
        Object[] globalResources = Resources.LoadAll("Prefabs/Global");
        foreach (GameObject resource in globalResources)
        {
            GameObject[] sceneGameObjects = GameObject.FindGameObjectsWithTag(resource.tag);
            if(sceneGameObjects.Length > 1)
            {
                // spare the first gameobject, dont destroy it. We need exactly one of each.
                for(int i = 1; i < sceneGameObjects.Length; i++)
                {
                    Destroy(sceneGameObjects[i]);
                }
                DontDestroyOnLoad(sceneGameObjects[0]);
            }
        }
    }

    void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChange;
    }
}
