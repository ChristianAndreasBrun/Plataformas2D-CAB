using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public GameObject Cannon;
    public GameObject PlayerBullet;
    public float Speed;
    public float JumpForce;
    public float AirForce;
    public int MaxJumps;
    public bool Grounded;
    public float RayLenght;
    public LayerMask Mask;
    public List<Vector3> originPoints;


    //private Controler_LifeScript Health;
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private float LastShoot;
    private int Jumps;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Raycast detector
        Grounded = false;
        for (int i = 0; i < originPoints.Count; i++)
        {
            Debug.DrawRay(transform.position + originPoints[i], Vector3.down * RayLenght, Color.red);

            // Crea un rayo invisible que detecta colision
            RaycastHit2D hit = Physics2D.Raycast(transform.position + originPoints[i], Vector3.down, RayLenght, Mask);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "MobliePlataform")
                {
                    transform.parent = hit.transform;
                }

                Debug.DrawRay(transform.position + originPoints[i], Vector3.down * hit.distance, Color.green);
                Grounded = true;
            }
            else
            {
                transform.parent = null;
            }
        }
        if (!Grounded)
        {
            transform.parent = null;
        }


        // Movimiento Horizontal del Player
        Horizontal = Input.GetAxisRaw("Horizontal");
        if (Horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Horizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


        // Variable bool - running
        Animator.SetBool("running", Horizontal != 0.0f);
        Animator.SetBool("grounded", Grounded);


        if (Grounded)
        {
            Jumps = MaxJumps;
        }
        // Input del Player para saltar
        if (Input.GetKeyDown(KeyCode.X) && Jumps > 0)
        {
            Jump();
        }

        // Input del Player para disparar
        if (Input.GetKey(KeyCode.C) && Time.time > LastShoot + 0.15f)
        {
            Shoot();
            LastShoot = Time.time;
        }

        if (true)
        {

        }

    }


    private void Jump()
    {
        Rigidbody2D.AddForce(new Vector2(0, JumpForce));
        Jumps--;

        if (Grounded)
        {
            Rigidbody2D.AddForce(new Vector2(0, JumpForce));
        }
        else
        {
            Rigidbody2D.AddForce(new Vector2(0, AirForce));
        }
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }

        GameObject bullet = Instantiate(PlayerBullet, Cannon.transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }
}

