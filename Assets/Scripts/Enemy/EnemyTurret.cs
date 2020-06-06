using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    Transform target;
    public float timeBeforeShoot = 4;
    public float currentTimeBeforeShoot;
    public float detectionRange;
    public float distance;

    void Start()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        target = player.transform;
    }

    
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);
        transform.LookAt(target.position);
        if(distance <= detectionRange)
        {
            if(currentTimeBeforeShoot < timeBeforeShoot)
            {
                currentTimeBeforeShoot += Time.deltaTime;
            }
            else
            {
                currentTimeBeforeShoot = 0;
                
            }
        }
        else
        {
            currentTimeBeforeShoot = 0;
        }
    }
    void Shoot()
    {

    }
}
