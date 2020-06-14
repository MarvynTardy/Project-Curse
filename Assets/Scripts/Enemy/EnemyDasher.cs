using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDasher : MonoBehaviour
{

    public Animator animMonster;
    // Variables Dash
    public float dashForce;
    public float dashTime;
    public float currentTimeBeforeDash = 0;
    public float timeBeforeDash = 2;
    public float hitRange = 2;
    public float detectionRange = 10;
    private Rigidbody rb;

    Transform target;
    public bool isDashing = false;
    public bool isCharging = false;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerController player = FindObjectOfType<PlayerController>();
        target = player.transform;
        
      

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance <= detectionRange)
        {
            isCharging = true;
            if(isCharging == true && isDashing == false)
            {
                if (currentTimeBeforeDash < timeBeforeDash)
                {
                    animMonster.SetBool("isCharge", true);
                    currentTimeBeforeDash += Time.deltaTime;
                    transform.LookAt(new Vector3(target.position.x, this.transform.position.y, target.position.z));
                }
                else
                {
                    isCharging = false;
                    isDashing = true;
                    StartCoroutine(Dash());
                    currentTimeBeforeDash = 0;

                }
            }
            
           
            
        }
        else
        {
            animMonster.SetBool("isCharge", false);
            currentTimeBeforeDash = 0;
        }
        if (distance <= hitRange && isDashing == true)
        {
            target.GetComponent<HealthComponentPlayer>().TakeDamage(1);
        }


    }
    void EndAttack()
    {
        isDashing = false;
    }  

    




    public IEnumerator Dash()
    {
        animMonster.SetTrigger("isAttack");
        rb.AddForce(transform.forward * dashForce, ForceMode.VelocityChange);
        yield return new WaitForSeconds(dashTime);
        rb.velocity = Vector3.zero;
        

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.green;
        
        Gizmos.DrawWireSphere(transform.position, hitRange);

    }
}
