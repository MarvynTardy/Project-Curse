using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDasher : MonoBehaviour
{
    
    // Variables Dash
    public float dashForce;
    public float dashTime;
    public float currentTimeBeforeDash = 0;
    public float timeBeforeDash = 2;
    private Rigidbody rb;
    public Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTimeBeforeDash < timeBeforeDash)
        {
            currentTimeBeforeDash += Time.deltaTime;
        }
        else
        {
            StartCoroutine(Dash());
            currentTimeBeforeDash = 0;
        }
        transform.LookAt(player.transform.position);
    }
    
    
        
    
    public IEnumerator Dash()
    {
        rb.AddForce(transform.forward * dashForce, ForceMode.VelocityChange);
        yield return new WaitForSeconds(dashTime);
        rb.velocity = Vector3.zero;
    }
}
