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


    void Start()
    {
        instance = this;

        List<Sound[]> allSounds = new List<Sound[]>
        {
            PlayerSounds,
            WeaponSounds,
            SchmoveSounds,
            UISounds,
            EnemySounds,
            WorldSounds
        };

        foreach(Sound[] array in allSounds)
        {
            foreach (Sound s in array)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume * SettingsManager.instance.GetVFXVolume();
                s.source.pitch = s.pitch;
            }
        }

        //music
        foreach (Sound s in Music)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume * SettingsManager.instance.GetMusicVolume();
            s.source.pitch = s.pitch;
        }

    }

    public void Play(string _name)
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
                if (s.name == _name)
                {
                    s.source.Play();
                }
            }
        }
    }

    public void Stop(string _name)
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
                if (s.name == _name)
                {
                    s.source.Stop();
                }
            }
        }
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
                s.source.volume = s.volume * SettingsManager.instance.GetVFXVolume();
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
