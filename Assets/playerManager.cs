using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerManager : MonoBehaviour
{
    public Transform[] spawnlocations;
    private int curPlayerNumber = 0;
    private void OnPlayerJoined(PlayerInput pInput)
    {
        for (int i = 0; i < 1; i++)
        {
            curPlayerNumber++;
            Debug.Log("PlayerInput ID: " + curPlayerNumber);
            transform.position = new Vector3(7, 0, 0);
            pInput.gameObject.GetComponent<Transform>().position = spawnlocations[curPlayerNumber - 1].position;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-7, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
