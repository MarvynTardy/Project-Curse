using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthComponent : MonoBehaviour
{
    public Slider healthSlider;

    public int maxHealth = 100;
    private int currenthealth;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
    }
}


