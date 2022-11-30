using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class probeSoundScript : MonoBehaviour
{
    private bool justPlayed = false;

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.z < 359 && !justPlayed || transform.rotation.z > -1 && !justPlayed)
        {
            Debug.Log("Playing Probe Sounds");
            MusicManager.instance.PlayEffectNow("spacialShip");
            justPlayed = true;
        }
        else if (transform.rotation.z > 359 || transform.rotation.z < -1)
        {
            justPlayed = false;
        }
    }
}
