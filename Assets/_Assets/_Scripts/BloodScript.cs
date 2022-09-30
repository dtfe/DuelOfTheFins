using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodScript : MonoBehaviour
{
    private Transform WaterLevel;
    private ParticleSystem psSelf;
    // Start is called before the first frame update
    void Start()
    {
        psSelf = GetComponent<ParticleSystem>();
        WaterLevel = FindObjectOfType<ModifierManager>().WaterLevel;
    }

    // Update is called once per frame
    void Update()
    {
        var psMain = psSelf.main;
        if (WaterLevel.position.y < transform.position.y)
        {
            psMain.gravityModifier = 1;
        }
        else
        {
            psMain.gravityModifier = 0;
        }
    }
}
