using System.Collections;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] SpriteRenderer ren;
    [SerializeField] BoxCollider2D col;
    public void setActive(bool value)
    {
        ren.enabled = value;
        col.enabled = value;
        if (enabled)
        {
            StartCoroutine(killStuff());
        }
    }
    IEnumerator killStuff()
    {
        yield return new WaitForFixedUpdate();
        if (col.IsTouchingLayers(LayerManager.PlayerLayer))
        {
            PlayerController.Instance.Die();
        }
    }
}
