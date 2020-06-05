using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplosiveEnemy : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;
    public float distance;
    public float detectionRange = 10f;
    public float attackRange;
    public float timeExplode = 2.0f;
    public bool boom = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        PlayerController player = FindObjectOfType<PlayerController>();
        target = player.transform;
    }

    
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);

        if (distance <= detectionRange)
        {
           agent.SetDestination(target.position);
            

        }
        else
        {
            agent.SetDestination(transform.position);
        }
        if(distance <= attackRange)
        {
            agent.isStopped = true;
            boom = true;
        }
        if (boom == true)
        {
            Debug.Log("boom");
            StartCoroutine(Explosion());
        }
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(timeExplode);
        Collider[] objects = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider obj in objects)
        {
            if (obj.GetComponent<HealthComponentPlayer>())
            {
                obj.GetComponent<HealthComponentPlayer>().TakeDamage(2);
            }
        }
        Destroy(gameObject);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }
}
