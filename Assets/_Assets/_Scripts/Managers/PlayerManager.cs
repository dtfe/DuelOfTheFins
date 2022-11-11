using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public Transform[] spawnlocations;
    [SerializeField]
    private RoundManager roundManager;
    [SerializeField]
    private ModifierManager modifierManager;
    private int curPlayerNumber = 0;
    public List<GameObject> players;
    private List<PlayerManager> PMs;
    private int pmID;
    public bool hasSpawned;

    private void Awake()
    {
        PMs = new List<PlayerManager>();
        PMs.AddRange(FindObjectsOfType<PlayerManager>());
        if (PMs.Count == 2)
        {
            for (int i = 0; i < PMs.Count; i++)
            {
                if (PMs[i] != this)
                {
                    PMs[i].transform.Find("Spawn0").transform.position = transform.Find("Spawn0").transform.position;
                    PMs[i].transform.Find("Spawn1").transform.position = transform.Find("Spawn1").transform.position;
                    PMs[i].startMap();
                }
            }
            Destroy(gameObject);
        }
        players = new List<GameObject>();
    }

    private void Start()
    {
        roundManager = FindObjectOfType<RoundManager>();
        modifierManager = FindObjectOfType<ModifierManager>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.O) !& FindObjectsOfType<PlayerController>().Length == 1)
        {
            GameObject dummyPlayer = Instantiate(GetComponent<PlayerInputManager>().playerPrefab, spawnlocations[1].position, Quaternion.identity);
            dummyPlayer.GetComponent<PlayerController>().isDummy = true;
        }
    }

    private void OnPlayerJoined(PlayerInput pInput)
    {
        Debug.Log("PlayerInput ID: " + curPlayerNumber);
        pInput.transform.position = spawnlocations[curPlayerNumber].position;
        var playerController = pInput.gameObject;
        players.Add(playerController);
        curPlayerNumber++;

        if (curPlayerNumber == 1)
        {
            if (pInput.transform.Find("Sprite"))
            {
                pInput.transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.magenta; //Just to spite TO
            }
        }
        else
        {
            if (pInput.transform.Find("Sprite"))
            {
                pInput.transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.yellow;
            }
        }

        if (curPlayerNumber == spawnlocations.Length)
        {
            roundManager.StartRound(players);
            modifierManager.ApplyPlayerObjects(players);
        }
    }

    public void startMap()
    {
        StartCoroutine(newMap());
    }

    public IEnumerator newMap()
    {
        Debug.Log("NewMap activated");
        players[0].GetComponent<PlayerController>().ResetCharacter();
        players[1].GetComponent<PlayerController>().ResetCharacter();
        roundManager = FindObjectOfType<RoundManager>();
        modifierManager = FindObjectOfType<ModifierManager>();
        roundManager.StartRound(players);
        modifierManager.ApplyPlayerObjects(players);
        yield return new WaitForSeconds(0);
    }
}
