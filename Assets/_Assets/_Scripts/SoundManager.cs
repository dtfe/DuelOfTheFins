using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip glassBreak, bloodkill, poop;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        glassBreak = Resources.Load<AudioClip>("Audio/GlassCrash1");
        audioSrc = GetComponent<AudioSource>();

        bloodkill = Resources.Load<AudioClip>("Audio/Blood");
        
        poop = Resources.Load<AudioClip>("Audio/Explosion2 try");
        Debug.Log(poop);
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    public static void PlaySound(string clip)
    {
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
        }
    }

}   


