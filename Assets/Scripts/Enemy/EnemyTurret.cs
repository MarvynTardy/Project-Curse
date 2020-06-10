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
                currentTimeBeforeShoot += Time.deltaTime;
            }
            else
            {
                currentTimeBeforeShoot = 0;
                
            }
            if (currentTimeBeforeShoot >= timeBeforeShoot)
            {
                Shoot();
            }
        }
        else
        {
            currentTimeBeforeShoot = 0;
        }
    }
    void Shoot()
    {
        Debug.Log("shoot");
        BulletController newBullet = Instantiate(bulletController, firePoint.position, firePoint.rotation);
        newBullet.speed = bulletSpeed;
    }
}
