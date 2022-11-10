using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip glassBreak, bloodkill, pop, dash, pickSword, uiSelection, wahwaterfall, wahspace, shootSw;
    static AudioSource audioSrc;

    //public static bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        glassBreak = Resources.Load<AudioClip>("Audio/GlassCrash1");
        audioSrc = GetComponent<AudioSource>();
        bloodkill = Resources.Load<AudioClip>("Audio/ao");        
        pop = Resources.Load<AudioClip>("Audio/Explosion2 try");
        dash = Resources.Load<AudioClip>("Audio/Dash2");
        pickSword = Resources.Load<AudioClip>("Audio/Dash");       
        uiSelection = Resources.Load<AudioClip>("Audio/SwordplayUI SELECTION");
        wahwaterfall = Resources.Load<AudioClip>("Audio/waaaaahriver");
        wahspace = Resources.Load<AudioClip>("Audio/aaaaahspace");
        shootSw = Resources.Load<AudioClip>("Audio/Shoot sword");

    }

    public static void ChangeVolume(float soundLevel)
    {
        audioSrc.volume = soundLevel;
    }

    public static bool IsPlaying()
    {
        return audioSrc.isPlaying;
    }


    public static void PlaySound(string clip, bool pitchSound = true)
    {
        audioSrc.pitch = pitchSound ? Random.Range(0.85f, 1.15f) : 1f;
        
        ChangeVolume(0.7f);
        switch (clip)
        {
            case "Glass_break":
                audioSrc.PlayOneShot(glassBreak); 
                break;
            case "Bleeding":
                audioSrc.PlayOneShot(bloodkill);
                break;
            case "Explosion":
                audioSrc.PlayOneShot(pop);
                break;
            case "Dashing":
                audioSrc.PlayOneShot(dash);
                break;
            case "GetSword":
                audioSrc.PlayOneShot(pickSword);
                break;
            case "ThrowSw":
                audioSrc.PlayOneShot(shootSw);
                break;
            case "ui_selection":
                audioSrc.PlayOneShot(uiSelection);
                break;
            case "screamwaterfall":
                audioSrc.PlayOneShot(wahwaterfall);
                break;
            case "screamspace":
                ChangeVolume(0.3f);
                audioSrc.PlayOneShot(wahspace);
                break;
        }
    }

}   


