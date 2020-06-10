﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class HealthComponent : MonoBehaviour
{
    [Header("Références")]
    public Slider healthSlider;
    public Animator anim;
    public Enemy scriptEnemy;
    public ExplosiveEnemy scriptExplosiveEnemy;
    public EnemyTurret scriptEnemyTurret;
    public Canvas HUD;
    public Collider colliderEntity;
    public NavMeshAgent agent;
    [Range(0, 4)]
    public int enemyKind;
    SkinnedMeshRenderer characterRenderer = null;

    Material[] savedMat;
    public Material[] replaceMat;
    private bool m_IsFalling = false;

    [Header("Life Properties")]
    public int maxHealth = 100;
    public float timeBreak = 0.2f;
    public float currenthealth;
    public bool gettingHurt;
    private float m_CurrentTimeBreak;

    void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = healthSlider.maxValue / 2;
        currenthealth = maxHealth;

        m_CurrentTimeBreak = timeBreak;
        characterRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        savedMat = new Material[characterRenderer.materials.Length];
        for (int i = 0; i < characterRenderer.materials.Length; i++)
        {
            savedMat[i] = characterRenderer.materials[i];
        }
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10);
        }        
    }

    private void Update()
    {
        if (gettingHurt)
        {      
           
            ChangeMat();
            Invoke("ResetMat", 0.2f);
            gettingHurt = false;

        }

        if (enemyKind == 4 && m_IsFalling)
        {
            this.gameObject.transform.position += new Vector3(0, -0.05f, 0);
        }
    }

    public void PauseTime()
    {
        Time.timeScale = 0;
    }

    public void PlayTime()
    {
        Time.timeScale = 1;
    }

    public void TakeDamage(float damage)
    {
        currenthealth -= damage;

        healthSlider.value -= damage / 2;
        
        switch (enemyKind)
        {
            case 1:
                anim.SetTrigger("TakeDamage");
                break;
        }

        

        gettingHurt = true;

        if(currenthealth <= 0)
        {
            Die();
        }
    }

    public void ChangeMat()
    {
        characterRenderer.sharedMaterials = replaceMat;
    }

    public void ResetMat()
    {
        characterRenderer.sharedMaterials = savedMat;
    }

    public void Heal(int healing)
    {
        currenthealth += healing;
        healthSlider.value += healing;
    }

    void Die()
    {
        HUD.enabled = false;

        switch (enemyKind)
        {
            case 1:
                colliderEntity.enabled = false;
                scriptEnemy.enabled = false;
                agent.enabled = false;
                anim.SetTrigger("isDead");
                break;

            case 2:

                break;

            case 3:
                colliderEntity.enabled = false;
                scriptExplosiveEnemy.enabled = false;
                scriptExplosiveEnemy.ExplosionHealthComponent();
                agent.enabled = false;
                break;

            case 4:
                colliderEntity.enabled = false;
                scriptEnemyTurret.enabled = false;
                StartCoroutine(TurretDeath());
                m_IsFalling = true;
                break;

            default:
                Debug.Log("Pas d'enemyKind déclaré");
                break;
        }

        Debug.Log(this.gameObject + " is Dead");
        // Destroy(gameObject);
    }

    IEnumerator TurretDeath()
    {
        Debug.Log("Yes");
        // this.gameObject.transform.position += new Vector3(0, -1f ,0);
        yield return new WaitForSeconds(2);
        Debug.Log("Yessai");
        Destroy(transform.parent.gameObject);
    }

}


