using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager_Cs : MonoBehaviour
{
    public SoundClips[] sounds;

    public void Awake()
    {
        foreach(SoundClips s in sounds)
        {
            s.source= gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    void Start()
    {
        Play("Theme");    
    }

    public void Play(string name)
    {
        SoundClips s=Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not Found!");
            return;
        }
        s.source.Play();
        
    }

}
