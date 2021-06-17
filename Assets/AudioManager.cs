using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private string[] shanties = { "shanties-01", "shanties-02", "shanties-03", "shanties-04", "shanties-05" };
    private float[] len = new float[5];

    int curSong;
    float curTime;

    System.Random rand = new System.Random();
    void Awake()
    {
        int i = 0;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.vol;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            if (SceneManager.GetActiveScene().name == "Main Menu")
            {
                len[i] = s.source.clip.length;
                i++;
            }
        }
    }

    void Start() 
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            curSong = rand.Next(0, 5);
            curTime = len[curSong];

            Play(shanties[curSong]);
        }
        else 
        {
            Play("main theme");
            Play("water");
        }
    }

    void Update() 
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            if (curTime <= 0)
            {
                if (curSong == 4)
                {
                    curSong = 0;
                }
                else
                {
                    curSong += 1;
                }

                curTime = len[curSong];
                Play(shanties[curSong]);
            }

            curTime -= Time.deltaTime;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name) 
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
