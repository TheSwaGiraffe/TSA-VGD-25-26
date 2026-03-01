using System.Collections;
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
    [SerializeField] SpriteRenderer ren;
    float cooldown = 0;

    void Awake()
    {
        Open = _open;
    }
    void Update()
    {
        if(!Open || cooldown > 0)
        {
            ren.color = Color.white;
        }
        else
        {
            ren.color = color == ColColor.Red ? ColorManager.Red : ColorManager.Blue;
            if(color == ColColor.White){ ren.color = ColorManager.White;}
        }
        cooldown -= Time.deltaTime;
        if (doorCollider.IsTouchingLayers(LayerManager.PlayerLayer) && cooldown <= 0 && !_open)
        {
            PlayerController player = FindAnyObjectByType<PlayerController>();
            if (player.Key.activeSelf)
            {
                Open = true;
                TeleportTo.Open = true;
                SoundPlayer.SFXPlayer.PlaySound(3);
                cooldown = 2.5f;
                player.Key.SetActive(false);
            }
            else
            {
                return;
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
                StartCoroutine(Teleport(other.GetComponent<Teleportable>(), Direction.Left));
                return;
            }
        }
        if (Left.Contains(other) && !isVerticalPortal)
        {
            if(other.transform.position.x > transform.position.x)
            {
                StartCoroutine(Teleport(other.GetComponent<Teleportable>(), Direction.Right));
                return;
            }
        }
        if (Up.Contains(other) && isVerticalPortal)
        {
            if(other.transform.position.y < transform.position.y)
            {
                StartCoroutine(Teleport(other.GetComponent<Teleportable>(), Direction.Down));
                return;
            }
        }
        if (Down.Contains(other) && isVerticalPortal)
        {
            if(other.transform.position.y > transform.position.y)
            {
                StartCoroutine(Teleport(other.GetComponent<Teleportable>(), Direction.Up));
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
    IEnumerator Teleport(Teleportable teleportable, Direction dir)
    {
        if(!Open || cooldown > 0){yield break;}
        Vector2 teleportVector = new Vector2(TeleportTo.transform.position.x - transform.position.x, TeleportTo.transform.position.y - transform.position.y);
        teleportable.rb.position += teleportVector;
        teleportable.color = TeleportTo.color;
        foreach (Collider2D col in Teleportable.cols)
        {
            if(teleportable.col == col){continue;}
            ColliderDistance2D dist = teleportable.col.Distance(col);
            while (dist.isOverlapped)
            {
                Vector2 moveDir = new Vector2();
                switch (dir)
                {
                    case Direction.Up:
                        moveDir = Vector2.up;
                        break;
                    case Direction.Down:
                        moveDir = Vector2.down;
                        break;
                    case Direction.Left:
                        moveDir = Vector2.left;
                        break;
                    case Direction.Right:
                        moveDir = Vector2.right;
                        break;
                }
                Debug.Log($"{teleportable.gameObject.name} was teleported into {col.gameObject.name}");
                col.transform.GetComponent<Rigidbody2D>().position -= moveDir * dist.distance;
                Debug.Log(123456);
                dist = teleportable.col.Distance(col);
                yield return new WaitForFixedUpdate();
            }
        }
    }
#if UNITY_EDITOR
    void OnValidate()
    {
        if (animator.isActiveAndEnabled)
        {
            Open = Open;
        }
        ren.color = color == ColColor.Red ? ColorManager.Red : ColorManager.Blue;
        if(color == ColColor.White){ ren.color = ColorManager.White;}
    }
#endif
}
public enum Direction
{
    Up,
    Down,
    Left,
    Right
}