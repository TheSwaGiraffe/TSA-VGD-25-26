using UnityEngine;

public class Door : MonoBehaviour
{
    public bool Open {get=>_open; set{_open=value; animator.SetBool("Open", value);}}
    [SerializeField] Animator animator;
    [SerializeField] bool _open;

    void OnValidate()
    {
        animator.SetBool("Open", _open);
    }
}
