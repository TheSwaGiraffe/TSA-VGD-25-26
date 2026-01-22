using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
public class RedBlueUpdater : MonoBehaviour
{
    #region SingletonStuff
    static RedBlueUpdater Instance;
    public static bool redActive
    {
        get
        {
            if (!Instance) { Debug.Log("No RedBlueUpdater Instance"); return true; }
            return Instance._redActive;
        }
        set
        {
            if (!Instance) { Debug.Log("No RedBlueUpdater Instance"); return;}
            Instance.setRedActive(value);
        }
    }
    void Awake() => Instance = this;
    #endregion
    [SerializeField] bool _redActive = true;
    [Header("References")]
    public Tilemap RedBlueTilemap;
    public Tile[] redTiles;
    public Tile[] blueTiles;
    [SerializeField] Sprite[] _onSprites;
    [SerializeField] Sprite[] _offSprites;
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
            _onTiles[i].colliderType = Tile.ColliderType.Grid;

            _offTiles[i].sprite = _offSprites[i];
            _offTiles[i].colliderType = Tile.ColliderType.None;
        }
        RedBlueTilemap.RefreshAllTiles();
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
        }
    }
#endif
}