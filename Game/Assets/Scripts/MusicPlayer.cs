using System.Collections;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioSource[] Songs;
    AudioSource current;
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
            yield return new WaitForSecondsRealtime(songLength);
            Debug.Log($"{current.name} played after {songLength}");
            yield return new WaitForSecondsRealtime(2);
        }
    }
    void OnDestroy()
    {
        if(current){current.Stop();}
        StopCoroutine(PlaySongs());
    }
}
