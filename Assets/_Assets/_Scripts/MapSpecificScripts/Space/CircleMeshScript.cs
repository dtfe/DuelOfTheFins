using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the water physics for the space map. It is based off of this tutorial online: https://gamedevelopment.tutsplus.com/tutorials/make-a-splash-with-dynamic-2d-water-effects--gamedev-236 but edited to support vertecies instead of using the y pos, which is easier said than done.

public class CircleMeshScript : MonoBehaviour
{
    public PolygonCollider2D polyCollider;
    public Material material;
    private Vector3[] vertices;
    private Vector3[] verticesPositions;
    public bool enableVectorLines = false;
    private Mesh mesh;
    public float radius;
    public int resolution;

    //Spring Physics
    public float springconstant = 0.02f;
    public float dampening = 0.04f;
    public float spread = 0.05f;
    public float z = -1f;

    public Vector3[] velocities;
    public float[] accelerations;
    public float mass = 1;
    public float SizeAdjustment;
    public float test;

    void Start()
    {
        polyCollider = GetComponent<PolygonCollider2D>();
        PolyMesh(radius, resolution);
        //SizeAdjustment = radius / 2 + 5*spread;
    }

    private void Update()
    {
        if (enableVectorLines)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Debug.DrawLine(vertices[i], vertices[0], Color.red);
            }
        }
        SizeAdjustment = radius / (radius/2) + radius * spread;
    }

    // This creates the circle mesh. I found the solution online here: https://stackoverflow.com/questions/50606756/creating-a-2d-circular-mesh-in-unity, but made adjustments to fit what I needed it to do. The solution in the link had several issues which had to be fixed.
    public void PolyMesh(float radius, int n)
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        mesh = new Mesh();
        mf.mesh = mesh;

        //verticies
        List<Vector3> verticiesList = new List<Vector3> { };
        float x = 0;
        float y = 0;

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
        velocities = new Vector3[vertices.Length];
        accelerations = new float[vertices.Length];
    }
    private void FixedUpdate()
    {
        // This bit pulls the vertex back to it's default position
        for (int i = 0; i < vertices.Length; i++)
        {
            if (i == 0)
            {
                continue;
            }
            Vector3 wantsToGo = verticesPositions[i]*SizeAdjustment - vertices[i];
            Debug.DrawRay(wantsToGo, wantsToGo);
            velocities[i] *= (1-dampening*mass) + accelerations[i];
            vertices[i] += (wantsToGo + velocities[i]) * Time.deltaTime;
        }
        
        // The Deltas affect the velocities of it's neighbor vertices to create a wave going through the entire shape. Fixed update is used since we do not need incredible accuracy and running these loops as often as update() would be horrible for performance.

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
                if (i == 1)
                {
                    leftDeltas[i] = spread * (vertices[i] - vertices[vertices.Length-1]);
                    velocities[vertices.Length-1] += leftDeltas[i];
                }
                if (i > 1)
                {
                    leftDeltas[i] = spread * (vertices[i] - vertices[i - 1]);
                    velocities[i - 1] += leftDeltas[i];
                }
                if (i < vertices.Length - 1)
                {
                    rightDeltas[i] = spread * (vertices[i] - vertices[i + 1]);
                    velocities[i + 1] += rightDeltas[i];
                }
                if (i == vertices.Length-1)
                {
                    rightDeltas[i] = spread * (vertices[i] - vertices[1]);
                    velocities[1] += rightDeltas[i];
                }
            }

            for (int i = 0; i < vertices.Length; i++)
            {
                if (i == 0)
                {
                    continue;
                }
                if (i == 1)
                {
                    vertices[vertices.Length-1] += leftDeltas[i];
                }
                if (i > 1)
                {
                    vertices[i - 1] += leftDeltas[i];
                }
                if (i < vertices.Length - 1)
                {
                    vertices[i + 1] += rightDeltas[i];
                }
                if (i == vertices.Length-1)
                {
                    vertices[1] += rightDeltas[i];
                }
            }
        }
        UpdateMesh();
    }


    // This bit detects if the player or a pufferfish enters the bubble then applies a force to the closest vertex.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Deleteable"))
        {
            Vector3 closestVert = new Vector3(99, 99, 99);
            int vertNumber = 0;
            for (int i = 0; i < vertices.Length; i++)
            {
                if (Vector3.Distance(vertices[i] + transform.position, collision.transform.position) < Vector3.Distance(closestVert, collision.transform.position))
                {
                    closestVert = vertices[i] + transform.position;
                    vertNumber = i;
                }
            }
            Vector3 velocity = collision.attachedRigidbody.velocity;
            Vector3 wantsToGo = vertices[0] - verticesPositions[vertNumber];
            if (velocity.magnitude > -0.5f && velocity.magnitude < 0.5f)
            {
                Debug.Log("Velocity = zero");
                float howMuch2 = 9.7f;
                velocities[vertNumber] += wantsToGo.normalized * howMuch2 * 2;
                return;
            }
            float howMuch = Vector3.Dot(wantsToGo, velocity);
            velocities[vertNumber] += wantsToGo.normalized * howMuch * 2;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Deleteable"))
        {
            Vector3 closestVert = new Vector3(99, 99, 99);
            int vertNumber = 0;
            for (int i = 0; i < vertices.Length; i++)
            {
                if (Vector3.Distance(vertices[i] + transform.position, collision.transform.position) < Vector3.Distance(closestVert, collision.transform.position))
                {
                    closestVert = vertices[i] + transform.position;
                    vertNumber = i;
                }
            }
            Vector3 velocity = collision.attachedRigidbody.velocity;
            Vector3 wantsToGo = vertices[0] - verticesPositions[vertNumber];
            if (velocity.magnitude > -0.5f && velocity.magnitude < 0.5f)
            {
                Debug.Log("Velocity = zero");
                float howMuch2 = 9.7f;
                velocities[vertNumber] += wantsToGo.normalized * howMuch2 * 2;
                return;
            }
            float howMuch = Vector3.Dot(wantsToGo, velocity);
            velocities[vertNumber] += wantsToGo.normalized * howMuch * 2;
        }
    }

    // This simply updates the mesh of the object
    private void UpdateMesh()
    {
        mesh.vertices = vertices;
    }
} 
