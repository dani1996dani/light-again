﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum SFXType
{
    jump,
    playerDeath
}

public class SFX
{
    public SFXType type;
    public float minPitch;
    public float maxPitch;
    public AudioClip clip;

    public SFX(SFXType type, AudioClip clip, float minPitch, float maxPitch)
    {
        this.type = type;
        this.clip = clip;
        this.minPitch = minPitch;
        this.maxPitch = maxPitch;
    }
}

public class SFXManager : MonoBehaviour
{
    private AudioSource sfx;
    List<SFX> availableSFX = new List<SFX>();
    System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        sfx = gameObject.GetComponent<AudioSource>();

        AudioClip jumpSFXClip = (AudioClip)Resources.Load("Audio/sound-effects/female-jump");
        SFX jump = new SFX(SFXType.jump, jumpSFXClip, 1.0f, 1.2f);

        AudioClip playerDeathSFXClip = (AudioClip)Resources.Load("Audio/sound-effects/female-death");
        SFX playerDeath = new SFX(SFXType.playerDeath, playerDeathSFXClip, 1.0f, 1.2f);
        availableSFX.Add(jump);
        availableSFX.Add(playerDeath);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySFX(SFXType sfxType)
    {
        SFX sfxToUse = availableSFX.FirstOrDefault((x) => x.type == sfxType);
        AudioClip clipToPlay = sfxToUse.clip;
        sfx.pitch = (float)(random.NextDouble() * (sfxToUse.maxPitch - sfxToUse.minPitch) + sfxToUse.minPitch);
        sfx.PlayOneShot(clipToPlay);
    }
}

