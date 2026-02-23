using System.Collections;
using System.Collections.Generic;
using Unity.Loading;
using UnityEngine;
using UnityEngine.Tilemaps;
public class RedBlueUpdater : MonoBehaviour
{
    #region SingletonStuff
    public static RedBlueUpdater Instance;
    public static bool redActive
    {
        get
        {
            return Instance._redActive;
        }
        set
        {
            Instance.StartCoroutine(Instance.attemptSetRedActive(value));
        }
    }
    void Awake() => Instance = this;
    #endregion
    [SerializeField] bool _redActive = true;
    [Header("References")]
    [Tooltip("Layers of objects that get in the way of swapping")]
    public LayerMask OverlapLayers;
    public Tilemap Green;
    public Tilemap Red;
    public Tilemap Blue;
    public CompositeCollider2D GreenCol;
    public CompositeCollider2D RedCol;
    public CompositeCollider2D BlueCol;
    public Tile[] redTiles;
    public Tile[] blueTiles;
    [SerializeField] Sprite[] _onSprites;
    [SerializeField] Sprite[] _offSprites;
    public List<MovingPlatform> redBluePlatforms = new List<MovingPlatform>();
    public List<Teleportable> teleportables = new List<Teleportable>();
    void Start()
    {
        setRedActive(_redActive);
    }
    public void setRedActive(bool value)
    {
        _redActive = value;
        Tile[] _onTiles = redActive ? redTiles : blueTiles;
        Tile[] _offTiles = redActive ? blueTiles : redTiles;
        for (int i = 0; i < redTiles.Length; i++)
        {
            _onTiles[i].sprite = _onSprites[i];

            _offTiles[i].sprite = _offSprites[i];
        }
        if(PlayerController.Instance.teleportable.color == ColColor.White)
        {
            LayerManager.IgnoreLayerCollision("Player", "Red", !redActive);
            LayerManager.IgnoreLayerCollision("Player", "Blue", redActive);
        }
        if(PlayerController.Instance.teleportable.color == ColColor.Red)
        {
            LayerManager.IgnoreLayerCollision("Green", "Player", !redActive);
        }
        if(PlayerController.Instance.teleportable.color == ColColor.Blue)
        {
            LayerManager.IgnoreLayerCollision("Green", "Player", redActive);
        }
        LayerManager.IgnoreLayerCollision("Green", "Red", !redActive);
        LayerManager.IgnoreLayerCollision("Green", "Blue", redActive);
        LayerManager.IgnoreLayerCollision("GreenTrigger", "Red", !redActive);
        LayerManager.IgnoreLayerCollision("GreenTrigger", "Blue", redActive);
        Red.RefreshAllTiles();
        Blue.RefreshAllTiles();

        foreach(MovingPlatform p in redBluePlatforms)
        {
            if(p.color == ColColor.White){continue;}
            p.active = p.isRed == redActive;
        }
        foreach(Teleportable teleportable in teleportables)
        {
            if(teleportable.color == ColColor.White) { continue; }
            teleportable.OnSetRedActive.Invoke();
        }
    }
    IEnumerator attemptSetRedActive(bool value)
    {
        CompositeCollider2D activeCol = value ? RedCol : BlueCol;
        while (activeCol.IsTouchingLayers(OverlapLayers) || platformsToucingLayers(redBluePlatforms, OverlapLayers))
        {
            yield return new WaitForFixedUpdate();
        }
        setRedActive(value);

        bool platformsToucingLayers(List<MovingPlatform> platforms, int layerMask)
        {
            foreach(MovingPlatform p in platforms)
            {
                if(p.color == ColColor.White){continue;}
                if(p.isRed != value){continue;}//Skip ones that will be turned off
                if (p.col.IsTouchingLayers(layerMask))
                {
                    return true;
                }
            }
            return false;
        }
    }
#if UNITY_EDITOR
    //Reset tiles so Red & Blue are both on in the editor
    void OnApplicationQuit()
    {
        for (int i = 0; i < redTiles.Length; i++)
        {
            redTiles[i].sprite = _onSprites[i];
            blueTiles[i].sprite = _onSprites[i];
        }
    }
    //Update Red & Blue tiles when they are changed in the inspector
    bool _tilemapDirty = false;
    void OnValidate()
    {
        if (Application.isPlaying)
        {
            _tilemapDirty = true;
        }
    }
    void Update()
    {
        if (_tilemapDirty)
        {
            setRedActive(_redActive);
            _tilemapDirty = false;
        }
    }
#endif
}