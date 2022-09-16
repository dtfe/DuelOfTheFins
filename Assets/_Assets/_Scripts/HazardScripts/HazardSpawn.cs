using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawn : MonoBehaviour
{
    public GameObject pufferfish;
    public float spawnTimeStatic = 8;
    private float spawnTimeCurrent;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimeCurrent = spawnTimeStatic;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimeCurrent -= Time.deltaTime;
        if (spawnTimeCurrent <= 0)
        {
            Instantiate(pufferfish, transform.position, Quaternion.identity);
            spawnTimeCurrent = spawnTimeStatic * 2;
        }

    }
}
