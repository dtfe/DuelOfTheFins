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
            pInput.gameObject.GetComponent<Transform>().position = spawnlocations[curPlayerNumber - 1].position;
            pInput.gameObject.GetComponent<PlayerController>().playerNumber = curPlayerNumber;
        }
    }
}
