using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    public Transform Cam;

    private void LateUpdate()
    {
        transform.LookAt(transform.position + Cam.right);
    }
}
