using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplosiveEnemy : MonoBehaviour
{
    public Animator animMonster;
    NavMeshAgent agent;
    Transform target;
    public GameObject explosion;
    public float distance;
    public float detectionRange = 10f;
    public float attackRange;
    public float timeExplode = 5;
    public bool boom = false;
    public Canvas HUD;

    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();

        PlayerController player = FindObjectOfType<PlayerController>();
        target = player.transform;
    }

    
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);

        if (distance <= detectionRange)
        {
           agent.SetDestination(target.position);
           animMonster.SetBool("IsRunning", true);

        }
        else
        {
            agent.SetDestination(transform.position);
            animMonster.SetBool("IsRunning", false);
        }
        if(distance <= attackRange)
        {
            agent.isStopped = true;
            boom = true;
            animMonster.SetBool("IsRunning", false);
        }
        if (boom == true)
        {
            Debug.Log("boom");
            StartCoroutine(Explosion());
        }
    }

    public void ExplosionHealthComponent()
    {
        // timeExplode = 0.3f;
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        agent.isStopped = true;
        HUD.enabled = false;
        animMonster.SetTrigger("isExploding");
        yield return new WaitForSeconds(timeExplode);
        Collider[] objects = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider obj in objects)
        {
            if (obj.GetComponent<HealthComponentPlayer>())
            {
                obj.GetComponent<HealthComponentPlayer>().TakeDamage(2);
            }
        }
        Instantiate(explosion, transform.position, explosion.transform.rotation);

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
