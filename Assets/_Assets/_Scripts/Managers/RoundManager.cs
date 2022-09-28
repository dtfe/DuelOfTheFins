using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{
    private PlayerController player1, player2;
    private TextMeshProUGUI p1Counter, p2Counter, winnerTxt;
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
        GameObject roundCounter = FindObjectOfType<Canvas>().gameObject.transform.Find("RoundCounter").gameObject;
        winnerTxt = FindObjectOfType<Canvas>().transform.Find("WinnerText").GetComponent<TextMeshProUGUI>();
        p1Counter = roundCounter.transform.Find("Player1Counter").gameObject.GetComponent<TextMeshProUGUI>();
        p2Counter = roundCounter.transform.Find("Player2Counter").gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void StartRound(List<PlayerController> players)
    {
        player1 = players[0].GetComponent<PlayerController>();
        player2 = players[1].GetComponent<PlayerController>();
        roundStarted = true;
        Time.timeScale = 1;
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
        }
        else if (!player1.isDead && player2.isDead && !hasScored && !endGameActive)
        {
            hasScored = true;
            p1Score++;
            p1Counter.text = p1Score.ToString();
        }
        if (p1Score == 5 || p2Score == 5)
        {
            gameOver = true;
            winnerTxt.gameObject.SetActive(true);
            if (p1Score == 5)
            {
                winnerTxt.text = "Player 1 Wins!";
            }else if (p2Score == 5)
            {
                winnerTxt.text = "Player 2 Wins!";
            }
        }
        if (hasScored && !endGameActive && !gameOver)
        {
            endGameActive = true;
            StartCoroutine(endGame());
        }
    }

    IEnumerator endGame()
    {
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        yield return new WaitForSeconds(2);
        hasScored = false;
        Restart();
    }
    private void Restart()
    {
        player1.ResetCharacter();
        player2.ResetCharacter();
        cleanUp = GameObject.FindGameObjectsWithTag("Deleteable");
        for (int i = 0; i < cleanUp.Length; i++)
        {
            Destroy(cleanUp[i].gameObject);
        }
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        endGameActive = false;
    }
}
