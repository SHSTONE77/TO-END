using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//String형식으로 입력받은 이름의 노래를 musicSource에서 재생하도록 시킴 
public class BgmManager : MonoBehaviour
{
    public static BgmManager Instance;

    public Sound[] musicSounds;
    public AudioSource musicSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Intro");
    }


    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if(s == null)
        {
            Debug.Log("Sound not Found");
        }

        else
        {
            musicSource.Stop();
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
}
