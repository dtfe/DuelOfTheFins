using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    public AudioClip mainMenuMusic;

    [SerializeField]
    AudioClip aquariumMusic;

    [SerializeField]
    AudioClip oceanMusic;

    [SerializeField]
    AudioClip waterfallMusic;

    [SerializeField]
    AudioClip spaceMusic;


    private static MusicManager _instance;

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

    void Awake()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
            //PlayMusic();
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy me!
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        audioSource.clip = mainMenuMusic;
        PlayMusic();
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
                break;
            case "ocean":
                audioSource.clip = oceanMusic;
                break;
            case "waterfall":
                audioSource.clip = waterfallMusic;
                break;
            case "space":
                audioSource.clip = spaceMusic;
                break;


            default:
                audioSource.clip = mainMenuMusic;
                break;

        }
    }
}