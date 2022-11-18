using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private GameObject nose;
    public GameObject noseProjectile;

    //Movement variables
    private float movementX, movementY;
    public Vector2 playerMovement;
    public float speed = 1;

    //Timer for dodge cooldown
    private float dodgeCooldownCur;
    private float dodgeCooldownStatic = 2;

    //Nose Bools
    private bool hasNose;
    public bool regenerateNose;
    private bool isRegeneratingNose;
    public bool canAttack;

    //Control bools
    public bool isDummy;
    public bool isControllable = true;
    public bool canDashAndDodge = true;
    public bool isDead;
    public bool isDashing;

    public PhysicsMaterial2D PM2D;
    private Vector3 startPosition;
  
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); //Sets rb2d to be the component of the rigidbody of whatever gameobject this script is on
        nose = transform.Find("PHYS_Player_Nose").gameObject; //Sets nose to reference the child named "Nose"
        hasNose = true;
        isDead = false;
        isDashing = false;
        isControllable = true;
        var skin = Resources.Load<GameObject>("PlayerSkins/selectedSkin" + GetComponent<PlayerInput>().playerIndex);
        transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = skin.GetComponent<SpriteRenderer>().sprite;
    }

    private void OnMove(InputValue movementValue)
    {
        if (!isDead &! isDummy) //If the bool, isDead, is false then it gets the movement value from the input system and assigns the x and y values to the two floats
        {
            Vector2 movementVector = movementValue.Get<Vector2>(); //Assigns vector movementVector the values given from the input system.
            movementX = movementVector.x; //Assigns the float movementX the x value of movementVector
            movementY = movementVector.y; //Assigns the float movementY the y value of movementVector
        }
    }

    private void OnDodge(InputValue dodgeValue)
    {
        if (!isDead && !isDashing && dodgeCooldownCur <= 0 && canDashAndDodge & !isDummy)
        {
            Vector2 dodgeVector = dodgeValue.Get<Vector2>();
            if (dodgeVector.y > 0.5)
            {
                rb2d.AddForce(Vector2.up * speed * 25);
                dodgeCooldownCur = dodgeCooldownStatic;
            } else if (dodgeVector.y < -0.5)
            {
                rb2d.AddForce(Vector2.down * speed * 25);
                dodgeCooldownCur = dodgeCooldownStatic;
            } else if (dodgeVector.x > 0.5)
            {
                rb2d.AddForce(Vector2.right * speed * 25);
                dodgeCooldownCur = dodgeCooldownStatic;
            } else if (dodgeVector.x < -0.5)
            {
                rb2d.AddForce(Vector2.left * speed * 25);
                dodgeCooldownCur = dodgeCooldownStatic;
            }
        }
    }

    private void OnFire() //Whenever the west most button on the gamepad is pressed, this activates
    {
        if (hasNose && !isDead && !isDashing && !isDummy && canAttack) //Checks if the player has a nose, if its not dead and if its not dashing
        {
            AudioSource audioSrc2 = GetComponent<AudioSource>();
            //audioSrc2.PlayOneShot(Resources.Load<AudioClip>("Audio/Shoot sword"));

            GameObject noseProj = Instantiate(noseProjectile, nose.transform.position, transform.rotation); //Creates a projectile assigned the reference noseProj
            Vector3 shootDir = (transform.position - nose.transform.position).normalized; //Creates a vector for the direction the shot will go
            noseProj.GetComponentInChildren<NoseProjScript>().Setup(shootDir); //Calls on the method Setup with the vector 3 as a value to that method
            Debug.Log("nose has been shot");
            if (nose.transform.Find("PHYS_Player_Prefab(Clone)"))
            {
                Debug.Log("player killed");
                GameObject deadPlayer = nose.transform.Find("PHYS_Player_Prefab(Clone)").gameObject;
                deadPlayer.transform.parent = noseProj.transform;
            }
            hasNose = false; //Makes sure player cant shoot twice
        }
    }

    private void OnDash() //Whenever the RB button is pressed on the gamepad, this activates
    {
        if (!isDead && !isDashing && canDashAndDodge && !isDummy) //Checks if the player is still alive and not dashing already
        {

            SoundManager.PlaySound("Dashing");
            StartCoroutine(Dash()); //Starts coroutine which allows for delays using IENumerator
            isDashing = true; //Sets isDashing to true so player cant spam dash while dashing
            if (!canAttack)
            {
                return;
            }
            nose.GetComponent<BoxCollider2D>().enabled = true; //Makes the collider which can kill the opponent active
        }
    }

    private void Update()
    {
        if (transform.position.y < -6f || transform.position.x > 9f || transform.position.x < -9f)
        {
            Penetrated();
        }

        if (dodgeCooldownCur > 0)
        {
            dodgeCooldownCur -= Time.deltaTime;
        }
        
        if (!hasNose && !FindObjectOfType<NoseProjScript>() && !isRegeneratingNose)
        {
            isRegeneratingNose = true;
            StartCoroutine(RegenerateSword());
        }

        Vector2 lookDir = (nose.transform.position - transform.position).normalized; //Vector which is in the direction of where your character is looking
        playerMovement = lookDir; //Sets Vector2 playerMovement to be equal to vector2 lookDir
        if (hasNose && canAttack) //Activates and deactivates the nose of the player
        {
            nose.SetActive(true);
        }
        else
        {
            nose.SetActive(false);
        }
        if (!isDead)
        {
            GetComponent<CapsuleCollider2D>().enabled = true;
            GetComponent<PlayerInput>().enabled = true;
            GetComponent<PlayerController>().enabled = true;

            if (rb2d.velocity.magnitude > 0.1f) //Checks if the player is not moving
            {
                transform.up = rb2d.velocity.normalized; // Keeps the player looking towards the direction they are moving
            }
            Transform spriteTr = transform.Find("Sprite");
            if (transform.eulerAngles.z > 180)
            {
                if (spriteTr && spriteTr.localScale.x > 0)
                    spriteTr.localScale = new Vector3(-spriteTr.localScale.x, spriteTr.localScale.y, spriteTr.localScale.z);
                    //transform.Find("Sprite").GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (transform.eulerAngles.z < 180)
            {
                if (spriteTr && spriteTr.localScale.x < 0)
                    spriteTr.localScale = new Vector3(-spriteTr.localScale.x, spriteTr.localScale.y, spriteTr.localScale.z);
                    //transform.Find("Sprite").GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(movementX, movementY); //Creates a new vector2 based on the values from the floats MovementX and MovementY
        if (!isDashing && !isDead && isControllable && !isDummy) //Checks if you are not dashing and are not dead
        {
            rb2d.AddForce(movement * speed); //Adds vector 2 movement multiplied by your speed as a force on your Rigidbody2D
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("deathWall"))
        {
            Penetrated();
            rb2d.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PHYS_Nose_Projectile" && !hasNose && collision.gameObject.GetComponent<NoseProjScript>().isPickable) //Checks if you entered the trigger of a nose projectile and if you don't have a nose
        {
            SoundManager.PlaySound("GetSword");
            if (collision.GetComponent<NoseProjScript>().parent.Find("PHYS_Player_Prefab(Clone)"))
            {
                GameObject deadPlayer = collision.GetComponent<NoseProjScript>().parent.Find("PHYS_Player_Prefab(Clone)").gameObject;
                deadPlayer.transform.SetParent(nose.transform, true);
                deadPlayer.transform.localPosition = collision.GetComponent<NoseProjScript>().deadPlayerLocalPos;
                deadPlayer.transform.localEulerAngles = collision.GetComponent<NoseProjScript>().deadPlayerLocalRot;
            }
            Destroy(collision.gameObject.GetComponent<NoseProjScript>().parent.gameObject); //Destroys the nose projectile that you pick up
            hasNose = true; //Gives you your nose back
        }

        if (collision.gameObject.CompareTag("PlayArea") && !canDashAndDodge && isDashing)
        {
            rb2d.velocity = rb2d.velocity / 2;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayArea"))
        {
            isControllable = true;
            rb2d.gravityScale = 0f;
            rb2d.drag = 2;
            if (!canDashAndDodge)
            {
                canDashAndDodge = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayArea"))
        {
            isControllable = false;
            if (!FindObjectOfType<WaterLevel>())
            {
                rb2d.gravityScale = 0f;
                rb2d.drag = 0f;
                canDashAndDodge = false;
            }
            else
            {
                rb2d.gravityScale = 1f;
                rb2d.drag = 1f;
            }
        }
    }

    public void Penetrated() // Method which kills the player when called upon
    {
        isDead = true; //Sets isDead to true
    }

    private IEnumerator Dash()
    {
        rb2d.AddForce(playerMovement * speed * 100); //Adds a force in the direction of playerMovement which acts as a dash
        yield return new WaitForSeconds(1); //Waits for 1 second
        isDashing = false; //Then sets dash to false allowing the player to dash once more
        nose.GetComponent<BoxCollider2D>().enabled = false; //Disables collider that allows you to kill the other player
    }

    IEnumerator RegenerateSword()
    {
        yield return new WaitForSeconds(5);
        if (!hasNose)
        {
            hasNose = true;
        }
        isRegeneratingNose = false;
    }

    public void RangedHit()
    {
        StartCoroutine(LastStand());
    }

    public IEnumerator LastStand()
    {
        yield return new WaitForSeconds(3);
        if (transform.Find("PHYS_Nose_Projectile(Clone)"))
        {
            Penetrated();
        }
    }

    public void ResetCharacter()
    {
        transform.parent = null;
        isDead = false;
        if (!rb2d)
        {
            gameObject.AddComponent<Rigidbody2D>();
            rb2d = GetComponent<Rigidbody2D>();
            rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rb2d.sharedMaterial = PM2D;
        }
        foreach(Transform tr in transform)
        {
            if(tr.tag == "Deleteable")
            {
                Destroy(tr.gameObject);
            }
        }
        rb2d.gravityScale = 0;
        rb2d.drag = 2;
        hasNose = true;
        isDashing = false;
        isControllable = true;
        rb2d.velocity = Vector2.zero;
        rb2d.angularVelocity = 0;
        movementX = 0;
        movementY = 0;
        startPosition = FindObjectOfType<PlayerManager>().transform.Find("Spawn" + GetComponent<PlayerInput>().playerIndex).transform.position;
        transform.position = startPosition;
    }
}
