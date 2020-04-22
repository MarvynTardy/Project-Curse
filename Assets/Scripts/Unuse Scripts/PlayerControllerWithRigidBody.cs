using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerWithRigidBody : MonoBehaviour
{
    public float moveSpeed = 8;
    public float jumpForce = 8;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y, Input.GetAxis("Vertical") * moveSpeed);
    
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }
}
