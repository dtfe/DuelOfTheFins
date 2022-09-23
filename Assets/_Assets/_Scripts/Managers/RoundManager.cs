using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public GameObject player1, player2;
    private TextMesh p1Counter, p2Counter;
    private int p1Score, p2Score;
    private bool hasScored = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject roundCounter = FindObjectOfType<Canvas>().gameObject.transform.Find("RoundCounter").gameObject;
        p1Counter = roundCounter.transform.Find("Player1Counter").gameObject.GetComponent<TextMesh>();
        p2Counter = roundCounter.transform.Find("Player2Counter").gameObject.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasScored)
        {
            if (player1.GetComponent<PlayerController>().isDead & !player2.GetComponent<PlayerController>().isDead & !hasScored)
            {
                hasScored = true;
                p2Score++;
                p2Counter.text = p2Score.ToString();
            }
            else if (!player1.GetComponent<PlayerController>().isDead && player2.GetComponent<PlayerController>().isDead & !hasScored)
            {
                hasScored = true;
                p1Score++;
                p1Counter.text = p1Score.ToString();
            }
        }
    }

    private void Restart()
    {
        GameObject.FindGameObjectsWithTag("");
        player1.GetComponent<PlayerController>().ResetCharacter();
    }
}
