using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] bool BounceOffWall = true;
    [Tooltip("A corner of the bounding box in which the moving platform will stay inside")]
    [SerializeField] Transform Corner1;
    [Tooltip("A corner of the bounding box in which the moving platform will stay inside")]
    [SerializeField] Transform Corner2;
    [SerializeField] Vector2 StartVelocity;
    [SerializeField] Vector2Int Size;
    public PlatformColor color;
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
    [SerializeField] BoxCollider2D noOverlapRedBlue;
    bool wasTouchingPlayer = false;
    public bool active { get=>_active; set => setActive(value);}
    public bool isRed {get=> color == PlatformColor.Red;}
    bool _active = true;
    void Start()
    {
        if(color == PlatformColor.Red || color == PlatformColor.Blue){
            Destroy(noOverlapRedBlue.gameObject);
            gameObject.layer = LayerManager.GetLayerIndex(isRed? "Red" : "Blue");
            fwdHitbox.gameObject.layer = LayerManager.GetLayerIndex(isRed? "IgnoreBlue" : "IgnoreRed");
            bwdHitbox.gameObject.layer = LayerManager.GetLayerIndex(isRed? "IgnoreBlue" : "IgnoreRed");
            upwHitbox.gameObject.layer = LayerManager.GetLayerIndex(isRed? "IgnoreBlue" : "IgnoreRed");
            dwnHitbox.gameObject.layer = LayerManager.GetLayerIndex(isRed? "IgnoreBlue" : "IgnoreRed");
            RedBlueUpdater.Instance.redBluePlatforms.Add(this);
        }
        else if(color == PlatformColor.Green)
        {
            gameObject.layer = LayerManager.GetLayerIndex("Default");
            fwdHitbox.gameObject.layer = LayerManager.GetLayerIndex("Default");
            bwdHitbox.gameObject.layer = LayerManager.GetLayerIndex("Default");
            upwHitbox.gameObject.layer = LayerManager.GetLayerIndex("Default");
            dwnHitbox.gameObject.layer = LayerManager.GetLayerIndex("Default");
        }
        else if(color == PlatformColor.White)
        {
            gameObject.layer = LayerManager.GetLayerIndex("White");
            fwdHitbox.gameObject.layer = LayerManager.GetLayerIndex("White");
            bwdHitbox.gameObject.layer = LayerManager.GetLayerIndex("White");
            upwHitbox.gameObject.layer = LayerManager.GetLayerIndex("White");
            dwnHitbox.gameObject.layer = LayerManager.GetLayerIndex("White");
        }
        rb.linearVelocity = StartVelocity;
    }
    void setActive(bool value)
    {
        if(color == PlatformColor.White || color == PlatformColor.Green){return;}
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
        noOverlapRedBlue.size = col.size;
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
            r.color = isRed? ColorManager.Red : ColorManager.Blue;
            if(color == PlatformColor.White){r.color = ColorManager.White;}
            if(color == PlatformColor.Green){r.color = ColorManager.Green;}
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
        CollideBoundingBox();
        if (col.IsTouchingLayers(LayerManager.PlayerLayer))
        {
            wasTouchingPlayer = true;
            PlayerController.Instance.velocityOffset = rb.linearVelocity;
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
        if (TouchingWall(Xhitbox))
        {
            rb.linearVelocityX*=-1;
            if(!BounceOffWall){
                rb.linearVelocityX = 0;
            }
        }
        BoxCollider2D Yhitbox = rb.linearVelocityY > 0 ? upwHitbox : dwnHitbox;
        if (TouchingWall(Yhitbox))
        {
            rb.linearVelocityY*=-1;
            if(!BounceOffWall){
                rb.linearVelocityY = 0;
            }
        }
        bool TouchingWall(Collider2D hitbox)
        {
            foreach(Collider2D wall in LayerManager.Instance.Walls)
            {
                if(hitbox.IsTouching(wall)){ return true; }
            }
            return false;
        }
    }
    void CollideBoundingBox()
    {
        if(!Corner1 || !Corner2) { return; }
        Vector3 c1 = Corner1.position;
        Vector3 c2 = Corner2.position;
        float maxX = Mathf.Max(c1.x, c2.x);
        float minX = Mathf.Min(c1.x, c2.x);
        float maxY = Mathf.Max(c1.y, c2.y);
        float minY = Mathf.Min(c1.y, c2.y);

        if(transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
            if(BounceOffWall) {rb.linearVelocityX = Mathf.Abs(rb.linearVelocityX)*-1;}
            else if (rb.linearVelocityX > 0)
            {
                rb.linearVelocityX = 0;
            }
        }
        if(transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
            if(BounceOffWall){rb.linearVelocityX = Mathf.Abs(rb.linearVelocityX);}
            else if (rb.linearVelocityX < 0)
            {
                rb.linearVelocityX = 0;
            }
        }
        if(transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
            if(BounceOffWall){rb.linearVelocityY = Mathf.Abs(rb.linearVelocityY)*-1;}
            else if (rb.linearVelocityY > 0)
            {
                rb.linearVelocityY = 0;
            }
        }
        if(transform.position.y < minY)
        {
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
            if(BounceOffWall){rb.linearVelocityY = Mathf.Abs(rb.linearVelocityY);}
            else if (rb.linearVelocityY < 0)
            {
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
public enum PlatformColor
{
    Red,
    Blue,
    Green,
    White
}