using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public float attackRange;
    public int attackDamage;
   
    void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            Attack();
        }
    }
    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponentInParent<HealthComponent>().TakeDamage(attackDamage);
           
        }
    }
    private void OnDrawGizmos()
    {
        if (attackPoint != null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        
    }
}
