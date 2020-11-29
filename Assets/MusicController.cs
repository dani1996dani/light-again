using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Song
{
    MainSong,
    BossFight,
    Harp,
}

public class MusicController : MonoBehaviour
{
    AudioSource audioSource;

    AudioClip mainSong;
    AudioClip bossFight;
    AudioClip harp;

    float fadeSpeed = 0.1f;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        mainSong = (AudioClip)Resources.Load("Audio/main-song");
        bossFight = (AudioClip)Resources.Load("Audio/boss-fight");
        harp = (AudioClip)Resources.Load("Audio/harp");
    }

    void Update()
    {

    }

    public IEnumerator FadeOutCurrentMusic(float fadeLevel = 0.8f, float fadingSpeed = 0.1f)
    {
        while (audioSource.volume > fadeLevel)
        {
            yield return new WaitForSeconds(0.01f);
            audioSource.volume -= fadingSpeed;
        }
    }

    public IEnumerator FadeInCurrentMusic()
    {
        while (audioSource.volume < 1)
        {
            yield return new WaitForSeconds(0.01f);
            audioSource.volume += fadeSpeed;
        }
    }



    public IEnumerator FadeInNewMusic(Song newSong, float fadingSpeed = 0.1f)
    {
        switch (newSong)
        {
            case Song.MainSong:
                audioSource.clip = mainSong;
                audioSource.loop = true;
                break;
            case Song.BossFight:
                audioSource.clip = bossFight;
                audioSource.loop = true;
                break;
            case Song.Harp:
                audioSource.clip = harp;
                audioSource.loop = false;
                break;
            default: break;
        }

        audioSource.Play();

        //yield return null;
        while (audioSource.volume < 1)
        {
            yield return new WaitForSeconds(0.01f);
            audioSource.volume += fadingSpeed;
        }
        //StartCoroutine(FadeInCurrentMusic());

    }
}
