using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegacyFieldOfView : MonoBehaviour
{
    Mesh fovMesh;
    [SerializeField] private LayerMask fovMask;

    private void Start()
    {   fovMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = fovMesh;
        GetComponent<MeshRenderer>().sortingOrder = 50;
    }

    private void Update()
    {
        float fov = 90f;
        int rayCount = 20;
        float angle = 0f;
        float angleIncrease = fov / rayCount;
        float viewDistance = 10f;
        Vector3 origin = Vector3.zero;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;

            RaycastHit2D hit = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance,fovMask);
            if (hit.collider == null)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = hit.point;
            }
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncrease;
        }

        fovMesh.vertices = vertices;
        fovMesh.uv = uv;
        fovMesh.triangles = triangles;

        fovMesh.RecalculateBounds();
    }


    public Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI/180);
        return new Vector3(Mathf.Cos(angleRad),Mathf.Sin(angleRad));
    }


}
