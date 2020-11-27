using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class BossDeathListener : MonoBehaviour
{
    GameObject deadBossLightParticles;
    GameObject bossHPCanvas;

    private void Start()
    {
        deadBossLightParticles = GameObject.FindGameObjectWithTag(Settings.TagDeadBossLightParticles);
        deadBossLightParticles.SetActive(false);

        bossHPCanvas = GameObject.FindGameObjectWithTag(Settings.TagBossHpProgressUI);
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
        StartCoroutine("HideBossHpCanvas");

    }

    IEnumerator HideBossHpCanvas()
    {
        yield return new WaitForSeconds(5.0f);
        bossHPCanvas.SetActive(false);
    }
}
