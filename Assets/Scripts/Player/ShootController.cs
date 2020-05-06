using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public bool isFiring;

    public BulletController bullet;
    public float bulletSpeed = 1;

    public float timeBetweenShots = 3;
    private float m_ShotCounter;

    public Transform firePoint;

    void Start()
    {
        
    }

    void Update()
    {
        if (isFiring)
        {
            isFiring = false;
            m_ShotCounter = timeBetweenShots;
            BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            newBullet.speed = bulletSpeed;            
        }
    }

    public void Fire()
    {

    }
}
