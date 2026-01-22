using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField] float MoveSpeed;
    [SerializeField] float JumpPower;
    [Header("Camera Properties")]
    [SerializeField] Vector2 CamPadding;
    [SerializeField] bool CenterCam = false;
    [SerializeField] bool ResizeCam = false;
    [Header("References")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform sprite;
    [SerializeField] BoxCollider2D groundedHitbox;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] Camera cam;
    [SerializeField] Animator animator;
    [SerializeField] Tilemap GroundTilemap;
    Bounds camBounds;
    void Start()
    {
        calculateCamBounds();
    }
    void Update()
    {
        //Input
        float xInput = Input.GetAxis("Horizontal");
        bool jumping = Input.GetKey("up") || Input.GetKey("w");
        bool swap = Input.GetKeyDown("space");

        //Use Input
        rb.linearVelocity = new Vector2(xInput * MoveSpeed, rb.linearVelocity.y);
        if (jumping && groundedHitbox.IsTouchingLayers(GroundLayer))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpPower);
        }
        if (swap)
        {
            RedBlueUpdater.redActive = !RedBlueUpdater.redActive;
        }

        updateSpriteVisual();
        updateCam();

        animator.SetBool("IsMoving", xInput != 0 || jumping);
    }
    void updateSpriteVisual()
    {
        //round position to nearest 8th so the sprite is pixel perfect
        sprite.position = transform.position;
        float newX = Mathf.Round(sprite.position.x * 8) / 8;
        float newY = Mathf.Round(sprite.position.y * 8) / 8;
        float newZ = sprite.position.z;
        if (rb.linearVelocity.magnitude == 0) { transform.position = new Vector3(newX, newY, transform.position.z); }
        sprite.position = new Vector3(newX, newY, newZ);
    }
    void updateCam()
    {
        cam.transform.position = new Vector3(sprite.position.x, sprite.position.y, cam.transform.position.z);
        clampCam();
    }
    void clampCam()
    {
        Vector2 min = GroundTilemap.transform.TransformPoint(camBounds.min);
        Vector2 max = GroundTilemap.transform.TransformPoint(camBounds.max);
        
        //Clamp camera to be inside the bounds
        float newCamX = Mathf.Clamp(cam.transform.position.x, min.x, max.x);
        float newCamY = Mathf.Clamp(cam.transform.position.y, min.y, max.y);

        //Center camera if it is too big for the bounds
        if(camBounds.size.x <= 0){ newCamX = camBounds.center.x;}
        if(camBounds.size.y <= 0){ newCamY = camBounds.center.y;}
        
        cam.transform.position = new Vector3(newCamX, newCamY, cam.transform.position.z);
    }
    void calculateCamBounds()
    {
        float camHeight = cam.orthographicSize * 2;
        float camWidth = camHeight * cam.aspect;
        GroundTilemap.CompressBounds();
        camBounds = GroundTilemap.localBounds; //Bounds of level
        camBounds.size += new Vector3(CamPadding.x, CamPadding.y); //Pad bounds
        camBounds.size -= new Vector3(camWidth, camHeight); //Shrink bounds by Camera size
    }
    void resizeCam() //optimizes camera size so that 1 game pixel translates to an integer number of screen pixels
    {
        float pixelsWorld = cam.orthographicSize*8;
        float pixelsScreen = cam.scaledPixelHeight;
        Debug.Log(pixelsWorld);
        Debug.Log(pixelsScreen);
        Debug.Log(pixelsScreen/pixelsWorld);
        float targetPixelsWorld = 1/(Mathf.Round(pixelsScreen/pixelsWorld)/pixelsScreen);
        Debug.Log(targetPixelsWorld);
        cam.orthographicSize = targetPixelsWorld/8.0001f;
        pixelsWorld = cam.orthographicSize*8;
        pixelsScreen = cam.scaledPixelHeight;
        Debug.Log(pixelsScreen/pixelsWorld);

    }
    #if UNITY_EDITOR
    void OnValidate()
    {
        if (CenterCam)
        {
            CenterCam = false;
            calculateCamBounds();
            cam.transform.position = new Vector3(camBounds.center.x, camBounds.center.y, cam.transform.position.z);
        }
        if (ResizeCam)
        {
            resizeCam();
            ResizeCam = false;
        }
    }
    void debugBounds(Bounds bounds, Color color)
    {
        Debug.DrawLine(
            new Vector3(bounds.min.x, bounds.min.y, 0),
            new Vector3(bounds.min.x, bounds.max.y, 0),
            color
        );
        Debug.DrawLine(
            new Vector3(bounds.min.x, bounds.max.y, 0),
            new Vector3(bounds.max.x, bounds.max.y, 0),
            color
        );
        Debug.DrawLine(
            new Vector3(bounds.max.x, bounds.max.y, 0),
            new Vector3(bounds.max.x, bounds.min.y, 0),
            color
        );
        Debug.DrawLine(
            new Vector3(bounds.max.x, bounds.min.y, 0),
            new Vector3(bounds.min.x, bounds.min.y, 0),
            color
        );
    }
    #endif
}
