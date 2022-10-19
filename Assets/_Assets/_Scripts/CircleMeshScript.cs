using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMeshScript : MonoBehaviour
{
    public PolygonCollider2D polyCollider;
    public Material material;
    private Vector3[] vertices;
    private bool hasLoaded;

    void Start()
    {
        polyCollider = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.H))
        {
            PolyMesh(3, 20);
        }*/
        if (hasLoaded)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Debug.DrawLine(vertices[i], -vertices[i], Color.red);
            }
        }
    }

    public void PolyMesh(float radius, int n)
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        mf.mesh = mesh;

        //verticies
        List<Vector3> verticiesList = new List<Vector3> { };
        float x;
        float y;
        for (int i = 0; i < n; i++)
        {
            x = radius * Mathf.Sin((2 * Mathf.PI * i) / n);
            y = radius * Mathf.Cos((2 * Mathf.PI * i) / n);
            verticiesList.Add(new Vector3(x, y, 0f));
        }
        vertices = verticiesList.ToArray();

        //triangles
        List<int> trianglesList = new List<int> { };
        for (int i = 0; i < (n - 2); i++)
        {
            trianglesList.Add(0);
            trianglesList.Add(i + 1);
            trianglesList.Add(i + 2);
        }
        int[] triangles = trianglesList.ToArray();

        //normals
        List<Vector3> normalsList = new List<Vector3> { };
        for (int i = 0; i < vertices.Length; i++)
        {
            normalsList.Add(-Vector3.forward);
        }
        Vector3[] normals = normalsList.ToArray();

        //initialise
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;

        //polyCollider
        polyCollider.pathCount = 1;

        List<Vector2> pathList = new List<Vector2> { };
        for (int i = 0; i < n; i++)
        {
            pathList.Add(new Vector2(vertices[i].x, vertices[i].y));
        }
        Vector2[] path = pathList.ToArray();

        polyCollider.SetPath(0, path);

        gameObject.GetComponent<MeshRenderer>().material = material;

        Debug.Log("There's " + mesh.triangles.Length + " triangles");
        hasLoaded = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.layer == 7)
        {
            Vector3 closestVert = new Vector3(0, 0, 0);
            int vertNumber = 0;
            for (int i = 0; i < vertices.Length; i++)
            {
                if (Vector3.Distance(vertices[i], collision.transform.position) > Vector3.Distance(closestVert, collision.transform.position))
                {
                    closestVert = vertices[i];
                    vertNumber = i;
                }
            }
            Vector3 velocity = collision.attachedRigidbody.velocity;
            Transform vertPos = transform;
            vertPos.position = vertices[vertNumber];
            StartCoroutine(waveHere(vertNumber, velocity, vertPos));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.layer == 7)
        {
            Vector3 closestVert = new Vector3(0, 0, 0);
            for (int i = 0; i < vertices.Length; i++)
            {
                if (Vector3.Distance(vertices[i], collision.transform.position) > Vector3.Distance(closestVert, collision.transform.position))
                {
                    closestVert = vertices[i];
                }
            }
        }
    }

    IEnumerator waveHere(int vertNumber, Vector3 velocity, Transform vertPos)
    {
        yield return null;
    }
}
