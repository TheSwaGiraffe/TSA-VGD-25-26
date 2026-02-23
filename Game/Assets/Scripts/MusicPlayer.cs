using System.Collections;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    static MusicPlayer Instance;
    [SerializeField] AudioSource[] Songs;
    AudioSource current;
    void Awake()
    {
        if(Instance){Destroy(Instance.transform.parent.gameObject);}
        Instance = this;
        DontDestroyOnLoad(transform.parent.gameObject);
    }
    public void StartPlayingSongs()
    {
        Songs[0].Stop();
        StartCoroutine(PlaySongs());
    }
    IEnumerator PlaySongs()
    {
        while(true){
            AudioSource next = Songs[Random.Range(1, Songs.Length)];
            while(next == current)
            {
                next = Songs[Random.Range(1, Songs.Length)];
            }
            current = next;
            float songLength = current.clip.length;
            current.Play();
            yield return new WaitForSeconds(songLength);
            yield return new WaitForSeconds(10);
        }
    }
    void OnDestroy()
    {
        if(current){current.Stop();}
        StopCoroutine(PlaySongs());
    }
}
