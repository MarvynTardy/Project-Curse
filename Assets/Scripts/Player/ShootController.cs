using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootController : MonoBehaviour
{
    [Header("Références")]
    public Animator animPlayer;
    public Transform firePoint;
    public BulletController bullet;
    private PlayerController m_PlayerController;
    private PlayerHUD m_PlayerHUD;

    [Header("Shoot Properties")]
    public bool isFiring;
    public float bulletSpeed = 1;
       
    void Start()
    {
        m_PlayerHUD = FindObjectOfType<PlayerHUD>();
        m_PlayerController = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Shoot") && !m_PlayerController.isDodging)
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
            m_PlayerController.isMovable = false;
            m_PlayerController.moveDirection = Vector3.zero;

            animPlayer.SetTrigger("isFiring");

            m_PlayerController.playerModel.transform.LookAt(new Vector3(m_PlayerController.pointToLook.x, m_PlayerController.playerModel.transform.position.y, m_PlayerController.pointToLook.z));

            BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            newBullet.speed = bulletSpeed;
            if (m_PlayerHUD != null)
            {
                m_PlayerHUD.UpdateShootView();
            }
        }
    }

    public void Fire()
    {

    }
}
