using System.Collections;
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
        StartCoroutine(PlayAfterTerminal());
    }
    IEnumerator PlayAfterTerminal()
    {
        while (Terminal.Instance.isActive)
        {
            yield return null;
        }
        player.Play();
        while (true)
        {
            Debug.Log(player.time);
            yield return null;
        }
    }
}
