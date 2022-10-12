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

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.O))
        {
            GameObject dummyPlayer = Instantiate(GetComponent<PlayerInputManager>().playerPrefab, spawnlocations[1].position, Quaternion.identity);
            dummyPlayer.GetComponent<PlayerController>().isDummy = true;
        }
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
            pInput.transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.magenta;
        }
        else
        {
            pInput.transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.yellow;
        }

        if (curPlayerNumber == spawnlocations.Length)
        {
            roundManager.StartRound(players);
            modifierManager.ApplyPlayerObjects(players);
        }
    }
}
