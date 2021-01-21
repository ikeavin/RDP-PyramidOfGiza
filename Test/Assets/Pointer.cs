using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public List<Vector3> points;
    public List<Vector3> linePoints;
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        points = new List<Vector3>();
        linePoints = new List<Vector3>();
        points.Clear();
        linePoints.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        transform.Translate(Vector3.up * Time.deltaTime * speed * verticalInput);

        if (Input.GetKeyDown(KeyCode.F))
        {
            points.Add(transform.position);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            linePoints.Add(transform.position);
        }

        if(linePoints.Count == 2)
        {
            GameObject newLine = new GameObject("Line");
            LineRenderer lineRenderer = newLine.AddComponent<LineRenderer>();
            lineRenderer.SetPositions(linePoints.ToArray());
            lineRenderer.endWidth = .05f;
            lineRenderer.startWidth = .05f;
            linePoints.Clear();
        }

        if (points.Count == 4)
        {


            //Add all points together to make a plane
            GameObject newMesh = new GameObject("Generated Shape");
            MeshRenderer meshRenderer = newMesh.AddComponent<MeshRenderer>();
            meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));

            MeshFilter meshFilter = newMesh.AddComponent<MeshFilter>();

            Mesh mesh = new Mesh();

            Vector3[] vertices = points.ToArray();
            vertices = OrganizePoints(vertices);
            points.Clear();
            mesh.vertices = vertices;

            int[] tris = new int[6]
            {
                // lower left triangle
                0, 2, 1,
                // upper right triangle
                2, 3, 1
            };
            mesh.triangles = tris;
            mesh.RecalculateNormals();

            Vector2[] uv = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(1, 1)
            };
            mesh.uv = uv;

            meshFilter.mesh = mesh;

        }
    }

    private Vector3[] OrganizePoints(Vector3[] array)
    {

        int lowerLeft = 0;
        int upperRight = 3;
        int otherA = -1;
        int otherB = -1;

        for(int i = 0; i < array.Length; i++)
        {
            if(array[i].x <= array[lowerLeft].x && array[i].y <= array[lowerLeft].y && array[i].z <= array[lowerLeft].z)
            {
                lowerLeft = i;
            }

            if (array[i].x >= array[upperRight].x && array[i].y >= array[upperRight].y && array[i].z >= array[upperRight].z)
            {
                upperRight = i; 
            }
        }

        for(int i = 0; i < array.Length; i++)
        {
            if(lowerLeft != i && upperRight != i && otherA == -1)
            {
                otherA = i;
            }
            else if(lowerLeft != i && upperRight != i && otherB == -1)
            {
                otherB = i;
            }
        }

        if(array[otherA].x < array[otherB].x)
        {
            int t = otherA;
            otherA = otherB;
            otherB = t;
        }
        Vector3[] newVec = { array[lowerLeft], array[otherA], array[otherB], array[upperRight] };

        return newVec;
    }
}
