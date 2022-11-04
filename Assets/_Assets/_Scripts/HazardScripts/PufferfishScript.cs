using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferfishScript : MonoBehaviour
{
    public float accelerationTime = 2f;
    public float maxSpeed = 5f;
    public GameObject blood1;
    private Rigidbody2D rb2d;
    private Vector2 movement;
    private float timeLeft;
    private float spawntime = 4;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawntime > 0)
        {
            int LayerDefault = LayerMask.NameToLayer("Default");
            gameObject.layer = LayerDefault;
            GetComponent<SpriteRenderer>().color = new Color(0.83f, 0.8f, 0.43f, 1);
        }else
        {
            int LayerDefault = LayerMask.NameToLayer("Default");
            gameObject.layer = LayerDefault;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1);
        }
        spawntime -= Time.deltaTime;
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            timeLeft = accelerationTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            timeLeft = accelerationTime;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundManager.PlaySound("Explosion");
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerController>().Penetrated();
            Vector2 explosionDir = new Vector2(collision.transform.position.x - transform.position.x, collision.transform.position.y - transform.position.y);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(explosionDir * 500);
            GameObject blood = Instantiate(blood1, collision.GetContact(0).point, transform.rotation);
            blood.transform.parent = collision.transform;
        }
    }

    private void FixedUpdate()
    {
        rb2d.AddForce(movement);
    }
}
