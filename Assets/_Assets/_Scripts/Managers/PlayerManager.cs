using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public Transform[] spawnlocations;
    private RoundManager roundManager;
    private ModifierManager modifierManager;
    private int curPlayerNumber = 0;
    private List<GameObject> players;

    private void Awake()
    {
        players = new List<GameObject>();
    }

    private void Start()
    {
        roundManager = FindObjectOfType<RoundManager>();
        modifierManager = FindObjectOfType<ModifierManager>();
    }

    private void OnPlayerJoined(PlayerInput pInput)
    {
        if(curPlayerNumber == spawnlocations.Length)
        {
            return;
        }

        Debug.Log("PlayerInput ID: " + curPlayerNumber);
        pInput.transform.position = spawnlocations[curPlayerNumber].position;
        var playerController = pInput.gameObject;
        players.Add(playerController);
        curPlayerNumber++;

        if (curPlayerNumber == 1)
        {
            pInput.GetComponent<SpriteRenderer>().color = Color.magenta;
        }
        else
        {
            pInput.GetComponent<SpriteRenderer>().color = Color.yellow;
        }

        if (curPlayerNumber == spawnlocations.Length)
        {
            roundManager.StartRound(players);
            modifierManager.ApplyPlayerObjects(players);
        }
    }
}
