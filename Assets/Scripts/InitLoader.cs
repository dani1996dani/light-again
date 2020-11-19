﻿using UnityEngine;

public class InitLoader
{

    [RuntimeInitializeOnLoadMethod]
    static void LoadGlobalObjects()
    {
        Object[] globalResources = Resources.LoadAll("Prefabs/Global");

        foreach(GameObject resource in globalResources)
        {
            GameObject sceneGameObject = GameObject.FindGameObjectWithTag(resource.tag);
            if (sceneGameObject == null || !sceneGameObject.activeSelf)
            {
                GameObject.Instantiate(resource, new Vector3(0, 0, -5), Quaternion.identity);
            }
        }
    }
}
