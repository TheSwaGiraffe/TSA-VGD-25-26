using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] bool BounceOffWall = true;
    [SerializeField] bool LoopPath = false;
    [SerializeField] Transform[] PathPoints;
    [SerializeField] Vector2Int Size;
    public float velocity;
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
    int pointIndex;
    public bool active { get=>_active; set => setActive(value);}
    bool _active = true;
    Vector2 netMovement = Vector2.zero;
    void Start()
    {
        List<MovingPlatform> platforms = RedBlueUpdater.Instance.movingPlatforms;
        gameObject.layer = LayerManager.GetLayerIndex(isRed? "Red" : "Blue");
        fwdHitbox.gameObject.layer = LayerManager.GetLayerIndex(isRed? "IgnoreBlue" : "IgnoreRed");
        bwdHitbox.gameObject.layer = LayerManager.GetLayerIndex(isRed? "IgnoreBlue" : "IgnoreRed");
        upwHitbox.gameObject.layer = LayerManager.GetLayerIndex(isRed? "IgnoreBlue" : "IgnoreRed");
        dwnHitbox.gameObject.layer = LayerManager.GetLayerIndex(isRed? "IgnoreBlue" : "IgnoreRed");
        platforms.Add(this);
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
    void Update()
    {
        if(velocity == 0){return;}
        Vector2 rbPosOld = rb.position;
        rb.position = Vector2.MoveTowards(rb.position, PathPoints[pointIndex].position, Mathf.Abs(velocity) * Time.deltaTime);
        netMovement += rb.position - rbPosOld;
        if(rb.position.x == PathPoints[pointIndex].position.x && rb.position.y == PathPoints[pointIndex].position.y)
        {
            int direction = velocity > 0 ? 1 : -1;
            pointIndex += direction;
            if(direction == 1 && pointIndex >= PathPoints.Length)
            {
                if (LoopPath)
                {
                    pointIndex = 0;
                    return;
                }
                if (BounceOffWall)
                {
                    velocity *=-1;
                    pointIndex = PathPoints.Length-2;
                    return;
                }
                velocity = 0;
            }
            if(direction == -1 && pointIndex < 0)
            {
                if (LoopPath)
                {
                    pointIndex = PathPoints.Length-1;
                    return;
                }
                if (BounceOffWall)
                {
                    velocity *= -1;
                    pointIndex = 1;
                    return;
                }
                velocity = 0;
            }
        }
    }
    void FixedUpdate()
    {
        CollideWall();
        if (col.IsTouchingLayers(LayerManager.PlayerLayer))
        {
            PlayerController.Instance.rb.position += netMovement;
            Debug.Log($"{PlayerController.Instance.name} velOffset: {rb.linearVelocity}");
        }
        netMovement = Vector2.zero;
    }
    void Flip()
    {
        int direction = velocity > 0 ? 1 : -1;
        pointIndex -= direction;
        velocity *= -1;
    }
    void CollideWall()
    {
        BoxCollider2D Xhitbox = netMovement.x > 0 ? fwdHitbox : bwdHitbox;
        if (Xhitbox.IsTouching(RedBlueUpdater.Instance.GreenCol) || Xhitbox.IsTouching(RedBlueUpdater.Instance.RedCol) || Xhitbox.IsTouching(RedBlueUpdater.Instance.BlueCol))
        {
            Flip();
            if(!BounceOffWall){
                velocity = 0;
            }
            return;
        }
        BoxCollider2D Yhitbox = netMovement.y > 0 ? upwHitbox : dwnHitbox;
        if (Yhitbox.IsTouching(RedBlueUpdater.Instance.GreenCol) || Xhitbox.IsTouching(RedBlueUpdater.Instance.RedCol) || Xhitbox.IsTouching(RedBlueUpdater.Instance.BlueCol))
        {
            Flip();
            if(!BounceOffWall){
                velocity = 0;
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