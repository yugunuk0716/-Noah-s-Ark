using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("bgm")]
    public AudioClip ingameBgm;

    //[Header("sfx")]
    //

    
    public void PlayBgmSound(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }
    public void PlaySfxSound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void ChangeBgmVolume(float volume)
    {
        bgmSource.volume = volume;
    }
    public void ChangeSfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
