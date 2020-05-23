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
    public Image blackScreen;
    public PlayerController player;

    [Header("Life Properties")]
    public int maxHealth = 100;
    public float timeBreak = 0.2f;
    private int currenthealth;
    public bool gettingHurt;
    private float m_CurrentTimeBreak;

    private bool m_IsRespawning;
    private Vector3 m_RespawnPoint;
    private float respawnLength = 4;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;
    public float fadeSpeed;
    private float waitForFade = 2;

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
        
        // Diminue la vie et met à jour le HUD
        currenthealth -= damage;
        healthSlider.value -= damage;

        if (currenthealth <= 0)
        {
            Respawn();
            // Die();
        }
        else
        {
            anim.SetTrigger("TakeDamage");
            gettingHurt = true;
        }
    }

    public void Heal(int healing)
    {
        currenthealth += healing;
        healthSlider.value += healing;
    }

    public void Respawn()
    {
        // anim.SetTrigger("SeMeurtDansDatroceSouffrance");
        blackScreen.enabled = true;
        blackScreen.CrossFadeAlpha(0, 0.01f, false);
        
        blackScreen.CrossFadeAlpha(1, respawnLength, false);
        if (!m_IsRespawning)
        {
            StartCoroutine("RespawnCo");
        }
    }

    public IEnumerator RespawnCo()
    {
        m_IsRespawning = true;
        player.gameObject.SetActive(false);
        Debug.Log("Respawn");
        yield return new WaitForSeconds(respawnLength);
        // isFadeToBlack = true;
        blackScreen.CrossFadeAlpha(0, waitForFade, false);
        player.transform.position = m_RespawnPoint;
        currenthealth = maxHealth;
        yield return new WaitForSeconds(waitForFade);
        // isFadeFromBlack = false;
        player.gameObject.SetActive(true);
        // anim.SetTrigger("SeReleveDeLaMort");
        m_IsRespawning = false;
        blackScreen.enabled = false;
        
    }

    void Die()
    {
        Debug.Log(this.gameObject + " is Dead");
        // Destroy(gameObject);
    }
}
