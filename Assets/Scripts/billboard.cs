using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    private Camera Cam;

    private void Start()
    {
        Cam = FindObjectOfType<Camera>();
    }
    private void LateUpdate()
    {
        transform.LookAt(new Vector3(Cam.transform.position.x, -90, Cam.transform.position.z));

    }
}
