using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    Transform target;
    public Transform firePoint;
    public float timeBeforeShoot = 4;
    public float currentTimeBeforeShoot;
    public float detectionRange;
    public BulletController bulletController;
    public float distance;
    public float bulletSpeed = 20f;
    public ParticleSystem particleShoot;

    void Start()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        target = player.transform;
    }

    
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);
        transform.LookAt(new Vector3(target.position.x, this.transform.position.y, target.position.z));
        if (distance <= detectionRange)
        {
            if(currentTimeBeforeShoot < timeBeforeShoot)
            {
                particleShoot.Play();
                currentTimeBeforeShoot += Time.deltaTime;
            }
            else
            {
                currentTimeBeforeShoot = 0;
                
            }
            if (currentTimeBeforeShoot >= timeBeforeShoot)
            {
                particleShoot.Stop();
                Shoot();
            }
        }
        else
        {
            currentTimeBeforeShoot = 0;
            particleShoot.Stop();
            particleShoot.Clear();
        }
    }
    void Shoot()
    {
        // Debug.Log("shoot");
        BulletController newBullet = Instantiate(bulletController, firePoint.position, firePoint.rotation);
        newBullet.speed = bulletSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
