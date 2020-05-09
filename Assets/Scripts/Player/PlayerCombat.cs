using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Références")]
    public Animator animPlayer;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    private PlayerController m_PlayerController;

    [Header("Attack Properties")]
    public float attackRange = 2;
    public int attackDamage = 1;
    public float attackRate = 2f;
    public float attackBounce = 1;
    
    private float m_NextAttackTime = 0f;

    void Start()
    {
        m_PlayerController = GetComponentInParent<PlayerController>();
    }

    void LateUpdate()
    {
        if (Time.time >= m_NextAttackTime)
        {
            if (Input.GetButtonDown("Attack") && !m_PlayerController.isDodging)
            {
                Attack();
                m_NextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        m_PlayerController.isMovable = false;
        m_PlayerController.moveDirection = Vector3.zero;

        m_PlayerController.playerModel.transform.LookAt(new Vector3(m_PlayerController.pointToLook.x, m_PlayerController.playerModel.transform.position.y, m_PlayerController.pointToLook.z));

        m_PlayerController.moveDirection = transform.forward * attackBounce;

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
