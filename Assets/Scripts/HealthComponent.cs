using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthComponent : MonoBehaviour
{
<<<<<<< HEAD
    public Animator anim;
=======
    public Slider healthSlider;

>>>>>>> ee096bca91d968136e102d5f1f18c7bbec12ec4e
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
        currenthealth -= damage;
<<<<<<< HEAD
        anim.SetTrigger("TakeDamage");

=======
        healthSlider.value -= damage;
        
>>>>>>> ee096bca91d968136e102d5f1f18c7bbec12ec4e
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


