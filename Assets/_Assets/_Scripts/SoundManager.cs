using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip glassBreak, bloodkill, poop, dash, pickSword, uiSelection;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        glassBreak = Resources.Load<AudioClip>("Audio/GlassCrash1");
        audioSrc = GetComponent<AudioSource>();

        bloodkill = Resources.Load<AudioClip>("Audio/ao");
        
        poop = Resources.Load<AudioClip>("Audio/Explosion2 try");        

        dash = Resources.Load<AudioClip>("Audio/Dash2");

        pickSword = Resources.Load<AudioClip>("Audio/Dash");
        uiSelection = Resources.Load<AudioClip>("Audio/SwordplayUI SELECTION");
        

    }

    // Update is called once per frame
    void Update()
    {
     
    }
    public static void PlaySound(string clip)
    {
        audioSrc.pitch = Random.Range(0.85f, 1.15f);
        switch (clip)
        {
            case "Glass_break":
                audioSrc.PlayOneShot(glassBreak); 
                break;
            case "Bleeding":
                audioSrc.PlayOneShot(bloodkill);
                break;
            case "Explosion":
                audioSrc.PlayOneShot(poop);
                break;
            case "Dashing":
                audioSrc.PlayOneShot(dash);
                break;
            case "GetSword":
                audioSrc.PlayOneShot(pickSword);
                break;
            case "ui_selection":
                audioSrc.PlayOneShot(uiSelection);
                break;

        }
    }

}   


