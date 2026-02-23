using System.Collections;
using UnityEngine;

public class TypingSoundsPlayer : MonoBehaviour
{
    public static TypingSoundsPlayer Instance;
    void Start()
    {
        Instance = this;
    }
    [SerializeField] AudioSource[] sounds;
    public static void Character()
    {
        Instance.sounds[Random.Range(0, 5)].Play();
    }
    public static void Enter()
    {
        Instance.sounds[Random.Range(5, 10)].Play();
    }
    public static void Space()
    {
        Instance.sounds[Random.Range(10, 15)].Play();
    }
}
