using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Slider slider;

    public void setMaxDashValue(float dashValue)
    {
        slider.maxValue = dashValue;
        slider.value = dashValue;
    }

    public void SetDashCoolDown(float dashValue)
    {
        slider.value = dashValue;
    }
}
