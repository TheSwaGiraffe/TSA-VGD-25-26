using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraInstance : MonoBehaviour
{
    void Start()
    {
        Terminal.Instance.Canvas.worldCamera = GetComponent<Camera>();
    }
}
