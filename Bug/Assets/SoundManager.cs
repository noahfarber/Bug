using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip MainTheme;
    public AudioClip ExitTheme;

    private void Awake()
    {
        AudioSingleton.Instance.Register(this);
    }

}

public class AudioSingleton : Singleton<AudioSingleton>
{
    public SoundManager SoundManager;

    public void Register(SoundManager sound)
    {
        SoundManager = sound;
    }

    public void PlayExitTheme()
    {
        SoundManager.Source.Stop();
        SoundManager.Source.clip = SoundManager.ExitTheme;
        SoundManager.Source.Play();
    }

    public void PlayMainTheme()
    {
        SoundManager.Source.Stop();
        SoundManager.Source.clip = SoundManager.MainTheme;
        SoundManager.Source.Play();
    }
}
