using UnityEngine.Audio;
using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    public AudioMixer audioMixer;

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
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.soundType;
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.PlayOneShot(s.source.clip);
    }

    // for keeping track of it
    private float masterVol = 0.5f;
    private float musicVol = 0.5f;
    private float SFXVol = 0.5f;

    public void UpdateVolumeSlider(string mixer, float vol)
    {
        switch (mixer)
        {
            case "Master":
                masterVol = vol;
                break;
            case "Music":
                musicVol = vol;
                break;
            case "SFX":
                SFXVol = vol;
                break;
        }
    }

    public float GetVolume(string mixer)
    {
        switch (mixer)
        {
            case "Master":
                return masterVol;
            case "Music":
                return musicVol;
            case "SFX":
                return SFXVol;
        }

        return 0f;
    }
}
