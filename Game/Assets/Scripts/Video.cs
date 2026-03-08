using UnityEngine;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    [SerializeField] string url;
    [SerializeField] VideoPlayer player;
    
    void Awake()
    {
        player.url = url;
        player.playOnAwake = false;
        player.Prepare();
        player.prepareCompleted += OnVideoPrepared;
    }

    void OnVideoPrepared(VideoPlayer source)
    {
        player.Play();
    }
}
