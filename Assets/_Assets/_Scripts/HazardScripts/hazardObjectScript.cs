using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazardObjectScript : MonoBehaviour
{
    public float acceleration = 1f;
    public float accelerationTime = 2f;
    public float maxSpeed = 5f;
    public GameObject blood1;
    public bool rotateOverTime = true;
    public bool isHoming = false;
    private Rigidbody2D rb2d;
    private Vector2 movement;
    private float timeLeft;
    private float spawntime = 4;

    private PlayerController[] players;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        if (!rotateOverTime)
        {
            rb2d.angularVelocity = Random.Range(-60, 61);
        }

        if (isHoming)
        {
            players = FindObjectsOfType<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawntime > 0)
        {
            int LayerDefault = LayerMask.NameToLayer("noPlayerCollision");
            gameObject.layer = LayerDefault;
        }else
        {
            int LayerDefault = LayerMask.NameToLayer("Default");
            gameObject.layer = LayerDefault;
        }
        spawntime -= Time.deltaTime;
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0 && rotateOverTime)
        {
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            timeLeft = accelerationTime;
        }

        if (isHoming)
        {
            float closest = 55555;
            int closestPlayerIndex = 0;
            for (int i = 0; i < 2; i++)
            {
                float distance = Vector3.Distance(players[i].transform.position, transform.position); 
                if (distance < closest)
                {
                    closest = distance;
                    closestPlayerIndex = i;
                }
            }
            homing(players[closestPlayerIndex].transform);
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
        rb2d.AddForce(movement * acceleration);
    }

    public void SetAccelerationAndMaxSpeed(float mAcceleration, float mSpeed)
    {
        maxSpeed = mSpeed;
        acceleration = mAcceleration;
    }

    public void homing(Transform player)
    {
        movement = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y)/4;
        
    }
}
