using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public HealthComponentPlayer healthComponentPlayer;
    public Renderer chRenderer;
    public Material[] chOff;
    public Material[] chOn;
    public ParticleSystem chParticle;

    void Start()
    {
        healthComponentPlayer = FindObjectOfType<HealthComponentPlayer>();
    }

    public void CheckpointOn()
    {
        healthComponentPlayer.Heal();


        Checkpoint[] checkpointsArray = FindObjectsOfType<Checkpoint>();
        foreach(Checkpoint cp in checkpointsArray)
        {
            cp.CheckpointOff();
        }

        chRenderer.materials = chOn;

        chParticle.Play();

        // chRenderer.material = chOn;
    }

    public void CheckpointOff()
    {
        chRenderer.materials = chOff;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (other.gameObject.layer.Equals("Player"))
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Debug.Log("Respawn Point set");
            healthComponentPlayer.SetSpawnPoint(new Vector3 (transform.position.x, transform.position.y + 0.5f ,transform.position.z));
            // Debug.Log(healthComponentPlayer.m_RespawnPoint);
            CheckpointOn();

            int soundCheckpoint = Random.Range(0, 2);

            switch (soundCheckpoint)
            {
                case 0:
                    FindObjectOfType<AudioManager>().Play("SoundCheckpoint1");
                    break;

                case 1:
                    FindObjectOfType<AudioManager>().Play("SoundCheckpoint2");
                    break;

                case 2:
                    FindObjectOfType<AudioManager>().Play("SoundCheckpoint3");
                    break;
            }
        }
    }
}
