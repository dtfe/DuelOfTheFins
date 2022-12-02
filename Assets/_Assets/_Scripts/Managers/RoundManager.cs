using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Manages the round and detects which player dies giving the point to the surviving player as well as manages which player should have a taunt appear. Also controls the countdown timer and victory screen.
public class RoundManager : MonoBehaviour
{
    private cameraController2 cam;
    private PlayerController player1, player2;
    private PauseMenu pauseMenu;
    public Texture2D purpleScore, yellowScore, emptyScore, player1Banner, player2Banner;
    public GameObject UI, scorePoints, victoryScreen, countDownTimer, taunt;
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
        pauseMenu = FindObjectOfType<PauseMenu>();
        cam = FindObjectOfType<cameraController2>();
        GameObject roundCounter = UI.transform.Find("GameOverlay").transform.Find("RoundCounter").gameObject;
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
        if (p1Score == pointsToWin && UI.activeInHierarchy || p2Score == pointsToWin && UI.activeInHierarchy)
        {
            gameOver = true;
            GameObject vs = Instantiate(victoryScreen);
            vs.GetComponent<Canvas>().worldCamera = FindObjectOfType<cameraController2>().GetComponent<Camera>();
            
            if (p1Score == pointsToWin)
            {
                vs.GetComponentInChildren<RawImage>().texture = player1Banner;
                cam.SetTarget(player1.transform);
            }
            else if (p2Score == pointsToWin)
            {
                vs.GetComponentInChildren<RawImage>().texture = player2Banner;
                cam.SetTarget(player2.transform);
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
        pauseMenu.canPause(false);
        Time.timeScale = 0;
        GameObject cdTimerGO = Instantiate(countDownTimer);
        yield return new WaitForSecondsRealtime(3);
        Destroy(cdTimerGO);
        Time.timeScale = 1;
        pauseMenu.canPause(true);
    }

    IEnumerator endGame()
    {
        pauseMenu.canPause(false);
        yield return new WaitForSeconds(2);
        hasScored = false;
        Restart();
        pauseMenu.canPause(true);
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
        pauseMenu.canPause(false);
        UI.SetActive(false);
        yield return new WaitForSeconds(7);
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
        tauntGO.GetComponent<tauntScript>().setInsult(tauntTexts[Random.Range(0, tauntTexts.Length)]);
        tauntGO.GetComponent<tauntScript>().setTarget(target);
    }
}
