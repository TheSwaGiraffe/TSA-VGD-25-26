using System.Collections;
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
            Instance.StartCoroutine(Instance.attemptSetRedActive(value));
        }
    }
    void Awake() => Instance = this;
    #endregion
    [SerializeField] bool _redActive = true;
    [Header("References")]
    [Tooltip("Layers of objects that get in the way of swapping")]
    public LayerMask OverlapLayers;
    public Tilemap Red;
    public Tilemap Blue;
    public CompositeCollider2D RedCol;
    public CompositeCollider2D BlueCol;
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

            _offTiles[i].sprite = _offSprites[i];
        }
        RedCol.isTrigger = !redActive;
        BlueCol.isTrigger = redActive;
        RedCol.gameObject.layer = redActive? 3 : 0;
        BlueCol.gameObject.layer = redActive? 0 : 3;
        Red.RefreshAllTiles();
        Blue.RefreshAllTiles();
        Debug.Log("Swapped to "+(redActive? "red" : "blue"));
    }
    IEnumerator attemptSetRedActive(bool value)
    {
        CompositeCollider2D activeCol = value ? RedCol : BlueCol;
        while (activeCol.IsTouchingLayers(OverlapLayers))
        {
            yield return new WaitForFixedUpdate();
        }
        setRedActive(value);
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