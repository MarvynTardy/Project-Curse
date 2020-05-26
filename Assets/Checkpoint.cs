using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public HealthComponentPlayer healthComponentPlayer;
    public Renderer renderer;
    public Material chOff;
    public Material chOn;
    public ParticleSystem chParticle;

    void Start()
    {
        healthComponentPlayer = FindObjectOfType<HealthComponentPlayer>();
    }

    void Update()
    {

    }

    public void CheckpointOn()
    {
        Checkpoint[] checkpointsArray = FindObjectsOfType<Checkpoint>();
        foreach(Checkpoint cp in checkpointsArray)
        {
            cp.CheckpointOff();
        }

        chParticle.Play();

        renderer.material = chOn;
    }

    public void CheckpointOff()
    {
        renderer.material = chOff;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (other.gameObject.layer.Equals("Player"))
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Respawn Point set");
            healthComponentPlayer.SetSpawnPoint(transform.position);
            CheckpointOn();
        }
    }
}
