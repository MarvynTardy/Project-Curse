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
   
    Transform target;

    public float distance;
    public float detectionRange = 10f;
    // Variables Attack
    public float attackRange;
    public float hitRange = 2;
    public bool IsCharge = false;

    public ParticleSystem slashParticle;

    void Start()
    {
        //on Recup le Nav Mesh
        agent = GetComponent<NavMeshAgent>();
       
        PlayerController player = FindObjectOfType<PlayerController>();
        target = player.transform;
    }

    
    void Update()
    {
        // On créer une variable référence "distance" qui correspond a la distance entre le player et le monstre
        distance = Vector3.Distance(target.position, transform.position);

        
        
        

        // Condition de déplacement et d'attaque
        if(distance <= detectionRange)
        {
            if(IsCharge == false)
            {
                animMonster.SetBool("isRunning", true);
                IsMoveable();
                agent.SetDestination(target.position);
            }
            
        }
        else
        {
            
            agent.SetDestination(transform.position);
            animMonster.SetBool("isRunning", false);
            

        }
        if(distance <= attackRange)
        {
            
            IsCharge = true;
            animMonster.SetBool("isCharge",true);
            
            if (IsCharge)
            {
                transform.LookAt(new Vector3 (target.transform.position.x, transform.position.y, target.transform.position.z));
            }

            
        }
        else
        {
            agent.speed = 3.5f ;
            animMonster.SetBool("isCharge", false);
            
            
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, hitRange);
    }
    void Attack()
    {
        slashParticle.Play();

        if (distance <= hitRange)
        {
            GetComponent<HealthComponentPlayer>().TakeDamage(20);
        }
        agent.SetDestination(target.position);
        
        
    }
    void IsMoveable()
    {
        agent.isStopped = false;

    }
    void MultiplySpeed()
    {
        agent.speed = 1000;
    }
    void IsUnMoveable()
    {
        agent.isStopped = true;

    }
    void IsNotCharge()
    {
        IsCharge = false;
        
    }
}
