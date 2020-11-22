using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class StarStrikeListener : MonoBehaviour
{

    void OnEnable()
    {
        Publisher.publish.StarStrike += OnStarStrike;
    }

    void OnDisable()
    {
        Publisher.publish.StarStrike -= OnStarStrike;
    }

    private void OnDestroy()
    {
        Publisher.publish.StarStrike -= OnStarStrike;
    }

    void OnStarStrike()
    {
        Debug.Log("STAR STRIKE!!");
    }
}
