using TMPro;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshNormalsExample : MonoBehaviour
{
    public bool ReverseWinding;
    public float NormalLength = 0.6f;
    public TextMeshProUGUI debugText;

    public Vector3[] Vertices { get; private set; }
    public int[] Triangles { get; private set; }
    public Vector3[] FaceCenters { get; private set; }
    public Vector3[] FaceNormals { get; private set; }
    public Vector3[] VertexNormals { get; private set; }

    private Mesh generatedMesh;

    private void OnEnable()
    {
        BuildMesh();
    }

    private void OnValidate()
    {
        BuildMesh();
    }

    private void BuildMesh()
    {
        Vertices = new[]
        {
            new Vector3(-1.0f, 0.0f, -0.7f),
            new Vector3( 1.0f, 0.0f, -0.7f),
            new Vector3( 0.0f, 0.0f,  1.0f),
            new Vector3( 0.0f, 1.5f,  0.0f)
        };

        Triangles = new[]
        {
            0, 3, 1,
            1, 3, 2,
            2, 3, 0,
            0, 1, 2
        };

        if (ReverseWinding)
        {
            for (int i = 0; i < Triangles.Length; i += 3)
                (Triangles[i + 1], Triangles[i + 2]) =
                    (Triangles[i + 2], Triangles[i + 1]);
        }

        CalculateNormals();

        if (generatedMesh == null)
        {
            generatedMesh = new Mesh
            {
                name = "Cross Product Tetrahedron",
                hideFlags = HideFlags.DontSave
            };
        }

        Vector3[] flatVertices = new Vector3[Triangles.Length];
        Vector3[] flatNormals = new Vector3[Triangles.Length];
        int[] flatTriangles = new int[Triangles.Length];

        for (int triangle = 0; triangle < Triangles.Length; triangle++)
        {
            int face = triangle / 3;
            flatVertices[triangle] = Vertices[Triangles[triangle]];
            flatNormals[triangle] = FaceNormals[face];
            flatTriangles[triangle] = triangle;
        }

        generatedMesh.Clear();
        generatedMesh.vertices = flatVertices;
        generatedMesh.triangles = flatTriangles;
        generatedMesh.normals = flatNormals;
        generatedMesh.RecalculateBounds();

        GetComponent<MeshFilter>().sharedMesh = generatedMesh;

        if (debugText != null)
        {
            debugText.text = ReverseWinding
                ? "(B - A) x (C - A)\nGREEN: FACE NORMAL\nINWARD (REVERSED WINDING)"
                : "(B - A) x (C - A)\nGREEN: FACE NORMAL\nOUTWARD";
        }
    }

    private void CalculateNormals()
    {
        int faceCount = Triangles.Length / 3;
        FaceCenters = new Vector3[faceCount];
        FaceNormals = new Vector3[faceCount];
        VertexNormals = new Vector3[Vertices.Length];

        for (int face = 0; face < faceCount; face++)
        {
            int triangle = face * 3;
            int indexA = Triangles[triangle];
            int indexB = Triangles[triangle + 1];
            int indexC = Triangles[triangle + 2];

            Vector3 pointA = Vertices[indexA];
            Vector3 pointB = Vertices[indexB];
            Vector3 pointC = Vertices[indexC];

            Vector3 edgeAB = pointB - pointA;
            Vector3 edgeAC = pointC - pointA;
            Vector3 faceNormal = Vector3.Cross(edgeAB, edgeAC).normalized;

            FaceCenters[face] = (pointA + pointB + pointC) / 3.0f;
            FaceNormals[face] = faceNormal;

            VertexNormals[indexA] += faceNormal;
            VertexNormals[indexB] += faceNormal;
            VertexNormals[indexC] += faceNormal;
        }

        for (int vertex = 0; vertex < VertexNormals.Length; vertex++)
            VertexNormals[vertex].Normalize();
    }
}
