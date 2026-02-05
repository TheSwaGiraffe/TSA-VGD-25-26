using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [Header("Movement Properties")]
    public float MoveSpeed;
    public float JumpPower;
    [Header("Camera Properties")]
    [SerializeField] Vector2 CamPadding;
    [SerializeField] bool CenterCam = false;
    [SerializeField] bool ResizeCam = false;
    [Header("References")]
    public Rigidbody2D rb;
    [SerializeField] Transform sprite;
    [SerializeField] BoxCollider2D groundedHitbox;
    [SerializeField] BoxCollider2D playerHitbox;
    [SerializeField] Camera cam;
    [SerializeField] Animator animator;
    [SerializeField] Tilemap GroundTilemap;
    public GameObject Key;
    [HideInInspector] public Vector2 velocityOffset;
    Bounds camBounds;
    float screenHeight;
    void Awake()
    {
        Instance = this;
    }
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
        rb.linearVelocityX = xInput * MoveSpeed + velocityOffset.x;
        if(velocityOffset.y != 0){rb.linearVelocityY = velocityOffset.y;}
        if (jumping && groundedHitbox.IsTouchingLayers(LayerManager.GroundLayer))
        {
            rb.linearVelocityY = JumpPower + velocityOffset.y;
        }
        if (swap)
        {
            RedBlueUpdater.redActive = !RedBlueUpdater.redActive;
        }
        if(xInput > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        if(xInput < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Die();
        }

        //updateSpriteVisual();
        updateCam();
        animator.SetBool("IsMoving", xInput != 0 || jumping); //update animator

        if (playerHitbox.IsTouchingLayers(LayerManager.DeathLayer))
        {
            Die();
        }
    }
    void updateSpriteVisual()
    {
        //round position to nearest 8th so the sprite is pixel perfect
        sprite.position = transform.position;
        float newX = Mathf.Round(sprite.position.x * 8) / 8;
        float newY = Mathf.Round(sprite.position.y * 8) / 8;
        float newZ = sprite.position.z;
        if (rb.linearVelocity.magnitude == 0) { transform.position = new Vector3(newX, transform.position.y, transform.position.z); }
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
        if (camBounds.size.x <= 0) { newCamX = camBounds.center.x; }
        if (camBounds.size.y <= 0) { newCamY = camBounds.center.y; }

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
        if (cam.scaledPixelHeight == screenHeight) { return; } //Skip if screenheight is unchanged
        float pixelsWorld = cam.orthographicSize * 8;
        screenHeight = cam.scaledPixelHeight;
        float targetPixelsWorld = 1 / (Mathf.Floor(screenHeight / pixelsWorld) / screenHeight);
        float targetSize = targetPixelsWorld / 8f; //8.01 instead of 8.0 so no floating point errors when rendering
        cam.orthographicSize = targetSize;
        calculateCamBounds();
    }
    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
