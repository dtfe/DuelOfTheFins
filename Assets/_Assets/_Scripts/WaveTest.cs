using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTest : MonoBehaviour
{
    private float[] xPositions;
    private float[] yPositions;
    private float[] velocities;
    private float[] accelerations;
    private LineRenderer Body;

    GameObject[] meshObjects;
    Mesh[] meshes;

    GameObject[] colliders;

    public float springconstant = 0.02f;
    public float dampening = 0.04f;
    public float spread = 0.05f;
    public float z = -1f;

    private float baseHeight, left, bottom;

    public GameObject splash;

    public Material mat;

    public GameObject waterMesh;

    public void SpawnWater(float Left, float width, float top, float Bottom)
    {
        int edgecount = Mathf.RoundToInt(width) * 5;
        int nodecount = edgecount + 1;

        Body = gameObject.AddComponent<LineRenderer>();
        Body.material = mat;
        Body.material.renderQueue = 1000;
        Body.positionCount = nodecount;
        Body.startWidth = 0.1f;
        Body.endWidth = 0.1f;

        xPositions = new float[nodecount];
        yPositions = new float[nodecount];
        velocities = new float[nodecount];
        accelerations = new float[nodecount];

        meshObjects = new GameObject[edgecount];
        meshes = new Mesh[edgecount];
        colliders = new GameObject[edgecount];
        baseHeight = top;
        bottom = Bottom;
        left = Left;

        for (int i = 0; i < nodecount; i++)
        {
            yPositions[i] = top;
            xPositions[i] = Left + width * i / edgecount;
            accelerations[i] = 0;
            velocities[i] = 0;
            Body.SetPosition(i, new Vector3(xPositions[i], yPositions[i], z));
        }

        for (int i = 0; i < edgecount; i++)
        {
            meshes[i] = new Mesh();

            Vector3[] Vertices = new Vector3[4];
            Vertices[0] = new Vector3(xPositions[i], yPositions[i], z);
            Vertices[1] = new Vector3(xPositions[i + 1], yPositions[i + 1], z);
            Vertices[2] = new Vector3(xPositions[i], bottom, z);
            Vertices[3] = new Vector3(xPositions[i + 1], bottom, z);

            Vector2[] UVs = new Vector2[4];
            UVs[0] = new Vector2(0, 1);
            UVs[1] = new Vector2(1, 1);
            UVs[2] = new Vector2(0, 0);
            UVs[3] = new Vector2(1, 0);

            int[] tris = new int[6] { 0, 1, 3, 3, 2, 0 };

            meshes[i].vertices = Vertices;
            meshes[i].uv = UVs;
            meshes[i].triangles = tris;

            meshObjects[i] = Instantiate(waterMesh, Vector3.zero, Quaternion.identity) as GameObject;
            meshObjects[i].GetComponent<MeshFilter>().mesh = meshes[i];
            meshObjects[i].transform.parent = transform;

            colliders[i] = new GameObject();
            colliders[i].name = "Trigger";
            colliders[i].AddComponent<BoxCollider2D>();
            colliders[i].transform.parent = transform;
            colliders[i].transform.position = new Vector3(Left + width * (i + 0.5f) / edgecount, top - 0.5f, 0);
            colliders[i].transform.localScale = new Vector3(width / edgecount, 1, 1);
            colliders[i].GetComponent<BoxCollider2D>().isTrigger = true;
            //colliders[i].AddComponent<>();
        }
    }


}
