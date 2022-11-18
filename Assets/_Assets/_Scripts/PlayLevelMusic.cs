using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLevelMusic : MonoBehaviour
{
    public string musicName;
    public string[] effectNames;
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
        }
    }
}