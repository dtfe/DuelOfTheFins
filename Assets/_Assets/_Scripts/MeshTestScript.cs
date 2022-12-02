using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTestScript : MonoBehaviour
{
    public float radiusOfSphere;
    public int numberOfVertices;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            FindObjectOfType<CircleMeshScript>().PolyMesh(radiusOfSphere, numberOfVertices);
        }
    }
}
