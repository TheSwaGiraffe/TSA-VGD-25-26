using System.Collections.Generic;
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
        masterVolume = 1;
        musicVolume = 0.25f;
        SFXVolume = 1;
        if(Instance){Destroy(Instance.gameObject);}
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    List<float> defaultMusicVolumes = new List<float>();
    List<float> defaultSFXVolumes = new List<float>();
    List<float> defaultTypingVolumes = new List<float>();
    void Start()
    {
        foreach(AudioSource song in MusicPlayer.Songs)
        {
            defaultMusicVolumes.Add(song.volume);
        }
        foreach(AudioSource sound in SFXPlayer.Sounds)
        {
            defaultSFXVolumes.Add(sound.volume);
        }
        foreach(AudioSource sound in TypingSoundsPlayer.sounds)
        {
            defaultTypingVolumes.Add(sound.volume);
        }
        SetMasterVolume(masterVolume);
        SetMusicVolume(musicVolume);
        SetSFXVolume(SFXVolume);
    }
    public float masterVolume = 1;
    public float musicVolume = 0.25f;
    public float SFXVolume = 1;
    public void SetMasterVolume(float value)
    {
        Debug.Log($"SetMasterVolume({value});");
        masterVolume = value;
        SetMusicVolume(musicVolume);
        SetSFXVolume(SFXVolume);
    }
    public void SetMusicVolume(float value)
    {
        Debug.Log($"SetMusicVolume({value});");
        musicVolume = value;
        for(int i = 0; i < MusicPlayer.Songs.Length; i++)
        {
            MusicPlayer.Songs[i].volume = defaultMusicVolumes[i] * musicVolume * masterVolume;
        }
    }
    public void SetSFXVolume(float value)
    {
        Debug.Log($"SetSFXVolume({value});");
        SFXVolume = value;
        for(int i = 0; i < SFXPlayer.Sounds.Length; i++)
        {
            SFXPlayer.Sounds[i].volume = defaultSFXVolumes[i] * SFXVolume * masterVolume;
        }
        for(int i = 0; i < TypingSoundsPlayer.sounds.Length; i++)
        {
            TypingSoundsPlayer.sounds[i].volume = defaultTypingVolumes[i] * SFXVolume * masterVolume;
        }
    }
}
