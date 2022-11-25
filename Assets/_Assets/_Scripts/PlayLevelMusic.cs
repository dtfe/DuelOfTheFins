using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLevelMusic : MonoBehaviour
{
    public string musicName;
    public string[] effectNames;
    public string ambientMusicName;
    public int minWaitSoundEffect = 4;
    public int maxWaitSoundEffect = 6;
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<MusicManager>())
        {
            MusicManager.instance.StopMusic();
            MusicManager.instance.StopAmbientMusic();
            MusicManager.instance.PlayMusicByName(musicName);
            MusicManager.instance.StopEffectRandomly();
            if (effectNames.Length > 0)
            {
                MusicManager.instance.PlayEffectsRandomly(effectNames, minWaitSoundEffect, maxWaitSoundEffect);
            }
            if (ambientMusicName != "")
            {
                MusicManager.instance.PlayAmbientMusicByName(ambientMusicName);
            }
        }
    }
}