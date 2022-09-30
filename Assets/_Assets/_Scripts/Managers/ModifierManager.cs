using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierManager : MonoBehaviour
{
    public float gravity;
    public GameObject[] player;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ApplyPlayerObjects(List<GameObject> players)
    {
        player[0] = players[0];
        player[1] = players[1];
    }

    public void ApplyModifiers()
    {
        if (gravity != 0)
        {
            gravityModifier();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void gravityModifier()
    {
        Debug.Log("gravity Modified");
        for (int i = 0; i < 2;)
        {
            Debug.Log("Player " + i + " has been changed");
            player[i].GetComponent<Rigidbody2D>().gravityScale = gravity;
            i++;
        }
    }
}
