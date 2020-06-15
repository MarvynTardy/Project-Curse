using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorComponent : MonoBehaviour
{
    public Animator animDoor;
    public GameObject targetToKill;
    // public HealthComponent[] enemyToKill;
    // public int enemyKilled = 0;
    public bool doorUnlocked = false;
    private Collider m_ColliderDoor;
    public HealthComponent hcTarget;

    void Start()
    {
        m_ColliderDoor = GetComponent<Collider>();
        hcTarget = GetComponentInChildren<HealthComponent>();
    }

    void Update()
    {
        /*if (!hcTarget.isAlive)
        {
            doorUnlocked = true;
        }
        
        if (doorUnlocked)
        {
            animDoor.SetTrigger("isOpen");
            m_ColliderDoor.enabled = false;
        }*/

        /*if (enemyKilled == enemyToKill.Length + 1)
        {
            doorUnlocked = true;
        }


        foreach (HealthComponent hc in enemyToKill)
        {
            if (!hc.isAlive)
            {
                enemyKilled += 1;
            }
        }*/

    }
}
