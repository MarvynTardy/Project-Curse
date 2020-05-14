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
    SkinnedMeshRenderer characterRenderer = null;
    
    List<Material> savedMat = new List<Material>();
    public List<Material> replaceMat = new List<Material>();
    

    [Header("Feedback Properties")]
    float blinkDuration = 0.15f;

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
        characterRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        foreach (Material material in characterRenderer.materials)
        {
            savedMat.Add(material);
        }
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
            //StartCoroutine(Blink());
            
            ChangeMat();
            //Invoke("ResetMat", 0.2f);
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

    public void ChangeMat()
    {
        Debug.Log("change color");
        for (int i = 0; i < characterRenderer.materials.Length; i++)
        {
            Debug.Log(characterRenderer.materials[i]);
            characterRenderer.materials[i] = replaceMat[i];
            Debug.Log(characterRenderer.materials[i]);
        }
    }

    public void ResetMat()
    {
        Debug.Log("reset color");
        for (int i = 0; i < characterRenderer.materials.Length; i++)
        {
            characterRenderer.materials[i] = savedMat[i];
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
    /*public IEnumerator Blink()
    {
        characterRenderer.material.color = Color.Lerp(Color.white, characterRenderer.material.color, Mathf.Abs(Mathf.Sin(Time.time * 1000)));
        yield return new WaitForSeconds(blinkDuration);
        characterRenderer.material.color = Color.Lerp(Color.black, characterRenderer.material.color, Mathf.Abs(Mathf.Sin(Time.time * 1000)));
        gettingHurt = false;
    }*/
}


