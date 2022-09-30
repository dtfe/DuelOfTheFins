using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseScript : MonoBehaviour
{
    public GameObject blood;
    public GameObject hole;
    private GameObject hitPlayer;
    public GameObject parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Nose Collided");
        if (GetComponentInParent<PlayerController>().isDashing)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                hitPlayer = collision.gameObject;
                Debug.Log("Collided with Player");
                GetComponent<BoxCollider2D>().enabled = false;
                hitPlayer.GetComponent<PlayerController>().Penetrated();
                parent.transform.Translate(Vector2.up * 0.1f);
                hitPlayer.transform.parent = transform;
                StartCoroutine(Bleeding());
                Destroy(hitPlayer.GetComponent<Rigidbody2D>());
                hitPlayer.GetComponent<CapsuleCollider2D>().enabled = false;
            }
            if (collision.gameObject.CompareTag("Wall") && FindObjectOfType<ModifierManager>().WaterLevel)
            {
                GameObject Hole = Instantiate(hole, transform.position, transform.rotation);
                hole.transform.Translate(0.25f * Vector2.up);
                hole.transform.parent = null;
                FindObjectOfType<WaterLevel>().NewHole(Hole.transform);
            }
        }
    }

    IEnumerator Bleeding()
    {
        GameObject Bleed = Instantiate(blood, transform.position, transform.rotation);
        Bleed.transform.Translate(0.25f * Vector2.up);
        Bleed.transform.parent = hitPlayer.transform;
        yield return new WaitForSeconds(0.1f);
        var subEmitter = Bleed.GetComponent<ParticleSystem>().subEmitters;
        subEmitter.enabled = false;

        
    }
}
