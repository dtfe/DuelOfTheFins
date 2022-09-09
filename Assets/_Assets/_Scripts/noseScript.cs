using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noseScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector3 shootDir;
    private bool isActive;
    public float speed;

    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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
            other.gameObject.GetComponent<playerController>().Penetrated();
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            StartCoroutine(hitWall());
        }
    }

    private IEnumerator hitWall()
    {
        yield return new WaitForSeconds(0.2f);
        rb2d.velocity = Vector3.zero;
    }
}
