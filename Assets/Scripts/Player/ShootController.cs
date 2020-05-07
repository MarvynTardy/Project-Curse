using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootController : MonoBehaviour
{

    public bool isFiring;

    public BulletController bullet;
    public float bulletSpeed = 1;

    public float timeBetweenShots = 3;
    private float m_ShotCounter;

    public Transform firePoint;

    //View shoot controller
    private PlayerHUD m_PlayerHUD;

    void Start()
    {
        m_PlayerHUD = FindObjectOfType<PlayerHUD>(); 
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            isFiring = true;
            Shoot();
        }
    }

    public void Shoot()
    {
        if (isFiring)
        {
            isFiring = false;
            m_ShotCounter = timeBetweenShots;
            BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            newBullet.speed = bulletSpeed;
            if (m_PlayerHUD != null)
            {
                m_PlayerHUD.UpdateShootView();
            }
        }
    }
}
