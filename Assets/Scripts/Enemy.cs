using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 50f;
    private GameObject playerTarget;

    public GameObject BulletTemplate;

    public float shootPower = 1000f;
    private float shootInterval = 2f;
    private float nextFire = 0f;
    //public float bulletSpeed = 10f;

    private float shootTimer;

    // When the player enters the trigger, assign it as a target
    private void OnTriggerEnter(Collider other)
    {
        playerTarget = other.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Only move forward if there is a player target
        if (playerTarget != null)
        {
            transform.LookAt(playerTarget.transform.position);
            //transform.position += transform.forward * Time.deltaTime * speed;

            if (Time.time > nextFire)
            {
                nextFire = Time.time + shootInterval;

                GameObject newBullet = Instantiate(BulletTemplate, transform.position, transform.rotation);
                newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootPower);
            }
        }

        //transform.LookAt(player);

        //shootTimer += Time.deltaTime;

        //if (shootTimer >= shootInterval)
        //{
        //    Shoot();
        //    shootTimer = 0f;
        //}
    }

    //void Shoot()
    //{
    //    GameObject bullet = Instantiate(bullet2Prefab, transform.position + transform.forward, transform.rotation);
    //    Rigidbody rb = bullet.GetComponent<Rigidbody>();
    //    rb.velocity = transform.forward * bulletSpeed;
    //}


}
