using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Joueur")]
    public Transform target;

    [Header("Propriétés de la caméra")]
    // ↓ variable utilisé pour créer le décalage entre la position du joueur et celle de la caméra
    public Vector3 cameraOffset = new Vector3(-5, -10, 0);

    public bool useOffsetValues = true;

    public float rotateSpeed = 5;

    public Transform pivot;

    void Start()
    {
        if (!useOffsetValues)
        {
            cameraOffset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;

        pivot.transform.parent = null;

        // Permet de désactiver le curseur en play
        Cursor.lockState = CursorLockMode.Locked;
    }


    void LateUpdate()
    {
        pivot.transform.position = target.transform.position;

        float m_Horizontal = 0;
        pivot.Rotate(0, m_Horizontal, 0);

        if (Input.GetMouseButton(2))
        {
            // Récupère la position de la souris en X et applique la rotation à la cible
            m_Horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            pivot.Rotate(0, m_Horizontal, 0);
        }

        // Récupère la position de la souris en Y et applique la rotation à la cible
        float m_Vertical = Input.GetAxis("Mouse Y") * rotateSpeed;

        float desiredYAngle = pivot.eulerAngles.y;

        Quaternion rotation = Quaternion.Euler(0, desiredYAngle, 0);
        transform.position = target.position - (rotation * cameraOffset);

        // Permet à la caméra de toujours regarder dans la direction de la cible (joueur)
        transform.LookAt(target);
    }
}
