using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimMoveSimple : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public float travelTime = 1.0f;
    public float speed;
    public bool repeatable;

    float startTime = 0;
    Vector3 centerPoint;
    Vector3 startRelCenter;
    Vector3 endRelCenter;

    // Update is called once per frame
    void Update()
    {
        GetCenter(Vector3.up);
        if (!repeatable)
        {
            float fracComplete = (Time.time - startTime) / travelTime * speed;
            transform.position = Vector3.Slerp(startRelCenter, endRelCenter, fracComplete * speed);
            transform.position += centerPoint;
        }
        else
        {
            float fracComplete = Mathf.PingPong(Time.time - startTime, travelTime / speed);
            transform.position = Vector3.Slerp(startRelCenter, endRelCenter, fracComplete * speed);
            transform.position += centerPoint;
        }
        
    }

    public void GetCenter(Vector3 direction)
    {
        centerPoint = (startPos.position + endPos.position) * 0.5f;
        centerPoint -= direction;
        startRelCenter = startPos.position - centerPoint;
        endRelCenter = endPos.position - centerPoint;
    }
}
