using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour {

    public Sound[] sounds;

    public static SoundEffectManager instance;
    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;

        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            //connects the actual audio source to my class Sound
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        VolumeOn();
        /*
        if (PlayerPrefs.GetInt("AudioSound") == 1)
        {
            MuiscOn();
        }
        if (PlayerPrefs.GetInt("AudioSound") == 0)
        {
            MusicOff();
        }*/

    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Soubnd: " + name + " not found");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void VolumeOn()
    {
        foreach (Sound s in sounds)
        {
            s.volume = .225f;
            s.source.volume = s.volume;
            //PlayerPrefs.SetInt("AudioSound", 1);
        }
    }

    public void VolumeOff()
    {
        //MenuAudio.Music = 0;
        foreach (Sound s in sounds)
        {
            s.volume = 0f;
            s.source.volume = s.volume;
            //PlayerPrefs.SetInt("AudioSound", 0);
        }
    }
}
