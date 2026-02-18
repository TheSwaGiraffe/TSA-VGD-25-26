using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] ColColor color;
    public bool Open { get => _open; set { _open = value; animator.SetBool("Open", value); } }
    [SerializeField] Portal TeleportTo;
    [SerializeField] Animator animator;
    [SerializeField] bool _open;
    [SerializeField] BoxCollider2D doorCollider;
    [SerializeField] bool isVerticalPortal = false;
    List<Collider2D> Right = new List<Collider2D>();
    List<Collider2D> Left = new List<Collider2D>();
    List<Collider2D> Up = new List<Collider2D>();
    List<Collider2D> Down = new List<Collider2D>();
    bool entered = false;

    void Awake()
    {
        Open = _open;
    }
    void Update()
    {
        if (doorCollider.IsTouchingLayers(LayerManager.PlayerLayer) && !entered)
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

        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (Teleportable.cols.Contains(other))
        {
            Teleportable teleportable = other.GetComponent<Teleportable>();

            if (teleportable.color != color) { return; }
            if (other.transform.position.x > transform.position.x) { Right.Add(other); }
            else { Left.Add(other); }
            if (other.transform.position.y > transform.position.y) { Up.Add(other); }
            else { Down.Add(other); }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (Right.Contains(other) && !isVerticalPortal)
        {
            if(other.transform.position.x < transform.position.x)
            {
                Teleport(other.GetComponent<Teleportable>());
                return;
            }
        }
        if (Left.Contains(other) && !isVerticalPortal)
        {
            if(other.transform.position.x > transform.position.x)
            {
                Teleport(other.GetComponent<Teleportable>());
                return;
            }
        }
        if (Up.Contains(other) && isVerticalPortal)
        {
            if(other.transform.position.y < transform.position.y)
            {
                Teleport(other.GetComponent<Teleportable>());
                return;
            }
        }
        if (Down.Contains(other) && isVerticalPortal)
        {
            if(other.transform.position.y > transform.position.y)
            {
                Teleport(other.GetComponent<Teleportable>());
                return;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(Right.Contains(other)){Right.Remove(other);}
        if(Left.Contains(other)){Left.Remove(other);}
        if(Up.Contains(other)){Up.Remove(other);}
        if(Down.Contains(other)){Down.Remove(other);}
    }
    void Teleport(Teleportable teleportable)
    {
        Vector3 teleportVector = new Vector3(TeleportTo.transform.position.x - transform.position.x, TeleportTo.transform.position.y - transform.position.y, 0);
        teleportable.transform.position += teleportVector;
        teleportable.color = TeleportTo.color;
    }
#if UNITY_EDITOR
    void OnValidate()
    {
        if (animator.isActiveAndEnabled)
        {
            Open = Open;
        }
    }
#endif
}
