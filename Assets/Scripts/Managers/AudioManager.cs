using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] PlayerSounds;
    public Sound[] WeaponSounds;
    public Sound[] SchmoveSounds;
    public Sound[] UISounds;
    public Sound[] EnemySounds;
    public Sound[] WorldSounds;
    public Sound[] Music;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        List<Sound[]> allSounds = new List<Sound[]>
        {
            PlayerSounds,
            WeaponSounds,
            SchmoveSounds,
            UISounds,
            EnemySounds,
            WorldSounds,
        };

        foreach (Sound[] array in allSounds)
        {
            foreach (Sound s in array)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume * SettingsManager.instance.GetSFXVolume();
                s.source.pitch = s.pitch;
                s.source.loop = s.looped;
            }
        }

        //music
        foreach (Sound s in Music)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume * SettingsManager.instance.GetMusicVolume();
            s.source.pitch = s.pitch;
            s.source.loop = s.looped;
        }
    }

    Sound SearchSound(string _name)
    {
        List<Sound[]> allSounds = new List<Sound[]>
        {
            PlayerSounds,
            WeaponSounds,
            SchmoveSounds,
            UISounds,
            EnemySounds,
            WorldSounds,
            Music
        };

        foreach (Sound[] array in allSounds)
        {
            foreach (Sound s in array)
            {
                if (s.name == _name)
                {
                    return s;
                }
            }
        }

        return null;
    }

    public void Play(string _name)
    {
        Sound s = SearchSound(_name);
        if (s != null)
            s.source.Play();
    }

    public void Play(string _name, float _pitch)
    {
        Sound s = SearchSound(_name);
        if (s != null)
        {
            s.source.pitch = _pitch;
            s.source.Play();
        }
    }

    public void Play(string _name, float _pitch, AudioSource _source)
    {
        Sound s = SearchSound(_name);
        if (s != null)
        {
            s.source = _source;
            s.source.pitch = _pitch;
            s.source.Play();
        }
    }

    public void Play(string _name, AudioSource _source)
    {
        Sound s = SearchSound(_name);
        if (s != null)
        {
            s.source = _source;
            s.source.Play();
        }
    }

    public void Stop(string _name)
    {
        Sound s = SearchSound(_name);
        if (s != null)
            s.source.Stop();
    }

    public void UpdateVFXVolume()
    {
        List<Sound[]> allSounds = new List<Sound[]>
        {
            PlayerSounds,
            WeaponSounds,
            SchmoveSounds,
            UISounds,
            EnemySounds,
            WorldSounds
        };

        foreach (Sound[] array in allSounds)
        {
            foreach (Sound s in array)
            {
                s.source.volume = s.volume * SettingsManager.instance.GetSFXVolume();
            }
        }
    }

    public void UpdateMusicVolume()
    {
        foreach (Sound s in Music)
        {
            s.source.volume = s.volume * SettingsManager.instance.GetMusicVolume();
        }
    }
}
