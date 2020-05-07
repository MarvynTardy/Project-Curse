using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Références")]
    public Animator animPlayer;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public ShootController shootController;

    [Header("Attack Properties")]
    public float attackRange = 2;
    public int attackDamage = 1;
    public float attackRate = 2f;
    
    private float m_NextAttackTime = 0f;
   
    void Update()
    {
        if (Time.time >= m_NextAttackTime)
        {
            if (Input.GetButtonDown("Attack"))
            {
                Attack();
                m_NextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        animPlayer.SetTrigger("isAttack");

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponentInParent<HealthComponent>().TakeDamage(attackDamage);
           
        }
    }
    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        
    }

    public void TestEvent(float val)
    {
        Debug.Log("cokfc" + val);
    }
}
