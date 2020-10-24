using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;


public class SinglePlayerArcherController : MonoBehaviour
{
    public float speed = 5f;
    //public float health = 5f;
    public float projectileForce = 20f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public GameObject firePoint;
    public GameObject projectilePrefab;
    public Transform shootDirection1, shootDirection2, shootDirection3;
    
    private bool facingRight = true;
    private int shootingDirection = 1;

    private float cdVolley = 0f;
    private float cdShoot = 0f;

    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        cdShoot +=Time.deltaTime;
        cdVolley +=Time.deltaTime;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if ((Input.GetButtonDown("Fire1") && Time.timeScale > 0) && cdShoot > 0.25f)
        { 
            ShootProjectile();
            cdShoot = 0f;
        }

        if (Input.GetButtonDown("Fire2") && Time.timeScale > 0 && cdVolley > 1f )
        { 
            ArrowVolley();
            cdVolley = 0f;
        }

        if (Input.GetButtonDown("Pause"))
        {
            if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
            } else {
                Time.timeScale = 0;
            }
        }
    }

    void FixedUpdate()
    {
       rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
       

        //If's to flip the archer facing left and right
        if (Input.GetAxisRaw("Horizontal") == 1 && facingRight == false)
        {
            FlipLeftRight();
        }
        if (Input.GetAxisRaw("Horizontal") == -1 && facingRight == true){
            FlipLeftRight();
        }


        //If's to control the direction the firePoint is facing
        if (Input.GetAxisRaw("Horizontal") == 1 && shootingDirection != 1)
        {
            FlipTopBottom(1);
        }
        if (Input.GetAxisRaw("Horizontal") == -1 && shootingDirection != 2){
            FlipTopBottom(2);
        }
        if (Input.GetAxisRaw("Vertical") == 1 && shootingDirection != 3)
        {
            FlipTopBottom(3);
        }
        if (Input.GetAxisRaw("Vertical") == -1 && shootingDirection != 4)
        {
            FlipTopBottom(4);
        }
    }

    void FlipLeftRight()
    {
        facingRight = !facingRight;
        transform.Rotate(0f,180f,0f);
    }

    void FlipTopBottom(int directionfacing)
    {
        switch (directionfacing)
        {
            case 1:
                firePoint.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                shootingDirection = 1;
                break;
            case 2:
                firePoint.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                shootingDirection = 2;
                break;
            case 3:
                firePoint.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                shootingDirection = 3;
                break;
            case 4:
                firePoint.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                shootingDirection = 4;
                break;
        }
    }

    
    void ShootProjectile()
    {
        GameObject arrow = Instantiate(projectilePrefab, firePoint.transform.position, firePoint.transform.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.transform.right * projectileForce, ForceMode2D.Impulse);
    }

    void ArrowVolley()
    {

        Instantiate (projectilePrefab, shootDirection1.position, shootDirection1.rotation);
        Instantiate (projectilePrefab, shootDirection2.position, shootDirection2.rotation);
        Instantiate (projectilePrefab, shootDirection3.position, shootDirection3.rotation);
        //GameObject arrow = Instantiate(projectilePrefab, firePoint.transform.position, firePoint.transform.rotation);
        //Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        //rb.AddForce(firePoint.transform.right * projectileForce, ForceMode2D.Impulse);
    }
}
