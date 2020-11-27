using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class BossDeathListener : MonoBehaviour
{
    GameObject deadBossLightParticles;

    private void Start()
    {
        deadBossLightParticles = GameObject.FindGameObjectWithTag(Settings.TagDeadBossLightParticles);
        deadBossLightParticles.SetActive(false);
    }

    private void OnEnable()
    {
        Publisher.publish.BossDeath += OnBossDeath;
    }

    private void OnDisable()
    {
        Publisher.publish.BossDeath -= OnBossDeath;
    }

    private void OnBossDeath()
    {
        deadBossLightParticles.SetActive(true);
    }
}
