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

    /*
    // Gestion du dash
    public Vector3 Drag = new Vector3(1, 1, 1);
    public float DashDistance = 100;
    public Vector3 _velocity = new Vector3 (10, 10, 10);
    */

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // ↓ Permet de stocker la valeur du déplacement en y
        float yStore = m_MoveDirection.y;

        // ↓ Permet de gérer la direction du personnage relativement à sa position dans l'espace
        m_MoveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));

        // ↓ Permet de gérer le déplacement en diagonal
        m_MoveDirection = m_MoveDirection.normalized * moveSpeed;

        // ↓ Restaure la valeur de y avant qu'il soit Normalized pour pas que le normalized n'affecte l'axe y
        m_MoveDirection.y = yStore;



        // On incrémente le vecteur de direction en fonction de l'axe sur laquelle le joueur appuis (paramétrer dans le Settings de Unity)
        // m_MoveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, m_MoveDirection.y, Input.GetAxis("Vertical") * moveSpeed);

        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                m_MoveDirection.y = jumpForce;
            }
        }

        // Gestion de la chute en l'air
        m_MoveDirection.y = m_MoveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);

        // On applique le vector de direction à la fonction préfaite du CC move qui gère sa direction et vélocité
        controller.Move(m_MoveDirection * Time.deltaTime);

        /*if (Input.GetButtonDown("Dash"))
        {
            Debug.Log("Dash");
            _velocity += Vector3.Scale(transform.forward,
                                       DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * Drag.x + 1)) / -Time.deltaTime),
                                                                  0,
                                                                  (Mathf.Log(1f / (Time.deltaTime * Drag.z + 1)) / -Time.deltaTime)));
        }

        _velocity.x /= 1 + Drag.x * Time.deltaTime;
        _velocity.y /= 1 + Drag.y * Time.deltaTime;
        _velocity.z /= 1 + Drag.z * Time.deltaTime;*/
    }
}
