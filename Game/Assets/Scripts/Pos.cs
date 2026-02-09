using System;
using UnityEngine;

public class Pos : MonoBehaviour
{
    public float X {get=> transform.position.x; set=>transform.position = new Vector3(value, transform.position.y, transform.position.z);}
    public float Y {get=> transform.position.y; set=>transform.position = new Vector3(transform.position.x, value, transform.position.z);}
    public float Z {get=> transform.position.z; set=>transform.position = new Vector3(transform.position.x, transform.position.y, value);}
    public Vector2 XY {get => new Vector2(transform.position.x, transform.position.y); set => transform.position = new Vector3(value.x, value.y, transform.position.z);}
    public Vector2 YZ {get => new Vector2(transform.position.y, transform.position.z); set => transform.position = new Vector3(transform.position.x, value.x, value.y);}
    public Vector2 XZ {get => new Vector2(transform.position.x, transform.position.z); set => transform.position = new Vector3(value.x, transform.position.y, value.y);}
    public Vector2 YX {get => new Vector2(transform.position.y, transform.position.x); set => transform.position = new Vector3(value.y, value.x, transform.position.z);}
    public Vector2 ZY {get => new Vector2(transform.position.z, transform.position.y); set => transform.position = new Vector3(transform.position.y, value.y, value.x);}
    public Vector2 ZX {get => new Vector2(transform.position.z, transform.position.x); set => transform.position = new Vector3(value.y, transform.position.y, value.x);}
}