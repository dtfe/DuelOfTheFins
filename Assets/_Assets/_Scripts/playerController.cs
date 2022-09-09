using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private GameObject nose;
    public GameObject noseProjectile;
    private float movementX, movementY;
    private Vector2 playerMovement;
    public float speed = 1;
    public float dashLength;
    private bool hasNose;
    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); //Sets rb2d to be the component of the rigidbody of whatever gameobject this script is on
        nose = transform.Find("Nose").gameObject;
        hasNose = true;
        isDead = false;
    }

    private void OnMove(InputValue movementValue)
    {
        if (!isDead)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();
            movementX = movementVector.x;
            movementY = movementVector.y;
        }
    }

    private void OnFire()
    {
        if (hasNose &! isDead)
        {
            GameObject noseProj = Instantiate(noseProjectile, nose.transform.position, transform.rotation);
            Vector3 shootDir = (transform.position - nose.transform.position).normalized;
            noseProj.GetComponent<noseScript>().Setup(shootDir);
            Debug.Log("nose has been shot");
            hasNose = false;
        }
    }

    private void OnDash()
    {
        if (!isDead)
        {
            rb2d.AddForce(playerMovement * speed * 4);
        }
    }

    private void Update()
    {
        if(rb2d.velocity.magnitude > 0.1f)
        {
            transform.up = rb2d.velocity.normalized;
        }
        if (hasNose)
        {
            nose.SetActive(true);
        }
        else
        {
            nose.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(movementX, movementY);
        playerMovement = movement;
        rb2d.AddForce(movement * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "NoseProjectile(Clone)")
        {
            Destroy(collision.gameObject);
            hasNose = true;
        }
    }

    public void Penetrated()
    {
        isDead = true;
    }
}
