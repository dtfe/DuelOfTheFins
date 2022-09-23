using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private int playerIndex;
    private Rigidbody2D rb2d;
    private GameObject nose;
    public GameObject noseProjectile;
    private float movementX, movementY;
    public Vector2 playerMovement;
    public float speed = 1;
    private float dodgeCooldownCur;
    private float dodgeCooldownStatic = 2;
    private bool hasNose;
    public bool isDead;
    private bool isDashing;
    
    public int SetPlayerIndex(int playerNumber)
    {
        return playerIndex = playerNumber;
    }
    public int GetPlayerIndex()
    {
        return playerIndex;
    }
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); //Sets rb2d to be the component of the rigidbody of whatever gameobject this script is on
        nose = transform.Find("PHYS_Player_Nose").gameObject; //Sets nose to reference the child named "Nose"
        hasNose = true;
        isDead = false;

        if (playerIndex == 1)
        {
            GetComponent<SpriteRenderer>().color = Color.magenta;
            FindObjectOfType<RoundManager>().player1 = gameObject;
        }else 
        { 
            GetComponent<SpriteRenderer>().color = Color.yellow;
            FindObjectOfType<RoundManager>().player2 = gameObject;
        }
    }

    private void OnMove(InputValue movementValue)
    {
        if (!isDead) //If the bool, isDead, is false then it gets the movement value from the input system and assigns the x and y values to the two floats
        {
            Vector2 movementVector = movementValue.Get<Vector2>(); //Assigns vector movementVector the values given from the input system.
            movementX = movementVector.x; //Assigns the float movementX the x value of movementVector
            movementY = movementVector.y; //Assigns the float movementY the y value of movementVector
        }
    }

    private void OnDodge(InputValue dodgeValue)
    {
        if (!isDead & !isDashing && dodgeCooldownCur <= 0)
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
        if (hasNose &! isDead &! isDashing) //Checks if the player has a nose, if its not dead and if its not dashing
        {
            GameObject noseProj = Instantiate(noseProjectile, nose.transform.position, transform.rotation); //Creates a projectile assigned the reference noseProj
            Vector3 shootDir = (transform.position - nose.transform.position).normalized; //Creates a vector for the direction the shot will go
            noseProj.GetComponentInChildren<NoseProjScript>().Setup(shootDir); //Calls on the method Setup with the vector 3 as a value to that method
            Debug.Log("nose has been shot");
            if (nose.transform.Find("PHYS_Player_Prefab(Clone)"))
            {
                GameObject deadPlayer = nose.transform.Find("PHYS_Player_Prefab(Clone)").gameObject;
                deadPlayer.transform.parent = noseProj.transform;
            }
            hasNose = false; //Makes sure player cant shoot twice
        }
    }

    private void OnDash() //Whenever the RB button is pressed on the gamepad, this activates
    {
        if (!isDead &! isDashing) //Checks if the player is still alive and not dashing already
        {
            StartCoroutine(Dash()); //Starts coroutine which allows for delays using IENumerator
            isDashing = true; //Sets isDashing to true so player cant spam dash while dashing
            nose.GetComponent<BoxCollider2D>().enabled = true; //Makes the collider which can kill the opponent active
        }
    }

    private void Update()
    {
        if (dodgeCooldownCur > 0)
        {
            dodgeCooldownCur -= Time.deltaTime;
        }

        Vector2 lookDir = (nose.transform.position - transform.position).normalized; //Vector which is in the direction of where your character is looking
        playerMovement = lookDir; //Sets Vector2 playerMovement to be equal to vector2 lookDir

        if(rb2d.velocity.magnitude > 0.1f) //Checks if the player is 
        {
            transform.up = rb2d.velocity.normalized; // Keeps the player looking towards the direction they are moving
        }
        if (hasNose) //Activates and deactivates the nose of the player
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
        Vector2 movement = new Vector2(movementX, movementY); //Creates a new vector2 based on the values from the floats MovementX and MovementY
        if (!isDashing &! isDead) //Checks if you are not dashing and are not dead
        {
            rb2d.AddForce(movement * speed); //Adds vector 2 movement multiplied by your speed as a force on your Rigidbody2D
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PHYS_Nose_Projectile" & ! hasNose && collision.gameObject.GetComponent<NoseProjScript>().isPickable) //Checks if you entered the trigger of a nose projectile and if you don't have a nose
        {
            if (collision.GetComponent<NoseProjScript>().parent.Find("PHYS_Player_Prefab(Clone)"))
            {
                GameObject deadPlayer = collision.GetComponent<NoseProjScript>().parent.Find("PHYS_Player_Prefab(Clone)").gameObject;
                deadPlayer.transform.SetParent(nose.transform, true);
                deadPlayer.transform.localPosition = collision.GetComponent<NoseProjScript>().deadPlayerLocalPos;
                deadPlayer.transform.localEulerAngles = collision.GetComponent<NoseProjScript>().deadPlayerLocalRot;
                //deadPlayer.transform.Translate(0.3f * playerMovement);
            }
            Destroy(collision.gameObject.GetComponent<NoseProjScript>().parent.gameObject); //Destroys the nose projectile that you pick up
            hasNose = true; //Gives you your nose back
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

    public void ResetCharacter()
    {
        isDead = false;
        hasNose = true;
        isDashing = false;
        gameObject.transform.parent = transform.parent;
        rb2d.velocity = Vector2.zero;
    }
}
