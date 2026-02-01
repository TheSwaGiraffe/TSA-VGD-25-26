using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector2Int Size;
    public bool isRed;
    [SerializeField] bool update = false;
    [SerializeField] Transform[] Pieces;
    [SerializeField] SpriteRenderer[] PieceSpriteRenderers;
    [SerializeField] Sprite[] On;
    [SerializeField] Sprite[] Off;
    public BoxCollider2D col;
    public bool active { get=>_active; set => setActive(value);}
    bool _active = true;
    void Start()
    {
        List<MovingPlatform> platforms = RedBlueUpdater.Instance.movingPlatforms;
        gameObject.layer = LayerManager.GetLayerIndex(isRed? "Red" : "Blue");
        platforms.Add(this);
    }
    void setActive(bool value)
    {
        _active = value;
        Sprite[] newSprites = active? On : Off;
        for(int i = 0; i < Pieces.Length; i++)
        {
            PieceSpriteRenderers[i].sprite = newSprites[i];
        }
    }
    void updatePlatform()
    {
        col.size = new Vector2(Size.x, Size.y)*0.25f;
        float halfX = Size.x*0.25f/2f-0.125f;
        float halfY = Size.y*0.25f/2f-0.125f;
        UpdPos(Pieces[0], -halfX, halfY);
        UpdPos(Pieces[2], halfX, halfY);
        UpdPos(Pieces[6], -halfX, -halfY);
        UpdPos(Pieces[8], halfX, -halfY);

        UpdPos(Pieces[1], 0, halfY);
        UpdPos(Pieces[3], -halfX, 0);
        UpdPos(Pieces[5], halfX, 0);
        UpdPos(Pieces[7], 0, -halfY);

        PieceSpriteRenderers[1].size = new Vector2(Size.x-2, 1);
        PieceSpriteRenderers[3].size = new Vector2(1, Size.y-2);
        PieceSpriteRenderers[5].size = new Vector2(1, Size.y-2);
        PieceSpriteRenderers[7].size = new Vector2(Size.x-2, 1);
        
        Pieces[4].localScale = new Vector2(Size.x-2, Size.y-2)*0.25f;

        foreach(SpriteRenderer r in PieceSpriteRenderers)
        {
            r.color = isRed? new Color(1, 0, 0) : new Color(0, 0, 1);
        }
    }
    void UpdPos(Transform t, float x, float y)
    {
        t.localPosition = new Vector3(x, y, t.position.z);
    }
    #if UNITY_EDITOR
    void OnValidate()
    {
        active = true;
        if(update){
            updatePlatform();
            setActive(active);
            update = false;
        }
    }
    #endif
}
