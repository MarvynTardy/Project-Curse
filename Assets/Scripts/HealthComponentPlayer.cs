﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponentPlayer : MonoBehaviour
{
    [Header("Références")]
    public Slider healthSlider;
    public ParticleSystem bloodParticle;
    public ParticleSystem hitParticle;
    public Animator anim;
    public Image redWarning;
    public Image blackScreen;
    public PlayerController player;

    [Header("Life Properties")]
    public int maxHealth = 100;
    public float timeBreak = 0.2f;
    private int currenthealth;
    public bool gettingHurt;
    private float m_CurrentTimeBreak;

    private bool m_IsRespawning;
    public Vector3 m_RespawnPoint;
    private float respawnLength = 4;
    public float fadeSpeed;
    private float waitForFade = 2;
    public bool isDead = false;
    private float lifeCooldown = 1;
    private float m_CurrentLifeCooldown;

    void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = healthSlider.maxValue;
        currenthealth = maxHealth;

        m_CurrentTimeBreak = timeBreak;

        player = FindObjectOfType<PlayerController>();
        // Le respawn point se set par défaut là où le player commence le level
        m_RespawnPoint = player.transform.position;
    }


    private void Update()
    {
        m_CurrentLifeCooldown -= 1 * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal();
        }

        if (gettingHurt && !isDead)
        {
            m_CurrentTimeBreak -= 0.03f;
            if (m_CurrentTimeBreak > 0)
            {
                Time.timeScale = 0;
                redWarning.enabled = true;
                redWarning.CrossFadeAlpha(0, 0.1f, true);
            }
            else if (m_CurrentTimeBreak <= 0)
            {
                Time.timeScale = 1;
                redWarning.CrossFadeAlpha(0.5f, timeBreak, true);
                redWarning.enabled = false;
                gettingHurt = false;
                m_CurrentTimeBreak = timeBreak;
            }

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

    public void TakeDamage(int damage)
    {
        if (!isDead && m_CurrentLifeCooldown <= 0)
        {
            m_CurrentLifeCooldown = lifeCooldown;

            if (bloodParticle != null)
            {
                bloodParticle.Play();
            }

            if (hitParticle != null)
            {
                hitParticle.Clear();
                hitParticle.Play();
            }
        
            // Diminue la vie et met à jour le HUD
            currenthealth -= damage;
            healthSlider.value -= damage;

            if (currenthealth <= 0)
            {
                isDead = true;
                Respawn();
                // Die();
            }
            else
            {
                anim.SetTrigger("TakeDamage");
                gettingHurt = true;
            }
        }
    }

    public void Heal()
    {
        currenthealth = maxHealth;
        healthSlider.value = healthSlider.maxValue;
    }

    public void Respawn()
    {
        player.isMovable = false;
        player.moveDirection = Vector3.zero;

        blackScreen.enabled = true;
        blackScreen.CrossFadeAlpha(0, 0.01f, true);
        
        blackScreen.CrossFadeAlpha(1, respawnLength, true);

        m_IsRespawning = true;
        anim.SetTrigger("isDead");
        Debug.Log("Respawn");
        if (!m_IsRespawning)
        {
            StartCoroutine(RespawnCo());
        }
    }

    public IEnumerator RespawnCo()
    {
        Debug.Log("IsRespawning");

        yield return new WaitForSeconds(respawnLength);
        player.transform.position = m_RespawnPoint;
        healthSlider.value = healthSlider.maxValue;
        anim.SetTrigger("isRaise");
        blackScreen.CrossFadeAlpha(0, waitForFade, true);
        currenthealth = maxHealth;

        yield return new WaitForSeconds(waitForFade);
        m_IsRespawning = false;
        blackScreen.enabled = false;
        player.isMovable = true;
        isDead = false;
    }

    public void SetSpawnPoint(Vector3 newPosition)
    {
        m_RespawnPoint = newPosition;
    }

    void Die()
    {
        Debug.Log(this.gameObject + " is Dead");
        // Destroy(gameObject);
    }
}
