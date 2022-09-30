using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : MonoBehaviour
{
    public float waterLevel = 4;
    public List<Transform> hole;
    public Vector3 sinkTo;
    // Start is called before the first frame update
    void Start()
    {
        sinkTo = new Vector3(0, waterLevel, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Sink(sinkTo);
    }

    public void NewHole(Transform newHolePos)
    {
        hole.Add(newHolePos);
        for (int i = 0; i < hole.Count; i++)
        {
            if (hole[i].position.y < sinkTo.y)
            {
                sinkTo = new Vector3(0, hole[i].position.y, 0);
            }
        }
    }

    public void Sink(Vector3 lowerTo)
    {
        if (transform.position.y > lowerTo.y)
        {
            transform.position -= Vector3.up * Time.deltaTime;
        }
    }

    public void Restart()
    {
        Debug.Log("Restarting WaterLevel");
        hole.Clear();
        sinkTo = new Vector3(0, waterLevel, 0);
        transform.position = sinkTo;
    }
}
