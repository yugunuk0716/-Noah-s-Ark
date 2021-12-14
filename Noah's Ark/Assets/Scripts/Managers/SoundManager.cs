using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioSource bgmSource;

    [Header("bgm")]
    public AudioClip ingameBgm;

    //[Header("sfx")]
    //

    private void Start()
    {
        PlayBgmSound(ingameBgm);
    }

    public void PlayBgmSound(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }
    public void ChangeBgmVolume(float volume)
    {
        bgmSource.volume = volume;
    }
}
