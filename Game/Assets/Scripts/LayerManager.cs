using UnityEngine;
using System;

public class LayerManager : MonoBehaviour
{
    public static LayerManager Instance;
    void Awake()
    {
        Instance = this;
    }
    public string[] Layers;
    [Tooltip("Layer of the player")]
    public LayerMask _PlayerLayer;
    [Tooltip("Layer that the player can jump off of")]
    public LayerMask _GroundLayer;
    [Tooltip("Layer that resets the player when touched")]
    public LayerMask _DeathLayer;
    [Tooltip("Things that moving platforms consider walls")]
    public Collider2D[] Walls;
    public static LayerMask PlayerLayer{get=> Instance._PlayerLayer;}
    public static LayerMask GroundLayer{get=> Instance._GroundLayer;}
    public static LayerMask DeathLayer{get=> Instance._DeathLayer;}
    public static int GetLayerIndex(string layerName)
    {
        int i = Array.IndexOf(Instance.Layers, layerName);
        if(i < 0){Debug.LogError($"Tried to access layer {layerName}, does not exist");}
        return i;
    }
    public static LayerMask GetLayerMask(string layer)
    {
        return 1 << GetLayerIndex(layer);
    }
    public static void IgnoreLayerCollision(string layer1, string layer2, bool ignore = true)
    {
        int i1 = GetLayerIndex(layer1);
        int i2 = GetLayerIndex(layer2);
        Physics2D.IgnoreLayerCollision(i1, i2, ignore);
    }
}
[Serializable] public enum ColColor
{
    Red,
    Blue,
    White
}