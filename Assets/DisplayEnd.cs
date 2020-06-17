using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEnd : MonoBehaviour
{
    public bool isDisplaying = true;
    public Image logoJeu;
    public Image fadeWhite;
    public Image gradientBlack;
    public Text thankText;
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
                gradientBlack.CrossFadeAlpha(0, 0.01f, true);
                thankText.CrossFadeAlpha(0, 0.01f, true);

                logoJeu.enabled = true;
                fadeWhite.enabled = true;
                gradientBlack.enabled = true;
                thankText.enabled = true;

                logoJeu.CrossFadeAlpha(1, fadeDuration, true);
                fadeWhite.CrossFadeAlpha(1, fadeDuration / 2, true);
                gradientBlack.CrossFadeAlpha(0.3f, fadeDuration / 2, true);
                thankText.CrossFadeAlpha(1, fadeDuration, true);

                isActive = true;
            }
            else
            {
                logoJeu.CrossFadeAlpha(0, 1, true);
                fadeWhite.CrossFadeAlpha(0, 1, true);
                gradientBlack.CrossFadeAlpha(0, 1, true);
                thankText.CrossFadeAlpha(0, 1, true);

                isActive = true;
            }
        }
    }
}
