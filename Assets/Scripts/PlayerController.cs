﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Dash Properties")]
    [Range(0.1f, 10.0f)]
    public float DashDistance = 0.8f; // Variable pour tweaker la distance du Dash.

    [Header("Movement Properties")]
    [Range(1, 20)]
    public float moveSpeed = 8;
    [Range(1, 20)]
    public float jumpForce = 150;
    [Range(0, 5)]
    public float gravityScale = 1; // Variable qui gère l'attraction terrestre de notre character

    private CharacterController m_Controller;
    private Vector3 m_MoveDirection;

    public Animator animPlayer;
    public Transform pivot;
    public float rotateSpeed;

    public GameObject playerModel;

    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
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

        if (m_Controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                m_MoveDirection.y = jumpForce;
            }
            /*
            // ↓ Si la touche "Dash" est préssé alors..
            if (Input.GetButtonDown("Dash"))
            {
                Debug.Log("Dash!");
                // ↓ On dash dans la diréction dans laquel on se mouvoie déja.
                m_Controller.Move(m_MoveDirection * DashDistance);
            }
            else
            {
                // ↓ Si on ne dash pas, c'est qu'on ne bouge pas, donc le movedirection est a 0.
                m_MoveDirection = Vector3.zero;
            }*/
        }

        // Gestion de la chute en l'air
        m_MoveDirection.y = m_MoveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);

        // On applique le vector de direction à la fonction préfaite du CC move qui gère sa direction et vélocité
        m_Controller.Move(m_MoveDirection * Time.deltaTime);

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(m_MoveDirection.x, 0f, m_MoveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        // Gestion des conditions de l'animator du player
        animPlayer.SetBool("isGrounded", m_Controller.isGrounded);
        animPlayer.SetFloat("Speed", (Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal"))));
    }
}
