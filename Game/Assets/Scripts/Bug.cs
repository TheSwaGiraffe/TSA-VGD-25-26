using UnityEngine;

public class Bug : MonoBehaviour
{
    [SerializeField] float velocity;
    [SerializeField] BoxCollider2D Hitbox;
    [SerializeField] BoxCollider2D ForwardsHitbox;
    [Tooltip("Bug will 'bounce' off specified layers")]
    [SerializeField] LayerMask ReturnLayers;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] LayerMask PlayerLayer;
    void FixedUpdate()
    {
        if (ForwardsHitbox.IsTouchingLayers(ReturnLayers))
        {
            transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
            velocity *= -1;        
        }
        rb.linearVelocityX = velocity;
        if (Hitbox.IsTouchingLayers(PlayerLayer))
        {
            PlayerController player = FindAnyObjectByType<PlayerController>();
            player.rb.linearVelocityY = player.JumpPower*1.5f;
            player.rb.linearVelocityX *= 1.5f;
        }
    }
}
