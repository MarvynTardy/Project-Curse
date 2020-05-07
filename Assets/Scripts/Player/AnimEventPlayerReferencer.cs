﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventPlayerReferencer : MonoBehaviour
{
    private PlayerController m_PlayerController;

    void Start()
    {
        m_PlayerController = GetComponentInParent<PlayerController>();
    }

    void Update()
    {

    }

    public void SetMovable()
    {
        m_PlayerController.SetMovable();
    }

    public void ResSpeed()
    {
        m_PlayerController.ResSpeed();
    }

}