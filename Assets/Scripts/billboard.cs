using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    public Transform Cam;

    private void LateUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x, transform.position.y, Cam.transform.position.z));
    }
}
