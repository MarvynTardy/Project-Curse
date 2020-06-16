﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTextTrigger : MonoBehaviour
{
    public bool isDisplaying = true;
    public Text textTuto;
    public Image fadeTuto;
    private bool isActive = false;
    public float fadeDuration = 1;
    public Collider colliderTrigger;

    public void Start()
    {
        // colliderTrigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && !isActive)
        {
            if (isDisplaying)
            {
                textTuto.CrossFadeAlpha(0, 0.01f, true);
                fadeTuto.CrossFadeAlpha(0, 0.01f, true);

                textTuto.enabled = true;
                fadeTuto.enabled = true;

                textTuto.CrossFadeAlpha(1, fadeDuration, true);
                fadeTuto.CrossFadeAlpha(0.6f, fadeDuration, true);

                isActive = true;
            }
            else
            {
                textTuto.CrossFadeAlpha(0, 1, true);
                fadeTuto.CrossFadeAlpha(0, 1, true);

                isActive = true;
            }
        }
    }
}
