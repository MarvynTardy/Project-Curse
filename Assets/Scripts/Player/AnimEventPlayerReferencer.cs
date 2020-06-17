using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventPlayerReferencer : MonoBehaviour
{
    private PlayerController m_PlayerController;
    public ParticleSystem dust;
    public ParticleSystem fireLoad;
    public ParticleSystem fireFlash;
    public ParticleSystem fireRing;


    void Start()
    {
        m_PlayerController = GetComponentInParent<PlayerController>();
    }

    void Update()
    {

    }

    public void Dash()
    {
        m_PlayerController.Dash();
    }

    public void SetMovable()
    {
        m_PlayerController.SetMovable();
    }

    public void SetUnmovable()
    {
        m_PlayerController.moveDirection = Vector3.zero;
        m_PlayerController.isMovable = false;
    }

    public void ResSpeed()
    {
        m_PlayerController.ResSpeed();
    }

    public void DustDisplay()
    {
        dust.Play();
    }

    public void ShootDisplay()
    {
        fireLoad.Clear();
        fireRing.Clear();
        // fireFlash.Clear();
        fireLoad.Play();
        fireFlash.Play();
        fireRing.Play();
    }

    public void FootStepSound()
    {
        int footStep = Random.Range(0, 4);

        switch (footStep)
        {
            case 0:
                FindObjectOfType<AudioManager>().Play("FootStep1");
                break;

            case 1:
                FindObjectOfType<AudioManager>().Play("FootStep2");
                break;

            case 2:
                FindObjectOfType<AudioManager>().Play("FootStep3");
                break;

            case 3:
                FindObjectOfType<AudioManager>().Play("FootStep4");
                break;

            case 4:
                FindObjectOfType<AudioManager>().Play("FootStep5");
                break;
        }
    }

    public void DashSound()
    {
        int dashPlayer = Random.Range(0, 2);

        switch (dashPlayer)
        {
            case 0:
                FindObjectOfType<AudioManager>().Play("DashPlayer1");
                break;

            case 1:
                FindObjectOfType<AudioManager>().Play("DashPlayer2");
                break;

            case 2:
                FindObjectOfType<AudioManager>().Play("DashPlayer3");
                break;
        }
    }
}
