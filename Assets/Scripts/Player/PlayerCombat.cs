﻿using System.Collections;
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
    [Range(0, 5)]
    public float attackRange = 2;
    [Range(0, 5)]
    public int attackDamage = 1;
    [Range(1, 5)]
    public float attackRate = 2f;
    [Range(0, 5)]
    public float attackBounce = 1;
    
    private float m_NextAttackTime = 0f;
    private bool attackRevert = false;
    private float targetTime = 2;

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

        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            attackRevert = false;
            animPlayer.SetBool("attackRevert", attackRevert);
            targetTime = 2;
        }

    }

    void Attack()
    {
        m_PlayerController.isMovable = false;
        m_PlayerController.moveDirection = Vector3.zero;

        if (Input.GetJoystickNames().Length <= 0)
        {
            m_PlayerController.playerModel.transform.LookAt(new Vector3(m_PlayerController.pointToLook.x, m_PlayerController.playerModel.transform.position.y, m_PlayerController.pointToLook.z));
        }

        m_PlayerController.moveDirection = transform.forward * attackBounce;

        animPlayer.SetTrigger("isAttack");

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponentInParent<HealthComponent>().TakeDamage(attackDamage);
        }

        animPlayer.SetBool("attackRevert", attackRevert);

        if (attackRevert)
        {
            attackRevert = false;
        }
        else
        {
            attackRevert = true;
        }
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);        
    }
}
