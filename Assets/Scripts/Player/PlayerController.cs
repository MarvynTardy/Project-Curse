using System.Collections;
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
    [HideInInspector]
    public bool isMovable = true;
    [HideInInspector]
    public Vector3 moveDirection;
    public Vector3 pointToLook;

    [Header("Dash Properties")]
    [Range(0.1f, 2f)]
    public float dashRate = 1f;
    public bool isDodging = false;

    [Header("Références")]
    public Animator animPlayer;
    public Transform pivot;
    public GameObject playerModel;
    public ShootController shootAttack;
    public GameObject cursor;

    private CharacterController m_Controller;
    private Camera m_MainCamera;
    private float m_SpeedSave;
    private float m_NextDashTime = 0f;

    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
        m_MainCamera = FindObjectOfType<Camera>();
        Cursor.visible = false;
    }

    void Update()
    {
        MouseTarget();

        if (isMovable)
        {
            Move();
        }

        if (m_Controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            if (Time.time >= m_NextDashTime)
            {
                if (Input.GetButtonDown("Dash"))
                {
                    animPlayer.SetTrigger("isDodging");
                    m_NextDashTime = Time.time + 1f / dashRate;
                }
            }
        }

        // Gestion de la chute en l'air
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        // On applique le vector de direction à la fonction préfaite du CC move qui gère sa direction et vélocité
        m_Controller.Move(moveDirection * Time.deltaTime);

        // Gestion des conditions de l'animator du player
        animPlayer.SetBool("isGrounded", m_Controller.isGrounded);
        animPlayer.SetFloat("Speed", (Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal"))));
        
        if (Input.GetMouseButtonDown(1))
        {
            animPlayer.SetTrigger("isFiring");
        }
    }

    public void Move()
    {
        // ↓ Permet de stocker la valeur du déplacement en y
        float yStore = moveDirection.y;
        
        // ↓ Permet de gérer la direction du personnage relativement à sa position dans l'espace
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }
        // ↓ Permet de gérer le déplacement en diagonal
        moveDirection = moveDirection.normalized * moveSpeed;
        
        // ↓ Restaure la valeur de y avant qu'il soit Normalized pour pas que le normalized n'affecte l'axe y
        moveDirection.y = yStore;
    }

    public void Jump()
    {
        moveDirection.y = jumpForce;
    }

    public void Dash()
    {
        isMovable = true;

        isDodging = true;

        m_SpeedSave = moveSpeed;

        moveSpeed *= 2.5f;
    }

    public void ResSpeed()
    {
        moveSpeed = m_SpeedSave;
        isDodging = false;
    }

    public void MouseTarget()
    {
        // Ciblage à la souris
        Ray m_CameraRay = m_MainCamera.ScreenPointToRay(Input.mousePosition);
        Plane m_GroundPlane = new Plane(Vector3.up, Vector3.zero);
        float m_RayLength;

        if (m_GroundPlane.Raycast(m_CameraRay, out m_RayLength))
        {
            pointToLook = m_CameraRay.GetPoint(m_RayLength);
            Debug.DrawLine(m_CameraRay.origin, pointToLook, Color.blue);
            DisplayFBDrop(new Vector3(pointToLook.x, 1f, pointToLook.z));
            if (Input.GetButtonDown("Attack") || Input.GetMouseButtonDown(1))
            {
                // playerModel.transform.LookAt(new Vector3(pointToLook.x, playerModel.transform.position.y, pointToLook.z));
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
        isMovable = true;
    }
    private void DisplayFBDrop(Vector3 pos)
    {
        cursor.SetActive(true);
        cursor.transform.position = pos;
    }
}
