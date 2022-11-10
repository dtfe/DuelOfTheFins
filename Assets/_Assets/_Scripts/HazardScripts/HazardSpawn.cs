using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawn : MonoBehaviour
{
    public GameObject hazardGO;
    private hazardObjectScript spawnedHazard;
    public float spawnTimeStatic = 8;
    public bool enableRotationOverTime;
    public bool isHoming = false;

    public float hazardAcceleration = 1;
    public float hazardMaxSpeed = 5;
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
            Vector2 spawnPos = new Vector2(transform.position.x, transform.position.y);
            if (!enableRotationOverTime)
            {
                float y = 0;
                if (Random.Range(0,2) == 0)
                {
                    y = -6f;
                } else { y = 6f; }
                spawnPos = new Vector2(Random.Range(-10, 11), y);
            }
            GameObject hazard = Instantiate(hazardGO, spawnPos, Quaternion.identity);
            spawnedHazard = hazard.GetComponent<hazardObjectScript>();
            spawnedHazard.rotateOverTime = enableRotationOverTime;
            spawnedHazard.isHoming = isHoming;
            spawnedHazard.SetAccelerationAndMaxSpeed(hazardAcceleration, hazardMaxSpeed);
            spawnTimeCurrent = spawnTimeStatic * 2;
        }

    }
}
