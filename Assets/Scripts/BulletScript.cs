using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public AudioClip Sound;
    public float Speed;

    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;

    void Start()
    {
        Rigidbody2D= GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
    }

    void Update()
    {
        Rigidbody2D.velocity = Direction * Speed;
        //Rigidbody2D.AddForce(Direction * Speed);
    }



    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet() 
    {
        Destroy(gameObject);
    }
}