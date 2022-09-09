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
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); //Sets rb2d to be the component of the rigidbody of whatever gameobject this script is on
        nose = transform.Find("Nose").gameObject;
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Fire.performed += context => ShootNose();
    }

    private void Update()
    {
        transform.up = rb2d.velocity.normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(movementX, movementY);
        rb2d.AddForce(movement * speed);
    }

    void ShootNose()
    {
        GameObject noseProj = Instantiate(noseProjectile, nose.transform.position, transform.rotation);
        Vector3 shootDir = (transform.position - nose.transform.position).normalized;
        noseProj.GetComponent<noseScript>().Setup(shootDir);
        Debug.Log("nose has been shot");
        nose.SetActive(false);

    }
    
    public void Penetrated()
    {

    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Disable();
    }
}
