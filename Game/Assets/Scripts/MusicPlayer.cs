using System.Collections;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioSource[] Songs;
    AudioSource current;
    void Awake()
    {
        foreach(MusicPlayer player in FindObjectsByType<MusicPlayer>(FindObjectsSortMode.None))
        {
            if(player == this)
            {
                continue;
            }
            Destroy(player);
        }
        DontDestroyOnLoad(transform.parent.gameObject);
    }
    public void StartPlayingSongs()
    {
        Songs[0].Stop();
        StartCoroutine(PlaySongs());
    }
    IEnumerator PlaySongs()
    {
        Debug.Log("Playing Songs");
        while(true){
            AudioSource next = Songs[Random.Range(0, Songs.Length)];
            while(next == current)
            {
                next = Songs[Random.Range(1, Songs.Length)];
                Debug.Log(next.gameObject.name);
            }
            current = next;
            float songLength = current.clip.length;
            current.Play(0);
            yield return new WaitForSeconds(songLength);
            yield return new WaitForSeconds(10);
        }
    }
    void OnDestroy()
    {
        Debug.Log("Bye");
        current.Stop();
        StopCoroutine(PlaySongs());
    }
}
