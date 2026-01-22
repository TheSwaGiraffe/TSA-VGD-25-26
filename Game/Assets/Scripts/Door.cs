using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool Open {get=>_open; set{_open=value; animator.SetBool("Open", value);}}
    [SerializeField] Animator animator;
    [SerializeField] bool _open;
    [SerializeField] int sceneIndex; //Buildindex of the scene that loads when entering the door
    [SerializeField] BoxCollider2D doorCollider;
    [SerializeField] LayerMask playerLayer;

    void Awake()
    {
        Open = _open;
    }
    void Update()
    {
        if (doorCollider.IsTouchingLayers(playerLayer) && _open)
        {
            SceneManager.LoadSceneAsync(sceneIndex);
        }
    }
}
