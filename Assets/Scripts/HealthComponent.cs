using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthComponent : MonoBehaviour
{
    [Header("Références")]
    public Slider healthSlider;
    public ParticleSystem bloodParticle;

    public int maxHealth = 100;
    private int currenthealth;


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
    }

    void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = healthSlider.maxValue;
        currenthealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        if (bloodParticle != null)
        {
            bloodParticle.Play();
        }

        currenthealth -= damage;
        healthSlider.value -= damage;
        
        if(currenthealth <= 0)
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


