using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource natureAudio;
    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioSource effectAudioSource;

    [SerializeField]
    AudioClip mainMenuMusic;

    [SerializeField]
    AudioClip aquariumMusic;
    
    [SerializeField]
    AudioClip aquariumBubbles;

    [SerializeField]
    AudioClip oceanMusic;
    
    [SerializeField]
    AudioClip whaleSing;

    [SerializeField]
    AudioClip caveEnv;

    [SerializeField]
    AudioClip waterfallMusic;
    
    [SerializeField]
    AudioClip birdSing;

    [SerializeField]
    AudioClip spaceMusic;

    [SerializeField]
    AudioClip spaceShip;
    
    [SerializeField]
    AudioClip spaceBubbles;

    [SerializeField]
    AudioClip[] natureSounds;

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
    public void ChangeVolume(float soundLevel)
    {
        audioSource.volume = soundLevel;
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
        ChangeVolume(0.48f);
        switch (name)
        {
            case "aquarium":
                audioSource.clip = aquariumMusic;
                PlayMusic();
                break;
            case "ocean":
                ChangeVolume(0.2f);
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
    public void PlayEffectsRandomly(string[] names)
    {
        this.StartCoroutine(RandomEffectGenerator(names));
    }

    private IEnumerator RandomEffectGenerator(string[] names)
    {
        while (true)
        {
            PlayEffectRandomly(names);
            yield return new WaitForSeconds(3 + 7 * Random.Range(0f, 1f));
        }
    }

    private void PlayEffectRandomly(string[] names)
    {
        var effect = names[Random.Range(0, names.Length - 1)];
     
        
        switch (effect)
        {
            case "aquariumBubbls":
                effectAudioSource.clip = aquariumBubbles;
                effectAudioSource.Play();
                break;

            case "whale":
                effectAudioSource.clip = whaleSing;
                effectAudioSource.Play();
                break;
            case "caveWater":
                effectAudioSource.clip = caveEnv;
                effectAudioSource.Play();
                break;
            case "birds":
                effectAudioSource.clip = birdSing;
                effectAudioSource.Play();
                break;
            case "spacialShip":
                effectAudioSource.clip = spaceShip;
                effectAudioSource.Play();
                break;
            case "spacialbubbl":
                effectAudioSource.clip = spaceBubbles;
                effectAudioSource.Play();
                break;

            default:
                break;

        }
       

    }
    public void PlayAmbientMusicByName(string name )
    {
        switch (name)
        {
            case "waves":
                natureAudio.clip = natureSounds[0];
                natureAudio.Play();
                break;
            case "spacenoise":
                natureAudio.clip = natureSounds[1];
                natureAudio.Play();
                break;
            case "waterFall":
                natureAudio.clip = natureSounds[2];
                natureAudio.Play();
                break;
            case "volcano":
                natureAudio.clip = natureSounds[3];
                natureAudio.Play();
                break;
            case "Aquarium":
                natureAudio.clip = natureSounds[4];
                natureAudio.Play();
                break;
        }       
    }
    public void StopAmbientMusic() { natureAudio.Stop(); }  
}
    
