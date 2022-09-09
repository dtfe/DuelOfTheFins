using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noseScript : MonoBehaviour
{
    private BoxCollider2D hitbox;
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
            //other.gameObject.GetComponent<playerController>().Penetrated();
            transform.Translate(depth * Vector2.up);
            transform.parent = other.transform;
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            Destroy(gameObject.GetComponent<Collider2D>());
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            hitbox.offset = new Vector2(0, -0.37f);
            hitbox.isTrigger = true;
            transform.Translate(depth * Vector2.up);
        }
    }
}
