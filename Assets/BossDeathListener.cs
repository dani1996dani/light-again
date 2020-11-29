using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class BossDeathListener : MonoBehaviour
{
    GameObject deadBossLightParticles;
    GameObject bossHPCanvas;
    MusicController musicController;

    private void Start()
    {
        deadBossLightParticles = GameObject.FindGameObjectWithTag(Settings.TagDeadBossLightParticles);
        deadBossLightParticles.SetActive(false);

        bossHPCanvas = GameObject.FindGameObjectWithTag(Settings.TagBossHpProgressUI);
        musicController = GameObject.FindGameObjectWithTag(Settings.TagAudioSource).GetComponent<MusicController>();
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
        StartCoroutine(PlayVictorySound());
    }

    IEnumerator HideBossHpCanvas()
    {
        yield return new WaitForSeconds(5.0f);
        bossHPCanvas.SetActive(false);
    }

    

    IEnumerator PlayVictorySound()
    {
        yield return StartCoroutine(musicController.FadeOutCurrentMusic(0, 0.01f));
        StartCoroutine(musicController.FadeInNewMusic(Song.Harp, 0.05f));
    }
}
