using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] Collider2D col;
    void Update()
    {
        if (col.IsTouchingLayers(LayerManager.PlayerLayer))
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
