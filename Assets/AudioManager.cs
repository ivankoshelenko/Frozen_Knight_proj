using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioMixerGroup mixer;
    // Start is called before the first frame update
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = false;
            s.source.outputAudioMixerGroup = mixer;
        }
    }
    
    public void Play(string name)
    {
        Sound s = Array.Find<Sound>(sounds, sound => sound.name == name);
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find<Sound>(sounds, sound => sound.name == name);
        s.source.Stop();
    }
    public bool isPlaying(string name)
    {
        Sound s = Array.Find<Sound>(sounds, sound => sound.name == name);
        return s.source.isPlaying;
    }
}
