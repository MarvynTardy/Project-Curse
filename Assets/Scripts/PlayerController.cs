using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8;
    public float jumpForce = 150;

    // ↓ Variable qui gère l'attraction terrestre de notre character
    public float gravityScale = 1;
    public CharacterController controller;
    private Vector3 m_MoveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // On incrémente le vecteur de direction en fonction de l'axe sur laquelle le joueur appuis (paramétrer dans le Settings de Unity)
        m_MoveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0, Input.GetAxis("Vertical") * moveSpeed);

        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                m_MoveDirection.y = jumpForce;
            }
        }

        // Gestion de la chute en l'air
        m_MoveDirection.y = m_MoveDirection.y + (Physics.gravity.y * gravityScale);

        // On applique le vector de direction à la fonction préfaite du CC move qui gère sa direction et vélocité
        controller.Move(m_MoveDirection * Time.deltaTime);
    }
}
