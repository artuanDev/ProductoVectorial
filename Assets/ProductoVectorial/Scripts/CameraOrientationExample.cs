using TMPro;
using UnityEngine;

[ExecuteAlways]
public class CameraOrientationExample : MonoBehaviour
{
    public GameObject Target;
    public Vector3 WorldUp = Vector3.up;
    public float VectorLength = 2.0f;
    public TextMeshProUGUI debugText;

    public Vector3 Forward { get; private set; }
    public Vector3 Right { get; private set; }
    public Vector3 Up { get; private set; }
    public Matrix4x4 Orientation { get; private set; }
    public bool HasValidOrientation { get; private set; }

    private void OnEnable()
    {
        CalculateOrientation();
    }

    private void OnValidate()
    {
        CalculateOrientation();
    }

    private void Update()
    {
        CalculateOrientation();
    }

    private void CalculateOrientation()
    {
        if (Target == null || WorldUp == Vector3.zero)
            return;

        Forward = (Target.transform.position - transform.position).normalized;

        Vector3 cross = Vector3.Cross(WorldUp.normalized, Forward);
        HasValidOrientation = Forward != Vector3.zero && cross.sqrMagnitude > 0.0001f;

        if (!HasValidOrientation)
        {
            Right = Vector3.zero;
            Up = Vector3.zero;

            if (debugText != null)
                debugText.text = "CROSS PRODUCT = ZERO\nFORWARD AND REFERENCE UP ARE PARALLEL";

            return;
        }

        Right = cross.normalized;
        Up = Vector3.Cross(Forward, Right).normalized;

        Orientation = Matrix4x4.identity;
        Orientation.SetColumn(0, Right);
        Orientation.SetColumn(1, Up);
        Orientation.SetColumn(2, Forward);
        transform.rotation = Orientation.rotation;

        if (debugText != null)
        {
            debugText.text =
                "RIGHT = CROSS(REFERENCE UP, FORWARD)\n" +
                "UP = CROSS(FORWARD, RIGHT)";
        }
    }
}
