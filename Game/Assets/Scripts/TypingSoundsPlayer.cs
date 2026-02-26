using UnityEngine;

public class TypingSoundsPlayer : MonoBehaviour
{
    [SerializeField] AudioSource[] sounds;
    public void Character()
    {
        sounds[Random.Range(0, 5)].Play();
    }
    public void Enter()
    {
        sounds[Random.Range(5, 10)].Play();
    }
    public void Space()
    {
        sounds[Random.Range(10, 15)].Play();
    }
}
