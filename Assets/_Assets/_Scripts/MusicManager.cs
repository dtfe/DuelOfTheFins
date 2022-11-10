using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip mainMenuMusic;

    [SerializeField]
    AudioClip aquariumMusic;

    [SerializeField]
    AudioClip oceanMusic;

    [SerializeField]
    AudioClip waterfallMusic;

    [SerializeField]
    AudioClip spaceMusic;

    //make the instance private so it can't be modified in other scripts
    private static MusicManager _instance; 
    //make the music manager available as read only on the other scripts  

    public static MusicManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MusicManager>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }
    //loading the music manager as a singleton
    void Awake()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
          
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy me!
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

      public void PlayMusic()
    {
        audioSource.Play();
    }

    public void StopMusic()
    { 
        audioSource.Stop();
    }

    public void PlayMusicByName(string name)
    {
        switch (name)
        {
            case "aquarium":
                audioSource.clip = aquariumMusic;
                PlayMusic();
                break;
            case "ocean":
                audioSource.clip = oceanMusic;
                PlayMusic();
                break;
            case "waterfall":
                audioSource.clip = waterfallMusic;
                PlayMusic();
                break;
            case "space":
                audioSource.clip = spaceMusic;
                PlayMusic();
                break;
            case "main_Menu":
                audioSource.clip = mainMenuMusic;
                PlayMusic();
                break;

            default:
                break;

        }
    }
}