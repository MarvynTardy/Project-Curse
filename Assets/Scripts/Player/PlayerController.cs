﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerHUD healthBar;

    [Header("Movement Properties")]
    [Range(1, 20)]
    public float moveSpeed = 8;
    [Range(1, 20)]
    public float jumpForce = 150;
    [Range(0, 5)]
    public float gravityScale = 1; // Variable qui gère l'attraction terrestre de notre character
    [Range(1, 20)]
    public float rotateSpeed = 10;

    [Header("Dash Properties")]
    [Range(0.1f, 10.0f)]
    public float DashDistance = 0.8f; // Variable pour tweaker la distance du Dash.
    public float DashVelocity = 1;

    [Header("Attack Properties")]
    [Range(0, 1)]
    public float attackCooldown = 0.65f;
    [Range(0, 1)]
    public float attackBounce = 0.1f;

    [Header("Référence")]
    public Animator animPlayer;
    public Transform pivot;
    public GameObject playerModel;
    public ShootController shootAttack;
    public GameObject cursor;

    private CharacterController m_Controller;
    private Vector3 m_MoveDirection;
    private bool m_IsMovable = true;
    private Camera m_MainCamera;

    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
        m_MainCamera = FindObjectOfType<Camera>();
        Cursor.visible = false;
    }

    void Update()
    {
        MouseTarget();

        if (m_IsMovable)
        {
            Move();
        }

        if (m_Controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            if (Input.GetButtonDown("Dash"))
            {
                Dash();
            }
            else if (Input.GetButtonUp("Dash"))
            {
                // ↓ Si on ne dash pas, c'est qu'on ne bouge pas, donc le movedirection est a 0.
                m_MoveDirection = Vector3.zero;
            }
        }

        // Gestion de la chute en l'air
        m_MoveDirection.y = m_MoveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        // On applique le vector de direction à la fonction préfaite du CC move qui gère sa direction et vélocité
        m_Controller.Move(m_MoveDirection * Time.deltaTime);

        // Gestion des conditions de l'animator du player
        animPlayer.SetBool("isGrounded", m_Controller.isGrounded);
        animPlayer.SetFloat("Speed", (Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal"))));
        if (Input.GetButtonDown("Attack"))
        {
            animPlayer.SetTrigger("isAttack");
            m_IsMovable = false;
            Invoke("SetMovable", attackCooldown);
            m_Controller.Move(m_MoveDirection * attackBounce);
        }
        if (Input.GetMouseButtonDown(1))
        {
            animPlayer.SetTrigger("isFiring");
        }
    }

    public void Move()
    {
        // ↓ Permet de stocker la valeur du déplacement en y
        float yStore = m_MoveDirection.y;
        
        // ↓ Permet de gérer la direction du personnage relativement à sa position dans l'espace
        m_MoveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(m_MoveDirection.x, 0f, m_MoveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }
        // ↓ Permet de gérer le déplacement en diagonal
        m_MoveDirection = m_MoveDirection.normalized * moveSpeed;
        
        // ↓ Restaure la valeur de y avant qu'il soit Normalized pour pas que le normalized n'affecte l'axe y
        m_MoveDirection.y = yStore;
    }

    public void Jump()
    {
        m_MoveDirection.y = jumpForce;
    }

    public void Dash()
    {
        // ↓ On dash dans la diréction dans laquel on se mouvoie déja.
        m_Controller.Move(m_MoveDirection * DashDistance);
    }

    public void MouseTarget()
    {
        // Ciblage à la souris
        Ray m_CameraRay = m_MainCamera.ScreenPointToRay(Input.mousePosition);
        Plane m_GroundPlane = new Plane(Vector3.up, Vector3.zero);
        float m_RayLength;

        if (m_GroundPlane.Raycast(m_CameraRay, out m_RayLength))
        {
            Vector3 m_PointToLook = m_CameraRay.GetPoint(m_RayLength);
            Debug.DrawLine(m_CameraRay.origin, m_PointToLook, Color.blue);
            DisplayFBDrop(new Vector3(m_PointToLook.x, 1f, m_PointToLook.z));
            if (Input.GetButtonDown("Attack") || Input.GetMouseButtonDown(1))
            {
                playerModel.transform.LookAt(new Vector3(m_PointToLook.x, playerModel.transform.position.y, m_PointToLook.z));
                if (Input.GetMouseButtonDown(1))
                {
                    shootAttack.isFiring = true;
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                shootAttack.isFiring = false;
            }
        }

        // Gestion de la roation du curseur
        if (cursor.activeSelf)
        {
            cursor.transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z));
        }
    }

    public void SetMovable()
    {
        m_IsMovable = true;
    }
    private void DisplayFBDrop(Vector3 pos)
    {
        cursor.SetActive(true);
        cursor.transform.position = pos;
    }
}
