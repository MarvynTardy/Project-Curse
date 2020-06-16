using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorComponent : MonoBehaviour
{
    public GameObject targetToKill;
    public bool doorUnlocked = false;
    private HealthComponent hcTarget;
    // public Animator animDoor;
    // private Collider m_ColliderDoor;

 
    /* public HealthComponent[] enemyToKill;
    public int enemyKilled = 0;*/

    void Start()
    {
        // m_ColliderDoor = GetComponent<Collider>();
        hcTarget = targetToKill.GetComponentInChildren<HealthComponent>();
    }

    void Update()
    {
        if (doorUnlocked)
        {
            this.gameObject.transform.position += new Vector3(0, -0.05f, 0);
            StartCoroutine(DoorDestroy());
            // m_ColliderDoor.enabled = false;
            // animDoor.SetTrigger("isOpen");
        }

        if (!hcTarget.isAlive)
        {
            doorUnlocked = true;
        }

        /* foreach (HealthComponent hc in enemyToKill)
        {
            if (!hc.isAlive)
            {
                enemyKilled += 1;
            }
        }

        if (enemyKilled == enemyToKill.Length + 1)
        {
            doorUnlocked = true;
        }*/
    }

    IEnumerator DoorDestroy()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
