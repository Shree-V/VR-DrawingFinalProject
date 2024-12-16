using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyShoot : MonoBehaviour
{
    public Transform player;
    public GameObject bullet2Prefab;

    public float shootInterval = 2f;
    public float bulletSpeed = 10f;

    private float shootTimer;


    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);

        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bullet2Prefab, transform.position + transform.forward, transform.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
    }
}
