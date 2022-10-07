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
    public GameObject hole;
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
        transform.position = parent.transform.position;
        transform.rotation = parent.transform.rotation;
        if (isActive)
        {
            parent.position -= shootDir * speed * Time.deltaTime;
        }

        if (parent.Find("PHYS_Player_Prefab(Clone)"))
        {
            deadPlayerLocalPos = parent.Find("PHYS_Player_Prefab(Clone)").transform.localPosition;
            deadPlayerLocalRot = parent.Find("PHYS_Player_Prefab(Clone)").transform.localEulerAngles;
        }

        if (transform.position.y > 6f || transform.position.y < -6f || transform.position.x > 9f || transform.position.x < -9f)
        {
            Destroy(parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player") && isActive &! isPickable)
        {
            other.gameObject.GetComponent<PlayerController>().Penetrated();
            GetComponent<BoxCollider2D>().enabled = false;
            parent.Translate((depth * transform.localScale.y) * Vector2.up);
            parent.parent = other.transform;
            GameObject bloodyHit = Instantiate(blood, transform.position, transform.rotation);
            bloodyHit.transform.Translate(((depth * transform.localScale.y) * Vector2.up) * 2.5f);
            bloodyHit.transform.parent = other.transform;
            isActive = false;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            HitWall(false);
        }
        if (other.gameObject.CompareTag("GlassWall"))
        {
            HitWall(true);
        }
    }
    private void HitWall(bool breakable)
    {
        isActive = false;
        hitbox.offset = new Vector2(0, -0.173f);
        hitbox.isTrigger = true;
        parent.Translate((depth * transform.localScale.y) * Vector2.up);
        isPickable = true;
        if (breakable)
        {
            GameObject Hole = Instantiate(hole, transform.position, transform.rotation);
            Hole.transform.Translate(0.25f * Vector2.up);
            hole.transform.parent = null;
            FindObjectOfType<WaterLevel>().NewHole(Hole.transform);
        }
    }
}
