using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noseScript : MonoBehaviour
{
    private GameObject hitPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Nose Collided");
        if (collision.gameObject.CompareTag("Player"))
        {
            hitPlayer = collision.gameObject;
            Debug.Log("Collided with Player");
            GetComponent<BoxCollider2D>().enabled = false;
            hitPlayer.GetComponent<playerController>().Penetrated();
            StartCoroutine(deepStab());
            Destroy(hitPlayer.GetComponent<Rigidbody2D>());
            Destroy(hitPlayer.GetComponent<CapsuleCollider2D>());
            Destroy(hitPlayer.GetComponent<playerController>());
            hitPlayer.GetComponent<SpriteRenderer>().sortingOrder -= 2;
        }
    }

    IEnumerator deepStab()
    {
        yield return new WaitForSeconds(0.5f / GetComponentInParent<Rigidbody2D>().velocity.magnitude);
        hitPlayer.transform.parent = transform.parent;
    }
}
