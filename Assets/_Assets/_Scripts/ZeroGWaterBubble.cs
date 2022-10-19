using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroGWaterBubble : MonoBehaviour
{
    private CapsuleCollider2D selfCol;
    private Vector2 originalSize;
    private Vector2 currentSize;
    public Vector2 firstScale;
    public Vector2 secondScale;
    public float speed = 2f;
    public float duration = 5f;

    private void Start()
    {
        selfCol = GetComponent<CapsuleCollider2D>();
        originalSize = transform.localScale;
        currentSize = transform.localScale;
        firstScale = new Vector2((transform.localScale.x - transform.localScale.x / 15), (transform.localScale.y + transform.localScale.y / 15));
        secondScale = new Vector2((transform.localScale.x + transform.localScale.x / 30), (transform.localScale.y - transform.localScale.y / 30));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(Bounce(firstScale, secondScale, duration));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        firstScale = new Vector2((originalSize.x - (originalSize.x / 15 * (collision.GetComponent<Rigidbody2D>().velocity.magnitude / 2))), (originalSize.y + (originalSize.y / 15 * (collision.GetComponent<Rigidbody2D>().velocity.magnitude / 2))));
        secondScale = new Vector2((originalSize.x + (originalSize.x / 30 * (collision.GetComponent<Rigidbody2D>().velocity.magnitude / 2))), (originalSize.y - (originalSize.y / 30 * (collision.GetComponent<Rigidbody2D>().velocity.magnitude / 2))));
        currentSize = transform.localScale;
        StopAllCoroutines();
        StartCoroutine(Bounce(firstScale, secondScale, duration));
    }

    public IEnumerator Bounce(Vector2 a, Vector2 b, float time)
    {
        Debug.Log("Bouncing");
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while (i < 1.0f)
        {
            if (i > 0.5f)
                selfCol.direction = CapsuleDirection2D.Vertical;
            i += Time.deltaTime * rate;
            transform.localScale = Vector2.Lerp(currentSize, a, i);
            yield return null;
        }
        i = 0.0f;
        while (i < 1.0f)
        {
            if (i > 0.5f)
                selfCol.direction = CapsuleDirection2D.Horizontal;
            i += Time.deltaTime * rate;
            transform.localScale = Vector2.Lerp(a, b, i);
            yield return null;
        }
        i = 0.0f;
        while (i < 1.0f)
        {
            if (i > 0.5f)
                selfCol.direction = CapsuleDirection2D.Vertical;
            selfCol.offset.Scale(new Vector2(-1, -1));
            i += Time.deltaTime * rate;
            transform.localScale = Vector2.Lerp(b, originalSize, i);
            yield return null;
        }
    }
}
