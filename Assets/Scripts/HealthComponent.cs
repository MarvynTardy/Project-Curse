﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public int maxHealth = 100;
    private int currenthealth;

    void Start()
    {
        currenthealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currenthealth -= damage;

        if(currenthealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Enemy is Dead");
    }
}

