using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public AudioSource[] Sounds;
    public void PlaySound(int i)
    {
        Sounds[i].Play();
    }
}
