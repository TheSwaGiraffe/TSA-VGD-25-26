using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] Collider2D col;
    void Update()
    {
        if (col.IsTouchingLayers(PlayerLayer))
        {
            PlayerController player = FindAnyObjectByType<PlayerController>();
            if (player.Key.activeSelf)
            {
                return;
            }
            player.Key.SetActive(true);
            Destroy(gameObject);
        }
    }
}
