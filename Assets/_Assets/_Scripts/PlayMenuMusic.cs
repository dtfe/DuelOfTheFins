using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenuMusic : MonoBehaviour
{
   


    private void Start()
    {
        MusicManager.instance.PlayMusic();
    }

}