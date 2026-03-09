using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource[] Songs;
    AudioSource current;
    public void StartPlayingSongs()
    {
        Songs[0].Stop();
        StartCoroutine(PlaySongs());
    }
    IEnumerator PlaySongs()
    {
        while (true)
        {
            List<AudioSource> songsLeft = Songs.ToList();
            while (songsLeft.Count > 0)
            {
                current = songsLeft[Random.Range(0, songsLeft.Count)];
                songsLeft.Remove(current);
                current.Play();
                yield return new WaitForSecondsRealtime(1);
                while(current.time > 0)
                {
                    yield return null;
                }
                yield return new WaitForSecondsRealtime(5);
            }
        }
    }
    void OnDestroy()
    {
        if(current){current.Stop();}
        StopCoroutine(PlaySongs());
    }
}
