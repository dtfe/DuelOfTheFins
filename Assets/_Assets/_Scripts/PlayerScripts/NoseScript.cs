using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseScript : MonoBehaviour
{
    public GameObject blood;
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
            hitPlayer.GetComponent<PlayerController>().Penetrated();
            StartCoroutine(deepStab());
            Destroy(hitPlayer.GetComponent<Rigidbody2D>());
            hitPlayer.GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }

    IEnumerator deepStab()
    {
        yield return new WaitForSeconds((0.25f * GetComponentInParent<Transform>().localScale.y) / GetComponentInParent<Rigidbody2D>().velocity.magnitude);
        hitPlayer.transform.parent = transform;
        GameObject Bleed = Instantiate(blood, transform.position, transform.rotation);
        Bleed.transform.Translate(0.25f * Vector2.up);
        Bleed.transform.parent = hitPlayer.transform;
    }
}
