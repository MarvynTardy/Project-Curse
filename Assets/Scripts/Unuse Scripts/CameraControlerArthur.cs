using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlerArthur : MonoBehaviour
{
    public Transform target;

    // ↓ variable utilisé pour créer le décalage entre la position du joueur et celle de la caméra
    public Vector3 cameraOffset = new Vector3 (-5, -10, 0);

    public bool useOffsetValues;

    public float rotateSpeed = 1;

    // public Transform pivot;

    void Start()
    {
        if (!useOffsetValues)
        {
            cameraOffset = target.position - transform.position;
        }

        /*
        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;*/

        Cursor.lockState = CursorLockMode.Locked;
    }


    void LateUpdate()
    {
        // Récupère la position de la souris & rotate la target
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0, horizontal, 0);

        /*
        // Récupère la position de la souris & rotate la target
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(-vertical, 0, 0);*/

        float desiredYAngle = target.eulerAngles.y;
        /*float desiredXAngle = pivot.eulerAngles.x;*/

        Quaternion rotation = Quaternion.Euler(0, desiredYAngle, 0);
        transform.position = target.position - (rotation * cameraOffset);

        transform.LookAt(target);
    }
}
