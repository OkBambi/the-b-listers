using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] playerSounds;
    public Sound[] WeaponSounds;
    public Sound[] SchmoveSounds;
    public Sound[] UISounds;
    public Sound[] EnemySounds;
    public Sound[] WorldSounds;


    void Awake()
    {
        instance = this;

        List<Sound[]> allSounds = new List<Sound[]>
        {
            playerSounds,
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
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
            }
        }

    }

    public void Play(string _name)
    {
        List<Sound[]> allSounds = new List<Sound[]>
        {
            playerSounds,
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
            playerSounds,
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
}
