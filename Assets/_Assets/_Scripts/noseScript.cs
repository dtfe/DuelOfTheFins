using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noseScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Nose Collided");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with Player");
            GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.GetComponent<playerController>().Penetrated();
            transform.parent = collision.transform;
        }
    }
}
