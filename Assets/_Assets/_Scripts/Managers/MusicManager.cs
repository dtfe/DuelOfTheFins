using System.Collections;
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
    AudioClip waterfallMusicBox;

    [SerializeField]
    AudioClip birdSing;

    [SerializeField]
    AudioClip spaceMusic;

    [SerializeField]
    AudioClip spaceShip;
    
    [SerializeField]
    AudioClip spaceBubbles;
   
    [SerializeField]
    AudioClip fire;

    [SerializeField]
    AudioClip musicBox;

    [SerializeField]
    AudioClip horror;

    [SerializeField]
    AudioClip geiger;

    [SerializeField]
    AudioClip nuclearAlarm;
    
    [SerializeField]
    AudioClip ghostSong;

    [SerializeField]
    AudioClip ghostSound;

    [SerializeField]
    AudioClip Dolphin;

    [SerializeField]
    AudioClip Cat;

    [SerializeField]
    AudioClip doorBell;

    [SerializeField]
    AudioClip toothBrush;

    [SerializeField]
    AudioClip walk;

    [SerializeField]
    AudioClip seal;

    [SerializeField]
    AudioClip[] natureSounds;


    // keep a copy of the executing script
    private IEnumerator coroutine;
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
    
    public void ChangeVolumeForSoundEffect(float soundLevel)
    {
        effectAudioSource.volume = soundLevel;
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
                ChangeVolume(0.25f);
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
            case "waterfallBox":
                audioSource.clip = waterfallMusicBox;
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
    public void PlayEffectsRandomly(string[] names, int minWaitSoundEffect = 10, int maxWaitSoundEffect = 16)
    {
        coroutine = RandomEffectGenerator(names);
        this.StartCoroutine(coroutine);
    }

    private IEnumerator RandomEffectGenerator(string[] names, int minWaitSoundEffect = 10, int maxWaitSoundEffect = 16)
    {
        while (true)
        {
            PlayEffectRandomly(names);
            yield return new WaitForSeconds(Random.Range(minWaitSoundEffect, maxWaitSoundEffect));
        }
    }

    public void PlayEffectNow(string name)
    {
        var effect = name;
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
            case "FireSound":
                effectAudioSource.clip = fire;
                effectAudioSource.Play();
                break;
            case "CreepyMusicBox":
                effectAudioSource.clip = musicBox;
                effectAudioSource.Play();
                break;
            case "HorrorScream":
                effectAudioSource.clip = horror;
                effectAudioSource.Play();
                break;
            case "Geigercount":
                effectAudioSource.clip = geiger;
                effectAudioSource.Play();
                break;
            case "Nuc_Alarm":
                effectAudioSource.clip = nuclearAlarm;
                effectAudioSource.Play();
                break;
            case "Ghostsinging":
                effectAudioSource.clip = ghostSong;
                effectAudioSource.Play();
                break;
            case "Ghost":
                effectAudioSource.clip = ghostSound;
                effectAudioSource.Play();
                break;
            case "dolphinsound":
                effectAudioSource.clip = Dolphin;
                effectAudioSource.Play();
                break;
            case "kitty":
                effectAudioSource.clip = Cat;
                effectAudioSource.Play();
                break;
            case "door_Bell":
                ChangeVolumeForSoundEffect(0.5f);
                effectAudioSource.clip = doorBell;
                effectAudioSource.Play();
                break;
            case "brush":
                effectAudioSource.clip = toothBrush;
                effectAudioSource.Play();
                break;
            case "house_walk":
                effectAudioSource.clip = walk;
                effectAudioSource.Play();
                break;
            case "seal_sound":
                effectAudioSource.clip = seal;
                effectAudioSource.Play();
                break;

            default:
                break;

        }
    }

    private void PlayEffectRandomly(string[] names)
    {
        var effect = names[Random.Range(0, names.Length)];
        ChangeVolumeForSoundEffect(1.0f);
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
            case "FireSound":
                effectAudioSource.clip = fire;
                effectAudioSource.Play();
                break;
            case "CreepyMusicBox":
                effectAudioSource.clip = musicBox;
                effectAudioSource.Play();
                break;
            case "HorrorScream":
                effectAudioSource.clip = horror;
                effectAudioSource.Play();
                break;
            case "Geigercount":
                effectAudioSource.clip = geiger;
                effectAudioSource.Play();
                break;
            case "Nuc_Alarm":
                effectAudioSource.clip = nuclearAlarm;
                effectAudioSource.Play();
                break;
            case "Ghostsinging":
                effectAudioSource.clip = ghostSong;
                effectAudioSource.Play();
                break;
            case "Ghost":
                effectAudioSource.clip = ghostSound;
                effectAudioSource.Play();
                break;         
            case "dolphinsound":
                effectAudioSource.clip = Dolphin;
                effectAudioSource.Play();
                break;
            case "kitty":
                effectAudioSource.clip = Cat;
                effectAudioSource.Play();
                break;
            case "door_Bell":
                ChangeVolumeForSoundEffect(0.5f);
                effectAudioSource.clip = doorBell;
                effectAudioSource.Play();
                break;
            case "brush":
                effectAudioSource.clip = toothBrush;
                effectAudioSource.Play();
                break;
            case "house_walk":
                effectAudioSource.clip = walk;
                effectAudioSource.Play();
                break;
            case "seal_sound":
                effectAudioSource.clip = seal;
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

    public void StopEffectRandomly() {
        if (coroutine != null ) {
            effectAudioSource.Stop();
            this.StopCoroutine(coroutine);
        }
    }
}
    
