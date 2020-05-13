using System.Collections;
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
    Renderer characterRenderer;
    public Material originalMaterial;
    public Material emissiveMaterial;

    [Header("Feedback Properties")]
    float blinkDuration = 2.0f;

    [Header("Life Properties")]
    public int maxHealth = 100;
    public float timeBreak = 0.2f;
    private int currenthealth;
    public bool gettingHurt;
    private float m_CurrentTimeBreak;
    

    void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = healthSlider.maxValue / 2 ;
        currenthealth = maxHealth;

        m_CurrentTimeBreak = timeBreak;
        characterRenderer = GetComponentInChildren<Renderer>();
        originalMaterial = GetComponentInChildren<Material>();
        emissiveMaterial = Resources.Load("Assets/Graphics/Materials/EmissiveWhite", typeof(Material)) as Material;
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
            characterRenderer.material.color = Color.Lerp(Color.white, characterRenderer.material.color, Mathf.Abs(Mathf.Sin(Time.time * 100)));
            
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


