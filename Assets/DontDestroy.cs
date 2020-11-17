using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GameObject sceneChanger = GameObject.FindGameObjectWithTag(Settings.TagSceneChanger);
        DontDestroyOnLoad(sceneChanger);
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag(Settings.TagPauseMenu));
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag(Settings.TagUIEventSystem));
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag(Settings.TagEntryMenuCanvas));
    }
}
