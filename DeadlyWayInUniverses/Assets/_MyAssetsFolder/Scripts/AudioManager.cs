using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource effectsSource;
    public AudioClip[] musicClips;
    public AudioClip[] effectClips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        } 
    }

    public void PlayMusic(string name)
    {
        AudioClip clip = Array.Find(musicClips, musicClip => musicClip.name == name);
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Music clip {name} not found!");
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void PlayEffect(string name)
    {
        AudioClip clip = Array.Find(effectClips, effectClip => effectClip.name == name);
        if (clip != null)
        {
            effectsSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Effect clip {name} not found!");
        }
    }

    public void SetEffectsVolume(float volume)
    {
        effectsSource.volume = volume;
    }
}
