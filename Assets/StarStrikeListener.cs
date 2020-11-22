using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class StarStrikeListener : MonoBehaviour
{
    GameObject starStrikeWavePrefab;

    private void Start()
    {
        starStrikeWavePrefab = (GameObject)Resources.Load("Prefabs/StarStrikeWave");
    }

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
        Instantiate(starStrikeWavePrefab, transform.position, Quaternion.identity);
    }
}
