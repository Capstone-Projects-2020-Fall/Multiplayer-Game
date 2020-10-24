using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(PhotonTransformView))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class KnightController : MonoBehaviour
{
    public float speed;
    public float health;
    public float projectileForce = 20f;
    public Transform firePoint;
    //public GameObject projectilePrefab;

    private Rigidbody2D rb;
    private PhotonView pv;
    private Vector2 velocity;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            velocity = input.normalized * speed;
            if (Input.GetButtonDown("Fire1") && Time.timeScale > 0) SwingSword();
        }
    }
    private void FixedUpdate()
    {
        
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    void SwingSword()
    {
        GameObject sword = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonSword"), firePoint.position, firePoint.rotation);
        Rigidbody2D rb = sword.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * projectileForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<PhotonView>().IsMine) return;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health--;
            Debug.Log("ow");
            if (health <= 0)
            {
                PhotonNetwork.Destroy(collision.gameObject);
            }
        }
    }
}
