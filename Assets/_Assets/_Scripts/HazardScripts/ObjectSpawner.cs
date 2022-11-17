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
    public float timerModifier = 1;
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
            curTimer = staticTimer * timerModifier;
            SpawnObject();
            if (timerModifier < 1)
            {
                SpawnObject();
                SpawnObject();
                SpawnObject();
                SpawnObject();
            }
            if (timerModifier < 0.8)
            {
                SpawnObject();
            }
            if (timerModifier < 0.5)
            {
                SpawnObject();
            }
            if (timerModifier < 0.3)
            {
                SpawnObject();
            }
        }
        if (timerModifier > 0.3)
        {
            timerModifier -= Time.deltaTime / 100;
        }
    }

    public void SpawnObject()
    {
        SoundManager.PlaySound("trashFall");
        Vector3 randomPos = new Vector3(Random.Range(-8, 9), 5.5f, 0);
        Quaternion randomRot = new Quaternion(0, 0, Random.rotation.z, Quaternion.identity.w);
        StartCoroutine(SpawnTrash(randomPos, randomRot));
    }

    IEnumerator SpawnTrash(Vector3 ranPos, Quaternion ranRot)
    {
        GameObject spawnedWarning = Instantiate(warning, new Vector3(ranPos.x, 4, 0), Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(0.5f,2));
        Destroy(spawnedWarning);
        GameObject spawnedObject = Instantiate(objectToSpawn[Random.Range(0, objectToSpawn.Length)], ranPos, ranRot);
        while (spawnedObject.transform.position.y > -6)
        {
            yield return new WaitForSeconds(2);
        }
        if(spawnedObject.transform.position.y <= -6)
        {
            Destroy(spawnedObject);
        }
    }
}
