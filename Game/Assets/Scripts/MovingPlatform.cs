using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] bool BounceOffWall = true;
    [SerializeField] Vector2 StartVelocity;
    [SerializeField] Vector2Int Size;
    public bool isRed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] bool update = false;
    [SerializeField] Transform[] Pieces;
    [SerializeField] SpriteRenderer[] PieceSpriteRenderers;
    [SerializeField] Sprite[] On;
    [SerializeField] Sprite[] Off;
    public BoxCollider2D col;
    [SerializeField] BoxCollider2D fwdHitbox;
    [SerializeField] BoxCollider2D bwdHitbox;
    [SerializeField] BoxCollider2D upwHitbox;
    [SerializeField] BoxCollider2D dwnHitbox;
    bool wasTouchingPlayer = false;
    public bool active { get=>_active; set => setActive(value);}
    bool _active = true;
    void Start()
    {
        List<MovingPlatform> platforms = RedBlueUpdater.Instance.movingPlatforms;
        gameObject.layer = LayerManager.GetLayerIndex(isRed? "Red" : "Blue");
        fwdHitbox.gameObject.layer = LayerManager.GetLayerIndex(isRed? "IgnoreBlue" : "IgnoreRed");
        bwdHitbox.gameObject.layer = LayerManager.GetLayerIndex(isRed? "IgnoreBlue" : "IgnoreRed");
        upwHitbox.gameObject.layer = LayerManager.GetLayerIndex(isRed? "IgnoreBlue" : "IgnoreRed");
        dwnHitbox.gameObject.layer = LayerManager.GetLayerIndex(isRed? "IgnoreBlue" : "IgnoreRed");
        platforms.Add(this);
        rb.linearVelocity = StartVelocity;
    }
    void setActive(bool value)
    {
        _active = value;
        Sprite[] newSprites = active? On : Off;
        for(int i = 0; i < Pieces.Length; i++)
        {
            PieceSpriteRenderers[i].sprite = newSprites[i];
        }
    }
    void updatePlatform()
    {
        col.size = new Vector2(Size.x, Size.y)*0.25f;
        fwdHitbox.size = new Vector2(Size.x/2, Size.y-0.5f)*0.25f;
        bwdHitbox.size = fwdHitbox.size;
        upwHitbox.size = new Vector2(Size.x-0.5f, Size.y/2)*0.25f;
        dwnHitbox.size = upwHitbox.size;


        float halfX = Size.x*0.25f/2f-0.125f;
        float halfY = Size.y*0.25f/2f-0.125f;

        UpdPos(Pieces[0], -halfX, halfY);
        UpdPos(Pieces[2], halfX, halfY);
        UpdPos(Pieces[6], -halfX, -halfY);
        UpdPos(Pieces[8], halfX, -halfY);

        UpdPos(Pieces[1], 0, halfY);
        UpdPos(Pieces[3], -halfX, 0);
        UpdPos(Pieces[5], halfX, 0);
        UpdPos(Pieces[7], 0, -halfY);

        float quarX = Size.x*0.0625f;
        float quarY = Size.y*0.0625f;
        UpdPos(fwdHitbox.transform, quarX, 0);
        UpdPos(bwdHitbox.transform, -quarX, 0);
        UpdPos(upwHitbox.transform, 0, quarY);
        UpdPos(dwnHitbox.transform, 0, -quarY);

        PieceSpriteRenderers[1].size = new Vector2(Size.x-2, 1);
        PieceSpriteRenderers[3].size = new Vector2(1, Size.y-2);
        PieceSpriteRenderers[5].size = new Vector2(1, Size.y-2);
        PieceSpriteRenderers[7].size = new Vector2(Size.x-2, 1);
        
        Pieces[4].localScale = new Vector2(Size.x-2, Size.y-2)*0.25f;

        foreach(SpriteRenderer r in PieceSpriteRenderers)
        {
            r.color = isRed? new Color(1, 0, 0) : new Color(0, 0, 1);
        }
        void UpdPos(Transform t, float x, float y)
        {
            t.localPosition = new Vector3(x, y, t.position.z);
        }
    }
    public void MovePlatform(Vector2 amt)
    {
        rb.linearVelocity = amt;
    }
    void FixedUpdate()
    {
        CollideWall();
        if (col.IsTouchingLayers(LayerManager.PlayerLayer))
        {
            wasTouchingPlayer = true;
            PlayerController.Instance.velocityOffset = rb.linearVelocity;
            Debug.Log($"{PlayerController.Instance.name} velOffset: {rb.linearVelocity}");
        }
        else
        {
            if (wasTouchingPlayer)
            {
                PlayerController.Instance.velocityOffset = Vector2.zero;
            }
        }
    }
    void CollideWall()
    {
        BoxCollider2D Xhitbox = rb.linearVelocityX > 0 ? fwdHitbox : bwdHitbox;
        if (Xhitbox.IsTouching(RedBlueUpdater.Instance.GreenCol) || Xhitbox.IsTouching(RedBlueUpdater.Instance.RedCol) || Xhitbox.IsTouching(RedBlueUpdater.Instance.BlueCol))
        {
            rb.linearVelocityX*=-1;
            if(!BounceOffWall){
                rb.linearVelocityX = 0;
            }
        }
        BoxCollider2D Yhitbox = rb.linearVelocityY > 0 ? upwHitbox : dwnHitbox;
        if (Yhitbox.IsTouching(RedBlueUpdater.Instance.GreenCol) || Xhitbox.IsTouching(RedBlueUpdater.Instance.RedCol) || Xhitbox.IsTouching(RedBlueUpdater.Instance.BlueCol))
        {
            rb.linearVelocityY*=-1;
            if(!BounceOffWall){
                rb.linearVelocityY = 0;
            }
        }
    }
#if UNITY_EDITOR
    void OnValidate()
    {
        active = true;
        if(update){
            updatePlatform();
            setActive(active);
            update = false;
        }
    }
    #endif
}