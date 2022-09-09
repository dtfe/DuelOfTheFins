using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    private PlayerControls controls;
    private Rigidbody2D rb2d;
    private GameObject nose;
    public GameObject noseProjectile;
    private float movementX, movementY;
    public float speed = 1;
    public bool hasNose;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); //Sets rb2d to be the component of the rigidbody of whatever gameobject this script is on
        nose = transform.Find("Nose").gameObject;
        hasNose = true;
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void OnFire()
    {
        if (hasNose)
        {
            GameObject noseProj = Instantiate(noseProjectile, nose.transform.position, transform.rotation);
            Vector3 shootDir = (transform.position - nose.transform.position).normalized;
            noseProj.GetComponent<noseScript>().Setup(shootDir);
            Debug.Log("nose has been shot");
            hasNose = false;
        }
    }

    private void Update()
    {
        transform.up = rb2d.velocity.normalized;
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

    }
    private void Awake()
    {
        //controls = new PlayerControls();
    }

    private void OnEnable()
    {
        //.Player.Enable();
    }
    private void OnDisable()
    {
        //controls.Player.Disable();
    }
}
