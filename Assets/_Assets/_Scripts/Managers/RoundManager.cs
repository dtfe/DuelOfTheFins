using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{
    private PlayerController player1, player2;
    private TextMeshProUGUI p1Counter, p2Counter, winnerTxt;
    public GameObject UI;
    public int pointsToWin;
    private int p1Score = 0, p2Score = 0;
    private bool roundStarted = false;
    private bool hasScored = false;
    private bool endGameActive = false;
    private bool gameOver;
    private GameObject[] cleanUp;
    //private PlayerController player1, player2;

    private void Awake()
    {
        Time.timeScale = 0;
    }


    // Start is called before the first frame update
    void Start()
    {
        GameObject roundCounter = UI.transform.Find("GameOverlay").transform.Find("RoundCounter").gameObject;
        winnerTxt = UI.transform.Find("GameOverlay").transform.Find("WinnerText").GetComponent<TextMeshProUGUI>();
        p1Counter = roundCounter.transform.Find("Player1Counter").gameObject.GetComponent<TextMeshProUGUI>();
        p2Counter = roundCounter.transform.Find("Player2Counter").gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void StartRound(List<GameObject> players)
    {
        player1 = players[0].GetComponent<PlayerController>();
        player1.ResetCharacter();
        player2 = players[1].GetComponent<PlayerController>();
        player2.ResetCharacter();
        Time.timeScale = 1;
        roundStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!roundStarted || !player1 || !player2)
        {
            return;
        }

        if (player1.isDead && !player2.isDead && !hasScored && !endGameActive)
        {
            hasScored = true;
            p2Score++;
            p2Counter.text = p2Score.ToString();
            p2Counter.color = Color.cyan;
        }
        else if (!player1.isDead && player2.isDead && !hasScored && !endGameActive)
        {
            hasScored = true;
            p1Score++;
            p1Counter.text = p1Score.ToString();
            p1Counter.color = Color.cyan;
        } else if (player1.isDead && player2.isDead && !hasScored && !endGameActive)
        {
            hasScored = true;
            p1Counter.color = Color.cyan;
            p2Counter.color = Color.cyan;
        }
        if (p1Score == pointsToWin || p2Score == pointsToWin)
        {
            gameOver = true;
            winnerTxt.gameObject.SetActive(true);
            if (p1Score == pointsToWin)
            {
                winnerTxt.text = "Player 1 Wins!";
            }
            else if (p2Score == pointsToWin)
            {
                winnerTxt.text = "Player 2 Wins!";
            }
            StartCoroutine(NextMap());
        }
        if (hasScored && !endGameActive && !gameOver)
        {
            endGameActive = true;
            StartCoroutine(endGame());
        }
    }

    IEnumerator endGame()
    {
        yield return new WaitForSeconds(2);
        hasScored = false;
        Restart();
        p1Counter.color = Color.magenta;
        p2Counter.color = Color.yellow;
    }
    private void Restart()
    {
        player1.transform.parent = null;
        player2.transform.parent = null;
        player1.ResetCharacter();
        player2.ResetCharacter();
        cleanUp = GameObject.FindGameObjectsWithTag("Deleteable");
        for (int i = 0; i < cleanUp.Length; i++)
        {
            Destroy(cleanUp[i].gameObject);
        }
        GetComponent<ModifierManager>().ApplyModifiers();
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        endGameActive = false;
        if (FindObjectOfType<WaterLevel>())
        {
            Debug.Log("Found WaterLevel");
            FindObjectOfType<WaterLevel>().GetComponent<WaterLevel>().Restart();
        }
    }

    IEnumerator NextMap()
    {
        yield return new WaitForSeconds(5);
        //FindObjectOfType<PlayerManager>().newMap();
        GetComponent<MapRotator>().NextMap();
    }
}
