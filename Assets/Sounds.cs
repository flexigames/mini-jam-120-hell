using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public static Sounds instance;

    public AudioSource audioSource;

    public Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();

    void Start()
    {
        var sounds = Resources.LoadAll("Sounds", typeof(AudioClip));

        for (int i = 0; i < sounds.Length; i++)
        {
            clips.Add(sounds[i].name, (AudioClip)sounds[i]);
        }

        instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    public static void Play(string name)
    {
        if (instance != null)
        {
            instance.audioSource.PlayOneShot(instance.clips[name]);
        }
    }
}
