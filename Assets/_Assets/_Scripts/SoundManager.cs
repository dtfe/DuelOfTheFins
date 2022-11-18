using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip glassBreak, bloodkill, pop, dash, pickSword, uiSelection, wahwaterfall, wahspace, shootSw, trashAlert, trashHit, splashwtr, touchHardObject, touchCristal, sandTouch;
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
        trashAlert = Resources.Load<AudioClip>("Audio/warning sound trash");
        trashHit = Resources.Load<AudioClip>("Audio/Player hit by an object");
        splashwtr = Resources.Load<AudioClip>("Audio/Splash");
        touchHardObject = Resources.Load<AudioClip>("Audio/Sword environment");
        touchCristal = Resources.Load<AudioClip>("Audio/hit cristal");
        sandTouch = Resources.Load<AudioClip>("Audio/SandScratch");
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
                ChangeVolume(0.6f);
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
            case "trashFall":
                audioSrc.PlayOneShot(trashAlert);
                break;
            case "trashhitplayer":
                audioSrc.PlayOneShot(trashHit);
                break;
            case "splashingwtr":
                audioSrc.PlayOneShot(splashwtr);
                ChangeVolume(1.0f);
                break;
            case "touchrock_met":
                audioSrc.PlayOneShot(touchHardObject);
                break;
            case "touchglass":
                audioSrc.PlayOneShot(touchCristal);
                break;
            case "touchSand":
                ChangeVolume(2f);
                audioSrc.PlayOneShot(sandTouch);
                break;

        }
    }

}   


