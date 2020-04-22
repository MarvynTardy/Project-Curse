using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraAnchor;

    public Vector3 camOffset;

    [Range(0.01f, 1.0f)]
    public float delayMovement = 0.5f;

    public bool rotationY = true;

    public bool rotationButton = true;

    public float rotationSpeed = 5.0f;

    void Start()
    {
        camOffset = transform.position - cameraAnchor.position;
    }

    private bool isRotateActive
    {
        get
        {
            if (!rotationY)
                return false;

            if (!rotationButton)
                return true;

            if (rotationButton && Input.GetMouseButton(2))
                return true;

            return false;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {

        transform.LookAt(cameraAnchor);

        if (isRotateActive)
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);

            camOffset = camTurnAngle * camOffset;
        }

        Vector3 newPos = cameraAnchor.position + camOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, delayMovement);

        if(Input.GetAxis("Mouse X") < 0)
        {

        }
    }
}
