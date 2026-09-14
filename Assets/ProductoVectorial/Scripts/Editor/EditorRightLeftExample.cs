using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class EditorRightLeftExample : Editor
{
    static EditorRightLeftExample()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        RightLeftExample[] examples =
            Object.FindObjectsByType<RightLeftExample>();

        Handles.color = Color.blue;

        foreach (RightLeftExample example in examples)
        {
            if (example == null || example.self == null)
                continue;

            Vector3 start = example.self.transform.position;
            Vector3 end = start + example.transform.forward * 4.0f;

            Handles.DrawLine(start, end, 10.0f);

            Vector3 targetPos = example.Target.transform.position;

            Handles.color = Color.red;
            Vector3 targetEnd = Vector3.Normalize(targetPos - start);
            Handles.DrawLine(start, start + (targetEnd * 4), 10.0f);

            Handles.color = Color.green;

            Vector3 endY = Vector3.Cross(example.transform.forward,
                targetEnd);

            if (endY.y == 0) return;

            Handles.DrawLine(start, start + (endY * 4), 10.0f);

            if(endY.y < 0)
            {
                Debug.Log("Target is at the left");
            }
            else
            {
                Debug.Log("Target is at the right");
            }
        }
    }
}
