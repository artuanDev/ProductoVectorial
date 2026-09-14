using UnityEngine;

public class RightLeftExample : MonoBehaviour
{
    public GameObject Target;
    [HideInInspector]
    public GameObject self => gameObject;
    private void OnValidate()
    {
        Debug.Log("hey");
    }
}
