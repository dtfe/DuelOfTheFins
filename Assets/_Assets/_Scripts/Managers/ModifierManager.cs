using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierManager : MonoBehaviour
{
    public float gravity;
    public GameObject[] player;
    public Transform WaterLevel;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void ApplyPlayerObjects(List<GameObject> players)
    {
        player[0] = players[0];
        player[1] = players[1];
        ApplyModifiers();
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
        for (int i = 0; i < 2;)
        {
            player[i].GetComponent<Rigidbody2D>().gravityScale = gravity;
            i++;
        }
    }
}
