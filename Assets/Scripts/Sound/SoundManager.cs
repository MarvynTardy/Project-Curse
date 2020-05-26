using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] Sounds;

    private ShootController m_ShootController;

    private void Start()
    {
        
    }

    void Update()
    {
        if(m_ShootController.isFiring)
        {
            
        }
    }
}
