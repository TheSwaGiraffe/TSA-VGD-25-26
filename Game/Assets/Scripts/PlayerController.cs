using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float MoveSpeed;
    [SerializeField] float JumpPower;
    [Header("References")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform sprite;
    [SerializeField] BoxCollider2D groundedHitbox;
    [SerializeField] LayerMask GroundLayer;
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        bool jumping = Input.GetKey("up") || Input.GetKey("w");

        rb.linearVelocity = new Vector2(xInput * MoveSpeed, rb.linearVelocity.y);
        if (jumping && groundedHitbox.IsTouchingLayers(GroundLayer)){
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpPower);
        }
        updateSpriteVisual();
        if(rb.linearVelocity.magnitude == 0){
            transform.position = sprite.position;
            updateSpriteVisual();
        }
    }
    void updateSpriteVisual()
    {
        //round position to nearest 8th so the sprite is pixel perfect
        sprite.position = transform.position;
        float newX = Mathf.Round(sprite.position.x*8)/8;
        float newY = Mathf.Round(sprite.position.y*8)/8;
        float newZ = sprite.position.z;
        sprite.position = new Vector3(newX, newY, newZ);
    }
}
