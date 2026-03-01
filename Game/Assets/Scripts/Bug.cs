using System;
using UnityEngine;

public class Bug : MonoBehaviour
{
    [SerializeField] BoxCollider2D NoOverlapRedBlue;
    [SerializeField] GameObject DeathHitboxes;
    [SerializeField] Teleportable teleportable;
    [SerializeField] SpriteRenderer ren;
    [SerializeField] float velocity;
    [SerializeField] Collider2D Hitbox;
    [SerializeField] BoxCollider2D ForwardsHitbox;
    [Tooltip("Bug will 'bounce' off specified layers")]
    [SerializeField] LayerMask ReturnLayers;
    [SerializeField] Rigidbody2D rb;
    void Awake()
    {
        Physics2D.IgnoreCollision(Hitbox, ForwardsHitbox);
    }
    void FixedUpdate()
    {
        if (ForwardsHitbox.IsTouchingLayers(ReturnLayers))
        {
            transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
            velocity *= -1;        
        }
        rb.linearVelocityX = velocity;
        if (Hitbox.IsTouchingLayers(LayerManager.PlayerLayer))
        {
            PlayerController player = FindAnyObjectByType<PlayerController>();
            player.rb.linearVelocityY = player.JumpPower*1.5f;
            player.rb.linearVelocityX *= 1.5f;
            SoundPlayer.SFXPlayer.PlaySound(6);
        }
    }
    public void OnSetColor()
    {
        teleportable.DefaultOnSetColor();
        NoOverlapRedBlue.gameObject.SetActive(teleportable.color == ColColor.White);
        if(teleportable.color == ColColor.Red){
            ren.color = ColorManager.Red;
            DeathHitboxes.layer = LayerManager.GetLayerIndex("RedDeath");
        }
        if(teleportable.color == ColColor.White){
            ren.color = ColorManager.White;
            DeathHitboxes.layer = LayerManager.GetLayerIndex("Death");
        }
        if(teleportable.color == ColColor.Blue){
            ren.color = ColorManager.Blue;
            DeathHitboxes.layer = LayerManager.GetLayerIndex("BlueDeath");
        }
    }
}
