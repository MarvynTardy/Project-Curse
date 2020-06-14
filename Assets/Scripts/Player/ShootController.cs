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
    public ParticleSystem reloadParticle;
    // private PlayerHUD m_PlayerHUD;

    [Header("Shoot Properties")]
    public bool isFiring;
    public float bulletSpeed = 1;
    public float stamina = 5;
    public float reloadTime = 0.5f;
    public bool canFire = true;
       
    void Start()
    {
        // m_PlayerHUD = FindObjectOfType<PlayerHUD>();
        m_PlayerController = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Shoot") && !m_PlayerController.isDodging && stamina >= 1 && canFire)
        {
            isFiring = true;
            Shoot();
        }

        if (Input.GetButtonDown("Reload") && stamina < 5)
        {
            canFire = false;
        }

        if (stamina == 0)
        {
            canFire = false;
        }

        if (!canFire)
        {
            Reloading();
        }

        if (stamina >= 5)
        {
            stamina = 5;
        }

    }

    public void Shoot()
    {
        if (isFiring)
        {
            stamina -= 1;
            isFiring = false;
            m_PlayerController.isMovable = false;
            m_PlayerController.moveDirection = Vector3.zero;

            animPlayer.SetTrigger("isFiring");

            if (Input.GetJoystickNames().Length <= 0)
            {
                m_PlayerController.playerModel.transform.LookAt(new Vector3(m_PlayerController.pointToLook.x, m_PlayerController.playerModel.transform.position.y, m_PlayerController.pointToLook.z));
            }

            BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            newBullet.speed = bulletSpeed;
            /*if (m_PlayerHUD != null)
            {
                m_PlayerHUD.UpdateShootView();
            }*/
        }
    }

    public void Reloading()
    {
        reloadParticle.Play();

        stamina += reloadTime * Time.deltaTime;
        if (stamina >= 5)
        {
            canFire = true;
        }
    }
}
