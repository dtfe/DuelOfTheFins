using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// This script is for the projectile which spawns whenever a player shoots. It also detects if a player is attached to the nose and makes sure they stick to it correctly and also creates holes for the aquarium map

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

        if (transform.position.y > 40f || transform.position.y < -6f || transform.position.x > 9f || transform.position.x < -9f)
        {
            if (parent.Find("PHYS_Player_Prefab(Clone)"))
            {
                parent.Find("PHYS_Player_Prefab(Clone)").transform.parent = null;
            }
            Destroy(parent.gameObject);
        }
        Debug.DrawLine(transform.position, parent.position + shootDir/2, Color.red);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player") && isActive & !isPickable)
        {
            SoundManager.PlaySound("Bleeding");
            other.gameObject.GetComponent<PlayerController>().Penetrated();
            GetComponent<BoxCollider2D>().enabled = false;
            parent.Translate((depth * transform.localScale.y) * Vector2.up);
            parent.transform.position = GetPointOfContact();
            parent.transform.Translate(-0.2f * Vector2.up);
            parent.parent = other.transform;
            GameObject bloodyHit = Instantiate(blood, transform.position, transform.rotation);
            bloodyHit.transform.Translate(((depth * transform.localScale.y) * Vector2.up) * 3f);
            bloodyHit.transform.parent = other.transform;
            isActive = false;
        }
        string[] collidables = { "Wall",  "GlassWall", "Sand", "Stone" };
        if (collidables.Contains(other.gameObject.tag))
        {
            var shouldBreak = other.gameObject.CompareTag("GlassWall"); 
            HitWall(shouldBreak, other.transform);
        }
    }
    private void HitWall(bool breakable, Transform hitPos)
    {
        isActive = false;
        hitbox.offset = new Vector2(0, -0.173f);
        hitbox.isTrigger = true;
        parent.transform.position = GetPointOfContact();
        isPickable = true;
        if (breakable)
        {
            GameObject Hole = Instantiate(hole, new Vector2(hitPos.position.x, transform.position.y), transform.rotation);
            hole.transform.parent = null;
            FindObjectOfType<WaterLevel>().NewHole(Hole.transform);
        }
        while (Physics2D.CircleCast(transform.position, 0.1f, shootDir, 0.5f, LayerMask.GetMask("Wall")))
        {
            parent.position += shootDir / 20;
        }
    }

    private Vector3 GetPointOfContact()
    {
        RaycastHit2D hit;
        if (hit = Physics2D.Raycast(transform.position, parent.position - shootDir, 1f))
        {
            Debug.DrawLine(transform.position, Vector3.up, Color.red);
            return hit.point;
        }
        return transform.position;
    }
}
