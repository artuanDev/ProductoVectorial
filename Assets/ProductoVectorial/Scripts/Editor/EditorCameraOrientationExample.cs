using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class EditorCameraOrientationExample : Editor
{
    static EditorCameraOrientationExample()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        CameraOrientationExample[] examples =
            Object.FindObjectsByType<CameraOrientationExample>();

        foreach (CameraOrientationExample example in examples)
        {
            if (example.Target == null)
                continue;

            DrawOrientation(example);
        }
    }

    private static void DrawOrientation(CameraOrientationExample example)
    {
        Vector3 start = example.transform.position;
        float length = example.VectorLength;

        GUIStyle labelStyle = new GUIStyle(EditorStyles.boldLabel);
        labelStyle.fontSize = 20;
        labelStyle.normal.textColor = Color.white;

        Vector3 referenceUp = example.WorldUp.normalized;

        Handles.color = Color.yellow;
        Handles.DrawDottedLine(start, start + referenceUp * length, 4.0f);
        Handles.Label(start + referenceUp * length, "REFERENCE UP", labelStyle);

        Handles.color = Color.blue;
        Handles.DrawLine(start, start + example.Forward * length, 8.0f);
        Handles.Label(start + example.Forward * length, "FORWARD", labelStyle);

        if (!example.HasValidOrientation)
        {
            labelStyle.normal.textColor = Color.red;
            Handles.Label(start + example.Forward * length * 0.5f,
                "CROSS = ZERO", labelStyle);
            return;
        }

        Handles.color = Color.red;
        Handles.DrawLine(start, start + example.Right * length, 8.0f);
        Handles.Label(start + example.Right * length, "RIGHT", labelStyle);

        Handles.color = Color.green;
        Handles.DrawLine(start, start + example.Up * length, 8.0f);
        Handles.Label(start + example.Up * length, "REAL UP", labelStyle);
    }
}
