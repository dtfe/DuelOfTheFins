using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLevelMusic : MonoBehaviour
{
    public string musicName;
    public string[] effectNames;
    public string ambientMusicName;
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<MusicManager>())
        {
            MusicManager.instance.StopMusic();
            MusicManager.instance.PlayMusicByName(musicName);
            if (effectNames.Length > 0)
            {

                MusicManager.instance.PlayEffectsRandomly(effectNames);
            }
            if (ambientMusicName != "")
            {
                MusicManager.instance.PlayAmbientMusicByName(ambientMusicName);
            }
        }
    }
}