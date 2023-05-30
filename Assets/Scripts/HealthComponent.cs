using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class HealthComponent : MonoBehaviour
{
    [Header("Références")]
    public Slider healthSlider;
    public Animator anim;
    public Enemy scriptEnemy;
    public ExplosiveEnemy scriptExplosiveEnemy;
    public EnemyTurret scriptEnemyTurret;
    public EnemyDasher scriptEnemyDasher;
    public Canvas HUD;
    public Collider colliderEntity;
    public NavMeshAgent agent;
    public GameObject minimapRender;
    [Range(0, 4)]
    public int enemyKind;
    MeshRenderer characterRenderer = null;

    Material[] savedMat;
    public Material[] replaceMat;
    private bool m_IsFalling = false;

    [Header("Life Properties")]
    public int maxHealth = 100;
    public float timeBreak = 0.2f;
    public float currenthealth;
    public bool gettingHurt;
    private float m_CurrentTimeBreak;
    public bool isAlive = true;

    void Start()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = healthSlider.maxValue / 2;
        currenthealth = maxHealth;

        m_CurrentTimeBreak = timeBreak;
        characterRenderer = GetComponentInChildren<MeshRenderer>();
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

        if (enemyKind == 4 && m_IsFalling)
        {
            this.gameObject.transform.position += new Vector3(0, -0.1f, 0);
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
        currenthealth -= damage;

        healthSlider.value -= damage / 2;
        
        switch (enemyKind)
        {
            case 1:
                anim.SetTrigger("TakeDamage");
                break;
        }

        

        gettingHurt = true;

        if(currenthealth <= 0)
        {
            Die();
        }
    }

    public void ChangeMat()
    {
        characterRenderer.sharedMaterials = replaceMat;
    }

    public void ResetMat()
    {
        characterRenderer.sharedMaterials = savedMat;
    }

    public void Heal(int healing)
    {
        currenthealth += healing;
        healthSlider.value += healing;
    }

    void Die()
    {
        HUD.enabled = false;
        colliderEntity.enabled = false;
        isAlive = false;
        minimapRender.SetActive(false);

        switch (enemyKind)
        {
            // Ennemi de base
            case 1:
                scriptEnemy.enabled = false;
                agent.enabled = false;
                anim.SetTrigger("isDead");
                break;

            // Ennemi de dasher
            case 2:
                scriptEnemyDasher.enabled = false;
                anim.SetTrigger("isDead");
                break;

            // Ennemie kamikaze
            case 3:
                scriptExplosiveEnemy.enabled = false;
                scriptExplosiveEnemy.ExplosionHealthComponent();
                agent.enabled = false;
                break;

            // Tourrelle
            case 4:
                scriptEnemyTurret.enabled = false;
                StartCoroutine(TurretDeath());
                m_IsFalling = true;
                break;

            default:
                Debug.Log("Pas d'enemyKind déclaré");
                break;
        }

        // Debug.Log(this.gameObject + " is Dead");
        // Destroy(gameObject);
    }

    IEnumerator TurretDeath()
    {
        // this.gameObject.transform.position += new Vector3(0, -1f ,0);
        yield return new WaitForSeconds(2);
        Destroy(transform.parent.gameObject);
    }

}


