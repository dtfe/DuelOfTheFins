using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseProjScript : MonoBehaviour
{
    private BoxCollider2D hitbox;
    private Vector3 shootDir;
    public Vector3 deadPlayerLocalPos;
    public Vector3 deadPlayerLocalRot;
    public Transform parent;
    public GameObject blood;
    public bool isActive;
    public bool isPickable;
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
        isPickable = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            parent.position -= shootDir * speed * Time.deltaTime;
        }

        if (parent.Find("PHYS_Player_Prefab(Clone)"))
        {
            deadPlayerLocalPos = parent.Find("PHYS_Player_Prefab(Clone)").transform.localPosition;
            deadPlayerLocalRot = parent.Find("PHYS_Player_Prefab(Clone)").transform.localEulerAngles;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player") && isActive &! isPickable)
        {
            GameObject bloodyHit = Instantiate(blood, transform.position, transform.rotation);
            bloodyHit.transform.Translate(((depth * transform.localScale.y) * Vector2.up) * 2.5f);
            bloodyHit.transform.parent = other.transform;
            other.gameObject.GetComponent<PlayerController>().Penetrated();
            parent.Translate((depth * transform.localScale.y) * Vector2.up);
            parent.parent = other.transform;
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            isActive = false;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            isActive = false;
            hitbox.offset = new Vector2(0, -0.173f);
            //hitbox.size = new Vector2(5.86f, 0.68f);
            hitbox.isTrigger = true;
            parent.Translate((depth * transform.localScale.y) * Vector2.up);
            isPickable = true;
        }
    }
}
