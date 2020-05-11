using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    // Animation
    public Animator animMonster;
    // Variables pour Mouvements
    NavMeshAgent agent;
    public Transform playerTransform;
    public float detectionRange = 10f;
    // Variables Attack
    public float attackRange;
    void Start()
    {
        //on Recup le Nav Mesh
        agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        // On créer une variable référence "distance" qui correspond a la distance entre le player et le monstre
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        
        // Condition de déplacement et d'attaque
        if(distance <= detectionRange)
        {
            animMonster.SetBool("isRunning",true);
            agent.isStopped = false;
            agent.SetDestination(playerTransform.position);
        }
        else
        {
            
            agent.SetDestination(transform.position);
            animMonster.SetBool("isRunning", false);
            agent.isStopped = true;

        }
        if(distance <= attackRange)
        {
            animMonster.SetBool("isAttack",true);
        }
        else
        {
            animMonster.SetBool("isAttack", false);
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    void Attack()
    {
        playerTransform.GetComponent<HealthComponent>().TakeDamage(20);
        
    }
}
