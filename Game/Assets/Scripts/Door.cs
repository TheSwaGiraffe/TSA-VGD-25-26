using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
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
    bool entered = false;

    void Awake()
    {
        Open = _open;
    }
    void Update()
    {
        if(entered){Debug.Log("hi");}
        if (doorCollider.IsTouchingLayers(playerLayer) && !entered)
        {
            if (!_open)
            {
                PlayerController player = FindAnyObjectByType<PlayerController>();
                if (player.Key.activeSelf)
                {
                    Open = true;
                }
                else
                {
                    return;
                }
            }
            StartCoroutine(LoadNextScene(sceneIndex));
            entered = true;
        }
    }
    IEnumerator LoadNextScene(int index)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
#if UNITY_EDITOR
    void OnValidate()
    {
        if(animator.isActiveAndEnabled){
            Open = Open;
        }
    }
#endif
}
