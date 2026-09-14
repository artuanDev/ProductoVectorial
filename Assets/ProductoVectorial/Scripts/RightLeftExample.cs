using TMPro;
using UnityEngine;

public class RightLeftExample : MonoBehaviour
{
    public GameObject Target;
    [HideInInspector]
    public GameObject self => gameObject;

    public TextMeshProUGUI debugText;
}
