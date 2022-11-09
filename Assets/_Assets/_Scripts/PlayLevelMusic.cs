using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLevelMusic : MonoBehaviour
{


    public string musicName;
    // Start is called before the first frame update
    void Start()
    {
        MusicManager.instance.StopMusic();
        MusicManager.instance.PlayMusicByName(musicName);
    }

}
