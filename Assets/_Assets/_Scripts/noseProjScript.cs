using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseProjScript : MonoBehaviour
{
    private BoxCollider2D hitbox;
    private ParticleSystem ps;
    private Vector3 shootDir;
    private bool isActive;
    public float speed;
    public float depth;

    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
    }

    void Start()
    {
        hitbox = GetComponent<BoxCollider2D>();
        ps = GetComponent<ParticleSystem>();
        isActive = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            transform.position -= shootDir * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isActive = false;
        if (other.gameObject.CompareTag("Player"))
        {
            var psEmission = ps.emission;
            psEmission.enabled = true;
            other.gameObject.GetComponent<PlayerController>().Penetrated();
            transform.Translate(depth * Vector2.up);
            transform.parent = other.transform;
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            Destroy(gameObject.GetComponent<Collider2D>());
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            hitbox.offset = new Vector2(0, -0.58f);
            hitbox.size = new Vector2(5.86f, 0.68f);
            hitbox.isTrigger = true;
            transform.Translate(depth * Vector2.up);
        }
    }
}
