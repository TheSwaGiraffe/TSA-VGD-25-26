using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Teleportable : MonoBehaviour
{
    public ColColor color {get => _color; set => OnSetColor(value);}
    public ColColor _color;
    public UnityEvent OverrideOnSetColor;
    public Collider2D col;
    public static List<Collider2D> cols = new List<Collider2D>();
    bool started = false;
    void Start()
    {
        started = true;
        OnSetColor(color);
        cols.Add(col);
    }
    public void OnSetColor(ColColor newColor)
    {
        _color = newColor;
        //Run OverrideOnSetColor if it isn't empty
        for(int i = 0; i < OverrideOnSetColor.GetPersistentEventCount(); i++)
        {
            if(OverrideOnSetColor.GetPersistentTarget(i) != null)
            {
                OverrideOnSetColor.Invoke();
                return;
            }
        }
        //otherwise...
        DefaultOnSetColor();
    }
    public void DefaultOnSetColor()
    {
        gameObject.layer = LayerManager.GetLayerIndex(color.ToString());
    }
#if UNITY_EDITOR
    ColColor previousColor;
    void OnValidate()
    {
        if (started)
        {
            if(previousColor != color)
            {
                previousColor = color;
                OnSetColor(color);
            }
        }
    }
#endif
}