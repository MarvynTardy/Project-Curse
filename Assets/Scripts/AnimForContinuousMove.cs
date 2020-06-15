using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimForContinuousMove : MonoBehaviour
{
    public AnimationCurve axeX;
    public AnimationCurve axeY;
    public AnimationCurve axeZ;

    private Vector3 curveX = Vector3.zero;
    private Vector3 curveY = Vector3.zero;
    private Vector3 curveZ = Vector3.zero;
    private Vector3 curve = Vector3.zero;

    private float movementTimerX = 0f;
    private float movementTimerY = 0f;
    private float movementTimerZ = 0f;

    private float movementDurationX = 0f;
    private float movementDurationY = 0f;
    private float movementDurationZ = 0f;


    private void Start()
    {
        //BeginMovement();
    }

    private void Update()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        movementTimerX += Time.deltaTime;
        movementTimerY += Time.deltaTime;
        movementTimerZ += Time.deltaTime;
        curve = new Vector3(transform.parent.position.x + axeX.Evaluate(movementTimerX), transform.parent.position.y + axeY.Evaluate(movementTimerY), transform.parent.position.z + axeZ.Evaluate(movementTimerZ));
        transform.position = curve;
    }

    private float ComputeAnimCurveDuration(AnimationCurve curve)
    {
        if (curve.keys.Length > 0)
        {
            float maxTime = curve.keys[0].time;
            for (int i = 1; i < curve.keys.Length; i++)
            {
                // Je récupère la valeur la plus haute entre le dernier timing récupéré, et le timing de la keyframe 
                maxTime = Mathf.Max(maxTime, curve.keys[i].time);
            }
            return maxTime;
        }

        return 0f;
    }
    public void BeginMovement()
    {
        movementTimerX = 0f;
        movementDurationX = ComputeAnimCurveDuration(axeX);
        movementTimerY = 0f;
        movementDurationY = ComputeAnimCurveDuration(axeY);
        movementTimerZ = 0f;
        movementDurationZ = ComputeAnimCurveDuration(axeZ);
    }
    #region Axe X
    private float ComputeAnimCurveDurationX(AnimationCurve curve)
    {
        if (curve.keys.Length > 0)
        {
            float maxTime = curve.keys[0].time;
            for (int i = 1; i < curve.keys.Length; i++)
            {
                // Je récupère la valeur la plus haute entre le dernier timing récupéré, et le timing de la keyframe 
                maxTime = Mathf.Max(maxTime, curve.keys[i].time);
            }
            return maxTime;
        }

        return 0f;
    }

    public void BeginMovementX()
    {
        movementTimerX = 0f;
        movementDurationX = ComputeAnimCurveDurationX(axeX);
    }
    #endregion

    #region Axe Y
    private float ComputeAnimCurveDurationY(AnimationCurve curve)
    {
        if (curve.keys.Length > 0)
        {
            float maxTime = curve.keys[0].time;
            for (int i = 1; i < curve.keys.Length; i++)
            {
                // Je récupère la valeur la plus haute entre le dernier timing récupéré, et le timing de la keyframe 
                maxTime = Mathf.Max(maxTime, curve.keys[i].time);
            }
            return maxTime;
        }

        return 0f;
    }

    public void BeginMovementY()
    {

        movementTimerY = 0f;
        movementDurationY = ComputeAnimCurveDurationY(axeY);
    }

    #endregion

    #region Axe Z
    private float ComputeAnimCurveDurationZ(AnimationCurve curve)
    {
        if (curve.keys.Length > 0)
        {
            float maxTime = curve.keys[0].time;
            for (int i = 1; i < curve.keys.Length; i++)
            {
                // Je récupère la valeur la plus haute entre le dernier timing récupéré, et le timing de la keyframe 
                maxTime = Mathf.Max(maxTime, curve.keys[i].time);
            }
            return maxTime;
        }

        return 0f;
    }

    public void BeginMovementZ()
    {
        movementTimerZ = 0f;
        movementDurationZ = ComputeAnimCurveDurationZ(axeZ);
    }
    #endregion
}
