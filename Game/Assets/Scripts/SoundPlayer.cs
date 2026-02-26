using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer Instance;
    public MusicPlayer _MusicPlayer;
    public TypingSoundsPlayer _TypingSoundsPlayer;
    public SFXPlayer _SFXPlayer;
    public static MusicPlayer MusicPlayer {get => Instance._MusicPlayer; set => Instance._MusicPlayer = value;}
    public static TypingSoundsPlayer TypingSoundsPlayer {get => Instance._TypingSoundsPlayer; set => Instance._TypingSoundsPlayer = value;}
    public static SFXPlayer SFXPlayer {get => Instance._SFXPlayer; set => Instance._SFXPlayer = value;}
    void Awake()
    {
        if(Instance){Destroy(Instance.gameObject);}
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
