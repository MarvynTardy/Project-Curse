﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthComponent : MonoBehaviour
{
    [Header("Références")]
    public Slider healthSlider;
    public ParticleSystem bloodParticle;
    public ParticleSystem hitParticle;
    public Animator anim;
    public Enemy scriptEnemy;
    public Canvas HUD;
    public Collider colliderEntity;
    SkinnedMeshRenderer characterRenderer = null;

    Material[] savedMat;
    public Material[] replaceMat;

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
        if (bloodParticle != null)
        {
            bloodParticle.Play();
        }

        if (hitParticle != null)
        {
            hitParticle.Clear();
            hitParticle.Play();
        }

        currenthealth -= damage;

        healthSlider.value -= damage / 2;
        
            anim.SetTrigger("TakeDamage");
        

        gettingHurt = true;

        if(currenthealth <= 0)
        {
            Die();
        }
    }

    public void ChangeMat()
    {
        // Debug.Log("change color");
        /* for (int i = 0; i < characterRenderer.materials.Length; i++)
         {
             Debug.Log(characterRenderer.materials[i]);
             characterRenderer.materials[i] = replaceMat[i];
             Debug.Log(characterRenderer.materials[i]);
         }*/
        characterRenderer.sharedMaterials = replaceMat;
    }

    public void ResetMat()
    {
        // Debug.Log("reset color");
        /*for (int i = 0; i < characterRenderer.materials.Length; i++)
        {
            characterRenderer.materials[i] = savedMat[i];
        }*/
        characterRenderer.sharedMaterials = savedMat;
    }

    public void Heal(int healing)
    {
        currenthealth += healing;
        healthSlider.value += healing;
    }

    void Die()
    {
        colliderEntity.enabled = false;
        HUD.enabled = false;
        scriptEnemy.enabled = false;
        anim.SetTrigger("isDead");
        Debug.Log(this.gameObject + " is Dead");
        // Destroy(gameObject);
    }
    
}


