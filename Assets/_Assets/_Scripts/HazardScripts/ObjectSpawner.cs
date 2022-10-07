using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectToSpawn;
    public GameObject warning;
    private float curTimer;
    public float timeToDisappear;
    public float staticTimer = 3;
    // Start is called before the first frame update
    void Start()
    {
        curTimer = staticTimer;
    }

    // Update is called once per frame
    void Update()
    {
        curTimer -= Time.deltaTime;
        if (curTimer <= 0)
        {
            curTimer = staticTimer;
            SpawnObject();
        }
    }

    public void SpawnObject()
    {
        Vector3 randomPos = new Vector3(Random.Range(-8, 9), 5.5f, 0);
        Quaternion randomRot = new Quaternion(0, 0, Random.rotation.z, Quaternion.identity.w);
        StartCoroutine(DeleteTrash(randomPos, randomRot));
    }

    IEnumerator DeleteTrash(Vector3 ranPos, Quaternion ranRot)
    {
        GameObject spawnedWarning = Instantiate(warning, new Vector3(ranPos.x, 4, 0), Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(spawnedWarning);
        GameObject spawnedObject = Instantiate(objectToSpawn[Random.Range(0, objectToSpawn.Length)], ranPos, ranRot);
        yield return new WaitForSeconds(timeToDisappear);
        Destroy(spawnedObject);
    }
}
