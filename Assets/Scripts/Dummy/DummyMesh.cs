using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMesh : MonoBehaviour
{
    MeshFilter meshFilter;

    private void Awake()
    {
        Mesh myMesh = new Mesh();

        Vector3[] myVertices = new Vector3[4];
        Vector2[] myUV = new Vector2[4];
        int[] myTriangle = new int[6];

        myVertices[0] = new Vector2(0,0);
        myVertices[1] = new Vector2(0, 5);
        myVertices[2] = new Vector2(5, 5);
        myVertices[3] = new Vector2(5, 0);

        myUV[0] = new Vector2(0,0);
        myUV[1] = new Vector2(0, 1);
        myUV[2] = new Vector2(1, 1);
        myUV[3] = new Vector2(1, 0);

        myTriangle[0] = 0;
        myTriangle[1] = 1;
        myTriangle[2] = 2;

        myTriangle[3] = 0;
        myTriangle[4] = 2;
        myTriangle[5] = 3;

        myMesh.vertices = myVertices;
        myMesh.uv = myUV;
        myMesh.triangles = myTriangle;

        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = myMesh;
    }
}