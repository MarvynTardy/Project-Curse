using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    //Slider de shoot
    public Slider shootSlider;
    public ShootController shootController;


    public void Start()
    {
        // shootSlider.value = shootSlider.maxValue;
        shootSlider.value = shootController.stamina;
    }

    public void Update()
    {
        shootSlider.value = shootController.stamina;
    }

    /*public void UpdateShootView()
    {
        shootSlider.value -= 20;
    }*/

























    //Faut pas regarder, je fais table rase ça m'énèrve.
    /*public void setMaxHealthValue(float healthValue)
    {
        //healthSlider.maxValue = healthValue;
        //healthSlider.value = healthValue;
    }

    public void SetHealth(float healthValue)
    {
        //healthSlider.value = healthValue;
    }

    public void UpdateShootView(int numberOfShots)
    {
        for (int i = 5; i >= 5; i--)
        {
            if(shootController.isFiring == true)
            {
                shootsSliders.SetValue(0, i);
            }
        }*/
}

