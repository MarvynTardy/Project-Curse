using System.Collections;
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

    [Header("Life Properties")]
    public int maxHealth = 100;
    public float timeBreak = 0.2f;
    private int currenthealth;
    public bool gettingHurt;
    private float m_CurrentTimeBreak;

    void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = healthSlider.maxValue;
        currenthealth = maxHealth;

        m_CurrentTimeBreak = timeBreak;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10);
        }

        if (gettingHurt)
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

        healthSlider.value -= damage;


        anim.SetTrigger("TakeDamage");

        gettingHurt = true;

        if (currenthealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healing)
    {
        currenthealth += healing;
        healthSlider.value += healing;
    }

    void Die()
    {
        Debug.Log(this.gameObject + " is Dead");
        Destroy(gameObject);
    }
}
