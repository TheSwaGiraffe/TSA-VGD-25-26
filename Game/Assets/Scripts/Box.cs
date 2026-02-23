using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] BoxCollider2D NoOverlapRedBlue;
    [SerializeField] Teleportable teleportable;
    [SerializeField] SpriteRenderer ren;
    [SerializeField] Sprite on;
    [SerializeField] Sprite off;

    public void OnSetColor()
    {
        teleportable.DefaultOnSetColor();
        NoOverlapRedBlue.gameObject.SetActive(teleportable.color == ColColor.White);
        if(teleportable.color == ColColor.Red){
            ren.color = ColorManager.Red;
        }
        if(teleportable.color == ColColor.White){
            ren.color = ColorManager.White;
        }
        if(teleportable.color == ColColor.Blue){
            ren.color = ColorManager.Blue;
        }
    }
    public void OnSetRedActive()
    {
        if(teleportable.color == ColColor.White){return;}
        if(teleportable.color == ColColor.Red){ren.sprite = RedBlueUpdater.redActive ? on : off;}
        if(teleportable.color == ColColor.Blue){ren.sprite = RedBlueUpdater.redActive ? off : on;}
    }
}
