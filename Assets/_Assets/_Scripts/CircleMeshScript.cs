using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMeshScript : MonoBehaviour
{
    public PolygonCollider2D polyCollider;
    public Material material;
    private Vector3[] vertices;
    private Vector3[] verticesPositions;
    public bool enableVectorLines = false;
    private Mesh mesh;

    //Spring Physics
    public float springconstant = 0.02f;
    public float dampening = 0.04f;
    public float spread = 0.05f;
    public float z = -1f;

    private Vector3[] velocities;
    private float[] accelerations;
    public float mass = 1;

    void Start()
    {
        polyCollider = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        if (enableVectorLines)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                //Debug.DrawLine(vertices[i], vertices[0], Color.red);
                Debug.DrawLine(verticesPositions[i], verticesPositions[0], Color.cyan);
            }
        }
    }

    public void PolyMesh(float radius, int n)
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        mesh = new Mesh();
        mf.mesh = mesh;

        //verticies
        List<Vector3> verticiesList = new List<Vector3> { };
        float x = transform.localPosition.x;
        float y = transform.localPosition.y;

        verticiesList.Add(new Vector3(x, y, 0f));

        for (int i = 0; i < n+1; i++)
        {
            x = radius * Mathf.Sin((2 * Mathf.PI * i) / n);
            y = radius * Mathf.Cos((2 * Mathf.PI * i) / n);
            verticiesList.Add(new Vector3(x, y, 0f));
        }
        verticesPositions = verticiesList.ToArray();
        vertices = verticiesList.ToArray();

        //triangles
        List<int> trianglesList = new List<int> { };
        for (int i = 0; i < n; i++)
        {
            trianglesList.Add(0);
            trianglesList.Add(i + 1);
            trianglesList.Add(i + 2);
        }
        trianglesList.Add(0);
        trianglesList.Add(vertices.Length - 1);
        trianglesList.Add(1);
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
        for (int i = 0; i < n+2; i++)
        {
            pathList.Add(new Vector2(vertices[i].x, vertices[i].y));
        }
        Vector2[] path = pathList.ToArray();

        polyCollider.SetPath(0, path);

        gameObject.GetComponent<MeshRenderer>().material = material;

        Debug.Log("There's " + mesh.triangles.Length + " triangles");
        velocities = new Vector3[vertices.Length];
        accelerations = new float[vertices.Length];
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            if (i == 0)
            {
                continue;
            }
            //Euler's Method combined with Hooke's Law

            Vector3 wantsToGo = verticesPositions[i] - vertices[i];
            velocities[i] *= (1-dampening*mass);
            vertices[i] += (wantsToGo + velocities[i]) * Time.deltaTime;
            
            /*
            Vector3 force = springconstant * (vertices[i] - verticesPositions[i]) + velocities[i] * dampening;
            accelerations[i] = -force.magnitude / mass;
            vertices[i] += velocities[i];
            velocities[i] += vertices[i].normalized * accelerations[i];*/
        }
        
        Vector3[] leftDeltas = new Vector3[vertices.Length];
        Vector3[] rightDeltas = new Vector3[vertices.Length];


        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                if (i == 0)
                {
                    continue;
                }
                if (i > 0)
                {
                    leftDeltas[i] = spread * (vertices[i] - vertices[i - 1]);
                    velocities[i - 1] += leftDeltas[i];
                }
                if (i < vertices.Length - 1)
                {
                    rightDeltas[i] = spread * (vertices[i] - vertices[i + 1]);
                    velocities[i + 1] += rightDeltas[i];
                }
            }

            for (int i = 0; i < vertices.Length; i++)
            {
                if (i == 0)
                {
                    continue;
                }
                if (i > 1)
                {
                    vertices[i - 1] += leftDeltas[i];
                }
                if (i < vertices.Length - 1)
                {
                    vertices[i + 1] += rightDeltas[i];
                }
            }
        }
        UpdateMesh();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Deleteable"))
        {
            Vector3 closestVert = new Vector3(0, 0, 0);
            int vertNumber = 0;
            for (int i = 0; i < vertices.Length; i++)
            {
                if (Vector3.Distance(vertices[i], collision.transform.position) < Vector3.Distance(closestVert, collision.transform.position))
                {
                    closestVert = vertices[i];
                    vertNumber = i;
                }
            }
            Vector3 velocity = collision.attachedRigidbody.velocity;
            Vector3 wantsToGo = vertices[0] - verticesPositions[vertNumber];
            if (velocity.magnitude > -0.5f && velocity.magnitude < 0.5f)
            {
                Debug.Log("Velocity = zero");
                float howMuch2 = 9.7f;
                velocities[vertNumber] += wantsToGo.normalized * howMuch2;
                return;
            }
            float howMuch = Vector3.Dot(wantsToGo, velocity);
            velocities[vertNumber] += wantsToGo.normalized * howMuch;
            
            /*float velocityMagnitude = velocity.magnitude;
            velocities[vertNumber] = velocity / 20;*/
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Deleteable"))
        {
            Vector3 closestVert = new Vector3(0, 0, 0);
            int vertNumber = 0;
            for (int i = 0; i < vertices.Length; i++)
            {
                if (Vector3.Distance(vertices[i], collision.transform.position) < Vector3.Distance(closestVert, collision.transform.position))
                {
                    closestVert = vertices[i];
                    vertNumber = i;
                }
            }
            Vector3 velocity = collision.attachedRigidbody.velocity;
            Vector3 wantsToGo = vertices[0] - verticesPositions[vertNumber];
            float howMuch = Vector3.Dot(wantsToGo, velocity);
            velocities[vertNumber] += wantsToGo.normalized * howMuch;

            /*Vector3 velocity = collision.attachedRigidbody.velocity;
            velocities[vertNumber] = velocity / 20;*/
        }
    }

    private void UpdateMesh()
    {
        mesh.vertices = vertices;

        /*List<Vector2> pathList = new List<Vector2> { };
        for (int i = 0; i < vertices.Length; i++)
        {
            pathList.Add(new Vector2(vertices[i].x, vertices[i].y));
        }
        Vector2[] path = pathList.ToArray();

        polyCollider.SetPath(0, path);*/
    }
} 
