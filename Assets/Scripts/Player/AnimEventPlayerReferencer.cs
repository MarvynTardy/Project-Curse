using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventPlayerReferencer : MonoBehaviour
{
    private PlayerController m_PlayerController;
    public ParticleSystem dust;
    public TrailRenderer swordTrail;

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

    public void ResSpeed()
    {
        m_PlayerController.ResSpeed();
    }

    public void DustDisplay()
    {
        dust.Play();
    }

    public void SwordTrailDisplay()
    {
        dust.Play();
    }
}
