using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holeScript : MonoBehaviour
{
    private Transform waterLevel;
    // Start is called before the first frame update
    void Start()
    {
        waterLevel = FindObjectOfType<ModifierManager>().WaterLevel;
        SoundManager.PlaySound("Glass_break");

        Debug.Log("crush");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > waterLevel.position.y)
        {
            
            var psEmission = GetComponent<ParticleSystem>().emission;
            psEmission.rateOverTime = (waterLevel.position.y - transform.position.y) * 10;
        }
    }
}
