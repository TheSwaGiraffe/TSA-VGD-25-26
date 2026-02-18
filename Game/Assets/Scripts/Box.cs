using System;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] BoxCollider2D NoOverlapRedBlue;
    [SerializeField] Teleportable teleportable;
    [SerializeField] SpriteRenderer ren;

    public void OnSetColor()
    {
        teleportable.DefaultOnSetColor();
        NoOverlapRedBlue.gameObject.SetActive(teleportable.color == ColColor.Green);
        if(teleportable.color == ColColor.Red){
            ren.color = new Color(1, 0.25f, 0.25f);
        }
        if(teleportable.color == ColColor.Green){
            ren.color = new Color(1, 1, 1);
        }
        if(teleportable.color == ColColor.Blue){
            ren.color = new Color(0.25f, 0.25f, 1);
        }
    } 
}
