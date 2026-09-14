using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class EditorMeshNormalsExample : Editor
{
    static EditorMeshNormalsExample()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        MeshNormalsExample[] examples =
            Object.FindObjectsByType<MeshNormalsExample>();

        foreach (MeshNormalsExample example in examples)
        {
            if (example.Vertices == null || example.Triangles == null ||
                example.FaceNormals == null || example.VertexNormals == null)
                continue;

            DrawCrossProduct(example);
            DrawNormal(example);
        }
    }

    private static void DrawCrossProduct(MeshNormalsExample example)
    {
        int indexA = example.Triangles[0];
        int indexB = example.Triangles[1];
        int indexC = example.Triangles[2];

        Vector3 pointA = example.transform.TransformPoint(example.Vertices[indexA]);
        Vector3 pointB = example.transform.TransformPoint(example.Vertices[indexB]);
        Vector3 pointC = example.transform.TransformPoint(example.Vertices[indexC]);

        Handles.color = Color.blue;
        Handles.DrawLine(pointA, pointB, 8.0f);

        Handles.color = Color.red;
        Handles.DrawLine(pointA, pointC, 8.0f);

        GUIStyle labelStyle = new GUIStyle(EditorStyles.boldLabel);
        labelStyle.fontSize = 24;
        labelStyle.normal.textColor = Color.white;

        Handles.Label(pointA, "A", labelStyle);
        Handles.Label(pointB, "B", labelStyle);
        Handles.Label(pointC, "C", labelStyle);
    }

    private static void DrawNormal(MeshNormalsExample example)
    {
        Handles.color = Color.green;

        Vector3 start = example.transform.TransformPoint(example.FaceCenters[0]);
        Vector3 normal = example.transform.TransformDirection(example.FaceNormals[0]);
        Handles.DrawLine(start, start + normal * example.NormalLength, 5.0f);
    }
}
