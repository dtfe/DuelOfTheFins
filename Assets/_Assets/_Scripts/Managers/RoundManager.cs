using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundManager : MonoBehaviour
{
    private PlayerController player1, player2;
    private TextMeshProUGUI p1Counter, p2Counter, winnerTxt;
    public Texture2D purpleScore, yellowScore, emptyScore;
    public GameObject UI, scorePoints, taunt;
    public string[] tauntTexts;
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
        scorePoints = roundCounter.transform.Find("ScorePoints").gameObject;
        StartCoroutine(firstStartUp());
    }

    public void StartRound(List<GameObject> players)
    {
        player1 = players[0].GetComponent<PlayerController>();
        player1.ResetCharacter();
        player2 = players[1].GetComponent<PlayerController>();
        player2.ResetCharacter();
        roundStarted = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (!roundStarted || !player1 || !player2)
        {
            return;
        }

        if (player1.isDead && !player2.isDead && !hasScored && !endGameActive)
        {
            hasScored = true;
            p2Score++;

            if (p2Score == pointsToWin)
            {
                scorePoints.transform.Find("VictoryPoint").GetComponent<RawImage>().texture = yellowScore;
            }
            else
            {
                scorePoints.transform.Find("rightPoint" + p2Score.ToString()).GetComponent<RawImage>().texture = yellowScore;
            }
        }
        else if (!player1.isDead && player2.isDead && !hasScored && !endGameActive)
        {
            hasScored = true;
            p1Score++;
            if (p1Score == pointsToWin)
            {
                scorePoints.transform.Find("VictoryPoint").GetComponent<RawImage>().texture = purpleScore;
            }
            else
            {
                scorePoints.transform.Find("leftPoint" + p1Score.ToString()).GetComponent<RawImage>().texture = purpleScore;
            }
        } else if (player1.isDead && player2.isDead && !hasScored && !endGameActive)
        {
            hasScored = true;
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

    IEnumerator firstStartUp()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(3);
        if (player1 && player2)
        {
            Time.timeScale = 1;
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
        Restart();
        player1.ddol();
        player2.ddol();
        GetComponent<MapRotator>().NextMap();
    }

    public void SpawnTaunt(PlayerController sender)
    {
        Transform target;
        if (player1 == sender)
        {
            target = player2.transform;
        }
        else
        {
            target = player1.transform;
        }
        GameObject tauntGO = Instantiate(taunt);
        tauntGO.GetComponent<tauntScript>().setInsult(tauntTexts[Random.Range(0, tauntTexts.Length + 1)]);
        tauntGO.GetComponent<tauntScript>().setTarget(target);
    }
}
