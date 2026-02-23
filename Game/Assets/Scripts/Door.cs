using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool Open {get=>_open; set{_open=value; animator.SetBool("Open", value);}}
    [SerializeField] Animator animator;
    [SerializeField] bool _open;
    [SerializeField] int sceneIndex; //Buildindex of the scene that loads when entering the door
    [SerializeField] Terminal.Cutscene cutscene = Terminal.Cutscene.Select; //Alternatively play a cutscene upon entering
    [SerializeField] BoxCollider2D doorCollider;
    bool entered = false;
    float cooldown = 0;

    void Awake()
    {
        Open = _open;
    }
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (doorCollider.IsTouchingLayers(LayerManager.PlayerLayer) && !entered && cooldown <= 0)
        {
            if (!_open)
            {
                PlayerController player = FindAnyObjectByType<PlayerController>();
                if (player.Key.activeSelf)
                {
                    Open = true;
                    cooldown = 2.5f;
                    player.Key.SetActive(false);
                    return;
                }
                else
                {
                    return;
                }
            }
            StartCoroutine(Enter());
            entered = true;
        }
    }
    IEnumerator Enter()
    {
        yield return new WaitForSeconds(0.5f);
        if(cutscene != Terminal.Cutscene.Select){ Terminal.PlayCutscene(cutscene);}
        if(sceneIndex != -1){ SceneManager.LoadScene(sceneIndex); }
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
