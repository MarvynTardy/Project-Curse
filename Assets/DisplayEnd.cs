using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEnd : MonoBehaviour
{
    public bool isDisplaying = true;
    public Image logoJeu;
    public Image fadeWhite;
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
                logoJeu.CrossFadeAlpha(0, 0.01f, true);
                fadeWhite.CrossFadeAlpha(0, 0.01f, true);

                logoJeu.enabled = true;
                fadeWhite.enabled = true;

                logoJeu.CrossFadeAlpha(1, fadeDuration, true);
                fadeWhite.CrossFadeAlpha(0.6f, fadeDuration, true);

                isActive = true;
            }
            else
            {
                logoJeu.CrossFadeAlpha(0, 1, true);
                fadeWhite.CrossFadeAlpha(0, 1, true);

                isActive = true;
            }
        }
    }
}
