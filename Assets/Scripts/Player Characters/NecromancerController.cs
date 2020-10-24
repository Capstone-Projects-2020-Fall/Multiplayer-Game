using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

[RequireComponent(typeof(PhotonView))]
public class NecromancerController : MonoBehaviour
{
    public float speed;
    private PhotonView pv;
    private bool manaIncreasing;
    private float mana;
    private float skeletonCost;
    private float startTime;
    private Camera camera;
    private Vector2 movement;
    private Vector2 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {

        pv = GetComponent<PhotonView>();
        skeletonCost = 15;
        mana = 100;
        startTime = Time.time;
        manaIncreasing = true;
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            //TODO: Replace this with UI button after spawn positions have been implemented
            if (Input.GetButtonDown("Fire1"))
            {
                //TODO: Update this so it can be preset instead of just reading the mouse position
                spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                SpawnSkeleton(spawnPosition);
            }

            if (manaIncreasing)
            {
                if (mana < 100)
                {
                    mana = Mathf.Min(mana + (Time.time - startTime), 100);
                    startTime = Time.time;
                }
                else
                {
                    manaIncreasing = false;
                }
            }
        }

    }
    private void FixedUpdate()
    {
        camera.transform.Translate(movement * speed * Time.fixedDeltaTime);
    }

    //TODO: Add additional methods for all minions

    /// <summary>
    /// Spawns a new Skeleton minion at the given position.
    /// </summary>
    /// <param name="spawnPosition">A Vector2 position for the minion to be spawned.</param>
    private void SpawnSkeleton(Vector2 spawnPosition)
    {
        if (mana >= skeletonCost)
        {
            mana -= skeletonCost;
            startTime = Time.time;
            manaIncreasing = true;
            GameObject skeleton = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonSkeleton"), spawnPosition, Quaternion.identity);
        }
    }
}
