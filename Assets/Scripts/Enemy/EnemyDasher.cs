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
    public float detectionRange = 10;
    private Rigidbody rb;
    public Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= detectionRange)
        {
            if (currentTimeBeforeDash < timeBeforeDash)
            {
                animMonster.SetBool("isCharge",true);
                currentTimeBeforeDash += Time.deltaTime;
            }
            else
            {
                StartCoroutine(Dash());
                currentTimeBeforeDash = 0;
                
            }
            transform.LookAt(player.transform.position);
        }
        else
        {
            animMonster.SetBool("isCharge", false);
            currentTimeBeforeDash = 0;
        }

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

    }
}
